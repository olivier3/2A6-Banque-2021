using System.Diagnostics;
using System.IO;
using System.Text.Json;

using TireLireLib;
using static TPConsole.Instances;
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
            public decimal MontantTirelire6p { get; set; }
        }

        public static void Sauvegarder()
        {
            // 1. Créer l’objet à persister
            var programData = new ProgramData
            {
                MontantTirelire1 = Tirelire1.MontantTotal,
                MontantTirelire2 = Tirelire2.MontantTotal,
                MontantTirelire3a = MesInstances.Tirelire3a.MontantTotal,
                MontantTirelire3b = MesInstances.Tirelire3b.MontantTotal,
                MontantTirelire4a = MesInstances.Tirelire4a.MontantTotal,
                MontantTirelire5a = MesInstances.Tirelire5a.MontantTotal,
                MontantTirelire6a = MesInstances.Tirelire6a.MontantTotal,
                MontantTirelire6p = MesInstances.Tirelire6p.MontantTotal
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
            MesInstances.Tirelire3a.MontantTotal = programData.MontantTirelire3a;
            MesInstances.Tirelire3b.MontantTotal = programData.MontantTirelire3b;
            MesInstances.Tirelire4a.MontantTotal = programData.MontantTirelire4a;
            _ = Tirelire5.Déposer(MesInstances.Tirelire5a, programData.MontantTirelire5a);
            Debug.Assert(MesInstances.Tirelire6a.Reset(programData.MontantTirelire6a));
            Debug.Assert(MesInstances.Tirelire6p.Reset(programData.MontantTirelire6p));
        }

    }
}
