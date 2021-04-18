using System;
using System.Diagnostics;

using cstjean.info.fg.consoleplus;
using static TPConsole.Instances;
using TireLireLib;

namespace TPConsole
{
    public static class MenuGénéral
    {
        public static void Afficher()
        {
            do
            {
                ConsolePlus.Clear();
                AfficherEntête();
            } while (TraiterMenuEtContinuer());
        }

        private static void AfficherEntête()
        {
            ConsolePlus.WriteLine();
            ConsolePlus.WriteLine(ConsoleColor.Magenta, "OB - Menu Général");
            ConsolePlus.WriteLine(ConsoleColor.Magenta, "===============");
            ConsolePlus.WriteLine();
            ConsolePlus.Afficher("Historique", "");
            ConsolePlus.WriteLine(Historique.ConstructionHistorique());
            ConsolePlus.WriteLine();
            ConsolePlus.Afficher("Tirelire 1", $"{Tirelire1.MontantTotal:C}");
            ConsolePlus.Afficher("Tirelire 2", $"{Tirelire2.MontantTotal:C}");
            ConsolePlus.Afficher("Tirelire 3a", $"{MesInstances.Tirelire3a.MontantTotal:C}");
            ConsolePlus.Afficher("Tirelire 3a", $"{MesInstances.Tirelire3b.MontantTotal:C}");
            ConsolePlus.Afficher("Tirelire 4a", $"{MesInstances.Tirelire4a.MontantTotal:C}");
            ConsolePlus.Afficher("Tirelire 5a", $"{MesInstances.Tirelire5a.MontantTotal:C}");
            ConsolePlus.Afficher("Tirelire 6a", $"{MesInstances.Tirelire6a.MontantTotal:C}");
            ConsolePlus.Afficher("Tirelire 6p", $"{MesInstances.Tirelire6p.MontantTotal:C}");
            ConsolePlus.WriteLine();
        }

        private static bool TraiterMenuEtContinuer()
        {
            if (ConsolePlus.LireChoix(out string? choix, 'A',
                    "Quitter", "Reset", "Tirelire 1", "Tirelire 2", "Tirelire 3a", "Tirelire 3b", "Tirelire 4a",
                    "Tirelire 5a", "Tirelire 6a", "Tirelire 6p"))
            {
                ConsolePlus.WriteLine();
                switch (choix)
                {
                    case "Quitter":
                        return false;

                    case "Reset":
                        Reset();
                        break;
                    case "Tirelire 1":
                        //Mettre add \n
                        Historique.Suivi().Add("\n    >> Tirelire 1 ");
                        MenuTirelire1();
                        break;
                    case "Tirelire 2":
                        Historique.Suivi().Add("\n    >> Tirelire 2 ");
                        MenuTirelire2();
                        break;
                    case "Tirelire 3a":
                        Historique.Suivi().Add("\n    >> Tirelire 3a ");
                        MenuTirelire3(MesInstances.Tirelire3a, "3a");
                        break;
                    case "Tirelire 3b":
                        Historique.Suivi().Add("\n    >> Tirelire 3b ");
                        MenuTirelire3(MesInstances.Tirelire3b, "3b");
                        break;
                    case "Tirelire 4a":
                        Historique.Suivi().Add("\n    >> Tirelire 4a ");
                        MenuTirelire4(MesInstances.Tirelire4a, "4a");
                        break;
                    case "Tirelire 5a":
                        Historique.Suivi().Add("\n    >> Tirelire 5a ");
                        MenuTirelire5(MesInstances.Tirelire5a, "5a");
                        break;
                    case "Tirelire 6a":
                        Historique.Suivi().Add("\n    >> Tirelire 6a ");
                        MenuTirelire6(MesInstances.Tirelire6a, "6a");
                        break;
                    case "Tirelire 6p":
                        Historique.Suivi().Add("\n    >> Tirelire 6p ");
                        MenuTirelire6(MesInstances.Tirelire6p, "6p");
                        break;
                    default:
                        Debug.Fail($"Cas non traité: {choix}");
                        break;
                }
            }
            return true;
        }
        private static void Reset()
        {
            _ = ConsolePlus.LireBooléen("Voulez-vous vraiment (o/n)", out bool choix);
            if (choix == true)
            {
                Tirelire1.MontantTotal = 0;
                _ = Tirelire2.Vider();
                MesInstances.Tirelire3a.MontantTotal = 0;
                MesInstances.Tirelire3b.MontantTotal = 0;
                MesInstances.Tirelire4a.MontantTotal = 0;
                _ = Tirelire5.Vider(MesInstances.Tirelire5a);
                _ = MesInstances.Tirelire6a.Vider();
                _ = MesInstances.Tirelire6p.Vider();
                Historique.Suivi().Clear();
            }
            else
            {
                Afficher();
            }
        }
        private static void MenuTirelire1()
        {
            var continuer = true;
            while (continuer)
            {
                ConsolePlus.Clear();
                MenuUtil.AfficherEntête(Tirelire1.MontantTotal, "1");
                continuer = MenuUtil.TraiterMenuEtContinuer(
                    //Déposer
                    () => MenuUtil.Déposer(montant => Opérations1.Déposer(montant)),
                    //Retirer
                    () => MenuUtil.Retirer(montant => Opérations1.Retirer(montant)),
                    //Vider
                    () => MenuUtil.Vider(() => Opérations1.Vider()));
            }
        }
        private static void MenuTirelire2()
        {
            var continuer = true;
            while (continuer)
            {
                ConsolePlus.Clear();
                MenuUtil.AfficherEntête(Tirelire2.MontantTotal, "2");
                continuer = MenuUtil.TraiterMenuEtContinuer(
                    //Déposer
                    () => MenuUtil.Déposer(montant => Tirelire2.Déposer(montant)),
                    //Retirer
                    () => MenuUtil.Retirer(montant => Tirelire2.Retirer(montant)),
                    //Vider
                    () => MenuUtil.Vider(() => Tirelire2.Vider()));
            }
        }
        private static void MenuTirelire3(Tirelire3 tirelire, string noTirelire)
        {
            var continuer = true;
            while (continuer)
            {
                ConsolePlus.Clear();
                MenuUtil.AfficherEntête(tirelire.MontantTotal, noTirelire);
                continuer = MenuUtil.TraiterMenuEtContinuer(
                    //Déposer
                    () => MenuUtil.Déposer(montant => Opérations3.Déposer(tirelire, montant)),
                    //Retirer
                    () => MenuUtil.Retirer(montant => Opérations3.Retirer(tirelire, montant)),
                    //Vider
                    () => MenuUtil.Vider(() => Opérations3.Vider(tirelire)));
            }
        }
        private static void MenuTirelire4(this Tirelire4 tirelire, string noTirelire)
        {
            var continuer = true;
            while (continuer)
            {
                ConsolePlus.Clear();
                MenuUtil.AfficherEntête(tirelire.MontantTotal, noTirelire);
                continuer = MenuUtil.TraiterMenuEtContinuer(
                    //Déposer
                    () => MenuUtil.Déposer(montant => tirelire.Déposer(montant)),
                    //Retirer
                    () => MenuUtil.Retirer(montant => tirelire.Retirer(montant)),
                    //Vider
                    () => MenuUtil.Vider(() => tirelire.Vider()));
            }
        }
        private static void MenuTirelire5(this Tirelire5 tirelire, string noTirelire)
        {
            var continuer = true;
            while (continuer)
            {
                ConsolePlus.Clear();
                MenuUtil.AfficherEntête(tirelire.MontantTotal, noTirelire);
                continuer = MenuUtil.TraiterMenuEtContinuer(
                    //Déposer
                    () => MenuUtil.Déposer(montant => Tirelire5.Déposer(tirelire, montant)),
                    //Retirer
                    () => MenuUtil.Retirer(montant => Tirelire5.Retirer(tirelire, montant)),
                    //Vider
                    () => MenuUtil.Vider(() => Tirelire5.Vider(tirelire)));
            }
        }
        private static void MenuTirelire6(this Tirelire6 tirelire, string noTirelire)
        {
            var continuer = true;
            while (continuer)
            {
                ConsolePlus.Clear();
                MenuUtil.AfficherEntête(tirelire.MontantTotal, noTirelire);
                continuer = MenuUtil.TraiterMenuEtContinuer(
                    //Déposer
                    () => MenuUtil.Déposer(montant => tirelire.Déposer(montant)),
                    //Retirer
                    () => MenuUtil.Retirer(montant => tirelire.Retirer(montant)),
                    //Vider
                    () => MenuUtil.Vider(() => tirelire.Vider()));
            }
        }
    }
}
