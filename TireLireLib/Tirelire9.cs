using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TireLireLib
{
    public class Tirelire9 : ActifVidable
    {
        public void Déposer(decimal montant)
        {
            if (montant <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(montant), montant, "trop petit");
            }
            if (Math.Round(montant, 2) != montant)
            {
                throw new ArgumentException("trop précis", "montant");
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
            if (Math.Round(montant, 2) != montant)
            {
                throw new ArgumentException("trop précis", "montant");
            }
            this.MontantTotal -= montant;
        }
    }
}
