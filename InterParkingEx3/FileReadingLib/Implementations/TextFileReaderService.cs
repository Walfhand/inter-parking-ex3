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
        public async Task<string> Read(string filePath)
        {
            return await File.ReadAllTextAsync(filePath);
        }

        public async Task<string> ReadEncrypt(string filePath, EncryptionAlgorithmType encryptionAlgorithmType)
        {
            string encryptedText = await Read(filePath);
            switch (encryptionAlgorithmType)
            {
                case EncryptionAlgorithmType.RSA:
                    //decrypt rsa
                    return encryptedText;
                case EncryptionAlgorithmType.SHA_256:
                    //devrypt sha 256
                    return encryptedText;
                case EncryptionAlgorithmType.SHA_384:
                    //decrypt sha 384
                    return encryptedText;
                case EncryptionAlgorithmType.SHA_512:
                    //decrypt sha 512
                    return encryptedText;
                case EncryptionAlgorithmType.SHA_224:
                    //decrypt sha 224
                    return encryptedText;
                default:
                    return encryptedText;
            }
        }
    }
}
