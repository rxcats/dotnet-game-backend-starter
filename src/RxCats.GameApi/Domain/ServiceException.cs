namespace RxCats.GameApi.Domain;

public class ServiceException : Exception
{
    public ServiceException(ResultCode resultCode, string message) : base(message)
    {
        ResultCode = resultCode;
    }

    public ResultCode ResultCode { get; }
}