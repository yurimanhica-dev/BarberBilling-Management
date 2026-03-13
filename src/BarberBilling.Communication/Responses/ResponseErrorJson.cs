namespace BarberBilling.Communication.Responses;

public class ResponseErrorJson
{
    public List<string> ErrorMessages { get; }

    public ResponseErrorJson(string errorMessage)
    {
        ErrorMessages = [errorMessage];
    }

    public ResponseErrorJson(List<string> errorMessages)
    {
        ErrorMessages = errorMessages;
    }
}