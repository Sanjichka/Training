using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Trening.Models;

namespace Trening.ViewModels
{
    public class Filter
    {
        public IEnumerable<User> Users { get; set; }

        public string searchUsername { get; set; }
        public SelectList CityList { get; set; }
        public string searchCity { get; set; }
        public string searchMail { get; set; }
    }
}
