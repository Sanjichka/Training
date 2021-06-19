using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Trening.Data;
using Trening.Models;
using Trening.ViewModels;

namespace Trening.Controllers
{
    public class UsersController : Controller
    {
        private readonly TreningContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public UsersController(TreningContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: Users
        public IActionResult Index(string searchMail, string searchUsername, string searchCity)
        {

            IQueryable<User> users = _context.User.AsQueryable();
            IQueryable<string> query = _context.User.OrderBy(m => m.City).Select(m => m.City).Distinct();

            if (!string.IsNullOrEmpty(searchUsername))
            {
                users = users.Where(s => s.Username.ToLower().Contains(searchUsername.ToLower()));
            }
            if (!string.IsNullOrEmpty(searchMail))
            {
                users = users.Where(s => s.Mail.ToLower().Contains(searchMail.ToLower()));
            }
            if (!string.IsNullOrEmpty(searchCity))
            {
                users = users.Where(s => s.City == searchCity);
            }

            var VM = new Filter
            {
                CityList = new SelectList(query.AsEnumerable()),
                Users = users.AsEnumerable()
            };


            return View(VM);
        }




            // GET: Users/Details/5
            public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.User
                .FirstOrDefaultAsync(m => m.ID == id);
            if (user == null)
            {
                return NotFound();
            }

            ViewData["U"] = user.FullName;

            return View(user);
        }

        public IActionResult Create()
        {
            return View();
        }
        /*
                // POST: Students/Create
                // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
                // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
                [Authorize(Roles = "Admin")]
                [HttpPost]
                [ValidateAntiForgeryToken]
                public async Task<IActionResult> Create(StudentVM Vmodel)
                {
                    if (ModelState.IsValid)
                    {
                        string uniqueFileName = UploadedFile(Vmodel);

                        Student student = new Student
                        {
                            ProfilePicture = uniqueFileName,
                            StudentId = Vmodel.StudentId,
                            FirstName = Vmodel.FirstName,
                            LastName = Vmodel.LastName,
                            EnrollmentDate = Vmodel.EnrollmentDate,
                            AcquiredCredits = Vmodel.AcquiredCredits,
                            CurrentSemestar = Vmodel.CurrentSemestar,
                            EducationLevel = Vmodel.EducationLevel,
                            Enrollments = Vmodel.Enrollments,
                        };

                        _context.Add(student);
                        await _context.SaveChangesAsync();
                        return RedirectToAction(nameof(Index));
                    }
                    return View();
                }
                [Authorize(Roles = "Admin")]
                private string UploadedFile(StudentVM model)
                {
                    string uniqueFileName = null;

                    if (model.ProfilePicture != null)
                    {
                        string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images");
                        uniqueFileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(model.ProfilePicture.FileName);
                        string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                        using (var fileStream = new FileStream(filePath, FileMode.Create))
                        {
                            model.ProfilePicture.CopyTo(fileStream);

                        }
                    }
                    return uniqueFileName;
                }

                // GET: Students/Edit/5
                [Authorize(Roles = "Admin")]
                public async Task<IActionResult> Edit(long? id)
                {
                    if (id == null)
                    {
                        return NotFound();
                    }

                    var student = await _context.Students.FindAsync(id);
                    if (student == null)
                    {
                        return NotFound();
                    }
                    StudentVM Vmodel = new StudentVM
                    {
                        ID = student.ID,
                        FirstName = student.FirstName,
                        LastName = student.LastName,
                        StudentId = student.StudentId,
                        EnrollmentDate = student.EnrollmentDate,
                        AcquiredCredits = student.AcquiredCredits,
                        CurrentSemestar = student.CurrentSemestar,
                        EducationLevel = student.EducationLevel,
                        Enrollments = student.Enrollments
                    };
                    return View(Vmodel);
                }

                // POST: Students/Edit/5
                // To protect from overposting attacks, enable the specific properties you want to bind to, for 
                // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
                [HttpPost]
                [Authorize(Roles = "Admin")]
                [ValidateAntiForgeryToken]
                public async Task<IActionResult> Edit(long id, StudentVM Vmodel)
                {
                    if (id != Vmodel.ID)
                    {
                        return NotFound();
                    }

                    if (ModelState.IsValid)
                    {
                        try
                        {
                            string uniqueFileName = UploadedFile(Vmodel);

                            Student student = new Student
                            {
                                ID = Vmodel.ID,
                                FirstName = Vmodel.FirstName,
                                LastName = Vmodel.LastName,
                                ProfilePicture = uniqueFileName,
                                EnrollmentDate = Vmodel.EnrollmentDate,
                                CurrentSemestar = Vmodel.CurrentSemestar,
                                AcquiredCredits = Vmodel.AcquiredCredits,
                                StudentId = Vmodel.StudentId,
                                EducationLevel = Vmodel.EducationLevel,
                                Enrollments = Vmodel.Enrollments
                            };
                            _context.Update(student);
                            await _context.SaveChangesAsync();
                        }
                        catch (DbUpdateConcurrencyException)
                        {
                            if (!StudentExists(Vmodel.ID))
                            {
                                return NotFound();
                            }
                            else
                            {
                                throw;
                            }
                        }
                        return RedirectToAction(nameof(Index));
                    }
                    return View(Vmodel);
                }*/


        // POST: Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Username,gender,Embg,FirstName,LastName,Address,City,EnrollmentDate,ExerciseLevel,PhoneNumber,Mail")] User user)
        {
            if (ModelState.IsValid)
            {
                _context.Add(user);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }
        /*
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(UserVM Vmodel)
        {
            if (ModelState.IsValid)
            {
                string uniqueFileName = UploadedFile(Vmodel);

                User user = new User
                {
                    ProfilePicture = uniqueFileName,
                    Username = Vmodel.Username,
                    Embg = Vmodel.Embg,
                    gender = Vmodel.gender,
                    FirstName = Vmodel.FirstName,
                    LastName = Vmodel.LastName,
                    Address = Vmodel.Address,
                    City = Vmodel.City,
                    EnrollmentDate = Vmodel.EnrollmentDate,
                    ExerciseLevel = Vmodel.ExerciseLevel,
                    PhoneNumber = Vmodel.PhoneNumber,
                    Mail = Vmodel.Mail,
                    Enrollments = Vmodel.Enrollments
                };

                _context.Add(user);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        
        private string UploadedFile(UserVM model)
        {
            string uniqueFileName = null;

            if (model.ProfilePicture != null)
            {
                string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "usersi");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(model.ProfilePicture.FileName);
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.ProfilePicture.CopyTo(fileStream);
                }
            }
            return uniqueFileName;
        }*/

        // GET: Users/Edit/5
        /*public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.User.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            UserVM Vmodel = new UserVM
            {
                ID = user.ID,
                Username = user.Username,
                Embg = user.Embg,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Address = user.Address,
                gender = user.gender,
                City = user.City,
                EnrollmentDate = user.EnrollmentDate,
                ExerciseLevel = user.ExerciseLevel,
                PhoneNumber = user.PhoneNumber,
                Mail = user.Mail,
                Enrollments = user.Enrollments,
            };

            return View(Vmodel);
        }*/

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.User.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Username,gender,Embg,FirstName,LastName,Address,City,EnrollmentDate,ExerciseLevel,PhoneNumber,Mail,TrainingID")] User user)
        {
            if (id != user.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(user);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(user.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        /*[HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, UserVM Vmodel)
        {
            if (id != Vmodel.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    string uniqueFileName = UploadedFile(Vmodel);

                    User user = new User
                    {
                        ID = Vmodel.ID,
                        ProfilePicture = uniqueFileName,
                        Username = Vmodel.Username,
                        Embg = Vmodel.Embg,
                        FirstName = Vmodel.FirstName,
                        LastName = Vmodel.LastName,
                        Address = Vmodel.Address,
                        gender = Vmodel.gender,
                        City = Vmodel.City,
                        EnrollmentDate = Vmodel.EnrollmentDate,
                        ExerciseLevel = Vmodel.ExerciseLevel,
                        PhoneNumber = Vmodel.PhoneNumber,
                        Mail = Vmodel.Mail,
                        Enrollments = Vmodel.Enrollments,
                    };
                    _context.Update(user);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(Vmodel.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(Vmodel);
        }*/

        // GET: Users/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.User
                .FirstOrDefaultAsync(m => m.ID == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var user = await _context.User.FindAsync(id);
            _context.User.Remove(user);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserExists(int id)
        {
            return _context.User.Any(e => e.ID == id);
        }
    }
}
