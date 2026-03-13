namespace BarberBilling.Application.UseCases.Billings.Register;

public interface IRegisterBillingUseCase
{
    Task<RegisterBillingOutput> Execute(BillingInput input);
}