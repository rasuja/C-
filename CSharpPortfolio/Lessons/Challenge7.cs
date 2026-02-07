// Challenge 7: Updating Data (CRUD - Update)
Console.WriteLine("test");

List<ToDo> task = new List<ToDo>();

task.Add(new ToDo { Name = "買い物", IsDone = false });
task.Add(new ToDo { Name = "掃除", IsDone = false });
task.Add(new ToDo { Name = "勉強", IsDone = false });

for (int i = 0; i < task.Count; i++)
{
    Console.WriteLine($"[{i}] {task[i].Name}");
}

Console.WriteLine("完了にする番号を入力してください：");

string input = Console.ReadLine();
int index = int.Parse(input);
task[index].IsDone = true;

Console.WriteLine("--- 更新後のタスク ---");

for (int i = 0; i < task.Count; i++)
{
    Console.WriteLine($"[{i}] {task[i].Name}({task[i].IsDone})");
}

class ToDo
{
    public string Name { get; set; }
    public bool IsDone { get; set; }
}
