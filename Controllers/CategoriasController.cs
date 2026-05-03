using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GestaoTarefasApi.Data;
using GestaoTarefasApi.Models;
using GestaoTarefasApi.DTOs.Categoria;

namespace GestaoTarefasApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoriasController : ControllerBase {
    private readonly AppDbContext _context;

    public CategoriasController(AppDbContext context) {
        _context = context;
    }

    // GET api/categorias
    [HttpGet]
    public async Task<ActionResult<IEnumerable<CategoriaDto>>> GetAllAsync() {
        var categorias = await _context.Categorias
            .AsNoTracking()
            .ToListAsync();

        var result = categorias.Select(c => new CategoriaDto {
            Id = c.Id,
            Nome = c.Nome,
            Descricao = c.Descricao
        });

        return Ok(result);
    }

    // GET api/categorias/2
    [HttpGet("{id:int}", Name = "GetCategoriaById")]
    public async Task<ActionResult<CategoriaDto>> GetByIdAsync(int id) {
        var categoria = await _context.Categorias
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.Id == id);

        if (categoria is null) return NotFound();

        return Ok(new CategoriaDto {
            Id = categoria.Id,
            Nome = categoria.Nome,
            Descricao = categoria.Descricao
        });
    }

    // POST api/categorias  →  201 Created
    [HttpPost]
    public async Task<ActionResult<CategoriaDto>> CreateAsync(CategoriaCreateDto dto) {
        var categoria = new Categoria {
            Nome = dto.Nome,
            Descricao = dto.Descricao
        };

        _context.Categorias.Add(categoria);
        await _context.SaveChangesAsync();

        return CreatedAtRoute("GetCategoriaById", new { id = categoria.Id }, new { id = categoria.Id });
    }

    // PUT api/categorias/2  →  204 No Content
    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateAsync(int id, CategoriaUpdateDto dto) {
        if (id != dto.Id) return BadRequest("Id da URL não confere com o Id do corpo.");

        var categoria = await _context.Categorias.FindAsync(id);
        if (categoria is null) return NotFound();

        categoria.Nome = dto.Nome;
        categoria.Descricao = dto.Descricao;

        await _context.SaveChangesAsync();

        return NoContent();
    }

    // DELETE api/categorias/2  →  204 No Content
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteAsync(int id) {
        var categoria = await _context.Categorias.FindAsync(id);
        if (categoria is null) return NotFound();

        _context.Categorias.Remove(categoria);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
