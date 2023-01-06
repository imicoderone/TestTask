using System.Security.Claims;
using MediatR;
using TestTask.Application.Abstractions;

namespace TestTask.Application.UseCases.Users.Command
{
    public class LoginUserCommand : IRequest<ClaimsIdentity>
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }

    public class LoginUserHandler : IRequestHandler<LoginUserCommand, ClaimsIdentity>
    {
        private readonly IIdentityService _identityService;

        public LoginUserHandler(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        public async Task<ClaimsIdentity> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            return await _identityService.Login(request.UserName, request.Password, cancellationToken);
        }
    }
}
