using Cinema.Web.Models;
using FluentValidation;

namespace Cinema.Web.Validation
{
    public class OrderModelValidator : AbstractValidator<OrderModel>
    {
        public OrderModelValidator()
        {
            RuleFor(o => o.GuidId)
                .NotEmpty();

            RuleFor(o => o.UserFirstName)
                .NotEmpty()
                .MinimumLength(1)
                .MaximumLength(30)
                .Matches(@"[A-Za-z]{1,30}")
                .WithMessage("Incorrect first name");

            RuleFor(o => o.UserLastName)
                .NotEmpty()
                .MinimumLength(1)
                .MaximumLength(30)
                .Matches(@"[A-Za-z]{1,30}")
                .WithMessage("Incorrect last name");

            RuleFor(o => o.UserPhoneNumber)
                .NotEmpty()
                .Matches(@"\+38\({0,1}\d{3}\){0,1}\d{3}-{0,1}\d{2}-{0,1}\d{2}")
                .WithMessage("Incorrect phone number. Enter the phone number by pattern: +38(###)-###-##-##");
        }
    }
}
