using FluentValidation;
using RestaurantAPI.Entities;

namespace RestaurantAPI.Models.Validations
{
    public class RegisterUserDTOValidator : AbstractValidator<RegisterUserDTO>
    {
        public RegisterUserDTOValidator(RestaurantDbContext dbContext)
        {
            RuleFor(x => x.Email).NotEmpty().EmailAddress();

            RuleFor(x => x.ConfirmPassword).Equal(c => c.Password);

            RuleFor(x => x.Password).Length(6, 15);

            RuleFor(x => x.Email).Custom((value, context) =>
            {
                var emailInUse = dbContext.Users.Any(x => x.Email == value);
                if (emailInUse)
                {
                    context.AddFailure("Email", "This email is already taken");
                }
            });
        }
    }
}
