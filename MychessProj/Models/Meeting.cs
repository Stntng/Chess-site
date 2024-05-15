using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MychessProj.Models
{
    public partial class Meeting
    {
        [Display(Name ="ID встречи")]
        public int IdMeet { get; set; }
        [Display(Name ="Результат встречи")]
        public string? Result { get; set; }
        [Display(Name ="Дата встречи")]
        public DateTime? DateTime { get; set; }
        [Display(Name ="ID судьи")]
        public int? JudgeId { get; set; }
        [Display(Name ="ID турнира")]
        public int? IdTourn { get; set; }
        [Display(Name ="ID первого участника")]
        public int User1 { get; set; }
        [Display(Name = "ID второго участника")]
        public int User2 { get; set; }
        [Display(Name ="ID локации")]
        public int? IdLocation { get; set; }

        public virtual Location? IdLocationNavigation { get; set; }
    }
}
