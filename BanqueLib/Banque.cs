using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BanqueLib
{
    public class Banque
    {
        //Champs
        private readonly List<Compte3> comptes = new();

        // Propriete calculee
        public IEnumerable<Compte3> Comptes => this.comptes;

        // Propriete automatique
        public string Nom { get; init; }

        public Banque(string nom, IEnumerable<Compte3> comptes = null)
        {
            if (string.IsNullOrWhiteSpace(nom))
            {
                throw new ArgumentException("null ou blanc", nameof(nom));
            }
            this.Nom = nom;

            if (comptes != null)
            {
                /*foreach (Compte3 compte in comptes)
                {
                    if (this.comptes.GroupBy(x => x.Numéro).All(g => g.Count() == 1))
                    {
                        throw new ArgumentException("numéros en double");
                    }
                }*/

                foreach (Compte3 compte in comptes)
                {
                    this.comptes.Add(new Compte3(compte));
                }

                this.comptes.Sort((cpt1, cpt2) => cpt1.Numéro.CompareTo(cpt2.Numéro));
            }
        }
        public int ProchainNuméroDeCompte => this.comptes.Count != 0 ? this.comptes.Max(comptes => comptes.Numéro) + 1 : 1;

        public decimal ActifTotal => this.comptes.Sum(comptes => comptes.MontantTotal);

        public decimal ActifGelé
            => this.comptes.Where(comptes => comptes.État == ÉtatDuCompte.Gelé).Sum(comptes => comptes.MontantTotal);

        public int NbComptes => this.comptes.Count;

        public int NbActifs => this.comptes.Count(comptes => comptes.État == ÉtatDuCompte.Actif);

        public int NbGelés => this.comptes.Count(comptes => comptes.État == ÉtatDuCompte.Gelé);

        public int NbFermés => this.comptes.Count(comptes => comptes.État == ÉtatDuCompte.Fermé);



        public Compte3 OuvrirCompte(string nom, decimal montant = 0)
        {
            throw new NotImplementedException();
        }

        public Compte3 DétruireCompte(int numéroCompte)
        {
            throw new NotImplementedException();
        }

        public decimal VerserIntérêts(decimal pourcentage, out int montant)
        {
            throw new NotImplementedException();
        }
    }
}
