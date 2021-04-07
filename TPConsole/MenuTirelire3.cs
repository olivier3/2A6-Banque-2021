using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using cstjean.info.fg.consoleplus;

using TireLireLib;

namespace TPConsole
{
    public static class MenuTirelire3
    {
        private static Tirelire3 tirelireActive = null!;
        public static void Afficher(Tirelire3 tirelire, string noTirelire)
        {
            tirelireActive = tirelire;
            do
            {
                ConsolePlus.Clear();
                MenuUtil.AfficherEntête(tirelireActive.MontantTotal, noTirelire);
            } while (MenuUtil.TraiterMenuEtContinuer(Déposer, Retirer, Vider));
        }

        /// <summary>
        /// Permet de déposer un montant dans la tirelire
        /// </summary>
        private static void Déposer()
        {
            if (ConsolePlus.LireDécimal("Montant", out decimal montant))
            {
                if (Opérations3.Déposer(tirelireActive, montant))
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
                if (Opérations3.Retirer(tirelireActive, montant))
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
            decimal montant = Opérations3.Vider(tirelireActive);
            ConsolePlus.MessageOkBloquant($"Vous avez vidé la tirelire. Montant récupéré: {montant:C}");
            Historique.Suivi().Add($"> Vider {montant}");
        }
    }
}
