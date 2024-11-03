using System.Reflection;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TodoList.Models.Entities;

namespace TodoList.DataAccess.Contexts;

public class BaseDbContext : IdentityDbContext<User,IdentityRole, string>
{
    public BaseDbContext(DbContextOptions opt): base(opt)
    {
    
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
    
    public DbSet<Category> Categories { get; set; }
    public DbSet<Todo> Todos { get; set; }
}