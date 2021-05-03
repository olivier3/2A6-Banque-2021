using Microsoft.VisualStudio.TestTools.UnitTesting;
using static Microsoft.VisualStudio.TestTools.UnitTesting.Assert;
using BanqueLib;

using System;
using System.Linq;

using static Tests.TestUtil;
using System.Collections.Generic;

namespace Tests
{
    [TestClass, TestCategory("OB")]
    public partial class Tester_Banque
    {
        private static void ValiderBanque(
            Banque banque,
            string nom = "Inconnu",
            int prochain = 1,
            IEnumerable<Compte3>? comptes = null,
            decimal actifTotal = 0m,
            decimal actifGelé = 0m,
            int nbActifs = 0,
            int nbGelés = 0,
            int nbFermés = 0)
        {
            comptes ??= Enumerable.Empty<Compte3>();
            var nbComptes = nbActifs + nbGelés + nbFermés;
            AreEqual(nom, banque.Nom);
            AreEqual(prochain, banque.ProchainNuméroDeCompte);
            IsTrue(comptes.SequenceEqual(banque.Comptes));
            AreEqual(actifTotal, banque.ActifTotal);
            AreEqual(actifGelé, banque.ActifGelé);
            AreEqual(nbComptes, banque.NbComptes);
            AreEqual(nbActifs, banque.NbActifs);
            AreEqual(nbGelés, banque.NbGelés);
            AreEqual(nbFermés, banque.NbFermés);
        }

        private static Banque NewBanque(string nom = "Inconnu")
            => new Banque(nom);

        private static Banque NewBanque(string nom, IEnumerable<Compte3> comptes)
            => new Banque(nom, comptes);

        [TestMethod]
        [DataRow("Machin")]
        [DataRow("Truc")]
        public void T1_Construction_Avec_Nom(string nom)
        {
            ValiderBanque(NewBanque(nom), nom);
        }

        [TestMethod]
        [DataRow("")]
        [DataRow("   ")]
        [DataRow(null)]
        public void T1e_Construction_Avec_Nom_Erroné(string nom)
        {
            ArgumentInvalide(() => _ = NewBanque(nom), "null ou blanc", nameof(nom));
        }

        public static IEnumerable<Compte3> Comptes(int n = 9) => new Compte3[]
        {
            new Compte3(1, "Alpha", 1m),
            new Compte3(2, "Bêta", 2m),
            new Compte3(5, "Gamma", 4m),
            new Compte3(8, "Delta", 8m),
            new Compte3(10, "Epsilon", 16m, ÉtatDuCompte.Gelé),
            new Compte3(13, "Zêta", 32m, ÉtatDuCompte.Gelé),
            new Compte3(14, "Êta", 64m, ÉtatDuCompte.Gelé),
            new Compte3(15, "Thêta", 0m, ÉtatDuCompte.Fermé),
            new Compte3(19, "Iota", 0m, ÉtatDuCompte.Fermé),
        }.Take(n);

        [TestMethod]
        [DataRow(1, 2, 1, 1)]
        [DataRow(2, 3, 3, 2)]
        [DataRow(3, 6, 7, 3)]
        [DataRow(4, 9, 15, 4)]
        [DataRow(5, 11, 31, 4, 1, 16)]
        [DataRow(6, 14, 63, 4, 2, 48)]
        [DataRow(7, 15, 127, 4, 3, 112)]
        [DataRow(8, 16, 127, 4, 3, 112, 1)]
        [DataRow(9, 20, 127, 4, 3, 112, 2)]
        public void T2_Construction_Avec_Comptes(int nb, int prochain,
            int actifTotal, int nbActifs, int nbGelés = 0,
            int actifGelé = 0, int nbFermés = 0)
        {
            // On crée respectivement 9 banques à partir des comptes 1 à 9
            // et on valide que les caractéristiques sont telles qu'attendues
            var nom = "B3." + nb;
            var banque = NewBanque(nom, Comptes(nb));
            ValiderBanque(banque, nom, prochain, Comptes(nb),
                actifTotal, actifGelé, nbActifs, nbGelés, nbFermés);
        }

        [TestMethod]
        public void T2b_Construction_Avec_Comptes_Désordonnés()
        {
            // Les comptes peuvent être fournis en désordre 
            // Mais il faut les mettre en ordre dans le constructeur
            ValiderBanque(
                NewBanque("B3b", Comptes().Reverse()),
                "B3b", 20, Comptes(), 127, 112, 4, 3, 2);
            ValiderBanque(
                NewBanque("B3b", Comptes().Skip(5).Concat(Comptes().Take(5))),
                "B3b", 20, Comptes(), 127, 112, 4, 3, 2);
        }

        [TestMethod]
        public void T2c_Construction_Avec_Comptes_Clonés()
        {
            // Les comptes sont clonés/copiés au moment de la construction
            var comptes = Comptes();
            var banque = NewBanque("B3c", comptes.ToArray());
            for (int i = 0; i < 9; i++)
            {
                IsFalse(comptes.ElementAt(i) == banque.Comptes.ElementAt(i));
            }
        }

        [TestMethod]
        public void T2e_BONUS_Construction_Avec_Doublons()
        {
            // Le constructeur doit détecter la présence de doublons pour les numéros de compte.
            foreach (var compte in Comptes())
            {
                ArgumentInvalide(
                    () => _ = NewBanque("Bidule", Comptes().Append(compte)),
                    "numéros en double");
            }
        }


        [TestMethod]
        public void T3_Ouvrir_Compte()
        {
            // On ouvre consécutivement 5 comptes d'affilé
            // Et on vérifie que le tout se passe correctement
            var banque = NewBanque("B5");
            var comptes = new List<Compte3>();
            for (int i = 1; i <= 5; i++)
            {
                var compte = banque.OuvrirCompte("T" + i, 2 * i);
                AreEqual(new Compte3(i, "T" + i, 2 * i), compte);
                comptes.Add(compte);
            }
            ValiderBanque(banque, "B5", 6, comptes, 30, 0, 5);
        }

        [TestMethod]
        [DataRow(null)]
        [DataRow("")]
        [DataRow("  ")]
        public void T3e_Ouvrir_Compte_Titulaire_Erroné(string titulaire)
        {
            var banque = NewBanque();
            ArgumentInvalide(() => banque.OuvrirCompte(titulaire), "null ou blanc");
        }

        [TestMethod]
        [DataRow(-1)]
        [DataRow(-10)]
        public void T3f_Ouvrir_Compte_Montant_Erroné(int miseDeFond)
        {
            var banque = NewBanque();
            ArgumentOutOfRange(() => banque.OuvrirCompte("Yoda", miseDeFond),
                "trop petit", (decimal)miseDeFond);
        }

        [TestMethod]
        public void T4_Détruire_Compte()
        {
            // On tente de détruire successivement les comptes fermés
            foreach (var compte in Comptes().Where(c => c.État == ÉtatDuCompte.Fermé))
            {
                var banque = NewBanque("B6", Comptes());
                IsTrue(banque.Comptes.Contains(compte));
                var détruit = banque.DétruireCompte(compte.Numéro);
                AreEqual(compte, détruit);
                IsFalse(banque.Comptes.Contains(compte));
            }
        }

        [TestMethod]
        [DataRow(-1)]
        [DataRow(-10)]
        [DataRow(20)]
        [DataRow(3)]
        public void T4e_Détruire_Compte_Numéro_Erroné(int numéro)
        {
            var banque = NewBanque("B6", Comptes());
            ArgumentInvalide(() => banque.DétruireCompte(numéro), "inexistant");

            banque = NewBanque();
            ArgumentInvalide(() => banque.DétruireCompte(numéro), "inexistant");
        }

        [TestMethod]
        [DataRow(1)]
        [DataRow(2)]
        [DataRow(10)]
        [DataRow(13)]
        public void T4f_Détruire_Compte_Pas_Fermé(int numéro)
        {
            var banque = NewBanque("B6", Comptes());
            OpérationInvalide(() => banque.DétruireCompte(numéro), "pas fermé");
        }


        [TestMethod]
        [DataRow(1, 1.27)]
        [DataRow(10, 12.7)]
        [DataRow(100, 127)]
        [DataRow(1, 1.2, 3)]
        [DataRow(10, 12, 3)]
        [DataRow(1, 0, 7)]
        [DataRow(10, 0, 7)]
        public void T5_Verser_Intérêts(int pourcentage, double intérêtsAttendus, int nbVidés = 0)
        {
            var comptes = Comptes();
            // On vide un certain nombre de comptes au préalable
            foreach (var compte in comptes.Take(nbVidés))
            {
                try { compte.Réactiver(); } catch { }
                _ = compte.Vider();
            }
            var banque = NewBanque("B7", comptes);
            var intérêtsVersés = banque.VerserIntérêts(pourcentage, out int nbComptes);
            AreEqual(7 - nbVidés, nbComptes);
            AreEqual((decimal)intérêtsAttendus, intérêtsVersés);
        }


        [TestMethod]
        [DataRow(-1)]
        [DataRow(-10)]
        public void T5e_Verser_Intérêts_Pourcentage_Erroné(int pourcentage)
        {
            var banque = NewBanque("B7", Comptes());
            ArgumentOutOfRange(() => banque.VerserIntérêts(pourcentage, out int nbComptes), "trop petit", (decimal)pourcentage);
        }


    }
}
