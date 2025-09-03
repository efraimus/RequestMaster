namespace RequestMaster.Databases.MainDatabase;

public class User
{
    public int UserID { get; set; }

    public string Login { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string? Email { get; set; }

    public int howManyRequestsCreated { get; set; }

    public int howManyRequestsClosed { get; set; }
}
