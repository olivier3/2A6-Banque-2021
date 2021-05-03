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
        public string Nom { get; }

        public Banque(string nom, IEnumerable<Compte3> Comptes = null)
        {

        }

        public int ProchainNuméroDeCompte { get; set; }

        public decimal ActifTotal { get; set; }

        public decimal ActifGelé { get; set; }

        public int NbComptes { get; set; }

        public int NbActifs { get; set; }

        public int NbGelés { get; set; }

        public int NbFermés { get; set; }

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
