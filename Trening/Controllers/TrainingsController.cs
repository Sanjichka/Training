using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Trening.Data;
using Trening.Models;
using Trening.ViewModels;

namespace Trening.Controllers
{
    public class TrainingsController : Controller
    {
        private readonly TreningContext _context;

        public TrainingsController(TreningContext context)
        {
            _context = context;
        }

        [Authorize(Roles = "Admin")]
        // GET: Trainings
        public IActionResult Index(int searchBrMesec, string searchName, decimal searchPrice)
        {
            IQueryable<Training> trainings = _context.Training.AsQueryable();
            IQueryable<int> query = _context.Training.OrderBy(m => m.NumClMonth).Select(m => m.NumClMonth).Distinct();

            if (!string.IsNullOrEmpty(searchName))
            {
                trainings = trainings.Where(s => s.TrainingName.ToLower().Contains(searchName.ToLower()));
            }
            if (searchBrMesec != 0)
            {
                trainings = trainings.Where(s => s.NumClMonth == searchBrMesec);
            }
            if (searchPrice != 0)
            {
                trainings = trainings.Where(s => s.Price <= searchPrice);
            }

            var VM = new Filter1
            {
                BrMesecList = new SelectList(query.AsEnumerable()),
                Trainings = trainings.AsEnumerable()
            };


            return View(VM);
        }

        [Authorize(Roles = "Userr, Admin, Coach")]
        public IActionResult CoachPoTraining(int? id)
        {
            Coach coach = _context.Training.Where(s => s.ID == id).Select(s => s.Coach).FirstOrDefault();

            ViewData["Coach"] = coach.Username;

            return View(coach);
        }

        [Authorize(Roles = "Userr, Admin, Coach")]
        public IActionResult CoachPoTraining1(int? id)
        {
            Coach coach = _context.Training.Where(s => s.ID == id).Select(s => s.Coach).FirstOrDefault();

            ViewData["Coach"] = coach.Username;

            return View(coach);
        }

        /*public IActionResult Users(int? id)
        {
            IEnumerable<User> users = (IEnumerable<User>)_context.User
                .Where(m => m.TrainingID == id);

            ViewData["Training"] = _context.Training.Where(s => s.ID == id).Select(s => s.TrainingName).FirstOrDefault();

            return View(users);
        }*/

        [Authorize(Roles = "Admin")]
        public IActionResult UsersPoTraining(int? id)
        {
            IQueryable<User> users = _context.User.AsQueryable();

            IQueryable<Enrollment> enrollments = _context.Enrollment.AsQueryable();
            enrollments = enrollments.Include(c => c.User).Include(c => c.Training);
            enrollments = enrollments.Where(s => s.TrainingID == id);

            IEnumerable<int> enrolsouser = enrollments.OrderBy(e => e.UserID).Select(e => e.UserID).Distinct();

            users = users.Include(c => c.Enrollments).ThenInclude(c => c.Training);

            users = users.Where(s => enrolsouser.Contains(s.ID));

            ViewData["Training"] = _context.Training.Where(s => s.ID == id).Select(s => s.TrainingName).FirstOrDefault();

            return View(users);
        }

        [Authorize(Roles = "Userr, Admin, Coach")]
        // GET: Trainings/Details/5
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

            ViewData["T"] = training.TrainingName;

            return View(training);
        }


        [Authorize(Roles = "Userr, Admin, Coach")]
        public async Task<IActionResult> Details1(int? id)
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

            ViewData["T"] = training.TrainingName;

            return View(training);
        }

        // GET: Trainings/Create
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            ViewData["CoachID"] = new SelectList(_context.Coach, "ID", "Username");
            return View();
        }

        // POST: Trainings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([Bind("ID,TrainingName,Platform,LinkPlatform,CompanyCoache,StartDate,Price,Discipline,NumClMonth,CoachID")] Training training)
        {
            if (ModelState.IsValid)
            {
                _context.Add(training);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CoachID"] = new SelectList(_context.Coach, "ID", "Username", training.CoachID);
            return View(training);
        }

        // GET: Trainings/Edit/5
        [Authorize(Roles = "Admin, Coach")]
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
            ViewData["CoachID"] = new SelectList(_context.Coach, "ID", "Username", training.CoachID);
            return View(training);
        }

        // POST: Trainings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin, Coach")]
        public async Task<IActionResult> Edit(int id, [Bind("ID,TrainingName,Platform,LinkPlatform,CompanyCoache,StartDate,Price,Discipline,NumClMonth,CoachID")] Training training)
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
            ViewData["CoachID"] = new SelectList(_context.Coach, "ID", "Username", training.CoachID);
            return View(training);
        }

        // GET: Trainings/Delete/5
        [Authorize(Roles = "Admin")]
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

        // POST: Trainings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var training = await _context.Training.FindAsync(id);
            _context.Training.Remove(training);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "Admin, Coach, Userr")]
        private bool TrainingExists(int id)
        {
            return _context.Training.Any(e => e.ID == id);
        }
    }
}
