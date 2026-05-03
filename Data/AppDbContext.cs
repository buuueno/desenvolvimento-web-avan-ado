using Microsoft.EntityFrameworkCore;
using GestaoTarefasApi.Models;

namespace GestaoTarefasApi.Data;

public class AppDbContext : DbContext {
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Tarefa> Tarefas => Set<Tarefa>();
    public DbSet<Categoria> Categorias => Set<Categoria>();
    public DbSet<Responsavel> Responsaveis => Set<Responsavel>();

    protected override void OnModelCreating(ModelBuilder modelBuilder) {
        modelBuilder.Entity<Tarefa>(entity => {
            entity.Property(t => t.Nome).HasMaxLength(120).IsRequired();
            entity.Property(t => t.Descricao).HasMaxLength(200);
            entity.Property(t => t.Status).HasMaxLength(20).IsRequired();
        });

        modelBuilder.Entity<Categoria>(entity => {
            entity.Property(c => c.Nome).HasMaxLength(80).IsRequired();
            entity.Property(c => c.Descricao).HasMaxLength(200);
        });

        modelBuilder.Entity<Responsavel>(entity => {
            entity.Property(r => r.Nome).HasMaxLength(100).IsRequired();
            entity.Property(r => r.Email).HasMaxLength(200).IsRequired();
        });
    }
}
