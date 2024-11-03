namespace TodoList.Models.Entities;

public class Category
{
    public int Id { get; set; }
    public string Name { get; set; }
    public List<Todo> Todos { get; set; }
}