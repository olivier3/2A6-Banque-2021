using System.Collections.Generic;

namespace TireLireLib
{
    public static class Opérations1
    {
        public static bool Déposer(decimal montant)
        {
            if (montant > 0)
            {
                Tirelire1.MontantTotal += montant;
                return true;
            }
            else
            {
                return false;
            }
        }
        public static bool Retirer(decimal montant)
        {
            if (montant < Tirelire1.MontantTotal && montant > 0)
            {
                Tirelire1.MontantTotal -= montant;
                return true;
            }
            else
            {
                return false;
            }
        }
        public static decimal Vider()
        {
            decimal totalVider = Tirelire1.MontantTotal;
            Tirelire1.MontantTotal = 0;
            return totalVider;
        }
    }
}
