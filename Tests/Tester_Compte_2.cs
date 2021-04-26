using Microsoft.VisualStudio.TestTools.UnitTesting;
using static Microsoft.VisualStudio.TestTools.UnitTesting.Assert;
using BanqueLib;

using static Tests.TestUtil;
using static BanqueLib.ÉtatDuCompte;
using System;

namespace Tests
{
    [TestClass, TestCategory("OB")]
    public partial class Tester_Compte_2 : Tester_Compte_1
    {
        public override Compte2 NewCompte(int numéro, string titulaire)
            => new Compte2(numéro: numéro, titulaire: titulaire);

        public override Compte2 NewCompte(int numéro, string titulaire, decimal montantTotal)
            => new Compte2(numéro: numéro, titulaire: titulaire, montantTotal: montantTotal);

        public virtual Compte2 NewCompte(int numéro, string titulaire, decimal montantTotal, ÉtatDuCompte état)
            => new Compte2(numéro: numéro, titulaire: titulaire, montantTotal: montantTotal, état: état);

        public override Compte2 Copier(Compte1 compte)
        {
            var copie = this.NewCompte(1, "Bidule");
            GénérerCopie(ref copie, (Compte2)compte);
            return copie;
        }

        static partial void GénérerCopie(ref Compte2 copie, Compte2 original);
        static partial void EstSupp(ref bool estSupp);

        private static void Inconclusif()
        {
            bool estSupp = false;
            EstSupp(ref estSupp);
            if (!estSupp)
            {
                Inconclusive();
            }
        }

        [TestMethod]
        [DataRow(1, "Obiwan", 10)]
        [DataRow(2, "Yoda", 100.01)]
        public void T1c_Construction_État(int numéro, string titulaire, double montant)
        {
            var montantTotal = (decimal)montant;
            var compte = this.NewCompte(numéro, titulaire, montantTotal, Actif);

            AreEqual(numéro, compte.Numéro);
            AreEqual(titulaire, compte.Titulaire);
            AreEqual(montantTotal, compte.MontantTotal);
            AreEqual(Actif, compte.État);

            compte = this.NewCompte(numéro, titulaire, 0, Fermé);
            AreEqual(numéro, compte.Numéro);
            AreEqual(titulaire, compte.Titulaire);
            AreEqual(0, compte.MontantTotal);
            AreEqual(Fermé, compte.État);
        }

        [TestMethod]
        public void T2d_Construction_État_Invalide()
        {
            ArgumentInvalide(() => this.NewCompte(1, "Obiwan", 2, Fermé), "incompatible", "état");
            ArgumentInvalide(() => this.NewCompte(2, "Yoda", 0.01m, Fermé), "incompatible", "état");
        }

        public void Opérations_De_Base_Invalide(ÉtatDuCompte état)
        {
            var compte = this.NewCompte(999, "Yoda", 0, état);
            OpérationInvalide(() => compte.Déposer(1), "Impossible de déposer car le compte n'est pas actif");
            OpérationInvalide(() => compte.Retirer(1), "Impossible de retirer car le compte n'est pas actif");
            OpérationInvalide(() => compte.Vider(), "Impossible de vider car le compte n'est pas actif");
        }

        [TestMethod]
        public void T3b_OpérationsDeBase_Fermé()
        {
            this.Opérations_De_Base_Invalide(Fermé);
        }

        [TestMethod]
        [DataRow(1, "Leia", 80, Actif)]
        [DataRow(2, "Luke", 0, Fermé)]
        public void T4_ToString(int numéro, string titulaire, int montant, ÉtatDuCompte état)
        {
            Inconclusif();
            this.T4_ToString(numéro, titulaire, montant);
            var compte = this.NewCompte(numéro, titulaire, montant, état);
            StringAssert.Contains("" + compte, $"{état}");
        }

        [TestMethod]
        [DataRow(1, "Leia", 80, Actif)]
        [DataRow(2, "Luke", 0, Actif)]
        public void T5_Equals(int numéro, string titulaire, int montant, ÉtatDuCompte état)
        {
            Inconclusif();
            this.T5_Equals(numéro, titulaire, montant);
            var compte = this.NewCompte(numéro, titulaire, montant, état);
            var diff4 = this.NewCompte(numéro, titulaire, 0, Fermé);
            IsTrue(diff4.Equals(diff4));
            IsFalse(compte.Equals(diff4));
        }


        [TestMethod]
        [DataRow(1, "Obiwan", 10, Actif)]
        [DataRow(2, "Yoda", 100.01, Actif)]
        public void T6_Constructeur_Copie(int numéro, string titulaire, double montant, ÉtatDuCompte _)
        {
            Inconclusif();
            this.T6_Constructeur_Copie(numéro, titulaire, montant);
        }


        [TestMethod]
        public void T7_Fermer()
        {
            var compte = this.NewCompte(777, "Obiwan", 88.88m, Actif);

            AreEqual(Actif, compte.État);
            AreEqual(88.88m, compte.Fermer());
            AreEqual(Fermé, compte.État);
            OpérationInvalide(() => compte.Fermer(), "Impossible de fermer un compte déjà fermé");
        }

        [TestMethod]
        public void T8_Réactiver()
        {
            var compte = this.NewCompte(777, "Obiwan", 0m, Fermé);

            AreEqual(Fermé, compte.État);
            compte.Réactiver();
            AreEqual(Actif, compte.État);
            OpérationInvalide(() => compte.Réactiver(), "Impossible de réactiver un compte déjà actif");
        }

    }

    public static partial class TestUtil
    {
        public static void OpérationInvalide(Action action, string message)
        {
            var ex = ThrowsException<InvalidOperationException>(() => action());
            StringAssert.Contains(ex.Message, message);
        }
    }

}
