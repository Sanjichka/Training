using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Trening.Models;

namespace Trening.ViewModels
{
    public class Filter6
    {
        public IEnumerable<Enrollment> Enrollments { get; set; }
        public IEnumerable<User> Users { get; set; }
    }
}
