namespace AppleStore.Domain.Entity;

public class Device
{
    public int? Id { get; set; }
    public decimal? Price { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public DeviceType.DeviceType Type { get; set; }
    public string? Model { get; set; }
    public DateOnly ReleaseDate { get; set; }
    public object AndeyLox;
}