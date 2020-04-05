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
    public class Orar
    {
        //[Key]
        //[ForeignKey("Medic")]
        //public string Id { get; set; }

        [Key]
        [ForeignKey("Medic")]
        public string MedicCNP { get; set; }

        public Medic Medic { get; set; }

        [Display(Name = "Ora de început Luni")]
        public string LuniStart { get; set; }

        [Display(Name = "Ora de închidere Luni")]
        public string LuniEnd { get; set; }

        [Display(Name = "Zi libera?")]
        public bool ZiNelucratoareLuni { get; set; }

        [Display(Name = "Ora de început Marți")]
        public string MartiStart { get; set; }

        [Display(Name = "Ora de închidere Marți")]
        public string MartiEnd { get; set; }

        [Display(Name = "Zi libera?")]
        public bool ZiNelucratoareMarti { get; set; }

        [Display(Name = "Ora de început Miercuri")]
        public string MiercuriStart { get; set; }

        [Display(Name = "Ora de închidere Miercuri")]
        public string MiercuriEnd { get; set; }

        [Display(Name = "Zi libera?")]
        public bool ZiNelucratoareMiercuri { get; set; }

        [Display(Name = "Ora de început Joi")]
        public string JoiStart { get; set; }

        [Display(Name = "Ora de închidere Joi")]
        public string JoiEnd { get; set; }

        [Display(Name = "Zi libera?")]
        public bool ZiNelucratoareJoi { get; set; }

        [Display(Name = "Ora de început Vineri")]
        public string VineriStart { get; set; }

        [Display(Name = "Ora de închidere Vineri")]
        public string VineriEnd { get; set; }

        [Display(Name = "Zi libera?")]
        public bool ZiNelucratoareVineri { get; set; }

        [Display(Name = "Ora de început Sâmbătă")]
        public string SambataStart { get; set; }

        [Display(Name = "Ora de închidere Sâmbătă")]
        public string SmbataEnd { get; set; }

        [Display(Name = "Zi libera?")]
        public bool ZiNelucratoareSambata { get; set; }

        [Display(Name = "Ora de început Duminică")]
        public string DuminicaStart { get; set; }

        [Display(Name = "Ora de închidere Duminică")]
        public string DuminicaEnd { get; set; }

        [Display(Name = "Zi libera?")]
        public bool ZiNelucratoareDuminica { get; set; }

    }
}
