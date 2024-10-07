using MediatR;
using ASP.NET_CORE_Project_1.DTO;

namespace ASP.NET_CORE_Project_1.Commands.Users
{
    public class UpdateUserCommand : IRequest<(bool IsSuccess, string ErrorMessage)>
    {
        public Guid UserId { get; set; }
        public UpdateUserModel Model { get; set; }
        public string CurrentUserId { get; set; }
        public bool IsAdmin { get; set; }

        public UpdateUserCommand(Guid userId, UpdateUserModel model, string currentUserId, bool isAdmin)
        {
            UserId = userId;
            Model = model;
            CurrentUserId = currentUserId;
            IsAdmin = isAdmin;
        }
    }
}
