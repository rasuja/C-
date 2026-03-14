using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TodoWeb.Models;
using TodoWeb.Services;

namespace TodoWeb.Pages;

/// <summary>
/// ToDoアプリのメインページモデルです。
/// </summary>
public class IndexModel : PageModel
{
    private readonly FirestoreService _firestoreService;

    public IndexModel(FirestoreService firestoreService)
    {
        _firestoreService = firestoreService;
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
        TodoList = await _firestoreService.GetTodoItemsAsync();
    }

    /// <summary>
    /// 新規タスクの追加処理
    /// </summary>
    /// <param name="taskname">タスク名</param>
    /// <param name="priority">優先度 (1=高, 2=中, 3=低)</param>
    /// <param name="dueDate">期限（日付）</param>
    public async Task<IActionResult> OnPostAddAsync(string taskname, int priority, DateTime? dueDate)
    {
        if (!string.IsNullOrEmpty(taskname))
        {
            await _firestoreService.AddTodoItemAsync(new ToDoItem 
            { 
                Name = taskname, 
                IsDone = false,
                Priority = priority,
                DueDate = dueDate
            });
        }
        return RedirectToPage();
    }

    /// <summary>
    /// タスクの削除処理
    /// </summary>
    public async Task<IActionResult> OnPostDeleteAsync(string id)
    {
        await _firestoreService.DeleteTodoItemAsync(id);
        return RedirectToPage();
    }

    /// <summary>
    /// タスクの完了状態更新処理
    /// </summary>
    public async Task<IActionResult> OnPostDoneAsync(string id)
    {
        await _firestoreService.UpdateTodoItemDoneAsync(id);
        return RedirectToPage();
    }
}
