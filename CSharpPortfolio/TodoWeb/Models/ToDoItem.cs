namespace TodoWeb.Models;

/// <summary>
/// ToDoアイテムを表すクラスです。
/// </summary>
public class ToDoItem
{
    /// <summary>
    /// アイテムの一意な識別子
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// タスクの名前
    /// </summary>
    public string Name { get; set; } = "";

    /// <summary>
    /// 完了状態（trueの場合、完了）
    /// </summary>
    public bool IsDone { get; set; }

    /// <summary>
    /// 優先度 (1=高, 2=中, 3=低)
    /// </summary>
    public int Priority { get; set; } = 2;
}
