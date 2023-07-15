using Core.FileOperation.Abstractions;
using Core.FileOperation.Constants;
using Core.FileOperation.Exceptions;
using Core.FileOperation.Extensions;
using Microsoft.AspNetCore.Http;

namespace Core.FileOperation.Concretes;

public class FileOperation : IFileOperation
{
    #region Methods

    public string UploadFile(IFormFile file, string path, string[]? extensionFilter = null)
    {
        if (file is null) throw new ArgumentNullException(nameof(file));
        if (string.IsNullOrEmpty(path)) throw new ArgumentNullException(nameof(path));

        string fileExtension = Path.GetExtension(file.FileName);

        if (extensionFilter is null) { if (FileOperationConstants.VALID_FILE_EXTENSIONS.Contains(fileExtension) is false) throw new FileExtensionNotValidException("File extension is not valid."); }
        else { if (extensionFilter.Contains(fileExtension) is false) throw new FileExtensionNotValidException("File extension is not valid."); }

        string uploadPath = Path.Combine(Environment.CurrentDirectory, "wwwroot", path);

        if (!File.Exists(uploadPath)) Directory.CreateDirectory(uploadPath);

        string fileName = GenerateFileName(file.FileName);
        string uploadFilePath = Path.Combine(uploadPath, fileName);

        using (var stream = new FileStream(uploadFilePath, FileMode.Create))
        {
            file.CopyTo(stream);
        }

        return uploadFilePath.Replace(Path.Combine(Environment.CurrentDirectory, "wwwroot"), "");
    }

    public void DeleteFile(string path)
    {
        if (string.IsNullOrEmpty(path)) throw new ArgumentNullException(nameof(path));
        if (!File.Exists(path)) throw new FileNotFoundException();

        File.Delete(path);
    }

    private static string GenerateFileName(string fileName)
    {
        string fileExtension = Path.GetExtension(fileName);
        string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(fileName);

        DateTime currentDate = DateTime.UtcNow;
        string year = currentDate.Year.ToString();
        string month = currentDate.Month.ToString().PadLeft(2, '0');
        string day = currentDate.Day.ToString().PadLeft(2, '0');
        string generatedFileName = $"{year}{month}{day}_{Guid.NewGuid().ToString()[..5]}_{fileNameWithoutExtension.Slugify()}{fileExtension.ToLower()}";

        return generatedFileName;
    }

    #endregion Methods
}
