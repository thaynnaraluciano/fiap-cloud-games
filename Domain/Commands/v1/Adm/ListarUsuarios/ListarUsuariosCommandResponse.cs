namespace Domain.Commands.v1.Adm.ListarUsuarios
{
    public class ListarUsuariosCommandResponse
    {
        public Guid Id { get; set; }

        public string? Nome { get; set; }

        public string? Email { get; set; }

        public int PerfilUsuario { get; set; }
    }
}
