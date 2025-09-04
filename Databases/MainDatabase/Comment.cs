namespace RequestMaster.Databases.MainDatabase
{
    public class Comment
    {
        public int CommentID { get; set; }

        public string Content { get; set; } = null!;

        public string WhoCreated { get; set; } = null!;

        public string DateTimeOfCreating { get; set; } = null!;

        public int RequestID { get; set; }

        public Request? Request { get; set; }
    }
}
