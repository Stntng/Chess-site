using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace MychessProj.Models
{
    public partial class User
    {
        public User()
        {
            Registrations = new HashSet<Registration>();
        }

		public int IdUser { get; set; }
		[Required(ErrorMessage ="Необходимо ввести имя")]
		[StringLength(20, ErrorMessage = "Имя не может иметь длину более 20-ти букв.")]
        [RegularExpression("^[а-яА-я]{2,}$", ErrorMessage = "Неккоректное имя")]
        [Display(Name = "Имя")]
		public string FirstName { get; set; } = null!;
		[Required(ErrorMessage ="Необходимо ввести фамилию")]
		[StringLength(20,ErrorMessage ="Фамилия не может иметь длину более 20-ти букв.")]
        [RegularExpression("^[а-яА-я]{2,}$", ErrorMessage = "Неккоректная фамилия")]
        [Display(Name = "Фамилия")]
		public string SecondName { get; set; } = null!;
		[Required(ErrorMessage ="Необходимо ввести пароль")]
		[DataType(DataType.Password)]
		[RegularExpression("^[a-zA-Z\\d]{8,}$", ErrorMessage ="Пароль должен иметь длинну минимум 8 символов.И содержать только латинские буквы.")]
        [StringLength(100, ErrorMessage = "Поле {0} должно иметь минимум {2} и максимум {1} символов.", MinimumLength = 8)]
        [Display(Name = "Пароль")]
		public string Password { get; set; } = null!;
		[StringLength(20,ErrorMessage ="Отчество не может иметь длину более 20-ти букв.")]
        [RegularExpression("^[а-яА-я]{2,}$", ErrorMessage = "Неккоректное отчество")]
        [Display(Name = "Отчество")]
		public string? LastName { get; set; }
		[Required(ErrorMessage ="Необходимо ввести дату рождения.")]
		[Display(Name = "Дата Рождения")]
		[DataType(DataType.Date)]
		public DateTime Age { get; set; }
		[Display(Name = "Код города")]
		public int? CityCode { get; set; }
		[Display(Name = "Ранг")]
		public rank current_rank { get; set; } = rank.no_cat;
		public enum rank
		{
			[Display(Name ="Нет разряда")]
			no_cat,
			[Display(Name ="Третий юношеский разряд")]
			third_jun_cat,
            [Display(Name = "Второй юношеский разряд")]
            second_jun_cat,
            [Display(Name = "Первый юношеский разряд")]
            first_jun_cat,
            [Display(Name = "Третий разряд")]
            third_cat,
            [Display(Name = "Второй разряд")]
            second_cat,
            [Display(Name = "Первый разряд")]
            first_cat,
            [Display(Name = "Кандидат в мастера спорта")]
            k_m_s
		}
		public roles role { get; set; } = roles.player;
		public enum roles
		{
			[Display(Name ="Судья")]
			judge,
            [Display(Name = "Игрок")]
            player,
            [Display(Name = "Модератор")]
            mod
		}
		[Display(Name ="Код города")]
		public virtual City? CityCodeNavigation { get; set; }
        public virtual ICollection<Registration> Registrations { get; set; }
    }
}
