
using TireLireLib;

using TirelirePlusLib;

namespace TPConsole
{
    public class Instances
    {
        public Tirelire3 Tirelire3a { get; init; } = new Tirelire3();
        public Tirelire3 Tirelire3b { get; init; } = new Tirelire3();
        public Tirelire4 Tirelire4a { get; init; } = new Tirelire4();
        public Tirelire5 Tirelire5a { get; init; } = new Tirelire5();
        public Tirelire6 Tirelire6a { get; init; } = new Tirelire6();
        public Tirelire6Plus Tirelire6p { get; init; } = new Tirelire6Plus();
        public Tirelire7 Tirelire7a { get; init; } = new Tirelire7();
        public static Instances MesInstances { get; set; } = new Instances();
    }
}
