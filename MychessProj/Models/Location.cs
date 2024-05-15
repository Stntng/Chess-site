using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace MychessProj.Models
{
    public partial class Location
    {
        public Location()
        {
            Meetings = new HashSet<Meeting>();
            Tournaments = new HashSet<Tournament>();
        }
        [Display(Name = "ID локации")]
        public int IdLocation { get; set; }
        [Display(Name = "Код города")]
        public int CityCode { get; set; }
        [Display(Name = "Количество столов")]
        public int? NumberOfTables { get; set; }
        [Display(Name = "Адрес локации")]
        public string Addres { get; set; } = null!;

        public virtual City CityCodeNavigation { get; set; } = null!;
        public virtual ICollection<Meeting> Meetings { get; set; }
        public virtual ICollection<Tournament> Tournaments { get; set; }
    }
}
