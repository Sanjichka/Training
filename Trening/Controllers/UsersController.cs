using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
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
        [Authorize(Roles = "Admin")]
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

        [Authorize(Roles = "Userr")]
        public async Task<IActionResult> Profil_User(int? id)
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

            ViewData["Name"] = user.FullName;

            return View(user);
        }

        [Authorize(Roles = "Admin, Coach, Userr")]
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

        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }
        

        // POST: Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
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

        [Authorize(Roles = "Admin, Userr")]
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
        [Authorize(Roles = "Admin, Userr")]
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

        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var user = await _context.User.FindAsync(id);
            _context.User.Remove(user);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "Admin, Userr, Coach")]
        private bool UserExists(int id)
        {
            return _context.User.Any(e => e.ID == id);
        }
    }
}
