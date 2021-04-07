using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TireLireLib
{
    public static class Opérations3
    {
        public static bool Déposer(Tirelire3 tirelire3, decimal montant)
        {
            if (montant > 0)
            {
                tirelire3.MontantTotal += montant;
                return true;
            }
            else
            {
                return false;
            }
        }
        public static bool Retirer(Tirelire3 tirelire3, decimal montant)
        {
            if (montant < tirelire3.MontantTotal && montant > 0)
            {
                tirelire3.MontantTotal -= montant;
                return true;
            }
            else
            {
                return false;
            }
        }
        public static decimal Vider(Tirelire3 tirelire3)
        {
            decimal totalVider = tirelire3.MontantTotal;
            tirelire3.MontantTotal = 0;
            return totalVider;
        }
    }
}
