using TeamTodoApp.Services;

var credentialPath = Environment.GetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS") 
    ?? @"C:\Users\S077\Desktop\ポートフォリオ\CSharpPortfolio\TeamTodoApp\portfolio-todo-app-700b4-firebase-adminsdk-fbsvc-484a03b376.json";
Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", credentialPath);

var builder = WebApplication.CreateBuilder(args);

// Cloud Run から割り当てられるポート（デフォルトは8080）を取得してListenする
var port = Environment.GetEnvironmentVariable("PORT") ?? "8080";
builder.WebHost.UseUrls($"http://0.0.0.0:{port}");

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddSingleton<FirestoreService>(new FirestoreService("portfolio-todo-app-700b4"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();
app.MapStaticAssets();
app.MapRazorPages();
app.Run();
