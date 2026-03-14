using Microsoft.EntityFrameworkCore;
using TodoWeb.Data;

var builder = WebApplication.CreateBuilder(args);

// 1. Razor Pages を使うためのサービスを追加
builder.Services.AddRazorPages();

builder.Services.AddDbContext<MyDbContext>(options => options.UseSqlite("Data Source=todo.db"));


var app = builder.Build();

// Razor Pages へのルーティングを有効にする
app.MapRazorPages();

app.Run();

