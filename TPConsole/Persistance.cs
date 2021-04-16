using System.Diagnostics;
using System.IO;
using System.Text.Json;

using TireLireLib;

using TirelirePlusLib;

namespace TPConsole
{
    public static class Persistance
    {
        private const string jsonFileName = "program-data.json";

        private class ProgramData
        {
            public decimal MontantTirelire1 { get; set; }
            public decimal MontantTirelire2 { get; set; }
            public decimal MontantTirelire3a { get; set; }
            public decimal MontantTirelire3b { get; set; }
            public decimal MontantTirelire4a { get; set; }
            public decimal MontantTirelire5a { get; set; }
            public decimal MontantTirelire6a { get; set; }
        }

        public static void Sauvegarder()
        {
            // 1. Créer l’objet à persister
            var programData = new ProgramData
            {
                MontantTirelire1 = Tirelire1.MontantTotal,
                MontantTirelire2 = Tirelire2.MontantTotal,
                MontantTirelire3a = Instances.Tirelire3a.MontantTotal,
                MontantTirelire3b = Instances.Tirelire3b.MontantTotal,
                MontantTirelire4a = Instances.Tirelire4a.MontantTotal,
                MontantTirelire5a = Instances.Tirelire5a.MontantTotal,
                MontantTirelire6a = Instances.Tirelire6a.MontantTotal
            };
            // 2. Créer un objet de configuration
            var options = new JsonSerializerOptions
            {
                WriteIndented = true,
            };
            // 3. Convertir les données en string
            string jsonString = JsonSerializer.Serialize(programData, options);
            // 4. Stocker la string dans un fichier
            File.WriteAllText(jsonFileName, jsonString);
        }

        public static void Charger()
        {
            // 1. Vérifier que le fichier existe
            if (!File.Exists(jsonFileName)) { return; }
            // 2. Lire le contenu du fichier
            var jsonString = File.ReadAllText(jsonFileName);
            // 3. Convertir le contenu du fichier en un objet (désérialiser)
            var programData = JsonSerializer.Deserialize<ProgramData>(jsonString);
            // 4. Si la désérialisation réussit, 
            if (programData is null) { return; }
            //    récupérer le montant et le mettre dans la tirelire.
            Tirelire1.MontantTotal = programData.MontantTirelire1;
            _ = Tirelire2.Déposer(programData.MontantTirelire2);
            Instances.Tirelire3a.MontantTotal = programData.MontantTirelire3a;
            Instances.Tirelire3b.MontantTotal = programData.MontantTirelire3b;
            Instances.Tirelire4a.MontantTotal = programData.MontantTirelire4a;
            _ = Tirelire5.Déposer(Instances.Tirelire5a, programData.MontantTirelire5a);
            Debug.Assert(Instances.Tirelire6a.Reset(programData.MontantTirelire6a));
            //_ = Instances.Tirelire6a.Déposer(programData.MontantTirelire6a);
        }

    }
}
