using FileReadingLib.Enums;
using FileReadingLib.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace FileReadingLib.Implementations
{
    public class EncryptService : IEncryptService
    {
        public string Decrypt(string text,EncryptionAlgorithmType encryptionAlgorithmType)
        {
            return encryptionAlgorithmType switch
            {
                EncryptionAlgorithmType.RSA => text,//decrypt rsa
                EncryptionAlgorithmType.SHA_256 => text,//devrypt sha 256
                EncryptionAlgorithmType.SHA_384 => text,//decrypt sha 384
                EncryptionAlgorithmType.SHA_512 => text,//decrypt sha 512
                EncryptionAlgorithmType.SHA_224 => text,//decrypt sha 224
                _ => text,
            };
        }
    }
}
