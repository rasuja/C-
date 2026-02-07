// Challenge 6: List of ToDo Objects
Console.WriteLine("タスク名を入力：");

List<ToDo> task = new List<ToDo>();

while (true)
{
    string input = Console.ReadLine();
    if (input == "exit")
    {
        break;
    }
    ToDo item = new ToDo();
    item.Name = input;
    item.IsDone = false;
    task.Add(item);
}

Console.WriteLine("--- 登録一覧 ---");

foreach (ToDo t in task)
{
    string status = t.IsDone ? "完了" : "未完了";
    Console.WriteLine($"・{t.Name}[{status}]");
}

class ToDo
{
    public string Name { get; set; }
    public bool IsDone { get; set; }
}
