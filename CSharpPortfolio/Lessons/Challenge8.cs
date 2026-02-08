// Challenge 8: Removing Data (CRUD - Delete)
Console.WriteLine("---現在のタスク---");

List<ToDo> task = new List<ToDo>();

task.Add(new ToDo("買い物", false));
task.Add(new ToDo("掃除", false));
task.Add(new ToDo("勉強", false));

for (int i = 0; i < task.Count; i++)
{
    Console.WriteLine($"[{i}] {task[i].Task}");
}

Console.WriteLine("削除する番号を入力してください");
string input = Console.ReadLine();
int index = int.Parse(input);
task.RemoveAt(index);

Console.WriteLine("---削除後のタスク---");

for (int i = 0; i < task.Count; i++)
{
    Console.WriteLine($"[{i}]" + task[i].Task);
}

class ToDo
{
    public string Task { get; set; }
    public bool IsDone { get; set; }

    public ToDo(string Task, bool IsDone)
    {
        this.Task = Task;
        this.IsDone = IsDone;
    }
}
