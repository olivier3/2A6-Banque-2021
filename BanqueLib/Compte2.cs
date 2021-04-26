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
            this.État = ÉtatDuCompte.Fermé;
            return this.Vider();
        }
        public void Réactiver()
        {
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
    }
}
