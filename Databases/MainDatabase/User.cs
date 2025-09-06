namespace RequestMaster.Databases.MainDatabase;

public class User
{
    public int UserID { get; set; }

    public string Login { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string? Email { get; set; }

    public string Theme { get; set; } = null!;

}
