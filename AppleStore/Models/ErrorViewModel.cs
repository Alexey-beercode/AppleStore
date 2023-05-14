namespace AppleStore.Models;

public class ErrorViewModel
{
    public string? RequestId { get; set; }

    public bool IsRequestIdEmpty => !string.IsNullOrEmpty(RequestId);
}