using Microsoft.VisualStudio.TestTools.UnitTesting;
using static Microsoft.VisualStudio.TestTools.UnitTesting.Assert;
using TireLireLib;

namespace Tests
{
    [TestClass, TestCategory("OB")]
    public class Tester_Actif
    {


        [TestMethod]
        public void T1_ActifDeBase()
        {
            IsTrue(typeof(ActifDeBase).IsAbstract);
            var tirelire = new Tirelire9();
            tirelire.Déposer(100);
            AreEqual(100m, ((ActifDeBase)(object)tirelire).MontantTotal);
        }

        [TestMethod]
        public void T2_ActifVidable()
        {
            IsTrue(typeof(ActifVidable).IsAbstract);
            var tirelire = new Tirelire9();
            tirelire.Déposer(100);
            AreEqual(100m, ((ActifVidable)(object)tirelire).Vider());
            AreEqual(0m, ((ActifVidable)(object)tirelire).MontantTotal);
        }

        [TestMethod]
        public void T3_Héritage_Tirelire5()
        {
            IsTrue(new Tirelire5() is ActifDeBase);
        }

        [TestMethod]
        public void T4_Héritage_Tirelire6()
        {
            IsTrue(new Tirelire6() is ActifDeBase);
            IsTrue(new Tirelire6() is ActifVidable);
        }

        [TestMethod]
        public void T5_Héritage_Tirelire9()
        {
            IsTrue(new Tirelire9() is ActifDeBase);
            IsTrue(new Tirelire9() is ActifVidable);
        }

    }
}
