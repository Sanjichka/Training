using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Trening.Areas.Identity.Data;
using Trening.Data;

namespace Trening.Models
{
    public class SeedData
    {

        public static async Task CreateUserRoles(IServiceProvider serviceProvider)
        {
            var RoleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var UserManager = serviceProvider.GetRequiredService<UserManager<TreningKorisnik>>();
            IdentityResult roleResult;
            IdentityResult roleResult1;
            IdentityResult roleResult2;
            IdentityResult roleResult3;
            //Add Admin Role
            var roleCheck = await RoleManager.RoleExistsAsync("Admin");
            if (!roleCheck) { roleResult = await RoleManager.CreateAsync(new IdentityRole("Admin")); }
            TreningKorisnik user = await UserManager.FindByEmailAsync("admin@mvctrening.com");
            if (user == null)
            {
                var User = new TreningKorisnik();
                User.Email = "admin@mvctrening.com";
                User.UserName = "admin@mvctrening.com";
                string userPWD = "Admin123";
                IdentityResult chkUser = await UserManager.CreateAsync(User, userPWD);
                if (chkUser.Succeeded) { var result1 = await UserManager.AddToRoleAsync(User, "Admin"); }
            }
            //Add Coach Role
            var roleCheck1 = await RoleManager.RoleExistsAsync("Coach");
            if (!roleCheck1) { roleResult1 = await RoleManager.CreateAsync(new IdentityRole("Coach")); }
            TreningKorisnik coach = await UserManager.FindByEmailAsync("coach@mvctrening.com");
            if (coach == null)
            {
                var User = new TreningKorisnik();
                User.Email = "coach@mvctrening.com";
                User.UserName = "coach@mvctrening.com";
                string userPWD = "Coach123";
                IdentityResult chkUser = await UserManager.CreateAsync(User, userPWD);
                if (chkUser.Succeeded) { var result2 = await UserManager.AddToRoleAsync(User, "Coach"); }
            }
            //Add User Role
            var roleCheck2 = await RoleManager.RoleExistsAsync("Userr");
            if (!roleCheck2) { roleResult2 = await RoleManager.CreateAsync(new IdentityRole("Userr")); }
            TreningKorisnik userz = await UserManager.FindByEmailAsync("userr@mvctrening.com");
            if (userz == null)
            {
                var User = new TreningKorisnik();
                User.Email = "userr@mvctrening.com";
                User.UserName = "userr@mvctrening.com";
                string userPWD = "Userr123";
                IdentityResult chkUser = await UserManager.CreateAsync(User, userPWD);
                if (chkUser.Succeeded) { var result3 = await UserManager.AddToRoleAsync(User, "Userr"); }
            }
            //Add Nikoj Role
            var roleCheck3 = await RoleManager.RoleExistsAsync("Nikoj");
            if (!roleCheck3) { roleResult3 = await RoleManager.CreateAsync(new IdentityRole("Nikoj")); }
            TreningKorisnik nikoj = await UserManager.FindByEmailAsync("nikoj@mvctrening.com");
            if (nikoj == null)
            {
                var User = new TreningKorisnik();
                User.Email = "nikoj@mvctrening.com";
                User.UserName = "nikoj@mvctrening.com";
                string userPWD = "Nikoj123";
                IdentityResult chkUser = await UserManager.CreateAsync(User, userPWD);
                if (chkUser.Succeeded) { var result4 = await UserManager.AddToRoleAsync(User, "Nikoj"); }
            }

        }
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new TreningContext(serviceProvider.GetRequiredService<DbContextOptions<TreningContext>>()))
            {
                CreateUserRoles(serviceProvider).Wait();
                if (context.User.Any() || context.Coach.Any() || context.Training.Any() || context.Discipline.Any()) { return; }

                context.User.AddRange(
                    new User {
                        Username = "Sanjs",
                        Embg = "1234567891011",
                        FirstName = "Sanja",
                        LastName = "Siljanoska",
                        gender="F",
                        City="Skopje",
                        ExerciseLevel="Advanced",
                        Mail = "sanjavasil@gmail.com"
                    },
                    new User
                    {
                        Username = "V V",
                        Embg = "1234567891012",
                        FirstName = "V",
                        LastName = "V",
                        gender="M",
                        City="Bitola",
                        Mail = "vks@gmail.com"
                    },
                    new User
                    {
                        Username = "P SV",
                        Embg = "1234567891013",
                        FirstName = "Pufi",
                        LastName = "SV",
                        gender = "j",
                        City = "Negotino",
                        Mail = "pufisv@gmail.com"
                    }

                    );

                context.Coach.AddRange(
                    new Coach { 
                        Username = "Emi",
                        ExerciseRank = "Advanced",
                        BirthDate= DateTime.Parse("2000-01-17"),
                        Mail="emi@gmail.com"
                    },
                    new Coach { 
                        Username = "Slavica",
                        ExerciseRank = "Intermediate",
                        BirthDate= DateTime.Parse("1998-02-03"),
                        Mail="slavica@gmail.com"
                    },
                    new Coach
                    {
                        Username = "Aleks",
                        ExerciseRank = "Advanced",
                        BirthDate = DateTime.Parse("1991-01-22"),
                        Mail = "aleks@gmail.com"
                    },
                    new Coach
                    {
                        Username = "Anna",
                        ExerciseRank = "Advanced",
                        BirthDate = DateTime.Parse("2000-01-20"),
                        Mail = "anna@gmail.com"
                    }
                    );

                context.Discipline.AddRange(
                    new Discipline {
                        DisciplineName = "Fitness", 
                        Type = "Individual", 
                        Equipment = "Exercise Mat, Bands, Weights", 
                        Ground = "Anywhere" 
                    },
                    new Discipline
                    {
                        DisciplineName = "Yoga",
                        Type = "Individual",
                        Equipment = "Exercise Mat",
                        Ground = "Anywhere"
                    },
                    new Discipline
                    {
                        DisciplineName = "Dancing",
                        Type = "Individual",
                        Equipment = "Exercise Mat",
                        Ground = "Anywhere"
                    },
                    new Discipline
                    {
                        DisciplineName = "Chess",
                        Type = "Individual",
                        Equipment = "Chess Board",
                        Ground = "Anywhere"
                    },
                    new Discipline
                    {
                        DisciplineName = "Football Tricks",
                        Type = "Individual",
                        Equipment = "Football Ball",
                        Ground = "Outside"
                    },
                    new Discipline
                    {
                        DisciplineName = "Pilates",
                        Type = "Individual",
                        Equipment = "Exercise Mat, Pilates Ball, Bands, Rings, Chair",
                        Ground = "Anywhere"
                    },
                    new Discipline
                    {
                        DisciplineName = "Magic Tricks",
                        Type = "Individual",
                        Equipment = "Cards",
                        Ground = "Anywhere"
                    },
                    new Discipline
                    {
                        DisciplineName = "Home Gym",
                        Type = "Individual",
                        Equipment = "Gym Equipment at Home",
                        Ground = "Gym/Home Gym"
                    }
                    );

                context.Training.AddRange(
                    new Training {
                        TrainingName = "Emi's Personal Fitness Training", 
                        Platform = "Zoom", 
                        CompanyCoache = "FitMK", 
                        Price = 12.99M, 
                        NumClMonth = 12,
                        Discipline="Fitness"
                    },
                    new Training
                    {
                        TrainingName = "Yoga With Slavica",
                        Platform = "Microsoft Teams",
                        CompanyCoache = "FitMKz",
                        Price = 10.99M,
                        NumClMonth = 16,
                        Discipline="Yoga"
                    },
                    new Training
                    {
                        TrainingName = "Home Gym With Aleks",
                        Platform = "Skype",
                        CompanyCoache = "FitMK",
                        Price = 17.99M,
                        NumClMonth = 18,
                        Discipline = "Gym/Home Gym"
                    },
                    new Training
                    {
                        TrainingName = "Dance With Anna",
                        Platform = "Zoom",
                        CompanyCoache = "FitMKz",
                        Price = 20.99M,
                        NumClMonth = 13,
                        Discipline = "Dancing"
                    },
                    new Training
                    {
                        TrainingName = "Pilates With Emi",
                        Platform = "Zoom",
                        CompanyCoache = "FitMK",
                        Price = 16.99M,
                        NumClMonth = 13,
                        Discipline = "Pilates"
                    }
                    );

                context.Enrollment.AddRange(new Enrollment { UserID = 1, TrainingID = 1, Owe = 0M});

                context.SaveChanges();
            }
        }
    }
}
