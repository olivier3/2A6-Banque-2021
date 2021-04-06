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
            ConsolePlus.WriteLine();
        }

        private static bool TraiterMenuEtContinuer()
        {
            if (ConsolePlus.LireChoix(out string? choix, 'A',
                    "Quitter", "Reset", "Tirelire 1", "Tirelire 2"))
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
                        MenuTirelire1.Afficher();
                        break;
                    case "Tirelire 2":
                        Historique.Suivi().Add("\n    >> Tirelire 2 ");
                        MenuTirelire2.Afficher();
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
                Historique.Suivi().Clear();
            }
            else
            {
                Afficher();
            }
        }
    }
}
