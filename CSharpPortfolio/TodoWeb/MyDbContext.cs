using Microsoft.EntityFrameworkCore;

public class MyDbContext : DbContext
{
    public MyDbContext(DbContextOptions<MyDbContext> options) : base(options) { }
    public DbSet<ToDoItem> ToDoItems { get; set; }

}

public class ToDoItem
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public bool IsDone { get; set; }
}

