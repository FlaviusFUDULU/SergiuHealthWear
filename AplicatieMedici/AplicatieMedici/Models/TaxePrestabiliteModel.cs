using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AplicatieSalariati.Models
{
    public class TaxePrestabiliteModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [DefaultValue(0.16)]
        public double Impozit { get; set; }

        [Required]
        [DefaultValue(0.105)]
        public double CAS { get; set; }

        [Required]
        [DefaultValue(0.005)]
        public double Somaj { get; set; }

        [Required]
        [DefaultValue(0.055)]
        public double Sanatate { get; set; }
    }
}
