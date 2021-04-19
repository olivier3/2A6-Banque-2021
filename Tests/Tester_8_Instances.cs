using Microsoft.VisualStudio.TestTools.UnitTesting;
using static Microsoft.VisualStudio.TestTools.UnitTesting.Assert;
using System.Reflection;
using TPConsole;
using System.Linq;

namespace Tests
{
    [TestClass, TestCategory("OB")]
    public class Tester_8_Instances
    {


        [TestMethod]
        public void T1_Singleton_NonStatique()
        {
            IsTrue(Instances.MesInstances.Tirelire6a.Déposer(100));
            AreEqual(100m, Instances.MesInstances.Tirelire6a.MontantTotal);
            Instances.MesInstances = new Instances();
            AreEqual(0m, Instances.MesInstances.Tirelire6a.MontantTotal);
        }

        [TestMethod]
        public void T2_GetInit()
        {
            // Toutes les propriétés sont get-init
            // donc 0 qui ne le sont pas...
            AreEqual(0, typeof(Instances).GetFields(BindingFlags.Instance | BindingFlags.NonPublic)
                .Count(field => !field.IsInitOnly));
        }



    }
}
