
using TireLireLib;

using TirelirePlusLib;

namespace TPConsole
{
    public class Instances
    {
        public Tirelire3 Tirelire3a { get; set; } = new Tirelire3();
        public Tirelire3 Tirelire3b { get; set; } = new Tirelire3();
        public Tirelire4 Tirelire4a { get; set; } = new Tirelire4();
        public Tirelire5 Tirelire5a { get; set; } = new Tirelire5();
        public Tirelire6 Tirelire6a { get; set; } = new Tirelire6();
        public Tirelire6Plus Tirelire6p { get; set; } = new Tirelire6Plus();
        public static Instances MesInstances { get; set; } = new Instances();
    }
}
