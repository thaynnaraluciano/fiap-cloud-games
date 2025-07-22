namespace Infrastructure.Data.Models.Usuarios
{
    public class UsuarioModel
    {
        public Guid Id { get; private set; } = Guid.NewGuid();
        public string Nome { get; private set; }
        public string Email { get; private set; }

        //TO DO - Trocar INT por PerfilUsuarioEnum?
        public int PerfilUsuario { get; private set; }
        public bool Ativo { get; set; } = true;
        public UsuarioModel() { }

        public UsuarioModel(string nome, string email, int perfilUsuario)
        {
            Nome = nome;
            Email = email;
            PerfilUsuario = perfilUsuario;
            Ativo = true;
        }

        public void Atualizar(string nome, string email, int perfilUsuario)
        {
            Nome = nome;
            Email = email;
            PerfilUsuario = perfilUsuario;
        }
    }
}
