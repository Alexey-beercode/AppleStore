using System.ComponentModel.DataAnnotations;

namespace AppleStore.Domain.Entity;

public class Order
{
    public  int? Id { get; set; }
    public string? Name { get; set; }
    public string? Email { get; set; }
    [Required]
    [EmailAddress]
    public string? Address { get; set; }
    public string? DeviceName { get; set; }
}