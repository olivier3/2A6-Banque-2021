using System.Text.Json.Serialization;

namespace TireLireLib
{
    public class Tirelire6 : ActifVidable
    {
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
    }
}
