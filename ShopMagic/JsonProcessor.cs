using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

public class JsonProcessor : IDataProcessor<ModernArtifact>
{
    public List<ModernArtifact> LoadData(string filePath)
    {
        try
        {
            string jsonData = File.ReadAllText(filePath);
            List<ModernArtifact> artifacts = JsonConvert.DeserializeObject<List<ModernArtifact>>(jsonData);
            return artifacts;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"ОШИБКА при загрузке JSON: {ex.Message}");
            return new List<ModernArtifact>();
        }
    }

    public void SaveData(List<ModernArtifact> data, string filePath)
    {
        try
        {
            string jsonData = JsonConvert.SerializeObject(data, Newtonsoft.Json.Formatting.Indented);
            File.WriteAllText(filePath, jsonData);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"ОШИБКА при сохранении JSON: {ex.Message}");
        }
    }
}