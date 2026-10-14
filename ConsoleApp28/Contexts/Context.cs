using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using ConsoleApp28.Models;

namespace ConsoleApp28.Contexts;

public class Context : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        optionsBuilder.UseSqlServer("Server=localhost\\SQLEXPRESS;Database=task8;Trusted_Connection=true;TrustServerCertificate=true;");
    }
    public DbSet<Event> Events { get; set; }
    public DbSet<Ticket> Tickets { get; set; }
}
