using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

public class XmlProcessor : IDataProcessor<AntiqueArtifact>
{
    public List<AntiqueArtifact> LoadData(string filePath)
    {
        try
        {
            List<AntiqueArtifact> artifacts = new List<AntiqueArtifact>();
            XmlSerializer serializer = new XmlSerializer(typeof(List<AntiqueArtifact>));

            using (FileStream fileStream = new FileStream(filePath, FileMode.Open))
            {
                artifacts = (List<AntiqueArtifact>)serializer.Deserialize(fileStream);
            }

            return artifacts;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"ОШИБКА при загрузке XML: {ex.Message}");
            return new List<AntiqueArtifact>();
        }
    }

    public void SaveData(List<AntiqueArtifact> data, string filePath)
    {
        try
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<AntiqueArtifact>));
            using (TextWriter writer = new StreamWriter(filePath))
            {
                serializer.Serialize(writer, data);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"ОШИБКА при сохранении XML: {ex.Message}");
        }
    }
}