using Code420.UIOrchestrator.Core.Models.AuthP;
using FluentValidation;

namespace Code420.UIOrchestrator.Server.Validators
{
    public class LoginUserModelValidator : AbstractValidator<LoginUserModel>
    {
        public LoginUserModelValidator()
        {
            RuleFor(x => x.Email)
                .Cascade(CascadeMode.Stop)
                .NotNull()
                .NotEmpty()
                .EmailAddress()
                .MaximumLength(Core.Models.UIOrchestratorConstants.UIOrchestratorConstants.EmailSize)
                .WithName("Email Address")
                .WithMessage("An valid {PropertyName} is required.");

            RuleFor(x => x.Password)
                .Cascade(CascadeMode.Stop)
                .NotNull()
                .NotEmpty()
                .WithMessage("The {PropertyName} can not be empty or contain spaces.");
        }
    }
}
