var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

// 1. データを保存するリスト
List<ToDo> todoList = new List<ToDo>
{
    new ToDo("C# Webの勉強", false),
    new ToDo("Todoアプリ作成", false)
};

app.MapGet("/", () =>
{
    string css = @"
    <style>
        body { font-family: 'Segoe UI', Meiryo, sans-serif; background: #f4f7f6; padding: 40px; }
        .container { background: white; padding: 20px; border-radius: 12px; box-shadow: 0 4px 12px rgba(0,0,0,0.1); max-width: 400px; }
        h1 { color: #2c3e50; border-bottom: 2px solid #3498db; padding-bottom: 10px; }
        ul { list-style: none; padding: 0; }
        li { padding: 12px; border-bottom: 1px solid #eee; display: flex; align-items: center; }
        li:last-child { border-bottom: none; }
        .icon { margin-right: 12px; font-size: 1.2em; }
        form { margin-bottom: 20px; display: flex; gap: 10px; }
        input { padding: 8px; flex: 1; border: 1px solid #ddd; border-radius: 4px; }
        button { padding: 8px 16px; background: #3498db; color: white; border: none; border-radius: 4px; cursor: pointer; }
        .task { flex: 1; }
        .actions { display: flex; gap: 8px; }
        .done { color: #aaa; text-decoration: line-through; }
    </style>";

    string html = css + $@"
<div class='container'>
    <h1>Todoアプリ</h1>
    <p class='task-count'>現在のタスク数：{todoList.Count}</p>
    <form action='/add' method='POST'>
        <input type='text' name='taskname' placeholder='新しいタスクを入力' required>
        <button type='submit'>追加</button>
    </form>
    <ul>";

    for (int i = 0; i < todoList.Count; i++)
    {
        var t = todoList[i];
        string status = t.IsDone ? "✅" : "◎";
        html += $@"
<li>
    <span class='icon'>{status}</span>
    <span class='task {(t.IsDone ? "done" : "")}'>{t.Name}</span>
    <div class='actions'>";

        if (!t.IsDone)
        {
            html += $@"
        <form action='/done' method='POST'>
            <input type='hidden' name='index' value='{i}'>
            <button type='submit'>完了</button>
        </form>";
        }

        html += $@"
        <form action='/delete' method='POST'>
            <input type='hidden' name='index' value='{i}'>
            <button type='submit'>削除</button>
        </form>
    </div>
</li>";
    }

    html += "</ul></div>";
    return Results.Content(html, "text/html", System.Text.Encoding.UTF8);
});

app.MapPost("/add", async (HttpContext context) =>
{
    var form = await context.Request.ReadFormAsync();
    string? taskname = form["taskname"];
    if (!string.IsNullOrEmpty(taskname)) todoList.Add(new ToDo(taskname, false));
    return Results.Redirect("/");
});

app.MapPost("/delete", async (HttpContext context) =>
{
    var form = await context.Request.ReadFormAsync();
    int index = int.Parse(form["index"]);
    if (index >= 0 && index < todoList.Count) todoList.RemoveAt(index);
    return Results.Redirect("/");
});

app.MapPost("/done", async (HttpContext context) =>
{
    var form = await context.Request.ReadFormAsync();
    int index = int.Parse(form["index"]);
    if (index >= 0 && index < todoList.Count) todoList[index] = todoList[index] with { IsDone = true };
    return Results.Redirect("/");
});

app.Run();

public record ToDo(string Name, bool IsDone);
