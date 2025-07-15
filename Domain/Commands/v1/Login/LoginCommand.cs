using MediatR;

namespace Domain.Commands.v1.Login
{
    public class LoginCommand : IRequest<LoginCommandResponse>
    {
        public string? Email { get; set; }

        public string? Senha { get; set; }
    }
}
