using System.Text.Json.Serialization;

namespace TireLireLib
{
    public class Tirelire5 : ActifDeBase
    {
        public static bool Déposer(Tirelire5 tirelire, decimal montant)
        {
            if (montant > 0)
            {
                tirelire.MontantTotal += montant;
                return true;
            }
            else
            {
                return false;
            }
        }
        public static bool Retirer(Tirelire5 tirelire, decimal montant)
        {
            if (montant < tirelire.MontantTotal && montant > 0)
            {
                tirelire.MontantTotal -= montant;
                return true;
            }
            else
            {
                return false;
            }
        }
        public static decimal Vider(Tirelire5 tirelire)
        {
            decimal totalVider = tirelire.MontantTotal;
            tirelire.MontantTotal = 0;
            return totalVider;
        }
    }
}
