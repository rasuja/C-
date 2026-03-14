using Microsoft.EntityFrameworkCore;
using TodoWeb.Models;

namespace TodoWeb.Data;

public class MyDbContext : DbContext
{
    public MyDbContext(DbContextOptions<MyDbContext> options) : base(options) { }
    public DbSet<ToDoItem> ToDoItems { get; set; }

}

