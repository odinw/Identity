namespace Identity.DTOs;

public class SignUpResponse
{
    public int Id { get; set; }
    public string Account { get; set; } = string.Empty;
    public DateTimeOffset Create_At { get; set; }
}