using Microsoft.VisualStudio.TestTools.UnitTesting;
using static Microsoft.VisualStudio.TestTools.UnitTesting.Assert;
using BanqueLib;

namespace Tests
{
    public partial class Tester_Compte_1
    {
        public virtual Compte1 Copier(Compte1 compte)
            => new Compte1(compte);

        [TestMethod]
        [DataRow(1, "Obiwan", 10)]
        [DataRow(2, "Yoda", 100.01)]
        public void T4_ToString(int numéro, string titulaire, double montant)
        {
            var montantTotal = (decimal)montant;
            var compte = this.NewCompte(numéro, titulaire, montantTotal);

            StringAssert.Contains("" + compte, "" + numéro);
            StringAssert.Contains("" + compte, titulaire);
            StringAssert.Contains("" + compte, $"{montantTotal:C}");
        }

        [TestMethod]
        [DataRow(1, "Obiwan", 10)]
        [DataRow(2, "Yoda", 100.01)]
        public void T5_Equals(int numéro, string titulaire, double montant)
        {
            var montantTotal = (decimal)montant;
            var compte = this.NewCompte(numéro, titulaire, montantTotal);
            var copie = this.NewCompte(numéro, titulaire, montantTotal);
            var diff1 = this.NewCompte(numéro + 1, titulaire, montantTotal);
            var diff2 = this.NewCompte(numéro, titulaire + 1, montantTotal);
            var diff3 = this.NewCompte(numéro, titulaire, montantTotal + 1);

            IsTrue(compte.Equals(compte));
            IsTrue(compte.Equals(copie));

            IsFalse(compte.Equals(2));
            IsFalse(compte.Equals(diff1));
            IsFalse(compte.Equals(diff2));
            IsFalse(compte.Equals(diff3));
            IsFalse(compte.Equals(null));
        }

        [TestMethod]
        [DataRow(1, "Obiwan", 10)]
        [DataRow(2, "Yoda", 100.01)]
        public void T6_Constructeur_Copie(int numéro, string titulaire, double montant)
        {
            var montantTotal = (decimal)montant;
            var compte = this.NewCompte(numéro, titulaire, montantTotal);
            var copie = this.Copier(compte);
            IsTrue(compte.Equals(copie));
        }

    }
}
