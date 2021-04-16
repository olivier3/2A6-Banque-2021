using Microsoft.VisualStudio.TestTools.UnitTesting;
using static Microsoft.VisualStudio.TestTools.UnitTesting.Assert;
using TireLireLib;
using TirelirePlusLib;

namespace Tests
{
    [TestClass, TestCategory("OB")]
    public class Tester_Tirelire_6_Ext
    {

        [TestMethod]
        public void T1_Reset()
        {
            var tirelire = new Tirelire6();
            AreEqual(0, tirelire.MontantTotal);

            IsTrue(tirelire.Reset());
            AreEqual(0, tirelire.MontantTotal);

            IsFalse(tirelire.Reset(-1));
            AreEqual(0, tirelire.MontantTotal);

            IsTrue(tirelire.Reset(100));
            AreEqual(100, tirelire.MontantTotal);

            IsTrue(tirelire.Reset(500));
            AreEqual(500, tirelire.MontantTotal);

            IsFalse(tirelire.Reset(-1));
            AreEqual(500, tirelire.MontantTotal);

            IsTrue(tirelire.Reset());
            AreEqual(0, tirelire.MontantTotal);
        }

    }
}
