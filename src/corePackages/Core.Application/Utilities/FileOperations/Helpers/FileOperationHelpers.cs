using Core.Application.Utilities.Extensions;
using Core.Application.Utilities.FileOperations.Constants;

namespace Core.Application.Utilities.FileOperations.Helpers;

public static class FileOperationHelpers
{
    #region Methods

    public static string GetUploadPath(string? folderName = null)
    {
        return folderName is null
            ? Path.Combine(Environment.CurrentDirectory, FileOperationConstants.Directory_Name)
            : Path.Combine(Environment.CurrentDirectory, FileOperationConstants.Directory_Name, folderName);
    }

    public static string GenerateFileName(string fileName, string extensions)
    {
        DateTime currentDate = DateTime.UtcNow;
        string year = currentDate.Year.ToString();
        string month = currentDate.Month.ToString().PadLeft(2, '0');
        string day = currentDate.Day.ToString().PadLeft(2, '0');
        string generatedFileName = $"{year}{month}{day}_{Guid.NewGuid().ToString()[..5]}_{fileName.Slugify()}{extensions.ToLower()}";
        return generatedFileName;
    }

    #endregion Methods
}
