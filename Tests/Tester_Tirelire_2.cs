using Microsoft.VisualStudio.TestTools.UnitTesting;
using static Microsoft.VisualStudio.TestTools.UnitTesting.Assert;
using TireLireLib;

namespace Tests
{
    [TestClass, TestCategory("FG")]
    public class Tester_Tirelire_2
    {
        [TestMethod]
        public void T0_GetPrivateSet()
        {
            // Votre tirelire1 doit etre statique, donc aucun constructeur
            AreEqual(0, typeof(Tirelire1).GetConstructors().Length);
            // Vos opérations 1 doivent etre statiques, donc aucun constructeur
            AreEqual(0, typeof(Opérations1).GetConstructors().Length);
            // Votre tirelire2 doit etre statique
            AreEqual(0, typeof(Tirelire2).GetConstructors().Length);
            // Votre tirelire 2 contient trois méthodes de plus
            // que la tirelire 1, mais un setter public en moins
            // donc 2 membres de plus au total
            AreEqual(
                typeof(Tirelire1).GetMembers().Length + 2,
                typeof(Tirelire2).GetMembers().Length);
        }

        [TestMethod]
        public void T1_Déposer()
        {
            AreEqual(0, Tirelire2.MontantTotal);

            IsFalse(Tirelire2.Déposer(0));
            IsFalse(Tirelire2.Déposer(-10));

            IsTrue(Tirelire2.Déposer(100));
            IsTrue(Tirelire2.Déposer(50));

            IsFalse(Tirelire2.Déposer(0));
            IsFalse(Tirelire2.Déposer(-10));

            AreEqual(150, Tirelire2.MontantTotal);
        }

        [TestMethod]
        public void T2_Vider()
        {
            _ = Tirelire2.Vider();
            AreEqual(0, Tirelire2.Vider());
            IsTrue(Tirelire2.Déposer(100));
            AreEqual(100, Tirelire2.Vider());
            AreEqual(0, Tirelire2.MontantTotal);
            IsTrue(Tirelire2.Déposer(500));
            AreEqual(500, Tirelire2.Vider());
            AreEqual(0, Tirelire2.MontantTotal);
        }

        [TestMethod]
        public void T3_Retirer()
        {
            _ = Tirelire2.Vider();
            AreEqual(0, Tirelire2.MontantTotal);

            IsFalse(Tirelire2.Retirer(0));
            IsFalse(Tirelire2.Retirer(-10));
            IsFalse(Tirelire2.Retirer(1));

            IsTrue(Tirelire2.Déposer(1000));
            IsTrue(Tirelire2.Retirer(50));
            IsTrue(Tirelire2.Retirer(100));

            IsFalse(Tirelire2.Retirer(0));
            IsFalse(Tirelire2.Retirer(-10));
            IsFalse(Tirelire2.Retirer(851));

            AreEqual(850, Tirelire2.MontantTotal);
        }

    }
}
