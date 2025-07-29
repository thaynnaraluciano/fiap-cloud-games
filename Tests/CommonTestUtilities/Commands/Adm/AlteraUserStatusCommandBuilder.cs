using Bogus;
using Domain.Commands.v1.Adm.AlteraStatusUser;

namespace CommonTestUtilities.Commands.Adm
{
    public class AlteraUserStatusCommandBuilder
    {
        public static AlteraUserStatusCommand Build()
        {
            return new Faker<AlteraUserStatusCommand>()
                .RuleFor(c => c.cGuid, f => Guid.NewGuid())
                .RuleFor(c => c.bStatus, f => f.Random.Bool());
        }
    }
}
