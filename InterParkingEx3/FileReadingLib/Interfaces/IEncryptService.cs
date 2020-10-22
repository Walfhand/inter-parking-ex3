using FileReadingLib.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace FileReadingLib.Interfaces
{
    public interface IEncryptService
    {
        string Decrypt(string text,EncryptionAlgorithmType encryptionAlgorithmType);
    }
}
