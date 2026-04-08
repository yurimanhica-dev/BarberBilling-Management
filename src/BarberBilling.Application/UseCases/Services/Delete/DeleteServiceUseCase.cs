using BarberBilling.Application.Mappings;
using BarberBilling.Communication.Responses.Services.Delete;
using BarberBilling.Domain.Repositories;
using BarberBilling.Domain.Repositories.Services;
using BarberBilling.Exceptions.CustomExceptions;

namespace BarberBilling.Application.UseCases.Services.Delete;

public class DeleteServiceUseCase : IDeleteServiceUseCase
{
    private readonly IServiceWriteOnlyRepository _writeRepository;
    private readonly IServiceUpdateOnlyRepository _updateRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteServiceUseCase(
        IServiceWriteOnlyRepository writeRepository,
        IServiceUpdateOnlyRepository updateRepository,
        IUnitOfWork unitOfWork)
    {
        _writeRepository = writeRepository;
        _updateRepository = updateRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<ResponseSoftDeleteJson> Execute(Guid serviceId, bool deleted)
    {
        var service = await _updateRepository.GetByIdentifier(serviceId)
            ?? throw new NotFoundException("ServiceNotFound");

        if (service.IsDeleted == deleted)
            throw new ConflictException("ServiceStatusUnchanged");

        service.IsDeleted = deleted;
        
        await _writeRepository.SoftDelete(service, deleted);
        await _unitOfWork.Commit();

        return service.ToResponseSoftDelete();
    }
}