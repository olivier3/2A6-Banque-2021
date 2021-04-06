namespace TireLireLib
{
    public static class Tirelire2
    {
        public static decimal MontantTotal { get; private set; }

        public static bool Déposer(decimal montant)
        {
            if (montant > 0)
            {
                MontantTotal += montant;
                return true;
            }
            else
            {
                return false;
            }
        }
        public static bool Retirer(decimal montant)
        {
            if (montant < MontantTotal && montant > 0)
            {
                MontantTotal -= montant;
                return true;
            }
            else
            {
                return false;
            }
        }
        public static decimal Vider()
        {
            decimal totalVider = MontantTotal;
            MontantTotal = 0;
            return totalVider;
        }
    }
}
