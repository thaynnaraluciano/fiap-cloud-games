using Bogus;
using Domain.Commands.v1.Login;

namespace CommonTestUtilities.Commands
{
    public class LoginCommandBuilder
    {
        public static LoginCommand Build()
        {
            return new Faker<LoginCommand>()
                .RuleFor(r => r.Email, faker => faker.Internet.Email())
                .RuleFor(r => r.Senha, faker => faker.Random.String(8));
        }
    }
}
