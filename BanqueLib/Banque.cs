using System;
using System.Collections.Generic;
using System.Linq;

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
                foreach (Compte3 compte in comptes)
                {
                    if (this.comptes.Any(compteDouble => compteDouble.Numéro == compte.Numéro))
                    {
                        throw new ArgumentException("numéros en double");
                    }
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



        public Compte3 OuvrirCompte(string nom, decimal montantInitial = 0)
        {
            if (string.IsNullOrWhiteSpace(nom))
            {
                throw new ArgumentException("null ou blanc", nameof(nom));
            }

            //if (montantInitial < 0)
            //{
            //    throw new ArgumentOutOfRangeException(nameof(montantInitial), montantInitial, "trop petit");
            //}

            Compte3 compte = new Compte3(this.ProchainNuméroDeCompte, nom, montantInitial);

            this.comptes.Add(compte);

            return compte;
        }

        public Compte3 DétruireCompte(int numéroCompte)
        {

            if (!this.comptes.Exists(noCompte => noCompte.Numéro == numéroCompte))
            {
                throw new ArgumentException("compte inexistant", nameof(numéroCompte));
            }

            int emplCompteÀDétruire = this.comptes.FindIndex(indice => indice.Numéro == numéroCompte);

            if (this.comptes[emplCompteÀDétruire].État != ÉtatDuCompte.Fermé)
            {
                throw new InvalidOperationException("pas fermé");
            }

            Compte3 compteRetiré = this.comptes[emplCompteÀDétruire];

            this.comptes.RemoveAt(emplCompteÀDétruire);

            return compteRetiré;
        }

        public decimal VerserIntérêts(decimal pourcentage, out int nbCompte)
        {
            decimal totalIntérêts = 0;
            nbCompte = 0;
            foreach (Compte3 compte in this.comptes)
            {
                if (compte.État != ÉtatDuCompte.Fermé)
                {
                    decimal intérêts = compte.VerserIntérêts(pourcentage);
                    if (totalIntérêts < intérêts)
                    {
                        totalIntérêts += intérêts;
                        nbCompte += 1;
                    }
                }
            }

            return totalIntérêts;
        }
    }
}
