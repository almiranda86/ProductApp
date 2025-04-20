using FluentValidation;
using ProductApp.Domain.Commands;

namespace ProductApp.Service.Validations
{
    public sealed class RegisterProductRequestValidation : AbstractValidator<RegisterProductRequest>
    {
        public RegisterProductRequestValidation()
        {
            RuleFor(x => x.Price)
                .Must(NotBeLessThanZero)
                .WithMessage("Price should be greater than 0");

            RuleFor(x => x.Categories)
                .Must(NotBeEmpty)
                .WithMessage("Categories should not be empty");
        }

        static bool NotBeLessThanZero(float value)
        {
            const float compareValue = 0.0f;

            if (value < compareValue)
            {
                return false;
            }

            return true;
        }

        static bool NotBeEmpty(List<string> value)
        {
            return value.Count != 0;
        }
    }
}
