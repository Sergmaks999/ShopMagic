using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Serialization;

public class ShopManager
{
    public List<Artifact> Artifacts { get; set; } = new List<Artifact>();

    public void LoadAllData(string xmlPath, string jsonPath, string textPath)
    {
        XmlProcessor xmlProcessor = new XmlProcessor();
        JsonProcessor jsonProcessor = new JsonProcessor();
        LegendaryProcessor legendaryProcessor = new LegendaryProcessor();

        Artifacts.AddRange(xmlProcessor.LoadData(xmlPath)); 
        Artifacts.AddRange(jsonProcessor.LoadData(jsonPath));
        Artifacts.AddRange(legendaryProcessor.LoadData(textPath));
    }

    public void GenerateReport(string filePath)
    {
        try
        {
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                writer.WriteLine("Отчет по артефактам:");
                var rarityGroups = Artifacts.GroupBy(a => a.Rarity);

                foreach (var group in rarityGroups)
                {
                    double avgPower = group.Average(a => a.PowerLevel);
                    writer.WriteLine($"Редкость: {group.Key}, Количество: {group.Count()}, Средняя сила: {avgPower:F2}");
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"ОШИБКА при создании отчета: {ex.Message}");
        }

    }

    public List<LegendaryArtifact> FindCursedArtifacts()
    {
        return Artifacts.OfType<LegendaryArtifact>().Where(a => a.IsCursed && a.PowerLevel > 50).ToList();
    }

    public Dictionary<Rarity, int> GroupByRarity()
    {
        return Artifacts.GroupBy(a => a.Rarity).ToDictionary(g => g.Key, g => g.Count());
    }

    public List<Artifact> TopByPower(int count)
    {
        return Artifacts.OrderByDescending(a => a.PowerLevel).Take(count).ToList();
    }

    public void ExportArtifacts(List<Artifact> artifacts, string filePath, string format)
    {
        try
        {
            if (format == "xml")
            {
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<Artifact>));
                using (StreamWriter writer = new StreamWriter(filePath))
                {
                    xmlSerializer.Serialize(writer, artifacts);
                }
                Console.WriteLine($"Артефакты сохранены в XML: {filePath}");
            }
            else if (format == "json")
            {
                string jsonData = Newtonsoft.Json.JsonConvert.SerializeObject(artifacts, Newtonsoft.Json.Formatting.Indented);
                File.WriteAllText(filePath, jsonData);
                Console.WriteLine($"Артефакты сохранены в JSON: {filePath}");
            }
            else
            {
                Console.WriteLine("Неправильный формат! Нужно xml или json");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"ОШИБКА при сохранении артефактов: {ex.Message}");
        }
    }
}