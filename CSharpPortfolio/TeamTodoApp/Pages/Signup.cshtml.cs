using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TeamTodoApp.Models;
using TeamTodoApp.Services;

namespace TeamTodoApp.Pages;

public class SignupModel : PageModel
{
    private readonly FirestoreService _firestoreService;

    public string ErrorMessage { get; set; }

    public SignupModel(FirestoreService firestoreService)
    {
        _firestoreService = firestoreService;
    }

    public void OnGet()
    {
    }

    public async Task<IActionResult> OnPostAsync(string username, string displayName, string role, string password)
    {
        if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(displayName))
        {
            ErrorMessage = "すべての項目を入力してください。";
            return Page();
        }

        // 既に存在するユーザーIDかチェック
        var existingUser = await _firestoreService.GetUserByIdAsync(username);
        if (existingUser != null)
        {
            ErrorMessage = "このユーザーIDは既に使われています。別のIDを指定してください。";
            return Page();
        }

        // BCryptを使用してパスワードを安全にハッシュ化する
        string hashedPassword = BCrypt.Net.BCrypt.EnhancedHashPassword(password);

        var newUser = new AppUser
        {
            Id = username,
            DisplayName = displayName,
            Role = role,
            PasswordHash = hashedPassword,
            IsAdmin = role == "PM" // シンプルにPMなら自動でAdmin扱いにする等
        };

        await _firestoreService.CreateUserAsync(newUser);

        // 登録完了後にログイン画面へ誘導（あるいはここで自動ログインさせることも可能）
        return RedirectToPage("/Login", new { registered = true });
    }
}
