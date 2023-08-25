using System.Collections;
using AppleStore.Domain.Entity;

namespace AppleStore.Domain.ViewModels;

public class DeviceOrderViewModel : IEnumerable
{
    public List<Device> Devices { get; set; }
    public Order Order { get; set; }
    public IEnumerator GetEnumerator()
    {
        throw new NotImplementedException();
    }
}