using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;
using TeamTodoApp.Services;

namespace TeamTodoApp.Pages;

public class LoginModel : PageModel
{
    private readonly FirestoreService _firestoreService;

    public string ErrorMessage { get; set; }
    public bool IsRegistered { get; set; }

    public LoginModel(FirestoreService firestoreService)
    {
        _firestoreService = firestoreService;
    }

    public void OnGet(bool registered = false)
    {
        IsRegistered = registered;
    }

    public async Task<IActionResult> OnPostAsync(string username, string password)
    {
        if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
        {
            ErrorMessage = "ユーザーIDとパスワードを入力してください。";
            return Page();
        }

        var user = await _firestoreService.GetUserByIdAsync(username);

        if (user == null || !BCrypt.Net.BCrypt.EnhancedVerify(password, user.PasswordHash))
        {
            ErrorMessage = "ユーザーIDまたはパスワードが間違っています。";
            return Page();
        }

        // ログイン成功：クッキーにユーザー情報を保存（Claims方式）
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id),
            new Claim(ClaimTypes.Name, user.DisplayName),
            new Claim(ClaimTypes.Role, user.Role),
            new Claim("IsAdmin", user.IsAdmin.ToString())
        };

        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        var authProperties = new AuthenticationProperties
        {
            IsPersistent = true, // ブラウザを閉じてもログイン維持するか
            ExpiresUtc = DateTimeOffset.UtcNow.AddDays(7)
        };

        await HttpContext.SignInAsync(
            CookieAuthenticationDefaults.AuthenticationScheme, 
            new ClaimsPrincipal(claimsIdentity), 
            authProperties);

        return RedirectToPage("/Index");
    }
}
