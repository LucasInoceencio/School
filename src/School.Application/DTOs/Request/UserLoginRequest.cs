using System.ComponentModel.DataAnnotations;

namespace School.Application.DTOs.Request;

public class UserLoginRequest
{
    [Required(ErrorMessage = "The field email is required")]
    [EmailAddress(ErrorMessage = "The field email is invalid")]
    public string Email { get; set; }

    [Required(ErrorMessage = "The field password is required")]
    public string Password { get; set; }
}