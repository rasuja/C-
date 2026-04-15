using Google.Cloud.Firestore;

namespace TeamTodoApp.Models
{
    [FirestoreData]
    public class AppUser
    {
        [FirestoreDocumentId]
        public string Id { get; set; } // Username (e.g., "sato_pm")

        [FirestoreProperty]
        public string DisplayName { get; set; } // 表示名（例：「佐藤」）

        [FirestoreProperty]
        public string Role { get; set; } // PM, リーダー, メンバー

        [FirestoreProperty]
        public string PasswordHash { get; set; }

        [FirestoreProperty]
        public bool IsAdmin { get; set; } // 管理者権限（PMが持つ）
    }
}
