using FormsApp.Core.Data;
using FormsApp.Core.Data.Entities;

namespace FormsApp.Data;

public static class DatabaseSeeder
{
    public static void Seed(ApplicationDbContext context)
    {
        if (context.Submissions.Any())
            return;

        var submissions = new List<Submission>
        {
            new()
            {
                Content = """
                {
                  "fullName": "John Doe",
                  "email": "john.doe@example.com",
                  "country": "USA",
                  "birthDate": "1990-05-15",
                  "gender": "Male",
                  "newsletter": true
                }
                """,
                Created = DateTime.UtcNow.AddDays(-20)
            },
            new()
            {
                Content = """
                {
                  "fullName": "Jane Smith",
                  "email": "jane.smith@example.com",
                  "country": "Canada",
                  "birthDate": "1985-08-22",
                  "gender": "Female",
                  "newsletter": false
                }
                """,
                Created = DateTime.UtcNow.AddDays(-19)
            },
            new()
            {
                Content = """
                {
                  "fullName": "Bob Johnson",
                  "email": "bob.johnson@example.com",
                  "country": "UK",
                  "birthDate": "1992-03-10",
                  "gender": "Male",
                  "newsletter": true
                }
                """,
                Created = DateTime.UtcNow.AddDays(-18)
            },
            new()
            {
                Content = """
                {
                  "fullName": "Alice Williams",
                  "email": "alice.williams@example.com",
                  "country": "Australia",
                  "birthDate": "1988-11-30",
                  "gender": "Female",
                  "newsletter": true
                }
                """,
                Created = DateTime.UtcNow.AddDays(-17)
            },
            new()
            {
                Content = """
                {
                  "fullName": "Charlie Brown",
                  "email": "charlie.brown@example.com",
                  "country": "Germany",
                  "birthDate": "1995-07-04",
                  "gender": "Male",
                  "newsletter": false
                }
                """,
                Created = DateTime.UtcNow.AddDays(-16)
            },
            new()
            {
                Content = """
                {
                  "fullName": "Diana Prince",
                  "email": "diana.prince@example.com",
                  "country": "France",
                  "birthDate": "1991-12-25",
                  "gender": "Female",
                  "newsletter": true
                }
                """,
                Created = DateTime.UtcNow.AddDays(-15)
            },
            new()
            {
                Content = """
                {
                  "fullName": "Edward Norton",
                  "email": "edward.norton@example.com",
                  "country": "USA",
                  "birthDate": "1987-02-14",
                  "gender": "Male",
                  "newsletter": false
                }
                """,
                Created = DateTime.UtcNow.AddDays(-14)
            },
            new()
            {
                Content = """
                {
                  "fullName": "Fiona Green",
                  "email": "fiona.green@example.com",
                  "country": "Ireland",
                  "birthDate": "1993-09-18",
                  "gender": "Female",
                  "newsletter": true
                }
                """,
                Created = DateTime.UtcNow.AddDays(-13)
            },
            new()
            {
                Content = """
                {
                  "fullName": "George Miller",
                  "email": "george.miller@example.com",
                  "country": "New Zealand",
                  "birthDate": "1989-06-07",
                  "gender": "Male",
                  "newsletter": false
                }
                """,
                Created = DateTime.UtcNow.AddDays(-12)
            },
            new()
            {
                Content = """
                {
                  "fullName": "Hannah White",
                  "email": "hannah.white@example.com",
                  "country": "Sweden",
                  "birthDate": "1994-04-20",
                  "gender": "Female",
                  "newsletter": true
                }
                """,
                Created = DateTime.UtcNow.AddDays(-11)
            },
            new()
            {
                Content = """
                {
                  "fullName": "Isaac Newton",
                  "email": "isaac.newton@example.com",
                  "country": "UK",
                  "birthDate": "1986-01-04",
                  "gender": "Male",
                  "newsletter": false
                }
                """,
                Created = DateTime.UtcNow.AddDays(-10)
            },
            new()
            {
                Content = """
                {
                  "fullName": "Julia Roberts",
                  "email": "julia.roberts@example.com",
                  "country": "USA",
                  "birthDate": "1990-10-28",
                  "gender": "Female",
                  "newsletter": true
                }
                """,
                Created = DateTime.UtcNow.AddDays(-9)
            },
            new()
            {
                Content = """
                {
                  "fullName": "Kevin Hart",
                  "email": "kevin.hart@example.com",
                  "country": "Canada",
                  "birthDate": "1992-07-06",
                  "gender": "Male",
                  "newsletter": true
                }
                """,
                Created = DateTime.UtcNow.AddDays(-8)
            },
            new()
            {
                Content = """
                {
                  "fullName": "Laura Palmer",
                  "email": "laura.palmer@example.com",
                  "country": "Norway",
                  "birthDate": "1991-03-22",
                  "gender": "Female",
                  "newsletter": false
                }
                """,
                Created = DateTime.UtcNow.AddDays(-7)
            },
            new()
            {
                Content = """
                {
                  "fullName": "Michael Scott",
                  "email": "michael.scott@example.com",
                  "country": "USA",
                  "birthDate": "1988-05-11",
                  "gender": "Male",
                  "newsletter": true
                }
                """,
                Created = DateTime.UtcNow.AddDays(-6)
            },
            new()
            {
                Content = """
                {
                  "fullName": "Nancy Drew",
                  "email": "nancy.drew@example.com",
                  "country": "UK",
                  "birthDate": "1993-08-15",
                  "gender": "Female",
                  "newsletter": false
                }
                """,
                Created = DateTime.UtcNow.AddDays(-5)
            },
            new()
            {
                Content = """
                {
                  "fullName": "Oscar Wilde",
                  "email": "oscar.wilde@example.com",
                  "country": "Ireland",
                  "birthDate": "1987-11-03",
                  "gender": "Male",
                  "newsletter": true
                }
                """,
                Created = DateTime.UtcNow.AddDays(-4)
            },
            new()
            {
                Content = """
                {
                  "fullName": "Patricia Hill",
                  "email": "patricia.hill@example.com",
                  "country": "Australia",
                  "birthDate": "1995-02-28",
                  "gender": "Female",
                  "newsletter": true
                }
                """,
                Created = DateTime.UtcNow.AddDays(-3)
            },
            new()
            {
                Content = """
                {
                  "fullName": "Quentin Tarantino",
                  "email": "quentin.tarantino@example.com",
                  "country": "USA",
                  "birthDate": "1989-12-09",
                  "gender": "Male",
                  "newsletter": false
                }
                """,
                Created = DateTime.UtcNow.AddDays(-2)
            },
            new()
            {
                Content = """
                {
                  "fullName": "Rachel Green",
                  "email": "rachel.green@example.com",
                  "country": "Canada",
                  "birthDate": "1994-06-17",
                  "gender": "Female",
                  "newsletter": true
                }
                """,
                Created = DateTime.UtcNow.AddDays(-1)
            }
        };

        context.Submissions.AddRange(submissions);
        context.SaveChanges();
    }
}
