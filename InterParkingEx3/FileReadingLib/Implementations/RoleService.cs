using FileReadingLib.Enums;
using FileReadingLib.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace FileReadingLib.Implementations
{
    public class RoleService : IRoleService
    {
        private RoleType? _role;
        public void UseRoleSecutiry(RoleType role)
        {
            _role = role;
        }

        public bool IsAuthorizedToReadFile(string fileName)
        {
            if(_role == null)
            {
                return true;
            }

            int lenght = fileName.Split('.').Length;

            if (string.IsNullOrEmpty(fileName) ||  lenght < 2)
            {
                throw new Exception("The file name is incorrect");
            }

            string fileExtension = fileName.Split('.')[1];

            return _role switch
            {
                RoleType.Admin => true,
                RoleType.Xml => fileExtension == "xml",
                RoleType.Text => fileExtension == "txt",
                _ => false,
            };
        }

        public RoleType? GetCurrentRole()
        {
            return _role;
        }

        public bool IsAuthorizedToReadFileType(FileType fileType)
        {
            if(_role == RoleType.Admin || _role == null)
            {
                return true;
            }

            return fileType switch
            {
                FileType.Xml => _role == RoleType.Xml,
                FileType.Txt => _role == RoleType.Text,
                _ => false,
            };
        }
    }
}
