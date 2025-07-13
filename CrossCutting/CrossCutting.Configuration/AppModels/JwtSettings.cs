namespace CrossCutting.Configuration.AppModels
{
    public class JwtSettings
    {
        public string Chave { get; set; }

        public string Emissor { get; set; }
        
        public string Audiencia { get; set; }
        
        public int ExpiracaoEmMinutos { get; set; }
    }
}
