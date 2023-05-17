namespace AppleStore.Domain.ViewModels;

public class DeviceViewModel
{
    public int Id { get; set; }
    public decimal? Price { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public string? Type { get; set; }
    public string? Model { get; set; }
    public DateOnly ReleaseDate { get; set; }
}