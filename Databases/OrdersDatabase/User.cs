namespace OrdersApp.Databases.OrdersDatabase;

public class User
{
    public int UserId { get; set; }

    public int Balance { get; set; }

    public string Login { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string? Email { get; set; }

    public int? OrderID { get; set; }

    public Order? Order { get; set; }
}
