using System.ComponentModel.DataAnnotations;

namespace Identity.DTOs;

public class SignUpRequest
{
    [Required]
    public string Account { get; set; } = string.Empty;
    [Required]
    public string Password { get; set; } = string.Empty;
}
