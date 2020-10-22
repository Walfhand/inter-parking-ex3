using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FileReadingLib.Interfaces
{
    public interface IJsonFileReaderService
    {
        Task<string> Read(string filePath);
    }
}
