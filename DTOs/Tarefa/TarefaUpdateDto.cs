using System.ComponentModel.DataAnnotations;

namespace GestaoTarefasApi.DTOs.Tarefa;

public class TarefaUpdateDto {

    [Required]
    public int Id { get; set; }

    [Required(ErrorMessage = "Nome é obrigatório.")]
    [MinLength(3, ErrorMessage = "Nome deve ter entre 3 e 120 caracteres.")]
    [MaxLength(120, ErrorMessage = "Nome deve ter entre 3 e 120 caracteres.")]
    public string Nome { get; set; } = string.Empty;

    [MaxLength(200, ErrorMessage = "Descrição deve ter no máximo 200 caracteres.")]
    public string? Descricao { get; set; }

    [Required(ErrorMessage = "Status é obrigatório.")]
    [RegularExpression("Pendente|EmAndamento|Concluida",
        ErrorMessage = "Status deve ser: Pendente, EmAndamento ou Concluida.")]
    public string Status { get; set; } = string.Empty;
}
