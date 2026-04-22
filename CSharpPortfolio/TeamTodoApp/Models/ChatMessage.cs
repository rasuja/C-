using Google.Cloud.Firestore;

namespace TeamTodoApp.Models
{
    [FirestoreData]
    public class ChatMessage
    {
        [FirestoreDocumentId]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [FirestoreProperty]
        public string SenderId { get; set; } = "";

        [FirestoreProperty]
        public string SenderName { get; set; } = "";

        [FirestoreProperty]
        public string MessageText { get; set; } = "";

        [FirestoreProperty]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
