using System.ComponentModel.DataAnnotations;

namespace AppleStore.Domain.DeviceType;

public enum OrderStatus
{
    [Display(Name = "В обработке")] Processing,
    [Display(Name = "Отправлен")] Sent,
    [Display(Name = "Завершен")] Completed
}