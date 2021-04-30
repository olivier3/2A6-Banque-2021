using Microsoft.VisualStudio.TestTools.UnitTesting;
using static Microsoft.VisualStudio.TestTools.UnitTesting.Assert;
using BanqueLib;

using static Tests.TestUtil;
using static BanqueLib.ÉtatDuCompte;

namespace Tests
{
    [TestClass, TestCategory("OB")]
    public partial class Tester_Compte_3 : Tester_Compte_2
    {

        public override Compte3 NewCompte(int numéro, string titulaire)
            => new Compte3(numéro: numéro, titulaire: titulaire);

        public override Compte3 NewCompte(int numéro, string titulaire, decimal montantTotal)
            => new Compte3(numéro: numéro, titulaire: titulaire, montantTotal: montantTotal);

        public override Compte3 NewCompte(int numéro, string titulaire, decimal montantTotal, ÉtatDuCompte état)
            => new Compte3(numéro: numéro, titulaire: titulaire, montantTotal: montantTotal, état: état);

        public override Compte3 Copier(Compte1 compte)
            => new Compte3((Compte3)compte);


        [TestMethod]
        public void T3c_Opérations_De_Base_Gelé()
        {
            this.Opérations_De_Base_Invalide(Gelé);
        }


        [TestMethod]
        public void T9_Geler()
        {
            var compte = this.NewCompte(777, "Obiwan", 100m, Gelé);

            AreEqual(Gelé, compte.État);
            OpérationInvalide(() => compte.Geler(), "Impossible de geler car le compte n'est pas actif");
            compte.Réactiver();
            AreEqual(Actif, compte.État);
            compte.Geler();
            AreEqual(Gelé, compte.État);
            AreEqual(100m, compte.Fermer());
            AreEqual(Fermé, compte.État);
        }

        [TestMethod]
        [DataRow(1000, 10, 1100)]
        [DataRow(1000, 1, 1010)]
        [DataRow(1000, 0.1, 1001)]
        [DataRow(100, 0.1, 100.1)]
        [DataRow(100, 0.01, 100.01)]
        [DataRow(10, 0.01, 10)]
        [DataRow(34527.24, 1.385, 35005.44)]
        public void TT10_Verser_Intérêts(double _SoldeInitial, double _Pourcentage, double _SoldeFinal)
        {
            var soldeInitial = (decimal)_SoldeInitial;
            var pourcentage = (decimal)_Pourcentage;
            var soldeFinal = (decimal)_SoldeFinal;
            var intérêts = soldeFinal - soldeInitial;

            // Compte actif            
            var compte = this.NewCompte(777, "Obiwan", soldeInitial, Actif);
            AreEqual(intérêts, compte.VerserIntérêts(pourcentage));
            AreEqual(soldeFinal, compte.MontantTotal);
            ArgumentOutOfRange(() => compte.VerserIntérêts(-pourcentage), "trop petit", "pourcentage", -pourcentage);

            // Compte gelé
            compte = this.NewCompte(777, "Obiwan", soldeInitial, Gelé);
            AreEqual(intérêts, compte.VerserIntérêts(pourcentage));
            AreEqual(soldeFinal, compte.MontantTotal);
            ArgumentOutOfRange(() => compte.VerserIntérêts(-pourcentage), "trop petit", "pourcentage", -pourcentage);

            // Compte fermé
            compte = this.NewCompte(777, "Obiwan", 0, Fermé);
            OpérationInvalide(() => compte.VerserIntérêts(pourcentage), "Impossible de verser des intérêts dans un compte fermé");
        }

    }

}
