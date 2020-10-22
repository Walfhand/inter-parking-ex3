using FileReadingLib.Enums;
using FileReadingLib.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace FileReadingLib.Implementations
{
    public class TextFileReaderService : ITextFileReaderService
    {
        private readonly IEncryptService _encryptService;

        public TextFileReaderService(IEncryptService encryptService)
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
            return _encryptService.Decrypt(encryptedText, encryptionAlgorithmType);
        }
    }
}
