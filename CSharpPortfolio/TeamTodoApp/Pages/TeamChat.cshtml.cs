using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TeamTodoApp.Models;
using TeamTodoApp.Services;
using System.Text.RegularExpressions;
using System.Web;

namespace TeamTodoApp.Pages;

public class TeamChatModel : PageModel
{
    private readonly FirestoreService _firestoreService;

    public TeamChatModel(FirestoreService firestoreService)
    {
        _firestoreService = firestoreService;
    }

    public List<ChatMessage> Messages { get; set; } = new();
    public string CurrentUserName { get; set; } = "";

    public async Task OnGetAsync()
    {
        CurrentUserName = HttpContext.User.Identity?.Name ?? "";
        Messages = await _firestoreService.GetChatMessagesAsync();
    }

    public async Task<IActionResult> OnPostAsync(string messageText)
    {
        if (string.IsNullOrWhiteSpace(messageText))
        {
            return RedirectToPage();
        }

        var senderName = HttpContext.User.Identity?.Name ?? "不明";
        
        var newMessage = new ChatMessage
        {
            SenderId = senderName, 
            SenderName = senderName,
            MessageText = messageText,
            CreatedAt = DateTime.UtcNow
        };

        await _firestoreService.AddChatMessageAsync(newMessage);

        return RedirectToPage();
    }

    public string RenderMessage(string rawText)
    {
        // まずHTMLエンコードしてXSS（スクリプトインジェクション）を防ぐ
        var encoded = HttpUtility.HtmlEncode(rawText ?? "");

        // @ユーザー名 を正規表現で抽出（漢字・ひらがな・カタカナ・英数字に対応）
        // ユーザー名にスペースが含まれない前提で、スペースや句読点が来るまでを名前にします
        var regex = new Regex(@"@([a-zA-Z0-9_\u3040-\u309F\u30A0-\u30FF\u4E00-\u9FFF]+)");
        
        var result = regex.Replace(encoded, match => 
        {
            var username = match.Groups[1].Value;
            
            // 自分宛てかどうかで色を変える
            var isMe = username == CurrentUserName;
            var badgeClass = isMe ? "badge bg-danger text-white" : "badge bg-info text-dark"; 
            
            return $"<span class=\"{badgeClass}\">@{username}</span>";
        });

        // 改行を改行タグに変換
        return result.Replace("\r\n", "<br />").Replace("\n", "<br />");
    }
}
