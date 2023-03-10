using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace EfcDataAccess;

public class EchoContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Subforum> Subforums { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source = ../EfcDataAccess/Echo.db");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>().HasKey(user => user.Id);
        modelBuilder.Entity<Subforum>().HasKey(sf => sf.Id);
    }
}