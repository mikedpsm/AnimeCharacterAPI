using Microsoft.EntityFrameworkCore;

namespace AnimeCharacterAPI.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public DbSet<AnimeCharacter> AnimeCharacters { get; set; }
    }
}
