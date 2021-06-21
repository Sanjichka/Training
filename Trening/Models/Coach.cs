using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Trening.Models
{
    public class Coach
    {
        [Required]
        public int ID { get; set; }

        [Required]
        [StringLength(20)]
        public string Username { get; set; }

        [Display(Name = "Birth_Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime BirthDate { get; set; }


        [StringLength(20)]
        [Display(Name ="Exercise Rank")]
        public string ExerciseRank { get; set; }

        [StringLength(50)]
        public string Awards { get; set; }

        [StringLength(100)]
        public string Certificates { get; set; }

        [StringLength(20)]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

        [Required]
        [StringLength(30)]
        public string Mail { get; set; }

        public ICollection<Training> Training { get; set; }

    }
}
