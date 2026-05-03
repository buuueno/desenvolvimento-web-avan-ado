using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GestaoTarefasApi.Data;
using GestaoTarefasApi.Models;
using GestaoTarefasApi.DTOs.Responsavel;

namespace GestaoTarefasApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ResponsaveisController : ControllerBase {
    private readonly AppDbContext _context;

    public ResponsaveisController(AppDbContext context) {
        _context = context;
    }

    // GET api/responsaveis
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ResponsavelDto>>> GetAllAsync() {
        var responsaveis = await _context.Responsaveis
            .AsNoTracking()
            .ToListAsync();

        var result = responsaveis.Select(r => new ResponsavelDto {
            Id = r.Id,
            Nome = r.Nome,
            Email = r.Email,
            Idade = r.Idade
        });

        return Ok(result);
    }

    // GET api/responsaveis/3
    [HttpGet("{id:int}", Name = "GetResponsavelById")]
    public async Task<ActionResult<ResponsavelDto>> GetByIdAsync(int id) {
        var responsavel = await _context.Responsaveis
            .AsNoTracking()
            .FirstOrDefaultAsync(r => r.Id == id);

        if (responsavel is null) return NotFound();

        return Ok(new ResponsavelDto {
            Id = responsavel.Id,
            Nome = responsavel.Nome,
            Email = responsavel.Email,
            Idade = responsavel.Idade
        });
    }

    // POST api/responsaveis  →  201 Created
    [HttpPost]
    public async Task<ActionResult<ResponsavelDto>> CreateAsync(ResponsavelCreateDto dto) {
        var responsavel = new Responsavel {
            Nome = dto.Nome,
            Email = dto.Email,
            Idade = dto.Idade
        };

        _context.Responsaveis.Add(responsavel);
        await _context.SaveChangesAsync();

        return CreatedAtRoute("GetResponsavelById", new { id = responsavel.Id }, new { id = responsavel.Id });
    }

    // PUT api/responsaveis/3  →  204 No Content
    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateAsync(int id, ResponsavelUpdateDto dto) {
        if (id != dto.Id) return BadRequest("Id da URL não confere com o Id do corpo.");

        var responsavel = await _context.Responsaveis.FindAsync(id);
        if (responsavel is null) return NotFound();

        responsavel.Nome = dto.Nome;
        responsavel.Email = dto.Email;
        responsavel.Idade = dto.Idade;

        await _context.SaveChangesAsync();

        return NoContent();
    }

    // DELETE api/responsaveis/3  →  204 No Content
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteAsync(int id) {
        var responsavel = await _context.Responsaveis.FindAsync(id);
        if (responsavel is null) return NotFound();

        _context.Responsaveis.Remove(responsavel);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
