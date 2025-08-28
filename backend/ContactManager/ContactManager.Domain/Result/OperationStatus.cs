namespace ContactManager.Domain.Result
{
    public enum OperationStatus
    {
        Success,
        NotFound,
        Conflict,      
        InvalidData,
        ServerError 
    }
}
