using FileReadingLib.Enums;
using FileReadingLib.Interfaces;
using System.IO;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace FileReadingLib.Implementations
{
    public class XmlFileReaderService : IXmlFileReaderService
    {
        private readonly IEncryptService _encryptService;

        public XmlFileReaderService(IEncryptService encryptService)
        {
            _encryptService = encryptService;
        }
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

        public async Task<string> ReadEncrypt(string filePath, EncryptionAlgorithmType encryptionAlgorithmType)
        {
            string encryptedText = await Read(filePath);
            return _encryptService.Decrypt(encryptedText, encryptionAlgorithmType);
        }
    }
}
