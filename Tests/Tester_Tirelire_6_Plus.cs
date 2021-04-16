using Microsoft.VisualStudio.TestTools.UnitTesting;
using static Microsoft.VisualStudio.TestTools.UnitTesting.Assert;
using TireLireLib;
using TirelirePlusLib;

namespace Tests
{
    [TestClass, TestCategory("OB")]
    public class Tester_Tirelire_6_Plus
    {

        [TestMethod]
        public void T0_Réflexion()
        {
            // La tirelire 6 plus compte un membre de plus que la tirelire 6. 
            AreEqual(
                typeof(Tirelire6).GetMembers().Length + 1,
                typeof(Tirelire6Plus).GetMembers().Length);
        }

        [TestMethod]
        public void T1_Init()
        {
            var tirelire = new Tirelire6Plus();
            AreEqual(0, tirelire.MontantTotal);

            IsTrue(tirelire.Init());
            AreEqual(0, tirelire.MontantTotal);

            IsFalse(tirelire.Init(-1));
            AreEqual(0, tirelire.MontantTotal);

            IsTrue(tirelire.Init(100));
            AreEqual(100, tirelire.MontantTotal);

            IsTrue(tirelire.Init(500));
            AreEqual(500, tirelire.MontantTotal);

            IsFalse(tirelire.Init(-1));
            AreEqual(500, tirelire.MontantTotal);

            IsTrue(tirelire.Init());
            AreEqual(0, tirelire.MontantTotal);
        }

    }
}
