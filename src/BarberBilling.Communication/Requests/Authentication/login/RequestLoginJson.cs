namespace BarberBilling.Communication.Requests.Authentication.login;

public record RequestLoginJson
(
    string Email,
    string Password 
);