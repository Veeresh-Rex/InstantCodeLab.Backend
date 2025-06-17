namespace InstantCodeLab.Api.ExceptionHandlers;

public class GlobalExecptionHandler : BaseExecptionHandler<Exception>
{
    public override string ExceptionType => "System exception";
    public override IDictionary<string, string[]> GetErrorMessage(Exception exception)
    
    {
        return new Dictionary<string, string[]>
        {
            { "SystemError", new[] { exception.Message } },
        };
    }
}
