using FileReadingLib.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FileReadingLib.Interfaces
{
    public interface IXmlFileReaderService
    {
        Task<string> Read(string filePath);
        Task<string> ReadEncrypt(string filePath, EncryptionAlgorithmType encryptionAlgorithmType);
        Task<TModel> Read<TModel>(string filePath);
    }
}
