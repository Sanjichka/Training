using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Trening.Data;

namespace Trening.Models
{
    public class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new TreningContext(serviceProvider.GetRequiredService<DbContextOptions<TreningContext>>()))
            {
                if (context.User.Any() || context.Coach.Any() || context.Training.Any() || context.Discipline.Any()) { return; }

                context.User.AddRange(new User
                {
                    Username = "Sanjichka",
                    Embg = "1234567891011",
                    FirstName = "Sanja",
                    LastName = "Siljanoska",
                    Mail = "sanjavasil@gmail.com"
                });

                context.Coach.AddRange(new Coach { Username = "Emi", ExerciseRank = "Advanced", Awards = "None" });

                context.Discipline.AddRange(new Discipline {DisciplineName = "Fitness", Type = "Individual", Equipment = "Exercise Mat, Bands, Weights", Ground = "Anywhere" });

                context.Training.AddRange(new Training {TrainingName = "Emi's Personal Fitness Training", Platform = "Zoom", CompanyCoache = "FitMK", Price = 12.99M, NumClMonth = 12 });

                context.Enrollment.AddRange(new Enrollment { UserID = 1, TrainingID = 1, Owe = 0M});

                context.SaveChanges();
            }
        }
    }
}
