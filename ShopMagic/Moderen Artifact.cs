using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

public class ModernArtifact : Artifact, IExportable
{
    public double TechLevel { get; set; }
    public string Manufacturer { get; set; }

    public override string Serialize()
    {
        return $"Современный артефакт: {Name}, Тех. уровень: {TechLevel}, Производитель: {Manufacturer}";
    }

    public string ExportToJson()
    {
        return Newtonsoft.Json.JsonConvert.SerializeObject(this, Newtonsoft.Json.Formatting.Indented);
    }

    public string ExportToXml()
    {
        using (var stringWriter = new System.IO.StringWriter())
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(ModernArtifact));
            xmlSerializer.Serialize(stringWriter, this);
            return stringWriter.ToString();
        }
    }
}