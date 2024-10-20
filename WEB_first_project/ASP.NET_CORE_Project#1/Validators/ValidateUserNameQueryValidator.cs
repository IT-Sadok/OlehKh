using FluentValidation;
using Microsoft.AspNetCore.Identity;
using ASP.NET_CORE_Project_1.Models;
using ASP.NET_CORE_Project_1.Queries.Users;

public class ValidateUserNameQueryValidator : AbstractValidator<ValidateUserNameQuery>
{
    public ValidateUserNameQueryValidator(UserManager<ApplicationUser> userManager)
    {
        RuleFor(x => x.UserName)
            .NotEmpty().WithMessage("User name must not be empty.")
            .MustAsync(async (userName, cancellation) =>
                await userManager.FindByNameAsync(userName) == null)
            .WithMessage("User name already exists.");
    }
}
