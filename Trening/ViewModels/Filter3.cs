using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Trening.Models;

namespace Trening.ViewModels
{
    public class Filter3
    {
        public IEnumerable<Enrollment> Enrollments { get; set; }
        public IEnumerable<Training> Trainings { get; set; }
    }
}
