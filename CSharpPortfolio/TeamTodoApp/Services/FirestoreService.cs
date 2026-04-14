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
    }
}
