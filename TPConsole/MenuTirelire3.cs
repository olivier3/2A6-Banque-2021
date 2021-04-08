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
        public static void Afficher(Tirelire3 tirelire, string noTirelire)
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
