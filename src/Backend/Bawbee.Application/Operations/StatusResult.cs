namespace Bawbee.Application.Operations
{
    public enum StatusResult
    {
        Ok = 200,
        OkWithoutReturn = 204,
        InvalidOperation = 400,
        NotFoundData = 404,
        ApplicationError = 500
    }
}
