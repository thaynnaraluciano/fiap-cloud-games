namespace Domain.Commands.v1.Biblioteca.ComprarJogo;
public class ComprarJogoCommandResponse
{
    public bool Sucesso { get; set; }
    public string? Mensagem { get; set; }
    public DateTime? DtAdquirido { get; set; }
}