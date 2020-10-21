﻿using InterParkingEx3.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace InterParkingEx3.Implementations
{
    public class TextFileReaderService : ITextFileReaderService
    {
        public async Task<string> Read(string filePath)
        {
            return await File.ReadAllTextAsync(filePath);
        }
    }
}
