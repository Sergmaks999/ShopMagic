using System;
using System.Collections.Generic;
using System.IO;

public class LegendaryProcessor : IDataProcessor<LegendaryArtifact>
{
    public List<LegendaryArtifact> LoadData(string filePath)
    {
        try
        {
            List<LegendaryArtifact> artifacts = new List<LegendaryArtifact>();
            string[] lines = File.ReadAllLines(filePath);

            foreach (string line in lines)
            {
                string[] parts = line.Split('|');
                if (parts.Length == 5)
                {
                    LegendaryArtifact artifact = new LegendaryArtifact
                    {
                        Name = parts[0],
                        PowerLevel = int.Parse(parts[1]),
                        Rarity = (Rarity)Enum.Parse(typeof(Rarity), parts[2]),
                        CurseDescription = parts[3],
                        IsCursed = bool.Parse(parts[4])
                    };
                    artifacts.Add(artifact);
                }
            }

            return artifacts;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"ОШИБКА при загрузке текстового файла: {ex.Message}");
            return new List<LegendaryArtifact>();
        }
    }

    public void SaveData(List<LegendaryArtifact> data, string filePath)
    {
        try
        {
            List<string> lines = new List<string>();
            foreach (var artifact in data)
            {
                lines.Add($"{artifact.Name}|{artifact.PowerLevel}|{artifact.Rarity}|{artifact.CurseDescription}|{artifact.IsCursed}");
            }
            File.WriteAllLines(filePath, lines);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"ОШИБКА при сохранении текстового файла: {ex.Message}");
        }
    }
}