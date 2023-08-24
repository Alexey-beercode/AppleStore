using System.Collections;
using System.Net;
using AppleStore.Domain.Entity;

namespace AppleStore.Domain.Response;

public class BaseResponse<T> : IEnumerable
{
    public string? Description { get; set; }
    public HttpStatusCode StatusCode { get; set; }
    public T? Data { get; set; }
    public IEnumerator GetEnumerator()
    {
        throw new NotImplementedException();
    }
}