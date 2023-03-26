using Core.Application.Utilities.FileOperations.Abstractions;
using Core.Application.Utilities.FileOperations.Constants;
using Core.Application.Utilities.FileOperations.Helpers;
using Core.CrossCuttingConcern.Exceptions.Exceptions;
using Microsoft.AspNetCore.Http;

namespace Core.Application.Utilities.FileOperations.Concretes;

public class FileOperation : IFileOperation
{
    #region Methods

    public void Delete(string filePath)
    {
        if (filePath is null) throw new ArgumentNullException();
        if (File.Exists(filePath) is false) throw new BusinessException("File not exist!");

        File.Delete(filePath);
    }

    public string Upload(IFormFile file, string? folderName = null)
    {
        string uploadPath = FileOperationHelpers.GetUploadPath(folderName);

        if (Directory.Exists(uploadPath) is false) Directory.CreateDirectory(uploadPath);

        string ext = Path.GetExtension(file.FileName);
        string fileName = FileOperationHelpers.GenerateFileName(Path.GetFileNameWithoutExtension(file.FileName), String.IsNullOrWhiteSpace(ext) ? ".png" : ext);
        string filePath = Path.Combine(uploadPath, fileName);

        using (FileStream stream = new(filePath, FileMode.Create))
        {
            file.CopyTo(stream);
        }

        return Path.Combine("/" + uploadPath, fileName).Replace('\\', '/');
    }

    #endregion Methods
}
