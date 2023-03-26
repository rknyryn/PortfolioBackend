using FluentValidation;

namespace Core.Application.Pipelines.Validation;

public static class ValidationTool
{
    #region Methods

    public static void Validate(IValidator validator, object entity)
    {
        var context = new ValidationContext<object>(entity);

        var result = validator.Validate(context);
        if (!result.IsValid)
        {
            throw new ValidationException(result.Errors);
        }
    }

    #endregion Methods
}
