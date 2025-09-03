namespace RequestMaster.Databases.MainDatabase
{
    public class Request
    {
        public int RequestID { get; set; }

        public string Description { get; set; } = null!;

        public string TelephoneNumber { get; set; } = null!;

        public string Status { get; set; } = null!;
        
        public int whoCreatedID { get; set; }

        public int whoClosedID { get; set; }
    }
}
