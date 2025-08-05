namespace Domain.Commands.v1.Usuarios.BuscarUsuarioPorId
{
    public class BuscarUsuarioPorIdCommandResponse
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public int PerfilUsuario { get; set; }

        public DateTime? ConfirmadoEm { get; set; }
    }
}
