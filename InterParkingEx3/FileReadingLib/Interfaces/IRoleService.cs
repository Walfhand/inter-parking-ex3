using FileReadingLib.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace FileReadingLib.Interfaces
{
    public interface IRoleService
    {
        RoleType? GetCurrentRole();
        void UseRoleSecutiry(RoleType role);
        bool IsAuthorizedToReadFile(string fileName);
        bool IsAuthorizedToReadFileType(FileType fileType);
    }
}
