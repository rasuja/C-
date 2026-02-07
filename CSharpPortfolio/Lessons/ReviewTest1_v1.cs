// Review Test 1 Attempt (List of strings version)
Console.WriteLine("タスク名を入力してください（exitで終了）");

List<string> task = new List<string>();

while (true)
{
    string input = Console.ReadLine();
    if (input == "exit"){
        break;
    }
    task.Add(input);
}

foreach(string t in task)
{
    Console.WriteLine(t);
}
