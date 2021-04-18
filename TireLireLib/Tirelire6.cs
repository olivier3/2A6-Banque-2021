namespace TireLireLib
{
    public class Tirelire6
    {
        public decimal MontantTotal { get; protected set; }
        public bool Déposer(decimal montant)
        {
            if (montant > 0)
            {
                this.MontantTotal += montant;
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool Retirer(decimal montant)
        {
            if (montant < this.MontantTotal && montant > 0)
            {
                this.MontantTotal -= montant;
                return true;
            }
            else
            {
                return false;
            }
        }
        public decimal Vider()
        {
            decimal totalVider = this.MontantTotal;
            this.MontantTotal = 0;
            return totalVider;
        }
    }
}
