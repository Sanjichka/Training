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
    public class CoachesController : Controller
    {
        private readonly TreningContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public CoachesController(TreningContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        [Authorize(Roles = "Admin")]
        // GET: Coaches
        public IActionResult Index(string searchUsername, string searchRank)
        {

            IQueryable<Coach> coaches = _context.Coach.AsQueryable();
            IQueryable<string> query = _context.Coach.OrderBy(m => m.ExerciseRank).Select(m => m.ExerciseRank).Distinct();

            if (!string.IsNullOrEmpty(searchUsername))
            {
                coaches = coaches.Where(s => s.Username.ToLower().Contains(searchUsername.ToLower()));
            }
            if (!string.IsNullOrEmpty(searchRank))
            {
                coaches = coaches.Where(s => s.ExerciseRank == searchRank);
            }

            var VM = new Filter2
            {
                RankList = new SelectList(query.AsEnumerable()),
                Coaches = coaches.AsEnumerable()
            };


            return View(VM);
        }

        [Authorize(Roles = "Coach")]
        // GET: Coach
        public async Task<IActionResult> Profil_Coach(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }

            var coach = await _context.Coach
                .FirstOrDefaultAsync(m => m.ID == id);
            if (coach == null)
            {
                return NotFound();
            }

            ViewData["C"] = coach.Username;

            return View(coach);
        }

        [Authorize(Roles = "Userr, Admin, Coach")]
        // GET: Coaches/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var coach = await _context.Coach
                .FirstOrDefaultAsync(m => m.ID == id);
            if (coach == null)
            {
                return NotFound();
            }

            ViewData["Coach"] = coach.Username;

            return View(coach);
        }

        [Authorize(Roles = "Admin")]
        // GET: Coaches/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Coaches/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([Bind("ID,Username,ProfilePicture,BirthDate,ExerciseRank,Awards,Certificates,PhoneNumber,Mail")] Coach coach)
        {
            if (ModelState.IsValid)
            {
                _context.Add(coach);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(coach);
        }

        [Authorize(Roles = "Admin, Coach")]
        // GET: Coaches/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var coach = await _context.Coach.FindAsync(id);
            if (coach == null)
            {
                return NotFound();
            }
            return View(coach);
        }
        
        // POST: Coaches/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin, Coach")]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Username,ProfilePicture,BirthDate,ExerciseRank,Awards,Certificates,PhoneNumber,Mail")] Coach coach)
        {
            if (id != coach.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(coach);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CoachExists(coach.ID))
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
            return View(coach);
        }

        // GET: Coaches/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var coach = await _context.Coach
                .FirstOrDefaultAsync(m => m.ID == id);
            if (coach == null)
            {
                return NotFound();
            }

            return View(coach);
        }

        // POST: Coaches/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var coach = await _context.Coach.FindAsync(id);
            _context.Coach.Remove(coach);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "Admin, Coach, Userr")]
        private bool CoachExists(int id)
        {
            return _context.Coach.Any(e => e.ID == id);
        }
    }
}
