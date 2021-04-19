using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TireLireLib
{
    public class Tirelire9
    {
        public decimal MontantTotal { get; protected set; }
        public void Déposer(decimal montant)
        {
            if (montant <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(montant), montant, "trop petit");
            }
            this.MontantTotal += montant;
        }
        public void Retirer(decimal montant)
        {
            if (montant > this.MontantTotal)
            {
                throw new ArgumentOutOfRangeException(nameof(montant), montant, "trop grand");
            }

            if (montant <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(montant), montant, "trop petit");
            }
            this.MontantTotal -= montant;
        }
        public decimal Vider()
        {
            decimal totalVider = this.MontantTotal;
            this.MontantTotal = 0;
            return totalVider;
        }
    }
}
