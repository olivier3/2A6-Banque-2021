using Microsoft.VisualStudio.TestTools.UnitTesting;
using static Microsoft.VisualStudio.TestTools.UnitTesting.Assert;
using TireLireLib;
using System.Reflection;

namespace Tests
{
    [TestClass, TestCategory("OB")]
    public class Tester_Tirelire_5
    {
        [TestMethod]
        public void T0_Réflexion()
        {
            // Par rapport à la tirelire2, 
            // la tirelire5 possède un constructeur en plus
            // car il s'agit d'une classe non statique (instanciable).
            AreEqual(
                typeof(Tirelire2).GetMembers().Length + 1,
                typeof(Tirelire5).GetMembers().Length);

            // La propriété n'est pas statique et est publique
            AreEqual(1, typeof(Tirelire5).GetProperties(BindingFlags.Instance | BindingFlags.Public).Length);
        }

        [TestMethod]
        public void T1_Déposer()
        {
            var tirelire = new Tirelire5();
            AreEqual(0, tirelire.MontantTotal);

            IsFalse(Tirelire5.Déposer(tirelire, 0));
            IsFalse(Tirelire5.Déposer(tirelire, -10));

            IsTrue(Tirelire5.Déposer(tirelire, 100));
            IsTrue(Tirelire5.Déposer(tirelire, 50));

            AreEqual(150, tirelire.MontantTotal);

            IsFalse(Tirelire5.Déposer(tirelire, 0));
            IsFalse(Tirelire5.Déposer(tirelire, -10));

            AreEqual(150, tirelire.MontantTotal);
        }

        [TestMethod]
        public void T2_Vider()
        {
            var tirelire = new Tirelire5();
            AreEqual(0, tirelire.MontantTotal);

            AreEqual(0, Tirelire5.Vider(tirelire));
            IsTrue(Tirelire5.Déposer(tirelire, 100));
            AreEqual(100, Tirelire5.Vider(tirelire));
            AreEqual(0, tirelire.MontantTotal);
            IsTrue(Tirelire5.Déposer(tirelire, 500));
            AreEqual(500, Tirelire5.Vider(tirelire));
            AreEqual(0, tirelire.MontantTotal);
        }

        [TestMethod]
        public void T3_Retirer()
        {
            var tirelire = new Tirelire5();
            AreEqual(0, tirelire.MontantTotal);

            IsFalse(Tirelire5.Retirer(tirelire, 0));
            IsFalse(Tirelire5.Retirer(tirelire, -10));
            IsFalse(Tirelire5.Retirer(tirelire, 1));

            IsTrue(Tirelire5.Déposer(tirelire, 1000));
            IsTrue(Tirelire5.Retirer(tirelire, 50));
            IsTrue(Tirelire5.Retirer(tirelire, 100));

            IsFalse(Tirelire5.Retirer(tirelire, 0));
            IsFalse(Tirelire5.Retirer(tirelire, -10));
            IsFalse(Tirelire5.Retirer(tirelire, 851));

            AreEqual(850, tirelire.MontantTotal);
        }

    }
}
