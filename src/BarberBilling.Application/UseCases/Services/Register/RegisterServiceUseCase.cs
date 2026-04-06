using BarberBilling.Application.Mappings;
using BarberBilling.Communication.Requests.Services;
using BarberBilling.Communication.Responses.Services.Register;
using BarberBilling.Domain.Repositories;
using BarberBilling.Domain.Repositories.Services;
using BarberBilling.Exceptions.CustomExceptions;

namespace BarberBilling.Application.UseCases.Services.Register;

public class RegisterServiceUseCase : IRegisterServiceUseCase
{
    private readonly IServiceReadOnlyRepository _readRepository;
    private readonly IServiceWriteOnlyRepository _writeRepository;
    private readonly IUnitOfWork _unitOfWork;

    public RegisterServiceUseCase(
        IServiceReadOnlyRepository readRepository,
        IServiceWriteOnlyRepository writeRepository,
        IUnitOfWork unitOfWork)
    {
        _readRepository = readRepository;
        _writeRepository = writeRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<ResponseRegisterServiceJson> Execute(RequestServiceJson request)
    {
        var service = request.ToEntity();

        var exists = await _readRepository.GetByName(request.Services.ToString());

        if (exists is not null)
            throw new ConflictException("ServiceAlreadyExists");

        await _writeRepository.Add(service);
        await _unitOfWork.Commit();

        return service.ToRegisterResponse();
    }
}