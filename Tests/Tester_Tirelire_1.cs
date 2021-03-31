using Microsoft.VisualStudio.TestTools.UnitTesting;
using static Microsoft.VisualStudio.TestTools.UnitTesting.Assert;
using TireLireLib;

namespace Tests
{
    [TestClass, TestCategory("OB")]
    public class Tester_Tirelire_1
    {
        [TestMethod]
        public void T1_Déposer()
        {
            Tirelire1.MontantTotal = 0;
            AreEqual(0, Tirelire1.MontantTotal);

            IsFalse(Opérations1.Déposer(0));
            IsFalse(Opérations1.Déposer(-10));

            IsTrue(Opérations1.Déposer(100));
            IsTrue(Opérations1.Déposer(50));

            IsFalse(Opérations1.Déposer(0));
            IsFalse(Opérations1.Déposer(-10));

            AreEqual(150, Tirelire1.MontantTotal);
        }

        [TestMethod]
        public void T2_Vider()
        {
            Tirelire1.MontantTotal = 0;
            AreEqual(0, Opérations1.Vider());
            IsTrue(Opérations1.Déposer(100));
            AreEqual(100, Opérations1.Vider());
            AreEqual(0, Tirelire1.MontantTotal);
            IsTrue(Opérations1.Déposer(500));
            AreEqual(500, Opérations1.Vider());
            AreEqual(0, Tirelire1.MontantTotal);
        }

        [TestMethod]
        public void T3_Retirer()
        {
            Tirelire1.MontantTotal = 0;
            AreEqual(0, Tirelire1.MontantTotal);

            IsFalse(Opérations1.Retirer(0));
            IsFalse(Opérations1.Retirer(-10));
            IsFalse(Opérations1.Retirer(1));

            IsTrue(Opérations1.Déposer(1000));
            IsTrue(Opérations1.Retirer(50));
            IsTrue(Opérations1.Retirer(100));

            IsFalse(Opérations1.Retirer(0));
            IsFalse(Opérations1.Retirer(-10));
            IsFalse(Opérations1.Retirer(851));

            AreEqual(850, Tirelire1.MontantTotal);
        }

    }
}
