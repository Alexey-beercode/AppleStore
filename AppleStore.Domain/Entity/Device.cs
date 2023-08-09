using System.ComponentModel.DataAnnotations;

namespace AppleStore.Domain.Entity;

public class Device
{
    public int? Id { get; set; }
    public decimal? Price { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public DeviceType.DeviceType Type { get; set; }
    public string? ImageUrl { get; set; }
}