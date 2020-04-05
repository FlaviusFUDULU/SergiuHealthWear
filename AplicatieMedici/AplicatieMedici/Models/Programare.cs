using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AplicatieSalariati.Models
{
    public class Programare
    {
        [Key]
        public int Id { get; set; }

        public string MedicCNP { get; set; }

        [Display(Name ="Data/Ora de star")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy HH:mm}")]
        public DateTime StartDateTime { get; set; }

        [Display(Name = "Data/Ora de final")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy HH:mm}")]
        public DateTime EndDateTime { get; set; }

        public bool Confirmed { get; set; }

        public string PacientCNP { get; set; }

        public Pacient Pacient { get; set; }

        public Medic Medic { get; set; }
    }
}
