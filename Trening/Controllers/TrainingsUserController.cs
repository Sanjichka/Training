using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Trening.Data;
using Trening.Models;

namespace Trening.Controllers
{
    public class TrainingsUserController : Controller
    {
        private readonly TreningContext _context;

        public TrainingsUserController(TreningContext context)
        {
            _context = context;
        }

        // GET: TrainingsUser
        public async Task<IActionResult> Index()
        {
            var treningContext = _context.Training.Include(t => t.Coach);
            return View(await treningContext.ToListAsync());
        }

        // GET: TrainingsUser/Details/5
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
        
       
        private bool TrainingExists(int id)
        {
            return _context.Training.Any(e => e.ID == id);
        }
    }
}
