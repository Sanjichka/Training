using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Trening.Models
{
    public class User
    {
        [Required]
        public int ID { get; set; }

        [Required]
        [StringLength(20)]
        public string Username { get; set; }

        [StringLength(1)]
        public string gender { get; set; }

        [Required]
        [StringLength(13)]
        public string Embg { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [StringLength(50)]
        public string Address { get; set; }

        [StringLength(25)]
        public string City { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Enrollment Date")]
        public Nullable<DateTime> EnrollmentDate { get; set; }

        [Display(Name = "Exercise Level")]
        [StringLength(25)]
        public string ExerciseLevel { get; set; }

        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

        [Required]

        [Display(Name = "Mail")]
        public string Mail { get; set; }

        



        [Display(Name = "Full Name")]
        public string FullName
        {
            get { return string.Format("{0} {1}", FirstName, LastName); }
        }

        public ICollection<Enrollment> Enrollments { get; set; }

    }
}
