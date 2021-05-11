
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
        public Banque Banque { get; init; } = new Banque("Olivier Bilodeau");
        public Banque BanqueJedi { get; init; } = new Banque("Jedi",
            new[]
            {
                new Compte3(1, "Olivier Bilodeau", 111 ,ÉtatDuCompte.Actif),
                new Compte3(2, "Luke", 0, ÉtatDuCompte.Fermé),
                new Compte3(4, "Leia", 444 ,ÉtatDuCompte.Actif),
                new Compte3(7, "Obiwan", 777 ,ÉtatDuCompte.Actif),
                new Compte3(9, "Yoda", 999 ,ÉtatDuCompte.Gelé),
                new Compte3(12, "Anakin", 0 ,ÉtatDuCompte.Fermé),
                new Compte3(15, "Solo", 150 ,ÉtatDuCompte.Gelé),
                new Compte3(16, "Chewy", 2150 ,ÉtatDuCompte.Actif),
                new Compte3(19, "Sidious", 777 ,ÉtatDuCompte.Gelé),
            }
        );


        public static Instances MesInstances { get; set; } = new Instances();
    }
}
