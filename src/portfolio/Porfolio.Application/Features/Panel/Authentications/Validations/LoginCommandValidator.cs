using FluentValidation;
using Porfolio.Application.Features.Panel.Authentications.Commands;

namespace Porfolio.Application.Features.Panel.Authentications.Validations;

public class LoginCommandValidator : AbstractValidator<LoginCommandRequest>
{
    #region Constructors

    public LoginCommandValidator()
    {
        RuleFor(x => x.Email)
            .EmailAddress().WithMessage("E-posta adresi geçerli değil.").When(x => !string.IsNullOrEmpty(x.Email))
            .NotEmpty().WithMessage("E-posta adresi boş olamaz.");
        RuleFor(request => request.Password)
            .NotEmpty().WithMessage("Parola boş olamaz.");
            //.MinimumLength(8).WithMessage("Parola en az 8 karakterden oluşmalıdır.")
            //.Matches("[A-Z]").WithMessage("Parola bir veya daha fazla büyük harf içermelidir.")
            //.Matches("[a-z]").WithMessage("Parola bir veya daha fazla küçük harf içermelidir.")
            //.Matches(@"\d").WithMessage("Parola bir veya daha fazla rakam içermelidir.")
            //.Matches(@"[][""!@$%^&*(){}:;<>,.?/+_=|'~\\-]").WithMessage("Parola bir veya daha fazla özel karakter içermelidir.")
            //.Matches("^[^£# “”]*$").WithMessage("Parola aşağıdaki karakterleri £ # “” veya boşluk içermemelidir.");
    }

    #endregion Constructors
}
