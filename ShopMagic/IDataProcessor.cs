using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Serialization;


public interface IDataProcessor<T>
{
    List<T> LoadData(string filePath);
    void SaveData(List<T> data, string filePath);
}