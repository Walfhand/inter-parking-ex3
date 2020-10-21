using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FileReadingLib.Interfaces
{
    public interface IXmlFileReaderService
    {
        Task<string> Read(string filePath);
        Task<TModel> Read<TModel>(string filePath);
    }
}
