using FileReadingLib.Enums;
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

        public async Task<string> ReadEncrypt(string filePath, EncryptionAlgorithmType encryptionAlgorithmType)
        {
            string encryptedText = await Read(filePath);
            return encryptionAlgorithmType switch
            {
                EncryptionAlgorithmType.RSA => encryptedText,//decrypt rsa
                EncryptionAlgorithmType.SHA_256 => encryptedText,//devrypt sha 256
                EncryptionAlgorithmType.SHA_384 => encryptedText,//decrypt sha 384
                EncryptionAlgorithmType.SHA_512 => encryptedText,//decrypt sha 512
                EncryptionAlgorithmType.SHA_224 => encryptedText,//decrypt sha 224
                _ => encryptedText,
            };
        }
    }
}
