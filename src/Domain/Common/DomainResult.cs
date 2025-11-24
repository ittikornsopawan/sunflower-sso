using System;
using System.Net;
using Shared.Common;

namespace Domain.Common;

public abstract class DomainResult
{
    public ResponseModel result = new ResponseModel
    {
        status = new StatusResponseModel()
    };

    public void Success(HttpStatusCode statusCode = HttpStatusCode.OK) => this.result.status.statusCode = statusCode;
    public void Failure(HttpStatusCode statusCode = HttpStatusCode.BadRequest, string bizErrorCode = "20000")
    {
        this.result.status.statusCode = statusCode;
        this.result.status.bizErrorCode = bizErrorCode;
    }
}

public abstract class DomainResult<T>
{
    public ResponseModel<T> result = new ResponseModel<T>
    {
        status = new StatusResponseModel(),
        data = default!
    };

    public void Success(HttpStatusCode statusCode = HttpStatusCode.OK, T data = default!)
    {
        this.result.status.statusCode = statusCode;
        this.result.data = data;
    }
    public void Failure(HttpStatusCode statusCode = HttpStatusCode.BadRequest, string bizErrorCode = "20000")
    {
        this.result.status.statusCode = statusCode;
        this.result.status.bizErrorCode = bizErrorCode;
    }
}
