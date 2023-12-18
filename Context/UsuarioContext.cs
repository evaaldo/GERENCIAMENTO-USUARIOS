using GerenciamentoUsuarios.Models;
using Microsoft.EntityFrameworkCore;

namespace GerenciamentoUsuarios.Context.UsuarioContext
{
    public class UsuarioContext : DbContext
    {
        public UsuarioContext(DbContextOptions<UsuarioContext> options) : base(options)
        {

        }

        public DbSet<Usuario> Usuarios { get; set; }
    }
}