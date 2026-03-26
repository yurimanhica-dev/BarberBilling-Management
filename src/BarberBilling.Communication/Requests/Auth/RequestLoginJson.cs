namespace BarberBilling.Communication.Requests.Login;

public record RequestLoginJson
(
    string Email,
    string Password 
);