using Microsoft.EntityFrameworkCore;
var builder = WebApplication.CreateBuilder(args);

// 1. Razor Pages を使うためのサービスを追加
builder.Services.AddRazorPages();

builder.Services.AddDbContext<MyDbContext>(options => options.UseSqlite("Data Source=todo.db"));


var app = builder.Build();

// 2. Razor Pages への道（ルーティング）を有効にする
app.MapRazorPages();






// 一時的に古いリクエスト（/add など）を残しても良いですが、
// Razor Pagesへ移行するので一旦スッキリさせます。
app.Run();
