using Microsoft.VisualStudio.TestTools.UnitTesting;
using static Microsoft.VisualStudio.TestTools.UnitTesting.Assert;
using TireLireLib;
using System;

using static Tests.TestUtil;

namespace Tests
{
    [TestClass, TestCategory("OB")]
    public partial class Tester_Tirelire_9
    {
        [TestMethod]
        public void T0_Réflexion()
        {
            // La tirelire 9 est comparable à la tirelire 6
            AreEqual(
                typeof(Tirelire6).GetMembers().Length,
                typeof(Tirelire9).GetMembers().Length);
        }

        [TestMethod]
        public void T1_Déposer()
        {
            TesterDéposer(new Tirelire9(), false);
        }


        [TestMethod]
        public void T2_Vider()
        {
            TesterVider(new Tirelire9());
        }


        [TestMethod]
        public void T3_Retirer()
        {
            TesterRetirer(new Tirelire9(), false);
        }

        public static void TesterDéposer(Tirelire9 tirelire, bool avecPrécision = true)
        {
            AreEqual(0, tirelire.MontantTotal);

            tirelire.Déposer(100);
            tirelire.Déposer(50);

            AreEqual(150m, tirelire.MontantTotal);

            ArgumentOutOfRange(() => tirelire.Déposer(0), "trop petit", "montant", 0m);
            ArgumentOutOfRange(() => tirelire.Déposer(-10), "trop petit", "montant", -10m);

            AreEqual(150m, tirelire.MontantTotal);

            if (avecPrécision)
            {
                tirelire.Déposer(72.22m);
                ArgumentInvalide(() => tirelire.Déposer(12.345m), "trop précis", "montant");
                AreEqual(222.22m, tirelire.MontantTotal);
            }
        }

        public static void TesterRetirer(Tirelire9 tirelire, bool avecPrécision = true)
        {
            tirelire.Déposer(500);
            AreEqual(500m, tirelire.MontantTotal);

            ArgumentOutOfRange(() => tirelire.Retirer(0), "trop petit", "montant", 0m);
            ArgumentOutOfRange(() => tirelire.Retirer(-10), "trop petit", "montant", -10m);
            ArgumentOutOfRange(() => tirelire.Retirer(501), "trop grand", "montant", 501m);

            AreEqual(500m, tirelire.MontantTotal);

            tirelire.Retirer(50);
            AreEqual(450m, tirelire.MontantTotal);

            tirelire.Retirer(50);
            AreEqual(400m, tirelire.MontantTotal);

            tirelire.Retirer(400);
            AreEqual(0m, tirelire.MontantTotal);

            ArgumentOutOfRange(() => tirelire.Retirer(1), "trop grand", "montant", 1m);

            if (avecPrécision)
            {
                tirelire.Déposer(222.22m);
                ArgumentInvalide(() => tirelire.Retirer(12.345m), "trop précis", "montant");
                AreEqual(222.22m, tirelire.MontantTotal);
            }
        }

        public static void TesterVider(Tirelire9 tirelire)
        {
            AreEqual(0, tirelire.MontantTotal);
            AreEqual(0, tirelire.Vider());

            tirelire.Déposer(100);
            AreEqual(100m, tirelire.Vider());
            AreEqual(0, tirelire.MontantTotal);

            tirelire.Déposer(500);
            AreEqual(500m, tirelire.Vider());
            AreEqual(0, tirelire.MontantTotal);
        }

    }

    public static partial class TestUtil
    {
        public static void ArgumentOutOfRange(Action action, string message, string paramName, object actualValue)
        {
            var ex = ThrowsException<ArgumentOutOfRangeException>(() => action());
            StringAssert.Contains(ex.Message, message);
            AreEqual(paramName, ex.ParamName);
            AreEqual(actualValue, ex.ActualValue);
        }

        public static void ArgumentInvalide(Action action, string message, string paramName)
        {
            var ex = ThrowsException<ArgumentException>(() => action());
            StringAssert.Contains(ex.Message, message);
            AreEqual(paramName, ex.ParamName);
        }
    }
}
