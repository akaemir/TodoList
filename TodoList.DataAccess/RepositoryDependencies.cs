using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TodoList.DataAccess.Abstracts;
using TodoList.DataAccess.Concretes;
using TodoList.DataAccess.Contexts;

namespace TodoList.DataAccess;

public static class RepositoryDependencies
{
    public static IServiceCollection AddRepositoryDepencdencies(this IServiceCollection services,IConfiguration configuration)
    {
        services.AddScoped<IToDoRepository, EfTodoRepository>();
        services.AddScoped<ICategoryRepository, EfCategoryRepository>();
        services.AddDbContext<BaseDbContext>(opt => opt.UseSqlServer(configuration.GetConnectionString("SqlConnection")));
        return services;
    }
}