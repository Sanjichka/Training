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
    public class EnrollmentsUserController : Controller
    {
        private readonly TreningContext _context;

        public EnrollmentsUserController(TreningContext context)
        {
            _context = context;
        }

        [Authorize(Roles = "Userr, Admin")]
        // GET: EnrollmentsUser
        public IActionResult Index(int? id)
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

        [Authorize(Roles = "Userr, Admin")]
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

        [Authorize(Roles = "Userr")]
        private bool EnrollmentExists(int id)
        {
            return _context.Enrollment.Any(e => e.ID == id);
        }
    }
}
/*return RedirectToAction(
            nameof(Index), // action name
            nameof(HomeController).Replace("Controller", "") // controller name
        );*/