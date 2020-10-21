using FileReadingLib.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace FileReadingLib.Implementations
{
    public class XmlFileReaderService : IXmlFileReaderService
    {
        public async Task<string> Read(string filePath)
        {
            return await File.ReadAllTextAsync(filePath);
        }

        public async Task<TModel> Read<TModel>(string filePath)
        {
            string xml = await Read(filePath);
            XmlSerializer serializer = new XmlSerializer(typeof(TModel));

            using TextReader textReader = new StringReader(xml);
            return (TModel)serializer.Deserialize(textReader);
        }
    }
}
