// Challenge 8: Removing Data (CRUD - Delete)
Console.WriteLine("タスク名を入力してください（exitで終了）");

List<string> tasks = new List<string>();

while (true)
{
    string? input = Console.ReadLine();

    if (input == null)
        break;

    if (input.Equals("exit", StringComparison.OrdinalIgnoreCase))
        break;

    if (!string.IsNullOrWhiteSpace(input))
        tasks.Add(input);
}

foreach (string t in tasks)
{
    Console.WriteLine(t);
}
