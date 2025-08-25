namespace OrdersApp.Databases.OrdersDatabase
{
    public class Order
    {
        public int ID { get; set; }

        public int Payment { get; set; }

        public string Description { get; set; } = null!;

        public string TelephoneNumber { get; set; } = null!;

        public int CountOfPeopleNeeded { get; set; }

        public string Status { get; set; } = null!;
        
        public int EmployerID { get; set; }

        public User Employer { get; set; } = null!;

        public int? EmployeeID { get; set; }

        public string? WhoConfirmedCompletion { get; set; }

    }
}
