using TeamTodoApp.Services;

var credentialPath = Environment.GetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS") 
    ?? @"C:\Users\S077\Desktop\ポートフォリオ\CSharpPortfolio\TeamTodoApp\portfolio-todo-app-700b4-firebase-adminsdk-fbsvc-484a03b376.json";
Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", credentialPath);

var builder = WebApplication.CreateBuilder(args);

// Cloud Run から割り当てられるポート（デフォルトは8080）を取得してListenする
var port = Environment.GetEnvironmentVariable("PORT") ?? "8080";
builder.WebHost.UseUrls($"http://0.0.0.0:{port}");

// 認証基盤の追加（Cookieベース）
builder.Services.AddAuthentication("Cookies").AddCookie("Cookies", options =>
{
    options.LoginPath = "/Login"; // 未ログイン時の転送先
    options.AccessDeniedPath = "/AccessDenied";
});

// Add services to the container.
builder.Services.AddRazorPages(options =>
{
    options.Conventions.AuthorizeFolder("/"); // 全ページを原則ログイン必須に
    options.Conventions.AllowAnonymousToPage("/Login");
    options.Conventions.AllowAnonymousToPage("/Signup");
});
builder.Services.AddSingleton<FirestoreService>(new FirestoreService("portfolio-todo-app-700b4"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthentication(); // ルーティングの後に配置
app.UseAuthorization();

app.MapStaticAssets();
app.MapRazorPages();
app.Run();
