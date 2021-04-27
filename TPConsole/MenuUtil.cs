using System;
using System.Diagnostics;

using BanqueLib;

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
        public static void AfficherEntêteCompte(int numéro, string titulaire, decimal montantTirelire)
        {
            ConsolePlus.WriteLine();
            ConsolePlus.WriteLine(ConsoleColor.Magenta, $"OB - Mon compte {numéro}");
            ConsolePlus.WriteLine(ConsoleColor.Magenta, "===============");
            ConsolePlus.WriteLine();
            ConsolePlus.Afficher("Historique", "");
            ConsolePlus.WriteLine(Historique.ConstructionHistorique());
            ConsolePlus.WriteLine();
            ConsolePlus.Afficher("Titulaire", $"{titulaire}");
            ConsolePlus.Afficher("Solde", $"{montantTirelire:C}");
            ConsolePlus.WriteLine();
        }

        public static void AfficherEntêteCompte2(int numéro, string titulaire, decimal montantTirelire, ÉtatDuCompte état)
        {
            ConsolePlus.WriteLine();
            ConsolePlus.WriteLine(ConsoleColor.Magenta, $"OB - Mon compte {numéro}");
            ConsolePlus.WriteLine(ConsoleColor.Magenta, "===============");
            ConsolePlus.WriteLine();
            ConsolePlus.Afficher("Historique", "");
            ConsolePlus.WriteLine(Historique.ConstructionHistorique());
            ConsolePlus.WriteLine();
            ConsolePlus.Afficher("Titulaire", $"{titulaire}");
            ConsolePlus.Afficher("Solde", $"{montantTirelire:C}");
            ConsolePlus.Afficher("État", $"{état}");
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
        public static bool TraiterMenuEtContinuerCompte2(Action déposer, Action retirer, Action vider, Action fermer, Action réactiver)
        {
            if (ConsolePlus.LireChoix(out string? choix, '0',
                    "Quitter", "Déposer", "Retirer", "Vider", "Fermer", "Réactiver"))
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

                    case "Fermer":
                        fermer();
                        break;

                    case "Réactiver":
                        réactiver();
                        break;

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
                try
                {
                    _ = déposerDsTirelire(montant);
                    ConsolePlus.MessageOkBloquant("Dépot réussi");
                    Historique.Suivi().Add($"> Déposer {montant} ");
                }
                catch (InvalidOperationException ex)
                {
                    ConsolePlus.MessageErreurBloquant(ex.Message);
                }
                catch (ArgumentOutOfRangeException ex)
                {
                    ConsolePlus.MessageErreurBloquant(ex.Message);
                }
                catch (ArgumentException ex)
                {
                    ConsolePlus.MessageErreurBloquant(ex.Message);
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
                try
                {
                    _ = retirerDsTirelire(montant);
                    ConsolePlus.MessageOkBloquant("Retrait réussi");
                    Historique.Suivi().Add($"> Retirer {montant} ");
                }
                catch (InvalidOperationException ex)
                {
                    ConsolePlus.MessageErreurBloquant(ex.Message);
                }
                catch (ArgumentOutOfRangeException ex)
                {
                    ConsolePlus.MessageErreurBloquant(ex.Message);
                }
                catch (ArgumentException ex)
                {
                    ConsolePlus.MessageErreurBloquant(ex.Message);
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
            try
            {
                decimal montant = viderLaTirelire();
                ConsolePlus.MessageOkBloquant($"Vous avez vidé la tirelire. Montant récupéré: {montant:C}");
                Historique.Suivi().Add($"> Vider {montant} ");
            }
            catch (InvalidOperationException ex)

            {
                ConsolePlus.MessageErreurBloquant(ex.Message);
            }
        }

        public static void Réactiver(Action réactiver)
        {
            try
            {
                réactiver();
            }
            catch (InvalidOperationException ex)
            {
                ConsolePlus.MessageErreurBloquant(ex.Message);
            }
            ConsolePlus.MessageOkBloquant("Le compte a été réactivé.");
        }



        public static void Fermer(Action fermer, decimal montant)
        {
            try
            {
                fermer();
                ConsolePlus.MessageOkBloquant($"Votre compte a été fermé. Montant récupéré: {montant:C}.");
            }
            catch (InvalidOperationException ex)
            {
                ConsolePlus.MessageErreurBloquant(ex.Message);
            }
        }
    }
}
