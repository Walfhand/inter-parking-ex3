using InterParkingEx3.Implementations;
using InterParkingEx3.Interfaces;
using System;
using System.Threading.Tasks;

namespace InterParkingEx3
{
    class Program
    {
        static async Task Main(string[] args)
        {
            ITextFileReaderService textFileReader = new TextFileReaderService();
            bool errorToRead;
            do
            {
                Console.WriteLine("Add a path to a text file");
                string path = Console.ReadLine();

                try
                {
                    Console.WriteLine(await textFileReader.Read(path));
                    errorToRead = false;
                }
                catch (Exception e)
                {
                    Console.WriteLine("An error occurred while reading the file.");
                    Console.WriteLine($"Error : {e.Message}");
                    errorToRead = true;
                }
            }
            while (errorToRead);

        }
    }
}
