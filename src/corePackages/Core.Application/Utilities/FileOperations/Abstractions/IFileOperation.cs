using Microsoft.AspNetCore.Http;

namespace Core.Application.Utilities.FileOperations.Abstractions;

public interface IFileOperation
{
    #region Methods

    string Upload(IFormFile file, string? folderName = null);
    void Delete(string filePath);

    #endregion Methods
}
