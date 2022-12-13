using Microsoft.EntityFrameworkCore;
using ProEventos.API.Model;

namespace ProEventos.API.Context
{
    public class ProEventosContext : DbContext
    {
        public ProEventosContext(DbContextOptions<ProEventosContext> options) : base(options)
        {

        }

        public DbSet<Evento> Eventos { get; set; }
    }
}
