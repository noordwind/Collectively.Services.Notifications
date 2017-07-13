namespace Collectively.Services.Notifications.Domain
{
    public class NotificationSettings
    {
        public bool Enabled { get; set; }
        public bool RemarkCreated { get; set; }
        public bool RemarkCanceled { get; set; }
        public bool RemarkDeleted { get; set; }
        public bool RemarkProcessed { get; set; }
        public bool RemarkRenewed { get; set; }
        public bool RemarkResolved { get; set; }
        public bool PhotosToRemarkAdded { get; set; }
        public bool CommentAdded { get; set; }

        public static NotificationSettings Default
            => new NotificationSettings
            {
                Enabled = true,
                RemarkCreated = true,
                RemarkCanceled = true,
                RemarkDeleted = true,
                RemarkProcessed = true,
                RemarkRenewed = true,
                RemarkResolved = true,
                PhotosToRemarkAdded = true,
                CommentAdded = true
            };
    }
}