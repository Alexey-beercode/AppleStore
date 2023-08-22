using System.ComponentModel.DataAnnotations;

namespace AppleStore.Domain.ViewModels;

public class RegisterViewModel
{
    [Required]
    [Display(Name = "Логин")]
    public string UserName { get; set; }

    [Required]
    [Display(Name = "Пароль")]
    public string Password { get; set; }

    [Required]
    [EmailAddress]
    [Display(Name = "Email")]
    public string Email { get; set; }

}