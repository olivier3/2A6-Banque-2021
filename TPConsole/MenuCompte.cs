using System;
using System.Diagnostics;
using cstjean.info.fg.consoleplus;

using BanqueLib;

using TireLireLib;

namespace TPConsole
{
    public static class MenuCompte
    {
        public static void Afficher(Compte3 compte, Banque banque)
        {
            do
            {
                ConsolePlus.Clear();
                AfficherEntête(compte, banque);
            } while (TraiterMenuEtContinuer(compte, banque));
        }

        private static void AfficherEntête(Compte3 compte, Banque p_banque)
        {
            ConsolePlus.WriteLine();
            ConsolePlus.WriteLine(ConsoleColor.Magenta, $"OB - Banque {p_banque.Nom} : Compte #{compte.Numéro}");
            ConsolePlus.WriteLine(ConsoleColor.Magenta, "===============");
            ConsolePlus.WriteLine();
            ConsolePlus.Afficher("Historique", "");
            ConsolePlus.WriteLine(Historique.ConstructionHistorique());
            ConsolePlus.WriteLine();
            ConsolePlus.Afficher("Titulaire", $"{compte.Titulaire}");
            ConsolePlus.Afficher("Solde", $"{compte.MontantTotal:C}");
            ConsolePlus.Afficher("État", $"{compte.État}");
            ConsolePlus.WriteLine();
        }

        private static bool TraiterMenuEtContinuer(Compte3 p_compte, Banque p_banque)
        {
            if (ConsolePlus.LireChoix(out string? choix, '0',
                    "Quitter", "Déposer", "Retirer", "Vider", "Fermer", "Réactiver", "Geler", "Verser 1%", "Détruire"))
            {
                ConsolePlus.WriteLine();
                switch (choix)
                {
                    case "Déposer":
                        MenuUtil.Déposer(p_compte.Déposer);
                        break;

                    case "Retirer":
                        MenuUtil.Retirer(p_compte.Retirer);
                        break;

                    case "Vider":
                        MenuUtil.Vider(p_compte.Vider);
                        break;

                    case "Quitter":
                        return false;

                    case "Fermer":
                        Fermer(p_compte);
                        break;

                    case "Réactiver":
                        MenuUtil.Réactiver(p_compte.Réactiver);
                        break;

                    case "Geler":
                        MenuUtil.Geler(p_compte.Geler);
                        break;

                    case "Verser 1%":
                        MenuUtil.Verser(p_compte.VerserIntérêts);
                        break;
                    case "Détruire":
                        try
                        {
                            _ = ConsolePlus.LireBooléen("Voulez-vous vraiment (o/n)", out bool validation);
                            if (validation)
                            {
                                _ = p_banque.DétruireCompte(p_compte.Numéro);
                                ConsolePlus.MessageOkBloquant($"Le compte #{p_compte.Numéro} a été détruit.");
                                MenuBanque.Afficher(p_banque, p_banque.Nom);
                            }
                            Afficher(p_compte, p_banque);
                        }
                        catch (ArgumentException ex)
                        {
                            ConsolePlus.MessageErreurBloquant(ex.Message);
                        }
                        catch (InvalidOperationException)
                        {
                            ConsolePlus.MessageErreurBloquant("Impossible de détruire un compte qui n'est pas fermé");
                        }
                        break;

                    default:
                        Debug.Fail($"Cas non traité: {choix}");
                        break;
                }
            }
            return true;
        }

        public static void Fermer(Compte3 compte)
        {
            try
            {
                decimal montantTotal = compte.Fermer();
                ConsolePlus.MessageOkBloquant($"Le compte a été fermé. Montant récupéré: {montantTotal:C}");
            }
            catch (InvalidOperationException ex)
            {
                ConsolePlus.MessageErreurBloquant(ex.Message);
            }
        }
    }
}
