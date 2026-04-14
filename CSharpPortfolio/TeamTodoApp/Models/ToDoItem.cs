using Google.Cloud.Firestore;

namespace TeamTodoApp.Models
{
    [FirestoreData]
    public class ToDoItem
    {
        [FirestoreDocumentId]
        public string Id { get; set; }

        [FirestoreProperty]
        public string Title { get; set; }

        [FirestoreProperty]
        public string Assignee { get; set; }

        [FirestoreProperty]
        public string Role { get; set; } // PM, リーダー, メンバー

        [FirestoreProperty]
        public string Status { get; set; } // ToDo, InProgress, Review, Done

        [FirestoreProperty]
        public DateTime DueDate { get; set; }
    }
}
