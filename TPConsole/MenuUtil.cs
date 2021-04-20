﻿using System;
using System.Diagnostics;

using cstjean.info.fg.consoleplus;

using TireLireLib;

namespace TPConsole
{
    public static class MenuUtil
    {
        public static void AfficherEntête(decimal montantTirelire, string noTirelire)
        {
            ConsolePlus.WriteLine();
            ConsolePlus.WriteLine(ConsoleColor.Magenta, $"OB - Tirelire {noTirelire}");
            ConsolePlus.WriteLine(ConsoleColor.Magenta, "===============");
            ConsolePlus.WriteLine();
            ConsolePlus.Afficher("Historique", "");
            ConsolePlus.WriteLine(Historique.ConstructionHistorique());
            ConsolePlus.WriteLine();
            ConsolePlus.Afficher("Actif", $"{montantTirelire:C}");
            ConsolePlus.WriteLine();
        }

        public static bool TraiterMenuEtContinuer(Action déposer, Action retirer, Action vider)
        {
            if (ConsolePlus.LireChoix(out string? choix, '0',
                    "Quitter", "Déposer", "Retirer", "Vider"))
            {
                ConsolePlus.WriteLine();
                switch (choix)
                {
                    case "Déposer":
                        déposer();
                        break;

                    case "Retirer":
                        retirer();
                        break;

                    case "Vider":
                        vider();
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
        public static void Déposer(Func<decimal, bool> déposerDsTirelire)
        {
            if (ConsolePlus.LireDécimal("Montant", out decimal montant))
            {
                if (déposerDsTirelire(montant))
                {
                    ConsolePlus.MessageOkBloquant("Dépôt réussi");
                    Historique.Suivi().Add($"> Déposer {montant} ");
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
        public static void Déposer(Action<decimal> déposerDsTirelire)
        {
            if (ConsolePlus.LireDécimal("Montant", out decimal montant))
            {
                try
                {
                    déposerDsTirelire(montant);
                    ConsolePlus.MessageOkBloquant("Dépôt réussi");
                    Historique.Suivi().Add($"> Déposer {montant} ");
                }
                catch (ArgumentOutOfRangeException)
                {
                    ConsolePlus.MessageErreurBloquant($"Impossible de déposer {montant:C}.\n" +
                        "Le montant doit être positif.");
                }
                catch (ArgumentException)
                {
                    ConsolePlus.MessageErreurBloquant($"Impossible de déposer des fractions de cents.\n" +
                           "Le montant est trop précis.");
                }
            }
            else
            {
                ConsolePlus.Poursuivre();
            }
        }
        public static void Retirer(Func<decimal, bool> retirerDsTirelire)
        {
            if (ConsolePlus.LireDécimal("Montant", out decimal montant))
            {
                if (retirerDsTirelire(montant))
                {
                    ConsolePlus.MessageOkBloquant("Retrait réussi");
                    Historique.Suivi().Add($"> Retirer {montant} ");
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
        public static void Retirer(Action<decimal> retirerDsTirelire)
        {
            if (ConsolePlus.LireDécimal("Montant", out decimal montant))
            {
                try
                {
                    retirerDsTirelire(montant);
                    ConsolePlus.MessageOkBloquant("Retrait réussi");
                    Historique.Suivi().Add($"> Retirer {montant} ");
                }
                catch (ArgumentOutOfRangeException)
                {
                    if (montant < 0)
                    {
                        ConsolePlus.MessageErreurBloquant($"Impossible de retirer {montant:C}.\n" +
                            "Le montant doit être positif.");
                    }
                    else
                    {
                        ConsolePlus.MessageErreurBloquant($"Impossible de retirer {montant:C}.\n" +
                           "Actif insuffisant.");
                    }
                }
                catch (ArgumentException)
                {
                    ConsolePlus.MessageErreurBloquant($"Impossible de retirer des fractions de cents.\n" +
                           "Le montant est trop précis.");
                }
            }
            else
            {
                ConsolePlus.Poursuivre();
            }
        }
        public static void Vider(Func<decimal> viderLaTirelire)
        {
            decimal montant = viderLaTirelire();
            ConsolePlus.MessageOkBloquant($"Vous avez vidé la tirelire. Montant récupéré: {montant:C}");
            Historique.Suivi().Add($"> Vider {montant} ");
        }
    }
}
