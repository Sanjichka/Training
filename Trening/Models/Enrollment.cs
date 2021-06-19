using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Trening.Models
{
    public class Enrollment
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


        [Display(Name="Users")]
        public int UserID { get; set; }
        public User User { get; set; }


        [Display(Name = "Trainings")]
        public int TrainingID { get; set; }
        public Training Training { get; set; }
    }
}
