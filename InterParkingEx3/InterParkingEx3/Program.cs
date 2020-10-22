using FileReadingLib.Enums;
using FileReadingLib.Implementations;
using FileReadingLib.Interfaces;
using System;
using System.Threading.Tasks;

namespace InterParkingEx3
{
    class Program
    {
        private static bool choiseIsCorrect = true;
        static async Task Main(string[] args)
        {
            ITextFileReaderService textFileReader = new TextFileReaderService();
            IXmlFileReaderService xmlFileReaderService = new XmlFileReaderService();
            
            bool errorToRead = false;
            do
            {
                SelectFileType();
                if(int.TryParse(Console.ReadLine(),out int choice) && choice > 0 && choice <= 2)
                {
                    choiseIsCorrect = true;
                    try
                    {
                        FileType fileType = (FileType)choice;
                        switch (fileType)
                        {
                            case FileType.Xml:
                                Console.WriteLine(await xmlFileReaderService.Read(GetPath(fileType)));
                                break;
                            case FileType.Txt:
                                await ReadTxtFile(textFileReader, fileType);
                                break;
                        }
                        errorToRead = false;
                    }
                    catch (Exception e)
                    {
                        WriteException(e);
                        errorToRead = true;
                    }
                }
                else
                {
                    ChoiseIsIncorrect();
                }
                
            }
            while (errorToRead || !choiseIsCorrect);

        }

        static void ChoiseIsIncorrect()
        {
            Console.WriteLine("The value entered is incorrect");
            choiseIsCorrect = false;
        }
        static void WriteException(Exception e)
        {
            Console.WriteLine("An error occurred while reading the file.");
            Console.WriteLine($"Error : {e.Message}");
        }

        static void SelectFileType()
        {
            Console.WriteLine("Please indicate what type of file you want to read");
            Console.WriteLine("Xml --> 1");
            Console.WriteLine("Text --> 2");
        }
        static string GetPath(FileType fileType)
        {
            Console.WriteLine($"Add a path to a {fileType} file");
            return Console.ReadLine();
        }

        static async Task ReadTxtFile(ITextFileReaderService textFileReader, FileType fileType)
        {
            Console.WriteLine("Do you want to read an encrypted text file? (y/n)");
            switch (Console.ReadLine())
            {
                case "y":
                    Console.WriteLine("Choose your encryption algorithm");
                    foreach (EncryptionAlgorithmType encryptionAlgorithm in Enum.GetValues(typeof(EncryptionAlgorithmType)))
                    {
                        Console.WriteLine($"{encryptionAlgorithm} --> {(int)encryptionAlgorithm}");
                    }

                    if (int.TryParse(Console.ReadLine(), out int choiceEncryptType) && choiceEncryptType > 0 && choiceEncryptType <= 5)
                    {
                        Console.WriteLine(await textFileReader.ReadEncrypt(GetPath(fileType), (EncryptionAlgorithmType)choiceEncryptType));
                    }
                    else
                    {
                        ChoiseIsIncorrect();
                    }
                    break;
                case "n":
                    Console.WriteLine(await textFileReader.Read(GetPath(fileType)));
                    break;
                default:
                    ChoiseIsIncorrect();
                    break;
            }
        }
    }
}
