
using cstjean.info.fg.consoleplus;

using TireLireLib;

namespace TPConsole
{
    public static class MenuTirelire2
    {
        public static void Afficher()
        {
            var continuer = true;
            while (continuer)
            {
                ConsolePlus.Clear();
                MenuUtil.AfficherEntête(Tirelire2.MontantTotal, "2");
                continuer = MenuUtil.TraiterMenuEtContinuer(
                    //Déposer
                    () => MenuUtil.Déposer(montant => Tirelire2.Déposer(montant)),
                    //Retirer
                    () => MenuUtil.Retirer(montant => Tirelire2.Retirer(montant)),
                    //Vider
                    () => MenuUtil.Vider(() => Tirelire2.Vider()));
            }
        }
    }
}
