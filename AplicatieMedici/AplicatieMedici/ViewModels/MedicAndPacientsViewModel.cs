using AplicatieSalariati.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AplicatieSalariati.ViewModels
{
    public class MedicAndPacientsViewModel
    {
        public Medic Medic { get; set; }

        public IEnumerable<Pacient> Pacienti { get; set; }

        public Pacient Pacient { get; set; }

    }
}
