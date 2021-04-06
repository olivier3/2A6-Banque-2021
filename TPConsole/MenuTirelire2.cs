﻿using System;
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
                MenuUtil.AfficherEntête(Tirelire2.MontantTotal, 2);
            } while (MenuUtil.TraiterMenuEtContinuer(Déposer, Retirer, Vider));
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
