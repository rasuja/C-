using Google.Cloud.Firestore;

namespace TodoWeb.Models;

/// <summary>
/// ToDoアイテムを表すクラスです。
/// </summary>
[FirestoreData]
public class ToDoItem
{
    /// <summary>
    /// アイテムの一意な識別子
    /// </summary>
    [FirestoreDocumentId]
    public string Id { get; set; } = null!;

    /// <summary>
    /// タスクの名前
    /// </summary>
    [FirestoreProperty]
    public string Name { get; set; } = "";

    /// <summary>
    /// 完了状態（trueの場合、完了）
    /// </summary>
    [FirestoreProperty]
    public bool IsDone { get; set; }

    /// <summary>
    /// 優先度 (1=高, 2=中, 3=低)
    /// </summary>
    [FirestoreProperty]
    public int Priority { get; set; } = 2;

    /// <summary>
    /// 期限（日付）
    /// </summary>
    [FirestoreProperty]
    public DateTime? DueDate { get; set; }
}
