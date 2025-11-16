# Handling Large Attachments Architecture

> **Task:** Handle large file attachments (~100MB) for form submissions, considering thousands of submissions with multiple attachments each.

## Table of Contents
- [Executive Summary](#executive-summary)
- [Architecture Overview](#architecture-overview)
- [Data Structure](#data-structure)
- [REST API Endpoints](#rest-api-endpoints)
- [Frontend Implementation](#frontend-implementation)
- [Event-Driven Post-Processing](#event-driven-post-processing)
- [Security Considerations](#security-considerations)
- [Scalability & Performance](#scalability--performance)
- [Additional Enhancements](#additional-enhancements)
- [Summary](#summary)

---

## Executive Summary

For handling large file attachments (~100MB) across thousands of submissions with multiple files each, we use **AWS S3 with pre-signed URLs** combined with an **event-driven architecture**. This approach offloads file transfer bandwidth from the application server, provides scalability, and maintains security.

### Key Benefits
- ✅ Handles 100MB+ files efficiently
- ✅ Scales to millions of files
- ✅ Secure temporary URLs
- ✅ Cost-effective (minimal server resources)
- ✅ Event-driven processing
- ✅ Industry-standard approach

---

## Architecture Overview

```
┌─────────────┐         ┌──────────────┐         ┌─────────────┐
│   Frontend  │────────▶│  ASP.NET API │────────▶│  Database   │
│  (Vue.js)   │         │   (REST)     │         │ (Metadata)  │
└─────────────┘         └──────────────┘         └─────────────┘
       │ ▲                      │
       │ │                      │
       │ │                      ▼
       │ │              ┌──────────────┐
       │ └──────────────│   AWS S3     │
       └───────────────▶│  (Storage)   │
                        └──────────────┘
                               │
                               ▼
                        ┌──────────────┐
                        │   SQS Queue  │
                        └──────────────┘
                               │
                               ▼
                        ┌──────────────┐         ┌─────────────┐
                        │AWS Lambda    │────────▶│  Database   │
                        │(Post-process)│         │  (Update)   │
                        └──────────────┘         └─────────────┘
```

### Flow Diagrams

#### Upload Flow
```
1. User selects file in browser
2. Frontend → API: Request upload URL
3. API → Database: Create attachment record (status: Pending)
4. API → Frontend: Return pre-signed S3 URL
5. Frontend → S3: Upload file directly (using pre-signed URL)
6. S3 → SQS: Send ObjectCreated event
7. SQS → Lambda: Trigger post-processing
8. Lambda → Database: Update status to Completed
9. Lambda: Optional virus scan, thumbnail generation, etc.
```

#### Download Flow
```
1. User clicks download in submissions list
2. Frontend → API: Request download URL
3. API → Database: Verify attachment exists and is completed
4. API → Frontend: Return pre-signed S3 URL
5. Frontend → S3: Download file directly (using pre-signed URL)
6. Browser: Save file to user's computer
```

---

## Data Structure

### Database Tables

The system requires two main database tables:

**1. Submissions Table (Existing)**
- Stores form submission data as before (ID, Created date, Content as JSON)

**2. Attachments Table (New)**
- Links attachments to submissions
- Stores metadata: filename, file size, content type
- Tracks S3 location (bucket name and object key)
- Maintains upload status (Pending, Completed, Failed)
- Records timestamps for upload completion

The Attachments table has a foreign key relationship to Submissions for proper cascading deletes.

### S3 Bucket Structure

For optimal performance and to avoid hot partitions at scale, we use hash-based prefixing:

```
s3://formsapp-attachments/
├── submissions/
│   ├── 00/
│   │   ├── 100/
│   │   │   └── {attachment-id}_document.pdf
│   │   └── 200/
│   │       └── {attachment-id}_invoice.xlsx
│   ├── 01/
│   │   └── 101/
│   │       └── {attachment-id}_image.jpg
│   ├── 23/
│   │   └── 123/
│   │       ├── {attachment-id}_contract.pdf
│   │       └── {attachment-id}_schedule.docx
│   └── 99/
│       └── 299/
│           └── {attachment-id}_report.pdf
```

**Key Format:** `submissions/{hash}/{submission-id}/{attachment-id}_filename.ext`

Where `{hash}` is calculated as: `submission-id % 100` (formatted as 2 digits: "00" through "99")

**Example:**
- Submission ID: 123
- Hash: 123 % 100 = 23
- S3 Key: `submissions/23/123/3fa85f64-5717-4562-b3fc-2c963f66afa6_document.pdf`

**Naming Strategy Benefits:**
- **Hash-based distribution** - Distributes load across 100 prefixes (00-99)
- **Avoids hot partitions** - Each prefix can handle 3,500 PUT/sec independently
- **Scalable** - Supports up to 350,000 concurrent uploads/sec theoretical max
- **Maintains grouping** - All files for a submission remain in same hash bucket
- **Predictable lookups** - Easy to calculate hash for listing files
- **GUID attachment IDs** - Prevents filename conflicts
- **Preserves original filenames** - Better UX for downloads

---

## REST API Endpoints

### 1. Generate Upload Pre-Signed URL

**Endpoint:** `POST /api/attachments/upload-url`

**Purpose:**
Creates an attachment record in the database and generates a temporary pre-signed URL for uploading directly to S3.

**Request:**
```json
{
  "submissionId": 123,
  "fileName": "document.pdf",
  "fileSize": 104857600,
  "contentType": "application/pdf"
}
```

**Response:**
```json
{
  "attachmentId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "uploadUrl": "https://formsapp-attachments.s3.amazonaws.com/...",
  "expiresIn": 3600
}
```

**Backend Logic:**
1. Validate file size (max 100MB)
2. Validate content type against whitelist
3. Sanitize filename
4. Calculate hash prefix: `hash = submissionId % 100` (format as 2 digits)
5. Generate S3 key: `submissions/{hash}/{submissionId}/{attachmentId}_{filename}`
6. Create attachment record in database with status "Pending"
7. Generate pre-signed S3 URL (expires in 1 hour)
8. Return URL and attachment ID to frontend

**Example S3 Key Generation:**
```csharp
var hash = (submissionId % 100).ToString("D2");  // "23" for submission 123
var s3Key = $"submissions/{hash}/{submissionId}/{attachmentId}_{sanitizedFileName}";
// Result: submissions/23/123/3fa85f64-5717-4562-b3fc-2c963f66afa6_document.pdf
```

---

### 2. Generate Download Pre-Signed URL

**Endpoint:** `GET /api/attachments/{id}/download-url`

**Purpose:**
Verifies the attachment exists and is completed, then generates a temporary pre-signed URL for downloading from S3.

**Response:**
```json
{
  "downloadUrl": "https://formsapp-attachments.s3.amazonaws.com/...",
  "fileName": "document.pdf",
  "fileSize": 104857600,
  "expiresIn": 300
}
```

**Backend Logic:**
1. Retrieve attachment from database
2. Verify upload status is "Completed"
3. Generate pre-signed S3 URL (expires in 5 minutes)
4. Return URL with original filename

---

### 3. List Attachments for Submission

**Endpoint:** `GET /api/submissions/{id}/attachments`

**Purpose:**
Returns all attachments associated with a specific submission.

**Response:**
```json
{
  "submissionId": 123,
  "totalSize": 157286400,
  "attachments": [
    {
      "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
      "fileName": "document.pdf",
      "fileSize": 104857600,
      "contentType": "application/pdf",
      "uploadStatus": "Completed",
      "uploadedAt": "2025-01-16T10:30:00Z"
    }
  ]
}
```

**Backend Logic:**
1. Query attachments table for given submission ID
2. Order by creation date (descending)
3. Return list with metadata

---

### 4. Delete Attachment

**Endpoint:** `DELETE /api/attachments/{id}`

**Purpose:**
Deletes an attachment from both S3 and the database.

**Response:**
```json
{
  "success": true,
  "message": "Attachment deleted successfully"
}
```

**Backend Logic:**
1. Find attachment in database
2. Delete object from S3
3. Delete database record
4. Return success status

---

## Frontend Implementation

### Upload Flow

The frontend implements file upload with the following steps:

1. **File Selection**
   - User selects file using file input
   - Client-side validation (size, type) for better UX

2. **Request Upload URL**
   - Call API endpoint with file metadata
   - Receive pre-signed URL and attachment ID

3. **Direct Upload to S3**
   - Use HTTP PUT request to upload file to pre-signed URL
   - Track upload progress using progress events
   - Display progress bar to user

4. **Completion**
   - Show success message
   - Refresh attachments list
   - Reset upload form

**Key Features:**
- Progress tracking with percentage display
- Error handling and user feedback
- File validation before upload
- Support for multiple file types

---

### Download Flow

The download implementation follows these steps:

1. **Request Download URL**
   - Call API with attachment ID
   - Receive pre-signed S3 URL

2. **Download from S3**
   - Fetch file as blob using the pre-signed URL
   - No authentication needed (URL is time-limited)

3. **Trigger Browser Download**
   - Create blob URL
   - Programmatically click download link
   - Clean up blob URL after download

**Key Features:**
- Downloads with original filename
- Works across all browsers
- Automatic cleanup of memory

---

### Attachments List

The attachments list component displays:

- All attachments for a submission
- File name, size, and upload status
- Download buttons (enabled only when status is "Completed")
- File size formatted in human-readable format (KB/MB)
- Icons representing file types

---

## Event-Driven Post-Processing

### S3 Event Configuration

The S3 bucket is configured to send events to SQS when objects are created:

```json
{
  "QueueConfigurations": [
    {
      "QueueArn": "arn:aws:sqs:us-east-1:123456789012:formsapp-upload-events",
      "Events": [
        "s3:ObjectCreated:Put",
        "s3:ObjectCreated:CompleteMultipartUpload"
      ],
      "Filter": {
        "Key": {
          "FilterRules": [
            { "Name": "prefix", "Value": "submissions/" }
          ]
        }
      }
    }
  ]
}
```

---

### SQS Queue

**Queue Name:** `formsapp-upload-events`

**Configuration:**
- **Visibility Timeout:** 300 seconds (5 minutes)
- **Message Retention:** 4 days
- **Dead Letter Queue:** Enabled (for failed processing)
- **Maximum Receives:** 3 attempts before DLQ

**Purpose:**
- Buffers upload completion events from S3
- Decouples S3 from Lambda processing
- Provides retry mechanism for failures

---

### AWS Lambda Function

**Trigger:** SQS Queue
**Runtime:** .NET 8

**Responsibilities:**

1. **Process S3 Events**
   - Parse SQS messages containing S3 event notifications
   - Extract bucket name and object key from event

2. **Extract Attachment ID**
   - Parse S3 object key to extract attachment GUID
   - Format: `submissions/{submissionId}/{attachmentId}_filename.ext`

3. **Update Database**
   - Find attachment record by ID
   - Verify file size matches expected size (from S3 metadata)
   - Update status to "Completed" if valid, "Failed" if mismatch
   - Set upload timestamp

4. **Optional Processing**
   - Virus scanning integration
   - Thumbnail generation for images
   - File format validation
   - Metadata extraction

**Error Handling:**
- Failed messages retry up to 3 times
- After max retries, sent to Dead Letter Queue
- Logs all errors for monitoring

---

## Security Considerations

### 1. Pre-Signed URL Security

**Time-Limited Access:**
- Upload URLs expire in 1 hour
- Download URLs expire in 5 minutes
- URLs cannot be reused after expiration

**Scoped Permissions:**
- Each URL allows only one specific operation (PUT or GET)
- URLs are tied to specific S3 object keys
- No access to other objects in bucket

**No Credentials Exposed:**
- Frontend never receives AWS credentials
- Pre-signed URLs are generated server-side only
- Requires authenticated API call to obtain URL

---

### 2. File Validation

**Server-Side Checks:**
- File size limit (100MB maximum)
- Content-type whitelist (PDF, images, Office documents)
- Filename sanitization to prevent path traversal
- Database validation before URL generation

**Client-Side Validation:**
- File size check for better UX
- File type validation using accept attribute
- Immediate feedback to user

---

### 3. S3 Bucket Security

**Bucket Policy:**
- Block all public access
- Require HTTPS for all operations
- Pre-signed URLs are the ONLY access method

**CORS Configuration:**
- Whitelist allowed origins (production domain)
- Allow only PUT and GET methods
- Restrict headers as needed

---

## Scalability & Performance

### Performance Characteristics

| Metric | Traditional Approach | Pre-Signed URL Approach |
|--------|---------------------|-------------------------|
| **Upload Time (100MB)** | ~2-3 minutes (proxied) | ~30-60 seconds (direct) |
| **Server Bandwidth** | 100MB per upload | ~1KB (just URL generation) |
| **Concurrent Uploads** | Limited by server capacity | Unlimited (S3 auto-scales) |
| **Database Load** | High (file streaming) | Low (metadata only) |

---

### Scalability Benefits

#### 1. **Offloaded File Transfer**
- Files bypass application server entirely
- Direct transfer between client and S3
- Application server only generates URLs

#### 2. **Auto-Scaling Components**

| Component | Scaling Behavior | Max Capacity |
|-----------|------------------|--------------|
| **S3** | Automatic | Unlimited |
| **SQS** | Automatic | Unlimited messages |
| **Lambda** | Automatic | 1000 concurrent executions (default) |
| **API Server** | Manual/Auto | Based on configuration |

#### 3. **Throughput Example**

**Scenario:** 1000 users uploading 100MB files simultaneously

**Traditional Approach:**
- Server bandwidth: 100GB total
- Server instances needed: ~50 (2GB each)
- Time: 10-15 minutes per user

**Pre-Signed URL Approach:**
- Server bandwidth: ~1MB (just URL generation)
- Server instances needed: 2-3
- Time: 1-2 minutes per user

---

## Additional Enhancements

### 1. Multipart Upload for Large Files

For files larger than 5MB, AWS provides multipart upload capability:
- Splits file into smaller chunks (5MB each)
- Uploads chunks in parallel for faster transfer
- Supports resumable uploads if connection fails
- Better reliability for large files

This can be implemented using AWS SDK's built-in multipart upload support.

---

### 2. S3 Transfer Acceleration

For faster uploads from geographically distant locations, enable S3 Transfer Acceleration:

**How it works:**
- Uses Amazon CloudFront edge locations
- Routes uploads over AWS's optimized network paths
- Automatically selects the fastest route to S3

**Benefits:**
- 50-500% faster uploads for distant users
- Especially effective for international users
- No code changes required (just enable on bucket)

**Implementation:**
Enable Transfer Acceleration on the S3 bucket and use the accelerated endpoint:
- Standard: `https://formsapp-attachments.s3.amazonaws.com`
- Accelerated: `https://formsapp-attachments.s3-accelerate.amazonaws.com`

---

### 3. CloudFront CDN Integration

Add CloudFront distribution in front of S3 for downloads:

**Benefits:**
- Global edge locations (reduced latency)
- Caching for frequently downloaded files
- SSL/TLS encryption
- DDoS protection

**Implementation:**
Generate CloudFront signed URLs instead of direct S3 URLs for downloads.

---

### 4. S3 Lifecycle Policies

Automatically manage old attachments:

```json
{
  "Rules": [
    {
      "Id": "ArchiveOldAttachments",
      "Status": "Enabled",
      "Transitions": [
        { "Days": 90, "StorageClass": "GLACIER" }
      ],
      "Expiration": { "Days": 365 }
    }
  ]
}
```

**Benefits:**
- Move old files to cheaper storage after 90 days
- Automatically delete files after 1 year
- Significant cost savings for long-term storage

---

## Summary

This architecture provides a **production-ready, scalable solution** for handling large file attachments in a form submission system.

### Key Advantages

✅ **Scalability**
- Handles unlimited concurrent uploads
- S3 auto-scales to millions of files
- Event-driven processing scales independently

✅ **Performance**
- Direct S3 transfer (no server proxy)
- 2-3x faster uploads than traditional approach
- Optional CloudFront CDN for global distribution

✅ **Security**
- Time-limited pre-signed URLs
- No AWS credentials in frontend
- Bucket-level access controls

✅ **Cost-Effective**
- Minimal server resources needed
- Pay only for S3 storage and bandwidth
- Auto-archival to cheaper storage tiers

✅ **Reliability**
- Optional multipart upload for large files
- SQS buffering for event processing
- Dead letter queue for failed operations
- Retry mechanisms built-in

---

### Real-World Usage

This architecture is used by companies like:
- **Dropbox** - File storage and sharing
- **Slack** - File attachments in messages
- **GitHub** - Repository file uploads
- **Trello** - Card attachments
- **Notion** - Document attachments

---

### Next Steps for Production

1. **Set up AWS infrastructure**
   - Create S3 bucket with versioning
   - Configure SQS queue
   - Deploy Lambda function
   - Set up CloudFront (optional)

2. **Implement backend API endpoints**
   - Upload/download URL generation
   - Attachment metadata management
   - Authorization checks

3. **Build frontend components**
   - File upload with progress
   - Attachment list/download
   - Error handling

4. **Add monitoring**
   - CloudWatch metrics
   - Error logging
   - Cost tracking

5. **Security hardening**
   - Enable MFA for AWS
   - Set up WAF rules
   - Regular security audits

---

## Additional Resources

- [AWS S3 Pre-Signed URLs Documentation](https://docs.aws.amazon.com/AmazonS3/latest/userguide/PresignedUrlUploadObject.html)
- [AWS Lambda Best Practices](https://docs.aws.amazon.com/lambda/latest/dg/best-practices.html)
- [S3 Event Notifications](https://docs.aws.amazon.com/AmazonS3/latest/userguide/NotificationHowTo.html)
- [CloudFront Signed URLs](https://docs.aws.amazon.com/AmazonCloudFront/latest/DeveloperGuide/private-content-signed-urls.html)

---

**Document Version:** 1.0
**Last Updated:** Nov 16, 2025
**Author:** FormsApp Development Team
