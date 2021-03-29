namespace TireLireLib
{
    internal class Opérations1
    {
        public static bool Déposer(decimal montant)
        {
            if (montant > 0)
            {
                TireLire1.MontantTotal += montant;
                return true;
            }
            else
            {
                return false;
            }
        }
        public static bool Retirer(decimal montant)
        {
            if (montant < TireLire1.MontantTotal)
            {
                TireLire1.MontantTotal -= montant;
                return true;
            }
            else
            {
                return false;
            }
        }
        public static decimal Vider()
        {
            decimal totalVider = TireLire1.MontantTotal;
            TireLire1.MontantTotal = 0;
            return totalVider;
        }
    }
}
