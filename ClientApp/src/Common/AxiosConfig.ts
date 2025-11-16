import axios from 'axios';

// Use environment variable for API base URL
// In development: reads from .env file (VITE_API_BASE_URL)
// In production: set via environment variables during build/deployment
// Fallback to localhost for local development if not set
axios.defaults.baseURL = import.meta.env.VITE_API_BASE_URL || 'http://localhost:5026/';
axios.defaults.headers.common['Accept'] = '*/*';
axios.defaults.headers.post['Content-Type'] = 'application/json';
axios.defaults.headers.get['Cache-Control'] = 'no-cache';
axios.defaults.headers.get['Pragma'] = 'no-cache';
axios.defaults.headers.get['Expires'] = '0';

export default axios;