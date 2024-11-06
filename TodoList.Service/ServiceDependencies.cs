using System.Reflection;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using TodoList.Service.Abstracts;
using TodoList.Service.Concretes;
using TodoList.Service.Rules;

namespace TodoList.Service;

public static class ServiceDependencies
{
    public static IServiceCollection AddServiceDependenies(this IServiceCollection services)
    {
        services.AddScoped<ITodoService, TodoService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IRoleService, RoleService>();
        services.AddScoped<IAuthenticationService, AuthenticationService>();
        services.AddScoped<IJwtService, JwtService>();
        services.AddScoped<ICategoryService, CategoryService>();
        services.AddScoped<TodoBusinessRules>();
        services.AddScoped<CategoryBusinessRules>();
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddFluentValidationAutoValidation();
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        return services;
    }
}