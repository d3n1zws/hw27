using ConsoleApp28.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp28.Contexts;

public class TestDbContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=database1;Username=postgres;Password=deniz17;");
        base.OnConfiguring(optionsBuilder);
    }
    public DbSet<Event> Events { get; set; }
    public DbSet<Ticket> Tickets { get; set; }
}
