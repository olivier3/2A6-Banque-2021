using Microsoft.VisualStudio.TestTools.UnitTesting;
using static Microsoft.VisualStudio.TestTools.UnitTesting.Assert;
using System.Reflection;
using TireLireLib;

namespace Tests
{
    [TestClass, TestCategory("FG")]
    public class Tester_Tirelire_3
    {
        [TestMethod]
        public void T0_Réflexion()
        {
            // Par rapport à la tirelire1, 
            // la tirelire3 possède un constructeur en plus
            // car il s'agit d'une classe non statique (instanciable).
            // notez que le constructeur est créé implicitement
            // par le compilateur.
            AreEqual(
                typeof(Tirelire1).GetMembers().Length + 1,
                typeof(Tirelire3).GetMembers().Length);

            // La propriété n'est pas statique et est publique
            AreEqual(1, typeof(Tirelire3).GetProperties(BindingFlags.Instance | BindingFlags.Public).Length);
        }

        [TestMethod]
        public void T1_Déposer()
        {
            var tirelire = new Tirelire3();
            AreEqual(0, tirelire.MontantTotal);

            IsFalse(Opérations3.Déposer(tirelire, 0));
            IsFalse(Opérations3.Déposer(tirelire, -10));

            IsTrue(Opérations3.Déposer(tirelire, 100));
            IsTrue(Opérations3.Déposer(tirelire, 50));

            AreEqual(150, tirelire.MontantTotal);

            IsFalse(Opérations3.Déposer(tirelire, 0));
            IsFalse(Opérations3.Déposer(tirelire, -10));

            AreEqual(150, tirelire.MontantTotal);
        }

        [TestMethod]
        public void T2_Vider()
        {
            var tirelire = new Tirelire3();
            AreEqual(0, tirelire.MontantTotal);

            AreEqual(0, Opérations3.Vider(tirelire));
            IsTrue(Opérations3.Déposer(tirelire, 100));
            AreEqual(100, Opérations3.Vider(tirelire));
            AreEqual(0, tirelire.MontantTotal);
            IsTrue(Opérations3.Déposer(tirelire, 500));
            AreEqual(500, Opérations3.Vider(tirelire));
            AreEqual(0, tirelire.MontantTotal);
        }

        [TestMethod]
        public void T3_Retirer()
        {
            var tirelire = new Tirelire3();
            AreEqual(0, tirelire.MontantTotal);

            IsFalse(Opérations3.Retirer(tirelire, 0));
            IsFalse(Opérations3.Retirer(tirelire, -10));
            IsFalse(Opérations3.Retirer(tirelire, 1));

            IsTrue(Opérations3.Déposer(tirelire, 1000));
            IsTrue(Opérations3.Retirer(tirelire, 50));
            IsTrue(Opérations3.Retirer(tirelire, 100));

            IsFalse(Opérations3.Retirer(tirelire, 0));
            IsFalse(Opérations3.Retirer(tirelire, -10));
            IsFalse(Opérations3.Retirer(tirelire, 851));

            AreEqual(850, tirelire.MontantTotal);
        }

    }
}
