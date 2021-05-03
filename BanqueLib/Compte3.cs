using System;
using System.Text.Json.Serialization;

using TireLireLib;

namespace BanqueLib
{
    public class Compte3 : Compte2
    {
        public void Geler()
        {
            if (this.État == ÉtatDuCompte.Gelé)
            {
                throw new InvalidOperationException("Impossible de geler car le compte n'est pas actif");
            }
            this.État = ÉtatDuCompte.Gelé;
            Historique.Suivi().Add($"> Geler ");
        }
        public decimal VerserIntérêts(decimal pourcentage)
        {
            if (pourcentage <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(pourcentage), pourcentage, "trop petit");
            }
            if (this.État == ÉtatDuCompte.Fermé)
            {
                throw new InvalidOperationException("Impossible de verser des intérêts dans un compte fermé");
            }
            decimal intérêt = Math.Round(this.MontantTotal * (pourcentage / 100), 2);
            this.MontantTotal += intérêt;
            Historique.Suivi().Add($"> Verser {intérêt} ");
            return intérêt;
        }
        public Compte3(Compte3 copie)
            : base(copie)
        {
            this.État = copie.État;
        }
        [JsonConstructor]
        public Compte3(int numéro, string titulaire, decimal montantTotal = 0, ÉtatDuCompte état = ÉtatDuCompte.Actif)
            : base(numéro, titulaire, montantTotal, état)
        {
        }
    }
}
