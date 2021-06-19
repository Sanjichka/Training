using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Trening.Models;

namespace Trening.ViewModels
{
    public class Filter2
    {
        public IEnumerable<Coach> Coaches { get; set; }

        public string searchUsername { get; set; }
        public SelectList RankList { get; set; }
        public string searchRank { get; set; }
    }
}
