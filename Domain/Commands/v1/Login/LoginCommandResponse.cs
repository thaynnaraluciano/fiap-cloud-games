namespace Domain.Commands.v1.Login
{
    public class LoginCommandResponse
    {
        public LoginCommandResponse(string token)
        {
            Token = token;
        }

        public string Token { get; set; }
    }
}
