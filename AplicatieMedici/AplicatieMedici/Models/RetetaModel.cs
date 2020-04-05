using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AplicatieSalariati.Models
{
    public class RetetaModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public int IstoricId { get; set; }

        public string Medicament1 { get; set; }

        public string Administrare1 { get; set; }

        public bool MedicamentRetras1 { get; set; }

        public string Medicament2 { get; set; }

        public string Administrare2 { get; set; }

        public bool MedicamentRetras2 { get; set; }

        public string Medicament3 { get; set; }

        public string Administrare3 { get; set; }

        public bool MedicamentRetras3 { get; set; }

        public string Medicament4 { get; set; }

        public string Administrare4 { get; set; }

        public bool MedicamentRetras4 { get; set; }

        public string Medicament5 { get; set; }

        public string Administrare5 { get; set; }

        public bool MedicamentRetras5 { get; set; }

        public DateTime DataEmitere { get; set; }

        public DateTime DataRetragere { get; set; }

        public bool Retras { get; set; }
    }
}