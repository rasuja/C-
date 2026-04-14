using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TeamTodoApp.Models;
using TeamTodoApp.Services;

namespace TeamTodoApp.Pages;

public class IndexModel : PageModel
{
    private readonly FirestoreService _firestoreService;

    public IndexModel(FirestoreService firestoreService)
    {
        _firestoreService = firestoreService;
    }

    public List<ToDoItem> Tasks { get; set; } = new();

    public async Task OnGetAsync()
    {
        Tasks = await _firestoreService.GetTodoItemsAsync();
    }

    public async Task<IActionResult> OnPostAddAsync(string title, string assigneeAndRole, DateTime dueDate)
    {
        if (string.IsNullOrWhiteSpace(title) || string.IsNullOrWhiteSpace(assigneeAndRole))
        {
            return RedirectToPage();
        }

        // "佐藤（PM）" のような文字列から、名前と役割を分離する処理
        var parts = assigneeAndRole.Split(new[] { '（', '）', '(', ')' }, StringSplitOptions.RemoveEmptyEntries);
        var name = parts.Length > 0 ? parts[0] : "不明";
        var role = parts.Length > 1 ? parts[1] : "不明";

        var newItem = new ToDoItem
        {
            Title = title,
            Assignee = name,
            Role = role,
            Status = "ToDo", // 初期ステータス
            DueDate = dueDate
        };

        await _firestoreService.AddTodoItemAsync(newItem);
        return RedirectToPage();
    }

    public async Task<IActionResult> OnPostUpdateStatusAsync(string id, string newStatus)
    {
        await _firestoreService.UpdateTaskStatusAsync(id, newStatus);
        return RedirectToPage();
    }

    public async Task<IActionResult> OnPostDeleteAsync(string id)
    {
        await _firestoreService.DeleteTodoItemAsync(id);
        return RedirectToPage();
    }
}
