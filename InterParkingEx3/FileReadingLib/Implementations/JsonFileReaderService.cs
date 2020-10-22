using FileReadingLib.Enums;
using FileReadingLib.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace FileReadingLib.Implementations
{
    public class JsonFileReaderService : IJsonFileReaderService
    {
        private readonly IEncryptService _encryptService;

        public JsonFileReaderService(IEncryptService encryptService)
        {
            _encryptService = encryptService;
        }
        
        public async Task<string> Read(string filePath)
        {
            return await File.ReadAllTextAsync(filePath);
        }

        public async Task<string> ReadEncrypt(string filePath, EncryptionAlgorithmType encryptionAlgorithmType)
        {
            string encryptedText = await Read(filePath);
            return _encryptService.Decrypt(encryptedText,encryptionAlgorithmType);
        }
    }
}
