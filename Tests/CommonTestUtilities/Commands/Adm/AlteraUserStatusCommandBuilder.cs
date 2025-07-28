using Domain.Commands.v1.Adm.AlteraStatusUser;

namespace CommonTestUtilities.Commands.Adm
{
    public class AlteraUserStatusCommandBuilder
    {
        public static AlteraUserStatusCommand Build()
        {
            return new AlteraUserStatusCommand
            {
                cGuid = Guid.NewGuid(),
                bStatus = true
            };
        }
    }
}
