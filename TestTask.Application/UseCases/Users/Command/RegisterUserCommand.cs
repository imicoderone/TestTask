using MediatR;
using TestTask.Application.Abstractions;

namespace TestTask.Application.UseCases.Users.Command
{
    public class RegisterUserCommand : IRequest
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }

    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, Unit>
    {
        private readonly IIdentityService _identityService;

        public RegisterUserCommandHandler(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        public async Task<Unit> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            await _identityService.Register(request.UserName, request.Password, cancellationToken);

            return Unit.Value;
        }
    }

}
