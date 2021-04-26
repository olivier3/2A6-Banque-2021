using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BanqueLib
{
    public class Compte2 : Compte1
    {
        public ÉtatDuCompte État { get; set; }

        public decimal Fermer()
        {
            if (this.État == ÉtatDuCompte.Fermé)
            {
                throw new InvalidOperationException("Impossible de fermer un compte déjà fermé");
            }
            this.État = ÉtatDuCompte.Fermé;
            return MontantTotal;
        }
        public void Réactiver()
        {
            if (this.État == ÉtatDuCompte.Actif)
            {
                throw new InvalidOperationException("Impossible de réactiver un compte déjà actif");
            }
            this.État = ÉtatDuCompte.Actif;
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
    }
}
