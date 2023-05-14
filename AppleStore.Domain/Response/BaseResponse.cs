﻿using System.Net;

namespace AppleStore.Domain.Response;

public class BaseResponse<T>
{
    public string? Description { get; set; }
    public HttpStatusCode StatusCode { get; set; }
    public T Data { get; set; }
}