using Microsoft.EntityFrameworkCore;

namespace BlackJack.Models
{
    public class BlackJackContext : DbContext
    {
        public BlackJackContext(DbContextOptions<BlackJackContext> options)
            : base(options)
        {
        }

        public DbSet<Jogo> Jogos { get; set; }
    }
}