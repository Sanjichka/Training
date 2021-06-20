using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Trening.Models;

namespace Trening.ViewModels
{
    public class Filter4
    {
        public IEnumerable<Training> Trainings { get; set; }

        public SelectList TypeList { get; set; }
        public string searchType { get; set; }
    }
}
