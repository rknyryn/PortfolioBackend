using FluentValidation;
using Porfolio.Application.Features.Panel.Authentications.Commands;

namespace Porfolio.Application.Features.Panel.Authentications.Validations;

public class RefreshTokenCommandValidator : AbstractValidator<RefreshTokenCommandRequest>
{
    #region Constructors

    public RefreshTokenCommandValidator()
    {
        RuleFor(x => x.RefreshToken).NotEmpty().WithMessage("Yenileme jetonu boş olamaz.");
    }

    #endregion Constructors
}
