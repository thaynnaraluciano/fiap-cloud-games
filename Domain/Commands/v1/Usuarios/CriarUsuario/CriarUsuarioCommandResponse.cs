namespace Domain.Commands.v1.Usuarios.CriarUsuario
{
    public class CriarUsuarioCommandResponse
    {
        public Guid Id { get; set; }

        public string? Nome { get; set; }

        public string? Email { get; set; }
        
        public int PerfilUsuario { get; set; }
    }
}
