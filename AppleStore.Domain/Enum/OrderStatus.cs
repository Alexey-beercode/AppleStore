using System.ComponentModel.DataAnnotations;

namespace AppleStore.Domain.DeviceType;

public enum OrderStatus
{
    [Display(Name = "В обработке")] Processing = 0,
    [Display(Name = "Отправлен")] Sent = 1,
    [Display(Name = "Завершен")] Completed = 2
}