using Google.Cloud.Firestore;
using TodoWeb.Models;

namespace TodoWeb.Services;

/// <summary>
/// Firebase Firestoreと通信し、ToDoタスクのCRUD操作を行うサービスクラスです。
/// </summary>
public class FirestoreService
{
    private readonly FirestoreDb _db;
    private const string CollectionName = "todos"; // Firestoreに保存されるコレクション（テーブル）名

    public FirestoreService(string projectId)
    {
        // 渡されたプロジェクトIDでFirestoreに接続
        _db = FirestoreDb.Create(projectId);
    }

    /// <summary>
    /// すべてのToDoタスクを取得します。
    /// </summary>
    public async Task<List<ToDoItem>> GetTodoItemsAsync()
    {
        var collection = _db.Collection(CollectionName);
        var snapshot = await collection.GetSnapshotAsync();
        
        var items = new List<ToDoItem>();
        foreach (var document in snapshot.Documents)
        {
            if (document.Exists)
            {
                var item = document.ConvertTo<ToDoItem>();
                item.Id = document.Id; // [FirestoreDocumentId] で自動マッピングされない場合の保険
                items.Add(item);
            }
        }
        
        // C#側で優先度順、ID降順にソート（必要に応じてFirestoreのクエリでも可能）
        return items.OrderBy(t => t.Priority).ThenByDescending(t => t.Id).ToList();
    }

    /// <summary>
    /// 新しいToDoタスクを追加します。
    /// </summary>
    public async Task AddTodoItemAsync(ToDoItem item)
    {
        var collection = _db.Collection(CollectionName);
        
        // Firestoreは日付をUTC形式(世界標準時)で求めるため、変換して保存
        if (item.DueDate.HasValue)
        {
            item.DueDate = item.DueDate.Value.ToUniversalTime();
        }
        
        await collection.AddAsync(item);
    }

    /// <summary>
    /// 指定されたIDのタスクを「完了」状態に更新します。
    /// </summary>
    public async Task UpdateTodoItemDoneAsync(string id)
    {
        if (string.IsNullOrEmpty(id)) return;
        
        var docRef = _db.Collection(CollectionName).Document(id);
        var snapshot = await docRef.GetSnapshotAsync();
        
        if (snapshot.Exists)
        {
            var item = snapshot.ConvertTo<ToDoItem>();
            item.IsDone = true;
            // 変更内容を上書き保存
            await docRef.SetAsync(item);
        }
    }

    /// <summary>
    /// 指定されたIDのタスクを実レコードから削除します。
    /// </summary>
    public async Task DeleteTodoItemAsync(string id)
    {
        if (string.IsNullOrEmpty(id)) return;
        
        var docRef = _db.Collection(CollectionName).Document(id);
        await docRef.DeleteAsync();
    }
}
