using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TeamTodoApp.Models;
using TeamTodoApp.Services;

namespace TeamTodoApp.Pages;

public class TaskDetailModel : PageModel
{
    private readonly FirestoreService _firestoreService;

    public ToDoItem TaskItem { get; set; }

    public TaskDetailModel(FirestoreService firestoreService)
    {
        _firestoreService = firestoreService;
    }

    public async Task<IActionResult> OnGetAsync(string id)
    {
        if (string.IsNullOrEmpty(id))
            return RedirectToPage("/Index");

        // FirestoreServiceにGetTodoItemByIdAsyncを追加していない場合は必要
        var tasks = await _firestoreService.GetTodoItemsAsync();
        TaskItem = tasks.FirstOrDefault(t => t.Id == id);

        if (TaskItem == null)
            return RedirectToPage("/Index");

        return Page();
    }

    public async Task<IActionResult> OnPostAsync(string id, string description)
    {
        var tasks = await _firestoreService.GetTodoItemsAsync();
        var item = tasks.FirstOrDefault(t => t.Id == id);
        
        if (item != null)
        {
            item.Description = description ?? "";
            
            await _firestoreService.UpdateTodoItemDescriptionAsync(id, item.Description);

            TempData["Message"] = "保存しました！";
        }

        return RedirectToPage(new { id });
    }
}
