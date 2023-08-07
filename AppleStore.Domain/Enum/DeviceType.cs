using System.ComponentModel.DataAnnotations;

namespace AppleStore.Domain.DeviceType;

public enum DeviceType
{
    [Display(Name = "Часы")] Watch=1,
    [Display(Name = "Телефон")] Phone=0,
    [Display(Name = "Планшет")] Tablet=2,
    [Display(Name = "Ноутбук")] Laptop=3
}