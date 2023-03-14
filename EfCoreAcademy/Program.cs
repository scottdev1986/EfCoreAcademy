using EfCoreAcademy.Context;
using Microsoft.EntityFrameworkCore;


namespace EfCoreAcademy
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<EfCoreDbContext>().UseSqlite("Filename=EfCoreAcademy.db")
                .Options;
            var dbContext = new EfCoreDbContext(optionsBuilder);

            dbContext.Database.Migrate();

            ProcessDelete(dbContext);
            ProcessInserts();
        }

        private static void ProcessDelete(EfCoreDbContext dbContext)
        {
            var professor = dbContext.Professors.ToList();
            var student = dbContext.Students.ToList();
            var address = dbContext.Addresses.ToList();
            var classes = dbContext.Classes.ToList();

            dbContext.Professors.RemoveRange(professor);
            dbContext.Students.RemoveRange(student);
            dbContext.Addresses.RemoveRange(address);
            dbContext.Classes.RemoveRange(classes);

            dbContext.SaveChanges();
            dbContext.Dispose();
        }

        private static void ProcessInserts()
        {
            var optionsBuilder = new DbContextOptionsBuilder<EfCoreDbContext>().UseSqlite("Filename=EfCoreAcademy.db")
                .Options;
            var dbContext = new EfCoreDbContext(optionsBuilder);

            // Randomly generate data
            var street = new[] { "Main", "1st", "2nd", "3rd", "4th", "5th", "6th", "7th", "8th", "9th", "10th" };
            var city = new[]
            {
                "Seattle", "Tacoma", "Bellevue", "Redmond", "Kirkland", "Renton", "Kent", "Auburn", "Olympia", "Spokane"
            };
            var state = new[]
            {
                "WA", "OR", "CA", "ID", "NV", "UT", "AZ", "MT", "WY", "CO", "NM", "TX", "OK", "KS", "NE", "SD", "ND",
                "MN", "IA", "MO", "AR", "LA", "MS", "AL", "TN", "KY", "WV", "VA", "NC", "SC", "GA", "FL", "MI", "IN",
                "OH", "PA", "NY", "VT", "NH", "ME", "MA", "RI", "CT", "NJ", "DE", "MD", "DC"
            };
            var zipCode = new[]
            {
                12901, 03053, 08807, 21227, 21122, 15068, 27405, 35007, 07002, 46322, 11354, 54952, 27703, 11102, 36526,
                48060, 30701, 46410, 48021, 08053
            };

            var firstName = new[] { "John", "Jane", "Joe", "Jill", "Jack", "Jill", "Jenny", "Jen", "Jenna", "Jesse" };
            var lastName = new[] { "Doe", "Smith", "Jones", "Williams", "Brown", "Davis", "Miller", "Wilson", "Moore" };
            var classes = new[] { "Math", "Science", "English", "History", "CS 101", "CS 201", "CS 301", "CS 401" };

            var random = new Random();

            for (var i = 0; i < 100; i++)
            {
                var address = new Model.Address
                {
                    Street = $"{random.Next(1, 1000)} {street[random.Next(0, street.Length)]} Street",
                    City = city[random.Next(0, city.Length)],
                    State = state[random.Next(0, state.Length)],
                    ZipCode = zipCode[random.Next(0, zipCode.Length)].ToString()
                };
                var professor = new Model.Professor
                {
                    FirstName = firstName[random.Next(0, firstName.Length)],
                    LastName = lastName[random.Next(0, lastName.Length)],
                    Address = address
                };
                var student1 = new Model.Student
                {
                    FirstName = firstName[random.Next(0, firstName.Length)],
                    LastName = lastName[random.Next(0, lastName.Length)],
                    Address = address
                };
                var student2 = new Model.Student
                {
                    FirstName = firstName[random.Next(0, firstName.Length)],
                    LastName = lastName[random.Next(0, lastName.Length)],
                    Address = address
                };
                // Make sure that we do not have duplicate classes
                var class1 = new Model.Class
                {
                    Title = classes[random.Next(0, classes.Length)],
                    Professor = professor,
                    Students = new[] { student1, student2 }
                };
                var class2 = new Model.Class
                {
                    Title = classes[random.Next(0, classes.Length)],
                    Professor = professor,
                    Students = new[] { student1, student2 }
                };
                dbContext.Addresses.Add(address);
                dbContext.Professors.Add(professor);
                dbContext.Students.Add(student1);
                dbContext.Students.Add(student2);
                dbContext.Classes.Add(class1);
                dbContext.Classes.Add(class2);
            }
            
            dbContext.SaveChanges();
            dbContext.Dispose();
        }
    }
}