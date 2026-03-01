using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

public class IndexModel : PageModel
{
    private readonly MyDbContext _context;

    public IndexModel(MyDbContext context)
    {
        _context = context;
    }

    public List<ToDoItem> TodoList { get; set; } = new();

    public async Task OnGetAsync()
    {
        // データベースからすべてのタスクを取得します
        TodoList = await _context.ToDoItems.ToListAsync();
    }

    public async Task<IActionResult> OnPostAddAsync(string taskname)
    {
        if (!string.IsNullOrEmpty(taskname))
        {
            _context.ToDoItems.Add(new ToDoItem { Name = taskname, IsDone = false });
            await _context.SaveChangesAsync();
        }
        return RedirectToPage();
    }

    public async Task<IActionResult> OnPostDeleteAsync(int id)
    {
        var item = await _context.ToDoItems.FindAsync(id);
        if (item != null)
        {
            _context.ToDoItems.Remove(item);
            await _context.SaveChangesAsync();
        }
        return RedirectToPage();
    }

    public async Task<IActionResult> OnPostDoneAsync(int id)
    {
        var item = await _context.ToDoItems.FindAsync(id);
        if (item != null)
        {
            item.IsDone = true;
            await _context.SaveChangesAsync();
        }
        return RedirectToPage();
    }
}
