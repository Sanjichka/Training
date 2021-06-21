using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Trening.Models;

namespace Trening.ViewModels
{
    public class Filter5
    {
        public Training Training { get; set; }
        public IEnumerable<Enrollment> Enrollments { get; set; }
        public IEnumerable<int> SelectedUsers { get; set; }
        public IEnumerable<SelectListItem> UserList { get; set; }
    }
}
