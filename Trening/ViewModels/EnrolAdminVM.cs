using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Trening.Models;

namespace Trening.ViewModels
{
    public class EnrolAdminVM
    {
        public int ID { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Start Date")]
        public Nullable<DateTime> StartDate { get; set; }


        [DataType(DataType.Date)]
        [Display(Name = "Finish Date")]
        public Nullable<DateTime> FinishDate { get; set; }

        [Required]
        public decimal Owe { get; set; }


        [Display(Name = "Users")]
        public int UserID { get; set; }
        public User User { get; set; }


        [Display(Name = "Trainings")]
        public int TrainingID { get; set; }
        public Training Training { get; set; }

        public IEnumerable<int> SelectedUsers { get; set; }
        public IEnumerable<SelectListItem> UserList { get; set; }
    }
}
