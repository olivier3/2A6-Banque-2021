using System;
using System.Collections.Generic;
using System.Diagnostics;

using cstjean.info.fg.consoleplus;

using TireLireLib;

namespace TPConsole
{
    public static class MenuTirelire1
    {
        public static void Afficher()
        {
            List<string> suivi = new();
            do
            {
                ConsolePlus.Clear();
                AfficherEntête(suivi);
            } while (TraiterMenuEtContinuer(suivi));
        }


        /// <summary>
        /// Affiche l'entête de la Tirelire
        /// </summary>
        private static void AfficherEntête(List<string> p_suivi)
        {
            ConsolePlus.WriteLine();
            ConsolePlus.WriteLine(ConsoleColor.Magenta, "OB - Tirelire 1");
            ConsolePlus.WriteLine(ConsoleColor.Magenta, "===============");
            ConsolePlus.WriteLine();
            ConsolePlus.Afficher("Historique", Historique(p_suivi));
            ConsolePlus.WriteLine();
            ConsolePlus.Afficher("Actif", $"{Tirelire1.MontantTotal:C}");
            ConsolePlus.WriteLine();
        }

        /// <summary>
        /// Actionne une fonction de la tirelire selon le choix de l'utilisateur
        /// </summary>
        /// <returns>Détermine si on quitte ou non le programme</returns>
        private static bool TraiterMenuEtContinuer(List<string> p_suivi)
        {
            if (ConsolePlus.LireChoix(out string? choix, '0',
                    "Quitter", "Déposer", "Retirer", "Vider"))
            {
                ConsolePlus.WriteLine();
                switch (choix)
                {
                    case "Déposer":
                        Déposer(p_suivi);
                        break;

                    case "Retirer":
                        Retirer(p_suivi);
                        break;

                    case "Vider":
                        Vider(p_suivi);
                        break;

                    case "Quitter":
                        return false;

                    default:
                        Debug.Fail($"Cas non traité: {choix}");
                        break;
                }
            }
            return true;
        }

        /// <summary>
        /// Permet de déposer un montant dans la tirelire
        /// </summary>
        private static void Déposer(List<string> p_suivi)
        {
            if (ConsolePlus.LireDécimal("Montant", out decimal montant))
            {
                if (Opérations1.Déposer(montant))
                {
                    ConsolePlus.MessageOkBloquant("Dépôt réussi");
                    p_suivi.Add($"Déposer {montant}");
                }
                else
                {
                    ConsolePlus.MessageErreurBloquant("Échec du dépôt");
                }
            }
            else
            {
                ConsolePlus.Poursuivre();
            }
        }

        /// <summary>
        /// Permet de retirer un montant de la tirelire
        /// </summary>
        private static void Retirer(List<string> p_suivi)
        {
            if (ConsolePlus.LireDécimal("Montant", out decimal montant))
            {
                if (Opérations1.Retirer(montant))
                {
                    ConsolePlus.MessageOkBloquant("Retrait réussi");
                    p_suivi.Add($"Retirer {montant}");
                }
                else
                {
                    ConsolePlus.MessageErreurBloquant("Échec du retrait");
                }
            }
            else
            {
                ConsolePlus.Poursuivre();
            }
        }

        /// <summary>
        /// Permet de vider la tirelire
        /// </summary>
        private static void Vider(List<string> p_suivi)
        {
            decimal montant = Opérations1.Vider();
            ConsolePlus.MessageOkBloquant($"Vous avez vidé la tirelire. Montant récupéré: {montant:C}");
            p_suivi.Add($"Vider {montant}");
        }

        private static string Historique(List<string> p_suivi)
        {
            string historiqueComplet = "";
            foreach (string transaction in p_suivi)
            {
                historiqueComplet += $" > {transaction}";
            }
            return historiqueComplet;
        }
    }
}
