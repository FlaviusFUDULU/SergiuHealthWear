using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AplicatieSalariati.Models
{
    public class SalariatModel: IValidatableObject
    {
        [Key]
        [Display(Name = "Nr Crt")]
        public int Nr_Crt { get; set; }

        [Required]
        public string Nume { get; set; }

        [Required]
        public string Prenume { get; set; }

        [Required]
        public string Functie { get; set; }

        [Display(Name="Salar Brut")]
        [Required]
        public double Salar_Brut { get; set; }

        [Display(Name = "Salar Realizat")]
        [Required]
        public double Salar_Realizat { get; set; }

        [Required]
        public double Vechime { get; set; }

        [Required]
        public double Spor { get; set; }

        [Required]
        [Display(Name = "Premii Brute")]
        public int Premii_Brute { get; set; }

        [Required]
        public double Compensatie { get; set; }

        [Required]
        [Display(Name = "Total Brut")]
        public double Total_Brut { get; set; }

        [Required]
        [Display(Name = "Brut Impozabil")]
        public double Brut_Impozabil { get; set; }

        [Required]
        public double Impozit { get; set; }

        [Required]
        public double CAS { get; set; }

        [Required]
        public double Somaj { get; set; }

        [Required]
        public double Sanatate { get; set; }

        [Required]
        public double Avans { get; set; }

        [Required]
        public double Retineri { get; set; }

        [Required]
        public double RestPlata { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Salar_Realizat < 0 || Salar_Realizat > 100)
                yield return new ValidationResult("Valoarea trebuie să fie între 0 și 100", new[] { nameof(Salar_Realizat) });

            if (Vechime < 0 || Vechime > 100)
                yield return new ValidationResult("Valoarea trebuie să fie între 0 și 100", new[] { nameof(Vechime) });

            if (Spor < 0 || Spor > 100)
                yield return new ValidationResult("Valoarea trebuie să fie între 0 și 100", new[] { nameof(Spor) });

        }
    }
}
