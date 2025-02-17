using System;
using Crud_test.Models;
using Microsoft.EntityFrameworkCore;

namespace Crud_test.Data;

public class ProductContext : DbContext
{
    public ProductContext(DbContextOptions<ProductContext> options) : base(options)
    {
    }
    public DbSet<Users> Users { get; set; }
    public DbSet<Product> Products { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Users>().HasData(
            new { Id = 1, Username = "admin", Password = "123456" },
            new { Id = 2, Username = "user", Password = "username" }
        );
    }
}
