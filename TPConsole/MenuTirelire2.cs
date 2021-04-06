using System;
using System.Collections.Generic;
using System.Diagnostics;

using cstjean.info.fg.consoleplus;

using TireLireLib;

namespace TPConsole
{
    public static class MenuTirelire2
    {
        public static void Afficher()
        {

            do
            {
                ConsolePlus.Clear();
                AfficherEntête();
            } while (TraiterMenuEtContinuer());
        }


        /// <summary>
        /// Affiche l'entête de la Tirelire
        /// </summary>
        private static void AfficherEntête()
        {
            ConsolePlus.WriteLine();
            ConsolePlus.WriteLine(ConsoleColor.Magenta, "OB - Tirelire 2");
            ConsolePlus.WriteLine(ConsoleColor.Magenta, "===============");
            ConsolePlus.WriteLine();
            ConsolePlus.Afficher("Historique", "");
            ConsolePlus.WriteLine(Historique.ConstructionHistorique());
            ConsolePlus.WriteLine();
            ConsolePlus.Afficher("Actif", $"{Tirelire2.MontantTotal:C}");
            ConsolePlus.WriteLine();
        }

        /// <summary>
        /// Actionne une fonction de la tirelire selon le choix de l'utilisateur
        /// </summary>
        /// <returns>Détermine si on quitte ou non le programme</returns>
        private static bool TraiterMenuEtContinuer()
        {
            if (ConsolePlus.LireChoix(out string? choix, '0',
                    "Quitter", "Déposer", "Retirer", "Vider"))
            {
                ConsolePlus.WriteLine();
                switch (choix)
                {
                    case "Déposer":
                        Déposer();
                        break;

                    case "Retirer":
                        Retirer();
                        break;

                    case "Vider":
                        Vider();
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
        private static void Déposer()
        {
            if (ConsolePlus.LireDécimal("Montant", out decimal montant))
            {
                if (Tirelire2.Déposer(montant))
                {
                    ConsolePlus.MessageOkBloquant("Dépôt réussi");
                    Historique.Suivi().Add($"> Déposer {montant}");
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
        private static void Retirer()
        {
            if (ConsolePlus.LireDécimal("Montant", out decimal montant))
            {
                if (Tirelire2.Retirer(montant))
                {
                    ConsolePlus.MessageOkBloquant("Retrait réussi");
                    Historique.Suivi().Add($"> Retirer {montant}");
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
        private static void Vider()
        {
            decimal montant = Tirelire2.Vider();
            ConsolePlus.MessageOkBloquant($"Vous avez vidé la tirelire. Montant récupéré: {montant:C}");
            Historique.Suivi().Add($"> Vider {montant}");
        }
    }
}
