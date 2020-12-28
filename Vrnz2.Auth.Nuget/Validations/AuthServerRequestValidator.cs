using FluentValidation;
using Vrnz2.Auth.Nuget.DTOs;

namespace Vrnz2.Auth.Nuget.Validations
{
    public class AuthServerRequestValidator
        : AbstractValidator<AuthServerRequest>
    {
        #region Constructors

        public AuthServerRequestValidator()
        {
            RuleFor(v => v.token)
                .NotNull()
                .NotEmpty()
                .WithMessage("Token invalid!");//TODO: Arrumar!
        }

        #endregion
    }
}
