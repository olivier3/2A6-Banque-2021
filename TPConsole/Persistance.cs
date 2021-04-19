using System.IO;
using System.Text.Json;

using TireLireLib;

namespace TPConsole
{
    public static class Persistance
    {
        private const string jsonFileName = "program-data.json";

        private class ProgramData
        {
            public string DataVersion { get; init; } = "OB-2.0";
            public decimal MontantTirelire1 { get; init; }
            public decimal MontantTirelire2 { get; init; }
            public Instances MesInstances { get; init; } = new Instances();
        }

        public static void Sauvegarder()
        {
            // 1. Créer l’objet à persister
            var programData = new ProgramData
            {
                MontantTirelire1 = Tirelire1.MontantTotal,
                MontantTirelire2 = Tirelire2.MontantTotal,
                MesInstances = Instances.MesInstances
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
            if (programData.MesInstances is not null && programData.DataVersion == "OB-2.0")
            {
                Instances.MesInstances = programData.MesInstances;
            }
        }

    }
}
