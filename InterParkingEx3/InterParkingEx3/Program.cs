using FileReadingLib.Enums;
using FileReadingLib.Implementations;
using FileReadingLib.Interfaces;
using System;
using System.Threading.Tasks;

namespace InterParkingEx3
{
    class Program
    {
        static async Task Main(string[] args)
        {
            ITextFileReaderService textFileReader = new TextFileReaderService();
            IXmlFileReaderService xmlFileReaderService = new XmlFileReaderService();
            
            bool errorToRead = false;
            bool choiseIsCorrect;
            do
            {
                Console.WriteLine("Please indicate what type of file you want to read");
                Console.WriteLine("Xml --> 1");
                Console.WriteLine("Text --> 2");
                if(int.TryParse(Console.ReadLine(),out int choice) && choice > 0 && choice <= 2)
                {
                    choiseIsCorrect = true;
                    Console.WriteLine("Add a path to a text file");
                    string path = Console.ReadLine();

                    try
                    {
                        FileType fileType = (FileType)choice;
                        switch (fileType)
                        {
                            case FileType.Xml:
                                Console.WriteLine(await xmlFileReaderService.Read(path));
                                break;
                            case FileType.Txt:
                                Console.WriteLine(await textFileReader.Read(path));
                                break;
                        }
                        errorToRead = false;
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("An error occurred while reading the file.");
                        Console.WriteLine($"Error : {e.Message}");
                        errorToRead = true;
                    }
                }
                else
                {
                    Console.WriteLine("The value entered is incorrect");
                    choiseIsCorrect = false;
                }
                
            }
            while (errorToRead || !choiseIsCorrect);

        }
    }
}
