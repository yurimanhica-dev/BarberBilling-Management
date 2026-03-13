namespace BarberBilling.Domain.Repositories;

public interface IUnitOfWork
{
    Task Commit();
}