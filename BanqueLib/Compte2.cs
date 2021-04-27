using System;
using System.Text.Json.Serialization;

using TireLireLib;

namespace BanqueLib
{
    public class Compte2 : Compte1
    {
        public ÉtatDuCompte État { get; set; }

        public Compte2(Compte2 copie)
            : base(copie)
        {
            this.État = copie.État;
        }

        public decimal Fermer()
        {
            if (this.État == ÉtatDuCompte.Fermé)
            {
                throw new InvalidOperationException("Impossible de fermer un compte déjà fermé");
            }
            decimal montant = this.MontantTotal;
            this.MontantTotal = 0;
            Historique.Suivi().Add($"> Fermer {montant} ");
            this.État = ÉtatDuCompte.Fermé;
            return montant;
        }

        public void Réactiver()
        {
            if (this.État == ÉtatDuCompte.Actif)
            {
                throw new InvalidOperationException("Impossible de réactiver un compte déjà actif");
            }
            this.État = ÉtatDuCompte.Actif;
            Historique.Suivi().Add($"> Réactiver ");
        }

        [JsonConstructor]
        public Compte2(int numéro, string titulaire, decimal montantTotal = 0, ÉtatDuCompte état = ÉtatDuCompte.Actif)
            : base(numéro, titulaire, montantTotal)
        {
            if (état == ÉtatDuCompte.Fermé && montantTotal != 0)
            {
                throw new ArgumentException("Un état fermé est incompatible avec un solde non nul.", nameof(état));
            }
            this.État = état;
        }

        public override void Déposer(decimal montant)
        {
            if (this.État != ÉtatDuCompte.Actif)
            {
                throw new InvalidOperationException(
                    "Impossible de déposer car le compte n'est pas actif");
            }
            base.Déposer(montant);
        }

        public override void Retirer(decimal montant)
        {
            if (this.État != ÉtatDuCompte.Actif)
            {
                throw new InvalidOperationException(
                    "Impossible de retirer car le compte n'est pas actif");
            }
            base.Retirer(montant);
        }

        public override decimal Vider()
        {
            if (this.État != ÉtatDuCompte.Actif)
            {
                throw new InvalidOperationException(
                    "Impossible de vider car le compte n'est pas actif");
            }
            return base.Vider();
        }

        public override string ToString()
            => base.ToString() + $"{this.État}";

        public override bool Equals(object obj)
            => obj is Compte2 c
            && c.Numéro == this.Numéro
            && c.Titulaire == this.Titulaire
            && c.MontantTotal == this.MontantTotal
            && c.État == this.État;

        public override int GetHashCode()
        {
            throw new NotImplementedException();
        }
    }
}
