using System;
using System.Collections.Generic;

using BanqueLib;

using cstjean.info.fg.consoleplus;

using TireLireLib;

namespace TPConsole
{
    public static class MenuBanque
    {
        public static void Afficher(Banque banque)
        {
            do
            {
                ConsolePlus.Clear();
                AfficherEntête(banque);
            } while (TraiterMenuEtContinuer(banque));
        }

        private static void AfficherEntête(Banque p_banque)
        {
            ConsolePlus.WriteLine();
            ConsolePlus.WriteLine(ConsoleColor.Magenta, "OB - Banque OB");
            ConsolePlus.WriteLine(ConsoleColor.Magenta, "===============");
            ConsolePlus.WriteLine();
            ConsolePlus.Afficher("Historique", "");
            ConsolePlus.WriteLine(Historique.ConstructionHistorique());
            ConsolePlus.WriteLine();
            ConsolePlus.Afficher("Actif total", $"{p_banque.ActifTotal:C}");
            ConsolePlus.Afficher("Actif gelé", $"{p_banque.ActifGelé:C}", couleurValeur: (ConsoleColor.Red, null));
            ConsolePlus.Afficher("Nb Comptes", $"{p_banque.NbComptes}       = {p_banque.NbActifs} actifs + {p_banque.NbGelés} gelés + {p_banque.NbFermés} fermés");
            ConsolePlus.Afficher("Prochain No", p_banque.ProchainNuméroDeCompte, couleurValeur: (ConsoleColor.Red, null));
            ConsolePlus.WriteLine();
        }

        private static string[] Options(Banque p_banque)
        {
            List<string> menu = new() { "Quitter", "Ouvrir Compte", "Verser 2%" };
            foreach (Compte3 compte in p_banque.Comptes)
            {
                menu.Add($"{compte.Numéro,-10}{compte.Titulaire,-20}{compte.État,-10}{compte.MontantTotal,10:C}");
            }
            string[] menuFinal = menu.ToArray();
            return menuFinal;
        }

        private static bool TraiterMenuEtContinuer(Banque p_banque)
        {
            if (ConsolePlus.LireChoix(out string? choix, 'A', Options(p_banque)))
            {
                ConsolePlus.WriteLine();
                switch (choix)
                {
                    case "Quitter":
                        return false;
                    case "Ouvrir Compte":
                        try
                        {
                            OuvertureCompte(p_banque, "OB");
                        }
                        catch (InvalidOperationException ex)
                        {
                            ConsolePlus.MessageErreurBloquant(ex.Message);
                        }
                        catch (ArgumentException ex)
                        {
                            ConsolePlus.MessageErreurBloquant(ex.Message);
                        }
                        break;
                    case "Verser 2%":
                        Verser2Pourcent();
                        break;
                    default:
                        foreach (Compte3 compte in p_banque.Comptes)
                        {
                            if (choix == $"{compte.Numéro,-10}{compte.Titulaire,-20}{compte.État,-10}{compte.MontantTotal,10:C}")
                            {
                                MenuGénéral.MenuCompte3(compte, compte.Numéro);
                            }
                        }
                        break;
                }
            }
            return true;
        }

        private static void OuvertureCompte(Banque p_banque, string p_nomBanque)
        {
            string nom = ConsolePlus.LireTexte("Titulaire", séparateur: ":");

            if (ConsolePlus.LireDécimal(" Mise de fond initiale", out decimal miseDeFond, défaut: "0", bloquant: true))
            {
                if (miseDeFond < 0)
                {
                    throw new InvalidOperationException("Échec: La mise de fond est négative.");
                }
                if (Math.Round(miseDeFond, 2) != miseDeFond)
                {
                    throw new ArgumentException("Échec: Les fractions de cent ne sont pas admissibles.");
                }
                Compte3 compte = p_banque.OuvrirCompte(nom, miseDeFond);
                ConsolePlus.MessageOkBloquant($"Nouveau compte #{compte.Numéro} avec mise de fond {miseDeFond:C}");
                Historique.Suivi().Add($"\n    >> [{p_nomBanque}] Ouvrir #{compte.Numéro} avec {miseDeFond:C}");
            }
        }

        private static void Verser2Pourcent()
        {

        }
    }
}
