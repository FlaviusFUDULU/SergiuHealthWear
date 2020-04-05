using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AplicatieSalariati.Models
{
    public class DateAdministratorModel
    {
        [Key]
        [Display(Name = "CNP")]
        public string CNP { get; set; }

        [Required]
        public string Nume { get; set; }

        [Required]
        public string Prenume { get; set; }

        public string Email { get; set; }

        [Required]
        public string Functie { get; set; }

        [Required]
        public string Adresa { get; set; }

        public string TelefonPersonal { get; set; }

        public string TelefonServici { get; set; }
    }
}
