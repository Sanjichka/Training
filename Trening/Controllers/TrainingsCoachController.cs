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
    public class TrainingsCoachController : Controller
    {
        private readonly TreningContext _context;

        public TrainingsCoachController(TreningContext context)
        {
            _context = context;
        }

        // GET: TrainingsCoach
        public async Task<IActionResult> Index(int? id)
        {
            var treningContext = _context.Training.Include(t => t.Coach).Where(t => t.CoachID == id);
            ViewData["Coach"] = _context.Coach.Where(s => s.ID == id).Select(s => s.Username).FirstOrDefault();
            return View(await treningContext.ToListAsync());
        }

        public IActionResult UserPoTraining(int? id)
        {
            IQueryable<User> users = _context.User.AsQueryable();

            IQueryable<Enrollment> enrollments = _context.Enrollment.AsQueryable();
            enrollments = enrollments.Include(c => c.User).Include(c => c.Training);
            enrollments = enrollments.Where(s => s.TrainingID == id);
            enrollments = enrollments.OrderBy(x => x.UserID);

            IEnumerable<int> enrolsouser = enrollments.Select(e => e.UserID).Distinct();

            users = users.Include(c => c.Enrollments).ThenInclude(c => c.Training);

            users = users.Where(s => enrolsouser.Contains(s.ID));

            var vm = new Filter6
            {
                Enrollments = enrollments,
                Users = users
            };

            ViewData["Training"] = _context.Training.Where(s => s.ID == id).Select(s => s.TrainingName).FirstOrDefault();

            return View(vm);
        }

        public IActionResult UserPoTraining1(int? id)
        {
            IQueryable<User> users = _context.User.AsQueryable();

            IQueryable<Enrollment> enrollments = _context.Enrollment.AsQueryable();
            enrollments = enrollments.Include(c => c.User).Include(c => c.Training);
            enrollments = enrollments.Where(s => s.TrainingID == id);
            enrollments = enrollments.OrderBy(x => x.UserID);

            IEnumerable<int> enrolsouser = enrollments.Select(e => e.UserID).Distinct();

            users = users.Include(c => c.Enrollments).ThenInclude(c => c.Training);

            users = users.Where(s => enrolsouser.Contains(s.ID));

            var vm = new Filter6
            {
                Enrollments = enrollments,
                Users = users
            };

            ViewData["Training"] = _context.Training.Where(s => s.ID == id).Select(s => s.TrainingName).FirstOrDefault();

            return View(vm);
        }

        public IActionResult Statistika(int? id)
        {
            IQueryable<User> users = _context.User.AsQueryable();

            IQueryable<Enrollment> enrollments = _context.Enrollment.AsQueryable();
            enrollments = enrollments.Include(c => c.User).Include(c => c.Training);
            enrollments = enrollments.Where(s => s.TrainingID == id);
            enrollments = enrollments.OrderBy(x => x.UserID);

            IEnumerable<int> enrolsouser = enrollments.Select(e => e.UserID).Distinct();

            users = users.Include(c => c.Enrollments).ThenInclude(c => c.Training);

            users = users.Where(s => enrolsouser.Contains(s.ID));

            var vm = new Filter6
            {
                Enrollments = enrollments,
                Users = users
            };

            ViewData["Training"] = _context.Training.Where(s => s.ID == id).Select(s => s.TrainingName).FirstOrDefault();

            return View(vm);
        }


        // GET: TrainingsCoach/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var training = await _context.Training
                .Include(t => t.Coach)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (training == null)
            {
                return NotFound();
            }

            return View(training);
        }

        // GET: TrainingsCoach/Create
        public IActionResult Create()
        {
            ViewData["CoachID"] = new SelectList(_context.Coach, "ID", "Mail");
            return View();
        }

        // POST: TrainingsCoach/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,TrainingName,Platform,LinkPlatform,CompanyCoache,StartDate,Price,NumClMonth,Discipline,CoachID")] Training training)
        {
            if (ModelState.IsValid)
            {
                _context.Add(training);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CoachID"] = new SelectList(_context.Coach, "ID", "Mail", training.CoachID);
            return View(training);
        }

        // GET: TrainingsCoach/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var training = await _context.Training.FindAsync(id);
            if (training == null)
            {
                return NotFound();
            }
            ViewData["CoachID"] = new SelectList(_context.Coach, "ID", "Mail", training.CoachID);
            return View(training);
        }

        // POST: TrainingsCoach/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,TrainingName,Platform,LinkPlatform,CompanyCoache,StartDate,Price,NumClMonth,Discipline,CoachID")] Training training)
        {
            if (id != training.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(training);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TrainingExists(training.ID))
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
            ViewData["CoachID"] = new SelectList(_context.Coach, "ID", "Mail", training.CoachID);
            return View(training);
        }

        // GET: TrainingsCoach/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var training = await _context.Training
                .Include(t => t.Coach)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (training == null)
            {
                return NotFound();
            }

            return View(training);
        }

        // POST: TrainingsCoach/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var training = await _context.Training.FindAsync(id);
            _context.Training.Remove(training);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TrainingExists(int id)
        {
            return _context.Training.Any(e => e.ID == id);
        }
    }
}
