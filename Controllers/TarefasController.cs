using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GestaoTarefasApi.Data;
using GestaoTarefasApi.Models;
using GestaoTarefasApi.DTOs.Tarefa;

namespace GestaoTarefasApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TarefasController : ControllerBase {
    private readonly AppDbContext _context;

    public TarefasController(AppDbContext context) {
        _context = context;
    }

    // GET api/tarefas
    [HttpGet]
    public async Task<ActionResult<IEnumerable<TarefaDto>>> GetAllAsync() {
        var tarefas = await _context.Tarefas
            .AsNoTracking()
            .ToListAsync();

        var result = tarefas.Select(t => new TarefaDto {
            Id = t.Id,
            Nome = t.Nome,
            Descricao = t.Descricao,
            Status = t.Status
        });

        return Ok(result);
    }

    // GET api/tarefas/5
    [HttpGet("{id:int}", Name = "GetTarefaById")]
    public async Task<ActionResult<TarefaDto>> GetByIdAsync(int id) {
        var tarefa = await _context.Tarefas
            .AsNoTracking()
            .FirstOrDefaultAsync(t => t.Id == id);

        if (tarefa is null) return NotFound();

        return Ok(new TarefaDto {
            Id = tarefa.Id,
            Nome = tarefa.Nome,
            Descricao = tarefa.Descricao,
            Status = tarefa.Status
        });
    }

    // POST api/tarefas  →  201 Created
    [HttpPost]
    public async Task<ActionResult<TarefaDto>> CreateAsync(TarefaCreateDto dto) {
        var tarefa = new Tarefa {
            Nome = dto.Nome,
            Descricao = dto.Descricao,
            Status = dto.Status
        };

        _context.Tarefas.Add(tarefa);
        await _context.SaveChangesAsync();

        return CreatedAtRoute("GetTarefaById", new { id = tarefa.Id }, new { id = tarefa.Id });
    }

    // PUT api/tarefas/5  →  204 No Content
    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateAsync(int id, TarefaUpdateDto dto) {
        if (id != dto.Id) return BadRequest("Id da URL não confere com o Id do corpo.");

        var tarefa = await _context.Tarefas.FindAsync(id);
        if (tarefa is null) return NotFound();

        tarefa.Nome = dto.Nome;
        tarefa.Descricao = dto.Descricao;
        tarefa.Status = dto.Status;

        await _context.SaveChangesAsync();

        return NoContent();
    }

    // DELETE api/tarefas/5  →  204 No Content
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteAsync(int id) {
        var tarefa = await _context.Tarefas.FindAsync(id);
        if (tarefa is null) return NotFound();

        _context.Tarefas.Remove(tarefa);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
