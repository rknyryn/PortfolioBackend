using System.Runtime.Serialization;

namespace Core.FileOperation.Exceptions;

public class FileExtensionNotValidException : Exception
{
    #region Constructors

    public FileExtensionNotValidException()
    {
    }

    public FileExtensionNotValidException(string? message) : base(message)
    {
    }

    public FileExtensionNotValidException(string? message, Exception? innerException) : base(message, innerException)
    {
    }

    protected FileExtensionNotValidException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }

    #endregion Constructors
}
