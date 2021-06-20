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
    public class TrainingsUserController : Controller
    {
        private readonly TreningContext _context;

        public TrainingsUserController(TreningContext context)
        {
            _context = context;
        }

        // GET: TrainingsUser
        public IActionResult Index(int id, string searchType)
        {
            /*var treningContext = _context.Training.Include(t => t.Coach);
            return View(await treningContext.ToListAsync());*/


            IQueryable<Training> trainings = _context.Training.AsQueryable();

            IQueryable<Enrollment> enrollments = _context.Enrollment.AsQueryable();
            enrollments = enrollments.Include(c => c.User).Include(c => c.Training);
            enrollments = enrollments.Where(s => s.UserID == id);

            IEnumerable<int> enrolsotraining = enrollments.OrderBy(e => e.TrainingID).Select(e => e.TrainingID).Distinct();

            trainings = trainings.Include(c => c.Enrollments).ThenInclude(c => c.User);

            trainings = trainings.Where(s => !enrolsotraining.Contains(s.ID));

            IQueryable<string> query = trainings.OrderBy(m => m.Discipline).Select(m => m.Discipline).Distinct();
            if (!string.IsNullOrEmpty(searchType))
            {
                trainings = trainings.Where(s => s.Discipline == searchType);
            }

            var VM = new Filter4
            {
                TypeList = new SelectList(query.AsEnumerable()),
                Trainings = trainings.AsEnumerable()
            };

            return View(VM);

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
