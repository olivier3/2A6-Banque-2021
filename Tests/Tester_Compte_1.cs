using Microsoft.VisualStudio.TestTools.UnitTesting;
using static Microsoft.VisualStudio.TestTools.UnitTesting.Assert;
using BanqueLib;

using static Tests.Tester_Tirelire_9;
using static Tests.TestUtil;
using System;

namespace Tests
{
    [TestClass, TestCategory("OB")]
    public partial class Tester_Compte_1
    {
        public virtual Compte1 NewCompte(int numéro, string titulaire)
            => new Compte1(numéro: numéro, titulaire: titulaire);

        public virtual Compte1 NewCompte(int numéro, string titulaire, decimal montantTotal)
            => new Compte1(numéro: numéro, titulaire: titulaire, montantTotal: montantTotal);


        [TestMethod]
        [DataRow(1, "Obiwan")]
        [DataRow(2, "Yoda")]
        public void T1a_Construction_De_Base(int numéro, string titulaire)
        {
            var compte = this.NewCompte(numéro, titulaire);

            AreEqual(titulaire, compte.Titulaire);
            AreEqual(numéro, compte.Numéro);
            AreEqual(0, compte.MontantTotal);
        }

        [TestMethod]
        [DataRow(1, "Obiwan", 10)]
        [DataRow(2, "Yoda", 100.01)]
        public void T1b_Construction_Montant(int numéro, string titulaire, double montant)
        {
            var montantTotal = (decimal)montant;
            var compte = this.NewCompte(numéro, titulaire, montantTotal);

            AreEqual(titulaire, compte.Titulaire);
            AreEqual(numéro, compte.Numéro);
            AreEqual(montantTotal, compte.MontantTotal);
        }

        [TestMethod]
        public void T2a_Construction_Numéro_Invalide()
        {
            ArgumentOutOfRange(() => this.NewCompte(0, "Yoda"), "trop petit", "numéro", 0);
            ArgumentOutOfRange(() => this.NewCompte(-10, "Obiwan"), "trop petit", "numéro", -10);
        }

        [TestMethod]
        public void T2b_Construction_Titulaire_Invalide()
        {
            ArgumentInvalide(() => this.NewCompte(1, ""), "null ou blanc", "titulaire");
            ArgumentInvalide(() => this.NewCompte(1, "    "), "null ou blanc", "titulaire");
            ArgumentInvalide(() => this.NewCompte(1, null!), "null ou blanc", "titulaire");
        }

        [TestMethod]
        public void T2c_Construction_Montant_Invalide()
        {
            ArgumentOutOfRange(() => this.NewCompte(1, "Yoda", -10m), "trop petit", -10m);
            ArgumentInvalide(() => this.NewCompte(1, "Yoda", 1.001m), "trop précis");
        }

        [TestMethod]
        public void T3a_OpérationsDeBase()
        {
            TesterDéposer(this.NewCompte(22, "R2D2"));
            TesterRetirer(this.NewCompte(22, "R2D2"));
            TesterVider(this.NewCompte(22, "R2D2"));
        }
    }

    public static partial class TestUtil
    {
        public static void ArgumentOutOfRange(Action action, string message, object actualValue)
        {
            var ex = ThrowsException<ArgumentOutOfRangeException>(() => action());
            StringAssert.Contains(ex.Message, message);
            AreEqual(actualValue, ex.ActualValue);
        }

        public static void ArgumentInvalide(Action action, string message)
        {
            var ex = ThrowsException<ArgumentException>(() => action());
            StringAssert.Contains(ex.Message, message);
        }
    }

}
