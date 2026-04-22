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
    public List<AppUser> AllUsers { get; set; } = new();

    public async Task OnGetAsync()
    {
        Tasks = await _firestoreService.GetTodoItemsAsync();
        AllUsers = await _firestoreService.GetAllUsersAsync();
    }

    public async Task<IActionResult> OnPostAddAsync(string title, DateTime dueDate, string assigneeId)
    {
        if (string.IsNullOrWhiteSpace(title))
        {
            return RedirectToPage();
        }

        var loggedInUserName = HttpContext.User.Identity?.Name ?? "不明";
        var loggedInUserRole = HttpContext.User.Claims.FirstOrDefault(c => c.Type == System.Security.Claims.ClaimTypes.Role)?.Value ?? "メンバー";

        string targetAssigneeName = loggedInUserName;
        string targetAssigneeRole = loggedInUserRole;

        // PMかリーダーで、かつassigneeIdが送信されている場合は対象ユーザーを割り当てる
        if ((loggedInUserRole == "PM" || loggedInUserRole == "リーダー") && !string.IsNullOrWhiteSpace(assigneeId))
        {
            var assignedUser = await _firestoreService.GetUserByIdAsync(assigneeId);
            if (assignedUser != null)
            {
                targetAssigneeName = assignedUser.DisplayName ?? assignedUser.Id;
                targetAssigneeRole = assignedUser.Role;
            }
        }

        var newItem = new ToDoItem
        {
            Title = title,
            Assignee = targetAssigneeName,
            Role = targetAssigneeRole,
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
