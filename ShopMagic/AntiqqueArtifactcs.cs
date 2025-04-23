using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

[XmlRoot("AntiqueArtifact")] 
public class AntiqueArtifact : Artifact, IExportable
{
    public int Age { get; set; }
    public string OriginRealm { get; set; }

    public override string Serialize()
    {
        return $"Старинный артефакт: {Name}, Возраст: {Age}, Мир: {OriginRealm}";
    }

    public string ExportToJson()
    {
        return Newtonsoft.Json.JsonConvert.SerializeObject(this, Newtonsoft.Json.Formatting.Indented);
    }

    public string ExportToXml()
    {
        using (var stringWriter = new System.IO.StringWriter())
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(AntiqueArtifact)); // Важно! Указывать тип класса
            xmlSerializer.Serialize(stringWriter, this);
            return stringWriter.ToString();
        }
    }
}