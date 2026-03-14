using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TodoWeb.Data;
using TodoWeb.Models;

namespace TodoWeb.Pages;

/// <summary>
/// ToDoアプリのメインページモデルです。
/// </summary>
public class IndexModel : PageModel
{
    private readonly MyDbContext _context;

    public IndexModel(MyDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// 画面に表示するToDoアイテムのリスト
    /// </summary>
    public List<ToDoItem> TodoList { get; set; } = new();

    /// <summary>
    /// ページ読み込み時の処理。データベースから最新のリストを取得します。
    /// </summary>
    public async Task OnGetAsync()
    {
        // 優先度が高い順（1 > 2 > 3）に並べ替えて取得します
        TodoList = await _context.ToDoItems
            .OrderBy(t => t.Priority)
            .ThenByDescending(t => t.Id)
            .ToListAsync();
    }

    /// <summary>
    /// 新規タスクの追加処理
    /// </summary>
    /// <param name="taskname">タスク名</param>
    /// <param name="priority">優先度 (1=高, 2=中, 3=低)</param>
    public async Task<IActionResult> OnPostAddAsync(string taskname, int priority)
    {
        if (!string.IsNullOrEmpty(taskname))
        {
            _context.ToDoItems.Add(new ToDoItem 
            { 
                Name = taskname, 
                IsDone = false,
                Priority = priority
            });
            await _context.SaveChangesAsync();
        }
        return RedirectToPage();
    }

    /// <summary>
    /// タスクの削除処理
    /// </summary>
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

    /// <summary>
    /// タスクの完了状態更新処理
    /// </summary>
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
