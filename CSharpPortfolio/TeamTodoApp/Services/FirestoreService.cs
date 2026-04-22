using Google.Cloud.Firestore;
using TeamTodoApp.Models;

namespace TeamTodoApp.Services
{
    public class FirestoreService
    {
        private readonly FirestoreDb _db;

        public FirestoreService(string projectId)
        {
            _db = FirestoreDb.Create(projectId);
        }

        public async Task<List<ToDoItem>> GetTodoItemsAsync()
        {
            var collection = _db.Collection("team_tasks");
            var snapshot = await collection.GetSnapshotAsync();

            var items = new List<ToDoItem>();
            foreach (var document in snapshot.Documents)
            {
                if (document.Exists)
                {
                    var item = document.ConvertTo<ToDoItem>();
                    items.Add(item);
                }
            }
            return items.OrderBy(i => i.DueDate).ToList();
        }

        public async Task AddTodoItemAsync(ToDoItem item)
        {
            var collection = _db.Collection("team_tasks");
            item.DueDate = item.DueDate.ToUniversalTime();
            await collection.AddAsync(item);
        }

        public async Task UpdateTaskStatusAsync(string id, string newStatus)
        {
            var docRef = _db.Collection("team_tasks").Document(id);
            await docRef.UpdateAsync("Status", newStatus);
        }

        public async Task DeleteTodoItemAsync(string id)
        {
            var docRef = _db.Collection("team_tasks").Document(id);
            await docRef.DeleteAsync();
        }

        public async Task UpdateTodoItemDescriptionAsync(string id, string description)
        {
            var docRef = _db.Collection("team_tasks").Document(id);
            await docRef.UpdateAsync("Description", description ?? "");
        }

        // --- Chat 管理用のメソッド ---
        public async Task<List<ChatMessage>> GetChatMessagesAsync()
        {
            var collection = _db.Collection("team_messages");
            var snapshot = await collection.OrderByDescending("CreatedAt").Limit(50).GetSnapshotAsync();
            var messages = new List<ChatMessage>();
            foreach (var doc in snapshot.Documents)
            {
                if (doc.Exists)
                {
                    messages.Add(doc.ConvertTo<ChatMessage>());
                }
            }
            return messages.OrderBy(m => m.CreatedAt).ToList();
        }

        public async Task AddChatMessageAsync(ChatMessage message)
        {
            var collection = _db.Collection("team_messages");
            message.CreatedAt = message.CreatedAt.ToUniversalTime();
            await collection.AddAsync(message);
        }

        // --- User 管理用のメソッド ---

        public async Task<List<AppUser>> GetAllUsersAsync()
        {
            var collection = _db.Collection("users");
            var snapshot = await collection.GetSnapshotAsync();
            var users = new List<AppUser>();
            foreach (var doc in snapshot.Documents)
            {
                if (doc.Exists)
                {
                    users.Add(doc.ConvertTo<AppUser>());
                }
            }
            return users;
        }

        public async Task<AppUser?> GetUserByIdAsync(string id)
        {
            var docRef = _db.Collection("users").Document(id);
            var snapshot = await docRef.GetSnapshotAsync();
            if (snapshot.Exists)
            {
                return snapshot.ConvertTo<AppUser>();
            }
            return null;
        }

        public async Task CreateUserAsync(AppUser user)
        {
            var docRef = _db.Collection("users").Document(user.Id); // ID（Username）をドキュメントIDとする
            await docRef.SetAsync(user);
        }
    }
}
