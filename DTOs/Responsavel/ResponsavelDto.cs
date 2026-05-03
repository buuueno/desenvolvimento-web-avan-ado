namespace GestaoTarefasApi.DTOs.Responsavel;

public class ResponsavelDto {
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public int Idade { get; set; }
}
