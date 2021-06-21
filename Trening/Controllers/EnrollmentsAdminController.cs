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
    public class EnrollmentsAdminController : Controller
    {
        private readonly TreningContext _context;

        public EnrollmentsAdminController(TreningContext context)
        {
            _context = context;
        }
        
        public IActionResult Administrator_Create()
        {

            var users = _context.User.AsEnumerable();
            users = users.OrderBy(s => s.Username);
            EnrolAdminVM viewmodel = new EnrolAdminVM
            {
                UserList = new MultiSelectList(users, "ID", "Username"),
                SelectedUsers = (IEnumerable<int>)_context.Enrollment.Select(m => m.UserID)
            };
            ViewData["TrainingID"] = new SelectList(_context.Training, "ID", "TrainingName");
            return View(viewmodel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Administrator_Create(EnrolAdminVM viewmodel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    IEnumerable<int> listUsers = viewmodel.SelectedUsers;
                    IQueryable<User> toBeRemoved = _context.User.Where(s => !listUsers.Contains(s.ID) && s.ID == viewmodel.UserID); //?
                    _context.User.RemoveRange(toBeRemoved);
                    IEnumerable<int> existUsers = _context.Enrollment.Where(s => listUsers.Contains(s.UserID) && s.TrainingID == viewmodel.TrainingID).Select(s => s.UserID);
                    IEnumerable<int> newUsers = listUsers.Where(s => !existUsers.Contains(s));
                    /*var enrollment = await _context.Enrollment.FindAsync(id);*/
                    foreach (int usID in newUsers)
                        _context.Enrollment.Add(new Enrollment
                        {
                            UserID = usID,
                            TrainingID = viewmodel.TrainingID,
                            StartDate = null,
                            FinishDate = null,
                            Owe = 0
                            
                        });
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EnrollmentExists(viewmodel.ID)) { return NotFound(); }
                    else { throw; }
                }
                ViewData["TrainingID"] = new SelectList(_context.Training, "ID", "TrainingName", viewmodel.TrainingID);
                return RedirectToAction("Index", "Enrollments");
            }
            return View(viewmodel);
        }

        
        private bool EnrollmentExists(int id)
        {
            return _context.Enrollment.Any(e => e.ID == id);
        }
    }
}
