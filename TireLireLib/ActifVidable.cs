namespace TireLireLib
{
    public abstract class ActifVidable : ActifDeBase
    {
        public virtual decimal Vider()
        {
            decimal totalVider = this.MontantTotal;
            this.MontantTotal = 0;
            return totalVider;
        }
    }
}
