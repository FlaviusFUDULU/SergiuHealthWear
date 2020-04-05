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
    public class Pacient
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
        public string Adresa { get; set; }

        [Display(Name = "Numar de telefon")]
        public string Numartelefon { get; set; }

        [Display(Name = "Data nasterii")]
        public DateTime DataNasterii{get; set;}

        [Display(Name = "Sex")]
        public string Sex { get; set; }

        [Display(Name = "Act de identitate")]
        public string ActIdentitate { get; set; }


        [Display(Name = "Alergii/intolerante")]
        public string AlergiiIntolerante { get; set; }

        [Display(Name = "Boli Cronice")]
        public string BoliCronice { get; set; }

        [Display(Name = "Grupa de sange")]
        public string GrupaSange { get; set; }

        [ForeignKey("PacientCNP")]
        public ICollection<Istoric> Istorics { get; set; }

        [ForeignKey("PacientCNP")]
        public ICollection<Programare> Programari { get; set; }

        public string MedicCNP { get; set; }

        public Guid AccountGuid { get; set; }
    }
}
