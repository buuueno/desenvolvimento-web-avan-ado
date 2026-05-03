namespace GestaoTarefasApi.DTOs.Tarefa;

public class TarefaDto {
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string? Descricao { get; set; }
    public string Status { get; set; } = string.Empty;
}
