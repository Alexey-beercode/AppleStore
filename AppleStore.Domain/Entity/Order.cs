using System.ComponentModel.DataAnnotations;
using AppleStore.Domain.DeviceType;

namespace AppleStore.Domain.Entity;

public class Order
{
    public  int? Id { get; set; }
    public string? Name { get; set; }
    [Required]
    [EmailAddress]
    public string? Email { get; set; }
    public string? Address { get; set; }
    public int[] DeviceId { get; set; }
    public decimal Price { get; set; }
    public OrderStatus Status { get; set; }
}