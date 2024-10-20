using MediatR;
using ASP.NET_CORE_Project_1.Models;
using Microsoft.AspNetCore.Identity;
using FluentValidation;

namespace ASP.NET_CORE_Project_1.Queries.Users.Handlers
{
    public class ValidateUserNameQueryHandler : IRequestHandler<ValidateUserNameQuery, bool>
    {
        private readonly IValidator<ValidateUserNameQuery> _validator;

        public ValidateUserNameQueryHandler(IValidator<ValidateUserNameQuery> validator)
        {
            _validator = validator;
        }

        public async Task<bool> Handle(ValidateUserNameQuery request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
            {
                return false;
            }

            return true;
        }
    }
}
