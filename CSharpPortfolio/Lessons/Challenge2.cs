// Challenge 2: Age Checker
Console.WriteLine("年齢を入力してください：");

string input = Console.ReadLine();
int age = int.Parse(input);

if (age >= 18)
{
    Console.WriteLine("あなたは成人ですね。");
}
else
{
    Console.WriteLine("あなたは未成年ですね。");
}
