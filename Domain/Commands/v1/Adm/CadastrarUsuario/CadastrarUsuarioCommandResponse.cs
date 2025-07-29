using Domain.Enums;

namespace Domain.Commands.v1.Adm.CadastrarUsuario
{
    public class CadastrarUsuarioCommandResponse
    {
        public Guid Id { get; set; }

        public string? Nome { get; set; }

        public string? Email { get; set; }
        
        public int PerfilUsuario { get; set; }

        public bool Ativo { get; set; }
    }
}
