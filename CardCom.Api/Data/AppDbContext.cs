
using CardCom.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace CardCom.Api.Data;

public class AppDbContext: DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options):base(options) {}

    public DbSet<Card> Cards {get; set;}

    public DbSet<User> Users {get; set;}

    public DbSet<Pack> Packs {get; set;}

    public DbSet<Collection> Collections {get; set;}
}