using MediatR;
using System;

namespace ASP.NET_CORE_Project_1.Commands.Users
{
    public class DeleteUserCommand : IRequest<(bool IsSuccess, string ErrorMessage)>
    {
        public Guid UserId { get; set; }
        public string CurrentUserId { get; set; }
        public bool IsAdmin { get; set; }

        public DeleteUserCommand(Guid userId, string currentUserId, bool isAdmin)
        {
            UserId = userId;
            CurrentUserId = currentUserId;
            IsAdmin = isAdmin;
        }
    }
}
