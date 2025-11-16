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
            },
            new()
            {
                Content = """
                {
                  "fullName": "Samuel Jackson",
                  "email": "samuel.jackson@example.com",
                  "country": "USA",
                  "birthDate": "1991-09-21",
                  "gender": "Male",
                  "newsletter": false
                }
                """,
                Created = DateTime.UtcNow.AddHours(-23)
            },
            new()
            {
                Content = """
                {
                  "fullName": "Tina Turner",
                  "email": "tina.turner@example.com",
                  "country": "Germany",
                  "birthDate": "1986-05-30",
                  "gender": "Female",
                  "newsletter": true
                }
                """,
                Created = DateTime.UtcNow.AddHours(-22)
            },
            new()
            {
                Content = """
                {
                  "fullName": "Uma Thurman",
                  "email": "uma.thurman@example.com",
                  "country": "France",
                  "birthDate": "1990-04-29",
                  "gender": "Female",
                  "newsletter": true
                }
                """,
                Created = DateTime.UtcNow.AddHours(-21)
            },
            new()
            {
                Content = """
                {
                  "fullName": "Victor Hugo",
                  "email": "victor.hugo@example.com",
                  "country": "France",
                  "birthDate": "1988-02-26",
                  "gender": "Male",
                  "newsletter": false
                }
                """,
                Created = DateTime.UtcNow.AddHours(-20)
            },
            new()
            {
                Content = """
                {
                  "fullName": "Wendy Adams",
                  "email": "wendy.adams@example.com",
                  "country": "UK",
                  "birthDate": "1993-07-12",
                  "gender": "Female",
                  "newsletter": true
                }
                """,
                Created = DateTime.UtcNow.AddHours(-19)
            },
            new()
            {
                Content = """
                {
                  "fullName": "Xavier Woods",
                  "email": "xavier.woods@example.com",
                  "country": "Australia",
                  "birthDate": "1992-10-05",
                  "gender": "Male",
                  "newsletter": false
                }
                """,
                Created = DateTime.UtcNow.AddHours(-18)
            },
            new()
            {
                Content = """
                {
                  "fullName": "Yolanda King",
                  "email": "yolanda.king@example.com",
                  "country": "USA",
                  "birthDate": "1995-01-15",
                  "gender": "Female",
                  "newsletter": true
                }
                """,
                Created = DateTime.UtcNow.AddHours(-17)
            },
            new()
            {
                Content = """
                {
                  "fullName": "Zachary Taylor",
                  "email": "zachary.taylor@example.com",
                  "country": "Canada",
                  "birthDate": "1987-11-20",
                  "gender": "Male",
                  "newsletter": true
                }
                """,
                Created = DateTime.UtcNow.AddHours(-16)
            },
            new()
            {
                Content = """
                {
                  "fullName": "Amy Chen",
                  "email": "amy.chen@example.com",
                  "country": "New Zealand",
                  "birthDate": "1991-03-08",
                  "gender": "Female",
                  "newsletter": false
                }
                """,
                Created = DateTime.UtcNow.AddHours(-15)
            },
            new()
            {
                Content = """
                {
                  "fullName": "Brian Foster",
                  "email": "brian.foster@example.com",
                  "country": "Ireland",
                  "birthDate": "1989-08-14",
                  "gender": "Male",
                  "newsletter": true
                }
                """,
                Created = DateTime.UtcNow.AddHours(-14)
            },
            new()
            {
                Content = """
                {
                  "fullName": "Catherine Zeta",
                  "email": "catherine.zeta@example.com",
                  "country": "UK",
                  "birthDate": "1990-09-25",
                  "gender": "Female",
                  "newsletter": false
                }
                """,
                Created = DateTime.UtcNow.AddHours(-13)
            },
            new()
            {
                Content = """
                {
                  "fullName": "Daniel Craig",
                  "email": "daniel.craig@example.com",
                  "country": "UK",
                  "birthDate": "1986-03-02",
                  "gender": "Male",
                  "newsletter": true
                }
                """,
                Created = DateTime.UtcNow.AddHours(-12)
            },
            new()
            {
                Content = """
                {
                  "fullName": "Emma Watson",
                  "email": "emma.watson@example.com",
                  "country": "UK",
                  "birthDate": "1992-04-15",
                  "gender": "Female",
                  "newsletter": true
                }
                """,
                Created = DateTime.UtcNow.AddHours(-11)
            },
            new()
            {
                Content = """
                {
                  "fullName": "Frank Sinatra",
                  "email": "frank.sinatra@example.com",
                  "country": "USA",
                  "birthDate": "1988-12-12",
                  "gender": "Male",
                  "newsletter": false
                }
                """,
                Created = DateTime.UtcNow.AddHours(-10)
            },
            new()
            {
                Content = """
                {
                  "fullName": "Grace Kelly",
                  "email": "grace.kelly@example.com",
                  "country": "France",
                  "birthDate": "1993-11-12",
                  "gender": "Female",
                  "newsletter": true
                }
                """,
                Created = DateTime.UtcNow.AddHours(-9)
            },
            new()
            {
                Content = """
                {
                  "fullName": "Henry Cavill",
                  "email": "henry.cavill@example.com",
                  "country": "UK",
                  "birthDate": "1987-05-05",
                  "gender": "Male",
                  "newsletter": true
                }
                """,
                Created = DateTime.UtcNow.AddHours(-8)
            },
            new()
            {
                Content = """
                {
                  "fullName": "Iris West",
                  "email": "iris.west@example.com",
                  "country": "USA",
                  "birthDate": "1994-07-24",
                  "gender": "Female",
                  "newsletter": false
                }
                """,
                Created = DateTime.UtcNow.AddHours(-7)
            },
            new()
            {
                Content = """
                {
                  "fullName": "Jack Ryan",
                  "email": "jack.ryan@example.com",
                  "country": "USA",
                  "birthDate": "1989-02-18",
                  "gender": "Male",
                  "newsletter": true
                }
                """,
                Created = DateTime.UtcNow.AddHours(-6)
            },
            new()
            {
                Content = """
                {
                  "fullName": "Kate Middleton",
                  "email": "kate.middleton@example.com",
                  "country": "UK",
                  "birthDate": "1990-01-09",
                  "gender": "Female",
                  "newsletter": true
                }
                """,
                Created = DateTime.UtcNow.AddHours(-5)
            },
            new()
            {
                Content = """
                {
                  "fullName": "Liam Neeson",
                  "email": "liam.neeson@example.com",
                  "country": "Ireland",
                  "birthDate": "1985-06-07",
                  "gender": "Male",
                  "newsletter": false
                }
                """,
                Created = DateTime.UtcNow.AddHours(-4)
            },
            new()
            {
                Content = """
                {
                  "fullName": "Morgan Freeman",
                  "email": "morgan.freeman@example.com",
                  "country": "USA",
                  "birthDate": "1987-09-22",
                  "gender": "Male",
                  "newsletter": true
                }
                """,
                Created = DateTime.UtcNow.AddHours(-3)
            },
            new()
            {
                Content = """
                {
                  "fullName": "Natalie Portman",
                  "email": "natalie.portman@example.com",
                  "country": "USA",
                  "birthDate": "1991-06-09",
                  "gender": "Female",
                  "newsletter": true
                }
                """,
                Created = DateTime.UtcNow.AddHours(-2)
            },
            new()
            {
                Content = """
                {
                  "fullName": "Oliver Twist",
                  "email": "oliver.twist@example.com",
                  "country": "UK",
                  "birthDate": "1993-04-03",
                  "gender": "Male",
                  "newsletter": false
                }
                """,
                Created = DateTime.UtcNow.AddHours(-1)
            },
            new()
            {
                Content = """
                {
                  "fullName": "Penelope Cruz",
                  "email": "penelope.cruz@example.com",
                  "country": "Sweden",
                  "birthDate": "1988-08-28",
                  "gender": "Female",
                  "newsletter": true
                }
                """,
                Created = DateTime.UtcNow.AddMinutes(-55)
            },
            new()
            {
                Content = """
                {
                  "fullName": "Quinn Fabray",
                  "email": "quinn.fabray@example.com",
                  "country": "Canada",
                  "birthDate": "1995-05-16",
                  "gender": "Female",
                  "newsletter": false
                }
                """,
                Created = DateTime.UtcNow.AddMinutes(-50)
            },
            new()
            {
                Content = """
                {
                  "fullName": "Ryan Gosling",
                  "email": "ryan.gosling@example.com",
                  "country": "Canada",
                  "birthDate": "1986-11-12",
                  "gender": "Male",
                  "newsletter": true
                }
                """,
                Created = DateTime.UtcNow.AddMinutes(-45)
            },
            new()
            {
                Content = """
                {
                  "fullName": "Sarah Connor",
                  "email": "sarah.connor@example.com",
                  "country": "USA",
                  "birthDate": "1992-07-13",
                  "gender": "Female",
                  "newsletter": true
                }
                """,
                Created = DateTime.UtcNow.AddMinutes(-40)
            },
            new()
            {
                Content = """
                {
                  "fullName": "Tom Hanks",
                  "email": "tom.hanks@example.com",
                  "country": "USA",
                  "birthDate": "1987-03-19",
                  "gender": "Male",
                  "newsletter": false
                }
                """,
                Created = DateTime.UtcNow.AddMinutes(-35)
            },
            new()
            {
                Content = """
                {
                  "fullName": "Ursula Schmidt",
                  "email": "ursula.schmidt@example.com",
                  "country": "Germany",
                  "birthDate": "1990-10-31",
                  "gender": "Female",
                  "newsletter": true
                }
                """,
                Created = DateTime.UtcNow.AddMinutes(-30)
            },
            new()
            {
                Content = """
                {
                  "fullName": "Vincent Van Gogh",
                  "email": "vincent.vangogh@example.com",
                  "country": "Norway",
                  "birthDate": "1989-12-30",
                  "gender": "Male",
                  "newsletter": true
                }
                """,
                Created = DateTime.UtcNow.AddMinutes(-25)
            }
        };

        context.Submissions.AddRange(submissions);
        context.SaveChanges();
    }
}
