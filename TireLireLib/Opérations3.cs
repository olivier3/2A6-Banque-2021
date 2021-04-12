namespace TireLireLib
{
    public static class Opérations3
    {
        public static bool Déposer(Tirelire3 tirelire, decimal montant)
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
        public static bool Retirer(Tirelire3 tirelire, decimal montant)
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
        public static decimal Vider(Tirelire3 tirelire)
        {
            decimal totalVider = tirelire.MontantTotal;
            tirelire.MontantTotal = 0;
            return totalVider;
        }
    }
}
