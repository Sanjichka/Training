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

namespace Trening.Controllers
{
    public class EnrollmentsController : Controller
    {
        private readonly TreningContext _context;

        public EnrollmentsController(TreningContext context)
        {
            _context = context;
        }

        [Authorize(Roles = "Admin")]
        // GET: Enrollments
        public async Task<IActionResult> Index()
        {
            var treningContext = _context.Enrollment.Include(e => e.Training).Include(e => e.User);
            return View(await treningContext.ToListAsync());
        }

        [Authorize(Roles = "Admin, Coach, Userr")]
        // GET: Enrollments/Details/5
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

            ViewData["E"] = enrollment.ID;

            return View(enrollment);
        }

        [Authorize(Roles = "Admin")]
        // GET: Enrollments/Create
        public IActionResult Create()
        {
            ViewData["TrainingID"] = new SelectList(_context.Training, "ID", "TrainingName");
            ViewData["UserID"] = new SelectList(_context.User, "ID", "FullName");
            return View();
        }

        // POST: Enrollments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([Bind("ID,StartDate,FinishDate,Owe,UserID,TrainingID")] Enrollment enrollment)
        {
            if (ModelState.IsValid)
            {
                _context.Add(enrollment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["TrainingID"] = new SelectList(_context.Training, "ID", "TrainingName", enrollment.TrainingID);
            ViewData["UserID"] = new SelectList(_context.User, "ID", "FullName", enrollment.UserID);
            return View(enrollment);
        }

        [Authorize(Roles = "Admin, Coach")]
        // GET: Enrollments/Edit/5
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
            ViewData["TrainingID"] = new SelectList(_context.Training, "ID", "TrainingName", enrollment.TrainingID);
            ViewData["UserID"] = new SelectList(_context.User, "ID", "FullName", enrollment.UserID);
            return View(enrollment);
        }

        // POST: Enrollments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin, Coach")]
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
            ViewData["TrainingID"] = new SelectList(_context.Training, "ID", "TrainingName", enrollment.TrainingID);
            ViewData["UserID"] = new SelectList(_context.User, "ID", "FullName", enrollment.UserID);
            return View(enrollment);
        }

        // GET: Enrollments/Delete/5
        [Authorize(Roles = "Admin")]
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

        // POST: Enrollments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var enrollment = await _context.Enrollment.FindAsync(id);
            _context.Enrollment.Remove(enrollment);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "Admin, Coach, Userr")]
        private bool EnrollmentExists(int id)
        {
            return _context.Enrollment.Any(e => e.ID == id);
        }
    }
}
