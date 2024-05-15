using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MychessProj.Models
{
    public partial class Tournament
    {
        public Tournament()
        {
            Registrations = new HashSet<Registration>();
        }

        public int IdTourn { get; set; }
        [Display(Name = "Название турнира")]
        public string NameTourn { get; set; } = null!;
        [DataType(DataType.Date)]
        [Display(Name ="Дата турнира")]
        public DateTime DateTourn { get; set; }

        public int JudgeId { get; set; }
        [Display(Name = "ID локации турнира")]
        public int? IdLocation { get; set; }

        [Display(Name ="ID локации турнира")]
        public virtual Location? IdLocationNavigation { get; set; }
        public virtual ICollection<Registration> Registrations { get; set; }
    }
}
