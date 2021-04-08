
using cstjean.info.fg.consoleplus;

using TireLireLib;

namespace TPConsole
{
    public static class MenuTirelire1
    {
        public static void Afficher()
        {
            var continuer = true;
            while (continuer)
            {
                ConsolePlus.Clear();
                MenuUtil.AfficherEntête(Tirelire1.MontantTotal, "1");
                continuer = MenuUtil.TraiterMenuEtContinuer(
                    //Déposer
                    () => MenuUtil.Déposer(montant => Opérations1.Déposer(montant)),
                    //Retirer
                    () => MenuUtil.Retirer(montant => Opérations1.Retirer(montant)),
                    //Vider
                    () => MenuUtil.Vider(() => Opérations1.Vider()));
            }
        }
    }
}
