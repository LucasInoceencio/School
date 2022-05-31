using System.ComponentModel.DataAnnotations;

namespace School.Application.DTOs.Request;

public class UserRegisterRequest
{
    [Required(ErrorMessage = "The email field is required")]
    [EmailAddress(ErrorMessage = "The email field is invalid")]
    public string Email { get; set; }

    [Required(ErrorMessage = "The password field is required")]
    [StringLength(50, ErrorMessage = "The password field must be between {2} and {1} characters", MinimumLength = 6)]
    public string Password { get; set; }

    [Compare(nameof(Password), ErrorMessage = "The passwords must be the same")]
    public string ConfirmationPassword { get; set; }
}