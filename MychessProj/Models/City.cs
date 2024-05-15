using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace MychessProj.Models
{
    public partial class City
    {
        public City()
        {
            Locations = new HashSet<Location>();
            Users = new HashSet<User>();
        }
        [Display(Name = "Код города")]
        public int CityCode { get; set; }
        [Display(Name = "Название города")]
        public string NameCity { get; set; } = null!;

        public virtual ICollection<Location> Locations { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}
