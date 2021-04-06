using System.Collections.Generic;

namespace TireLireLib
{
    public static class Historique
    {
        private static List<string> suiviTirelire = new();

        public static List<string> Suivi()
        {
            return suiviTirelire;
        }

        public static string ConstructionHistorique()
        {
            string historiqueComplet = "";
            foreach (string transaction in suiviTirelire)
            {
                historiqueComplet += $"{transaction}";
            }
            return historiqueComplet;
        }
    }
}
