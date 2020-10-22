using FileReadingLib.Enums;
using FileReadingLib.Implementations;
using FileReadingLib.Interfaces;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace InterParkingEx3
{
    class Program
    {
        static async Task Main(string[] args)
        {
            ITextFileReaderService textFileReader = new TextFileReaderService();
            IXmlFileReaderService xmlFileReaderService = new XmlFileReaderService();
            IRoleService roleService = new RoleService();
            bool errorToRead = true;

            while (errorToRead) 
            {
                UseRole(roleService);
                FileType fileType = SelectFileType(roleService);
                try
                {
                   string[] fileNames;
                   fileNames = FilesToRead(fileType);
                    switch (fileType)
                    {
                        case FileType.Xml:
                            await ReadXml(fileNames, xmlFileReaderService, roleService);
                            break;
                        case FileType.Txt:
                            await ReadTxtFile(fileNames,roleService,textFileReader, fileType);
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

        }

        static async Task ReadXml(string[] fileNames , IXmlFileReaderService xmlFileReaderService, IRoleService roleService)
        {
            await ReadEncryptedFile(fileNames, roleService,
                async (path, encrypt) => await xmlFileReaderService.ReadEncrypt(path, encrypt),
                async (path) => await xmlFileReaderService.Read(path));
        }

        static string GetDirectoryPath()
        {
            return Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @$"Files");
        }
        static string GetFilePath(string fileName)
        {
            return $"{GetDirectoryPath()}\\{fileName}";
        }

        static string[] FilesToRead(FileType fileType)
        {
            DirectoryInfo directory = new DirectoryInfo(GetDirectoryPath());
            FileInfo[] files = new FileInfo[0];
            switch (fileType)
            {
                case FileType.Xml:
                    files = directory.GetFiles("*.xml");
                    break;
                case FileType.Txt:
                    files = directory.GetFiles("*.txt");
                    break;
            }
            return files.Select(f => f.Name).ToArray();
        }

        static void UseRole(IRoleService roleService)
        {
            bool inputValid = false;
            while (!inputValid)
            {
                Console.WriteLine("Do you want to use security with roles? (y/n)");
                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "y":
                        while (!inputValid)
                        {
                            Console.WriteLine("Choose your role");
                            var roles = Enum.GetValues(typeof(RoleType)).Cast<RoleType>();

                            foreach (RoleType role in roles)
                            {
                                Console.WriteLine($"{role} --> {(int)role}");
                            }

                            int lastRoleValue = (int)roles.Last();
                            int firstRoleValue = (int)roles.First();

                            if (int.TryParse(Console.ReadLine(), out int result) && result >= firstRoleValue && result <= lastRoleValue)
                            {
                                roleService.UseRoleSecutiry((RoleType)result);
                                Console.WriteLine($"You have chosen the role {(RoleType)result}");
                                inputValid = true;
                            }
                            else
                            {
                                inputValid = false;
                                Console.WriteLine("The value entered is incorrect");
                            }
                        }
                        
                        break;
                    case "n":
                        Console.WriteLine("No security!");
                        inputValid = true;
                        break;
                    default:
                        inputValid = false;
                        Console.WriteLine("The value entered is incorrect");
                        break;
                }
            }
            
        }
        
        static void WriteException(Exception e)
        {
            Console.WriteLine("An error occurred while reading the file.");
            Console.WriteLine($"Error : {e.Message}");
        }

        static FileType SelectFileType(IRoleService roleService)
        {
            bool inputValid = false;
            bool authorized = false;
            int fileType = 0;
            while (!inputValid || !authorized)
            {
                Console.WriteLine("Please indicate what type of file you want to read");
                Console.WriteLine("Xml --> 1");
                Console.WriteLine("Text --> 2");
                if (int.TryParse(Console.ReadLine(), out fileType) && fileType > 0 && fileType <= 2)
                {
                    if (!roleService.IsAuthorizedToReadFileType((FileType)fileType))
                    {
                        Console.WriteLine("You do not have sufficient rights to access this type of file.");
                        authorized = false;
                    }
                    else
                    {
                        authorized = true;
                    }
                    inputValid = true;
                }
                else
                {
                    inputValid = false;
                    Console.WriteLine("The value entered is incorrect");
                }
            }

            return (FileType)fileType;
        }
        static string GetPath(FileType fileType)
        {
            Console.WriteLine($"Add a path to a {fileType} file");
            return Console.ReadLine();
        }

        static async Task SelectFile(string[] fileNames, IRoleService roleService, Func<string,Task<string>> readfunc)
        {
            bool inputIsValid = false;
            while (!inputIsValid)
            {
                for (int i = 0; i < fileNames.Length; i++)
                {
                    Console.WriteLine($"{fileNames[i]} --> {i}");
                }

                if (int.TryParse(Console.ReadLine(), out int fileInput) && fileInput >= fileNames.Length - 1 && fileInput <= fileNames.Length - 1)
                {
                    inputIsValid = true;
                    string fileName = fileNames[fileInput];
                    if (!roleService.IsAuthorizedToReadFile(fileName))
                    {
                        throw new Exception("You are not authorized to view this file");
                    }

                    Console.WriteLine(await readfunc(GetFilePath(fileName)));
                }
                else
                {
                    inputIsValid = false;
                    Console.WriteLine("The value entered is incorrect");
                }
            }
        }

        static async Task SelectFileEncrypted(string[] fileNames, IRoleService roleService, 
            EncryptionAlgorithmType encryptionAlgorithmType,
            Func<string, EncryptionAlgorithmType, Task<string>> readfuncEncrypt)
        {
            bool inputIsValid = false;
            while (!inputIsValid)
            {
                Console.WriteLine("Choose the file you want to read");
                for (int i = 0; i < fileNames.Length; i++)
                {
                    Console.WriteLine($"{fileNames[i]} --> {i}");
                }

                if (int.TryParse(Console.ReadLine(), out int fileInput) && fileInput >= fileNames.Length - 1 && fileInput <= fileNames.Length - 1)
                {
                    inputIsValid = true;
                    string fileName = fileNames[fileInput];
                    if (!roleService.IsAuthorizedToReadFile(fileName))
                    {
                        throw new Exception("You are not authorized to view this file");
                    }

                    Console.WriteLine(await readfuncEncrypt(GetFilePath(fileName), encryptionAlgorithmType));
                }
                else
                {
                    inputIsValid = false;
                    Console.WriteLine("The value entered is incorrect");
                }
            }
        }

        static async Task ReadEncryptedFile(string[] fileNames,IRoleService roleService,
            Func<string, EncryptionAlgorithmType,Task<string>> readfuncEncrypt,
            Func<string, Task<string>> readFunc)
        {
            bool inputIsValid = false;
            while (!inputIsValid)
            {
                Console.WriteLine("Do you want to read an encrypted file? (y/n)");
                switch (Console.ReadLine())
                {
                    case "y":
                        while (!inputIsValid)
                        {
                            Console.WriteLine("Choose your encryption algorithm");
                            var encryptionsAlgo = Enum.GetValues(typeof(EncryptionAlgorithmType)).Cast<EncryptionAlgorithmType>();
                            int firstEncryptionValue = (int)encryptionsAlgo.First();
                            int lastEncryptionValue = (int)encryptionsAlgo.Last();

                            foreach (EncryptionAlgorithmType encryptionAlgorithm in encryptionsAlgo)
                            {
                                Console.WriteLine($"{encryptionAlgorithm} --> {(int)encryptionAlgorithm}");
                            }

                            if (int.TryParse(Console.ReadLine(), out int choiceEncryptType) && choiceEncryptType >= firstEncryptionValue && choiceEncryptType <= lastEncryptionValue)
                            {
                                inputIsValid = true;
                                await SelectFileEncrypted(fileNames, roleService, (EncryptionAlgorithmType)choiceEncryptType,
                                    async (path, algo) => await readfuncEncrypt(path, algo));
                            }
                            else
                            {
                                inputIsValid = false;
                                Console.WriteLine("The value entered is incorrect");
                            }
                        }

                        break;
                    case "n":
                        await SelectFile(fileNames,roleService,async (path) => await readFunc(path));
                        break;
                    default:
                        inputIsValid = false;
                        Console.WriteLine("The value entered is incorrect");
                        break;
                }
            }
        }

        static async Task ReadTxtFile(string[] fileNames,IRoleService roleService,ITextFileReaderService textFileReader, FileType fileType)
        {
            await ReadEncryptedFile(fileNames, roleService, 
                async (path, encrypt) => await textFileReader.ReadEncrypt(path, encrypt), 
                async (path) => await textFileReader.Read(path));
        }
    }
}
