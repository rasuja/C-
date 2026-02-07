// Challenge 5: Basic Class implementation
ToDo task1 = new ToDo();
task1.Name = "C#の勉強";
task1.IsDone = false;

Console.WriteLine($"タスク：{task1.Name}");
Console.WriteLine($"ステータス：{task1.IsDone}");

class ToDo
{
    public string Name;
    public bool IsDone;
}
