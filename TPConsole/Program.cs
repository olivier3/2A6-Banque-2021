using System;

using cstjean.info.fg.consoleplus;

namespace TPConsole
{
    public static class Program
    {
        public static void Main()
        {
            Console.Title = "OB - TP Banque";
            ConsolePlus.IndentationGénérale = 1;
            Persistance.Charger();
            MenuTirelire1.Afficher();
            Persistance.Sauvegarder();
        }
    }
}
