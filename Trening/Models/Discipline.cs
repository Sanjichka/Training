using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Trening.Models
{
    public class Discipline
    {
        [Required]
        public int ID { get; set; }

        //fitness, dancing, chess, football tricks, yoga, pilates, magic tricks...
        [Required]
        [StringLength(20)]
        [Display(Name ="Discipline Name")]
        public string DisciplineName { get; set; }

        [Required]
        //individual/partner/team
        [StringLength(10)]
        public string Type { get; set; }

        [Required]
        [StringLength(100)]
        public string Equipment { get; set; }

        //tip teren za trening
        [Required]
        [StringLength(28)]
        public string Ground { get; set; }

    }
}
