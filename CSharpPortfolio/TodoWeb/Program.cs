using TodoWeb.Services;

// コンテナ環境（またはローカル）で設定されるクレデンシャルパスを読み込み
// （ローカル開発では絶対パス、コンテナ環境では相対パス等になることを考慮）
var credentialPath = Environment.GetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS") 
    ?? @"C:\Users\S077\Desktop\ポートフォリオ\CSharpPortfolio\TodoWeb\portfolio-todo-app-700b4-firebase-adminsdk-fbsvc-9ee19d2e80.json";
Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", credentialPath);

var builder = WebApplication.CreateBuilder(args);

// Cloud Run から割り当てられるポート（デフォルトは8080）を取得してListenする
var port = Environment.GetEnvironmentVariable("PORT") ?? "8080";
builder.WebHost.UseUrls($"http://0.0.0.0:{port}");

// 1. Razor Pages を使うためのサービスを追加
builder.Services.AddRazorPages();

// 2. FirestoreSerivce をアプリ全体で使えるように登録（DIコンテナ）
builder.Services.AddSingleton<FirestoreService>(new FirestoreService("portfolio-todo-app-700b4"));

var app = builder.Build();

// Razor Pages へのルーティングを有効にする
app.MapRazorPages();

app.Run();

