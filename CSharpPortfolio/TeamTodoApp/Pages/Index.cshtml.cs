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

    public async Task<IActionResult> OnPostAddAsync(string title, DateTime dueDate)
    {
        if (string.IsNullOrWhiteSpace(title))
        {
            return RedirectToPage();
        }

        var loggedInUserName = HttpContext.User.Identity?.Name ?? "不明";
        var loggedInUserRole = HttpContext.User.Claims.FirstOrDefault(c => c.Type == System.Security.Claims.ClaimTypes.Role)?.Value ?? "メンバー";

        var newItem = new ToDoItem
        {
            Title = title,
            Assignee = loggedInUserName,
            Role = loggedInUserRole,
            Status = "ToDo", // 初期ステータス
            DueDate = dueDate,
            Description = "" // 初期値
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
