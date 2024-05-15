using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MychessProj.Models
{
    public partial class Registration
    {
        [Display(Name ="ID пользователя")]
        public int IdUser { get; set; }
        [Display(Name ="ID турнира")]
        public int IdTourn { get; set; }
        [Display(Name ="Дата регистрации")]
        public DateTime Time { get; set; }

        public virtual Tournament IdTournNavigation { get; set; } = null!;
        public virtual User IdUserNavigation { get; set; } = null!;
    }
}
