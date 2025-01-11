using Microsoft.EntityFrameworkCore;

namespace LawsuitM.Models;

public class LawsuitContext : DbContext
{
    public LawsuitContext(DbContextOptions<LawsuitContext> options)
        : base(options)
    {
    }

    public DbSet<LawsuitItem> LawsuitItem { get; set; } = null!;
}