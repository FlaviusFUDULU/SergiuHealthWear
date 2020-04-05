using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace AplicatieSalariati.Models
{
    public class DateEchipaModel
    {
        [Key]
        [Display(Name = "Nume Echipa")]
        public string NumeEchipa { get; set; }

        [Required]
        public string NumeManager { get; set; }

        [Required]
        public string NrTelManager { get; set; }

        public string EmailEchipa { get; set; }

        [Required]
        public string DomeniulDeFunctionare { get; set; }

        [Required]
        public string Adresa { get; set; }

        [NotMapped]
        public List<SelectListItem> ListNume { get; set; }
    }
}
