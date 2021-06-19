using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Trening.Models;

namespace Trening.ViewModels
{
    public class Filter1
    {
        public IEnumerable<Training> Trainings { get; set; }

        public string searchName { get; set; }
        public SelectList BrMesecList { get; set; }
        public int searchBrMesec { get; set; }
        public decimal searchPrice { get; set; }
    }
}
