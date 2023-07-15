using Microsoft.AspNetCore.Http;

namespace Core.FileOperation.Abstractions;

public interface IFileOperation
{
    #region Methods

    string UploadFile(IFormFile file, string path, string[]? extensionFilter = null);
    void DeleteFile(string path);

    #endregion Methods
}
