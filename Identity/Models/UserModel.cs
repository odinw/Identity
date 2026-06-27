namespace Identity.Models;

public class UserModel
{
    public int Id { get; set; }
    public string Account { get; set; } = string.Empty;    
    public string Password_Hash { get; set; } = string.Empty;

    public DateTimeOffset Create_At { get; set; }
}