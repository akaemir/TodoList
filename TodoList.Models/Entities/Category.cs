using Core.Entities;

namespace TodoList.Models.Entities;

public sealed class Category : Entity<int>
{
    public string Name { get; set; }
    public List<Todo> Todos { get; set; }
}