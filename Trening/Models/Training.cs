using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Trening.Models
{
    public class Training
    {
        [Required]
        public int ID { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name ="Training Name")]
        public string TrainingName { get; set; }

        [Required]
        [StringLength(30)]
        public string Platform { get; set; }

        [StringLength(100)]
        public string LinkPlatform { get; set; }

        [StringLength(30)]
        [Display(Name ="Company Coach")]
        public string CompanyCoache { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Start Date")]
        public Nullable<DateTime> StartDate { get; set; }

        [Required]
        [Range(1,100)]
        [DataType(DataType.Currency)]
        public decimal? Price { get; set; }

        [Required]
        [Display(Name ="Times Per Month")]
        public int NumClMonth { get; set; }

        [Required]
        [StringLength(28)]
        public string Discipline { get; set; }

        [Display(Name = "Coach")]
        public int? CoachID { get; set; }
        public Coach Coach { get; set; }

        public ICollection<Enrollment> Enrollments { get; set; }

    }
}
