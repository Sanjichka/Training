using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Trening.Data;
using Trening.Models;
using Trening.ViewModels;

namespace Trening.Controllers
{
    public class UsersTrainingController : Controller
    {
        private readonly TreningContext _context;

        public UsersTrainingController(TreningContext context)
        {
            _context = context;
        }

        // GET: UsersTraining
        public IActionResult Index(long? id)
        {
            IQueryable<Training> trainings = _context.Training.AsQueryable();

            IQueryable<Enrollment> enrollments = _context.Enrollment.AsQueryable();
            enrollments = enrollments.Include(c => c.User).Include(c => c.Training);
            enrollments = enrollments.Where(s => s.UserID == id);

            IEnumerable<int> enrolsouser = enrollments.OrderBy(e => e.TrainingID).Select(e => e.TrainingID).Distinct();

            trainings = trainings.Include(c => c.Enrollments).ThenInclude(c => c.User);

            trainings = trainings.Where(s => enrolsouser.Contains(s.ID));

            var vm = new Filter3
            {
                Enrollments = enrollments,
                Trainings = trainings
            };

            ViewData["User"] = _context.User.Where(s => s.ID == id).Select(s => s.FullName).FirstOrDefault();

            return View(vm);
        }

        // GET: UsersTraining/Details/5
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

        // GET: UsersTraining/Create
        public IActionResult Create()
        {
            ViewData["TrainingID"] = new SelectList(_context.Training, "ID", "Platform");
            return View();
        }

        // POST: UsersTraining/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Username,ProfilePicture,Embg,FirstName,LastName,Address,City,EnrollmentDate,ExerciseLevel,PhoneNumber,Mail,TrainingID")] User user)
        {
            if (ModelState.IsValid)
            {
                _context.Add(user);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }

        // GET: UsersTraining/Edit/5
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

        // POST: UsersTraining/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Username,ProfilePicture,Embg,FirstName,LastName,Address,City,EnrollmentDate,ExerciseLevel,PhoneNumber,Mail,TrainingID")] User user)
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

        // GET: UsersTraining/Delete/5
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

        // POST: UsersTraining/Delete/5
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
