
using BanqueLib;

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
        public Tirelire9 Tirelire9a { get; init; } = new Tirelire9();
        public Compte1 Compte1 { get; init; } = new Compte1(1, "Olivier Bilodeau");
        public Compte2 Compte2 { get; init; } = new Compte2(2, "Olivier Bilodeau");
        public Compte3 Compte3 { get; init; } = new Compte3(3, "Olivier Bilodeau");
        public static Instances MesInstances { get; set; } = new Instances();
    }
}
