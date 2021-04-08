using System;
using System.Diagnostics;

using cstjean.info.fg.consoleplus;

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
            ConsolePlus.Afficher("Tirelire 3a", $"{Instances.Tirelire3a.MontantTotal:C}");
            ConsolePlus.Afficher("Tirelire 3a", $"{Instances.Tirelire3b.MontantTotal:C}");
            ConsolePlus.WriteLine();
        }

        private static bool TraiterMenuEtContinuer()
        {
            if (ConsolePlus.LireChoix(out string? choix, 'A',
                    "Quitter", "Reset", "Tirelire 1", "Tirelire 2", "Tirelire 3a", "Tirelire 3b"))
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
                        MenuTirelire3(Instances.Tirelire3a, "3a");
                        break;
                    case "Tirelire 3b":
                        Historique.Suivi().Add("\n    >> Tirelire 3b ");
                        MenuTirelire3(Instances.Tirelire3b, "3b");
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
                Instances.Tirelire3a.MontantTotal = 0;
                Instances.Tirelire3b.MontantTotal = 0;
                Historique.Suivi().Clear();
            }
            else
            {
                Afficher();
            }
        }
        public static void MenuTirelire1()
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
        public static void MenuTirelire2()
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
        public static void MenuTirelire3(Tirelire3 tirelire, string noTirelire)
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
    }
}
