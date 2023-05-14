using System.ComponentModel.DataAnnotations;

namespace AppleStore.Domain.DeviceType;

public enum DeviceType
{
    [Display(Name = "Часы")] Watch,
    [Display(Name = "Телефон")] Phone,
    [Display(Name = "Планшет")] Tablet,
    [Display(Name = "Ноутбук")] Laptop
}