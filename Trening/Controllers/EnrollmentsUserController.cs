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
    public class EnrollmentsUserController : Controller
    {
        private readonly TreningContext _context;

        public EnrollmentsUserController(TreningContext context)
        {
            _context = context;
        }

        // GET: EnrollmentsUser
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

        // GET: EnrollmentsUser/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var enrollment = await _context.Enrollment
                .Include(e => e.Training)
                .Include(e => e.User)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (enrollment == null)
            {
                return NotFound();
            }

            return View(enrollment);
        }

        // GET: EnrollmentsUser/Create
        public IActionResult Create()
        {
            ViewData["TrainingID"] = new SelectList(_context.Training, "ID", "Platform");
            ViewData["UserID"] = new SelectList(_context.User, "ID", "Embg");
            return View();
        }

        // POST: EnrollmentsUser/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,StartDate,FinishDate,Owe,UserID,TrainingID")] Enrollment enrollment)
        {
            if (ModelState.IsValid)
            {
                _context.Add(enrollment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["TrainingID"] = new SelectList(_context.Training, "ID", "Platform", enrollment.TrainingID);
            ViewData["UserID"] = new SelectList(_context.User, "ID", "Embg", enrollment.UserID);
            return View(enrollment);
        }

        // GET: EnrollmentsUser/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var enrollment = await _context.Enrollment.FindAsync(id);
            if (enrollment == null)
            {
                return NotFound();
            }
            ViewData["TrainingID"] = new SelectList(_context.Training, "ID", "Platform", enrollment.TrainingID);
            ViewData["UserID"] = new SelectList(_context.User, "ID", "Embg", enrollment.UserID);
            return View(enrollment);
        }

        // POST: EnrollmentsUser/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,StartDate,FinishDate,Owe,UserID,TrainingID")] Enrollment enrollment)
        {
            if (id != enrollment.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(enrollment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EnrollmentExists(enrollment.ID))
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
            ViewData["TrainingID"] = new SelectList(_context.Training, "ID", "Platform", enrollment.TrainingID);
            ViewData["UserID"] = new SelectList(_context.User, "ID", "Embg", enrollment.UserID);
            return View(enrollment);
        }

        // GET: EnrollmentsUser/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var enrollment = await _context.Enrollment
                .Include(e => e.Training)
                .Include(e => e.User)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (enrollment == null)
            {
                return NotFound();
            }

            return View(enrollment);
        }

        // POST: EnrollmentsUser/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var enrollment = await _context.Enrollment.FindAsync(id);
            _context.Enrollment.Remove(enrollment);
            await _context.SaveChangesAsync();
            return RedirectToAction(
            nameof(Index), // action name
            nameof(HomeController).Replace("Controller", "") // controller name
        );
        }

        private bool EnrollmentExists(int id)
        {
            return _context.Enrollment.Any(e => e.ID == id);
        }
    }
}
