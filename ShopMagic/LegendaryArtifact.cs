using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

public class LegendaryArtifact : Artifact, IExportable
{
    public string CurseDescription { get; set; }
    public bool IsCursed { get; set; }

    public override string Serialize()
    {
        return $"Легендарный артефакт: {Name}, Проклят: {IsCursed}, Описание проклятия: {CurseDescription}";
    }

    public string ExportToJson()
    {
        return Newtonsoft.Json.JsonConvert.SerializeObject(this, Newtonsoft.Json.Formatting.Indented);
    }

    public string ExportToXml()
    {
        using (var stringWriter = new System.IO.StringWriter())
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(LegendaryArtifact));
            xmlSerializer.Serialize(stringWriter, this);
            return stringWriter.ToString();
        }
    }
}