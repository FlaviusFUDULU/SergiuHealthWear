using AplicatieSalariati.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AplicatieSalariati.ViewModels
{
    public class StatPlataViewModel
    {
        public SalariatModel Salariat { get; set; }

        public List<SalariatModel> Salariati { get; set; }

        public double TotalSalarNeg {
            get {
                double total = 0;
                foreach (var salariat in Salariati) {
                    total += salariat.Salar_Brut;
                }
                return total;
            }
        }

        public double TotalRestPlata {
            get
            {
                double total = 0;
                foreach (var salariat in Salariati)
                {
                    total += salariat.RestPlata;
                }
                return total;
            }
        }
    }
}
