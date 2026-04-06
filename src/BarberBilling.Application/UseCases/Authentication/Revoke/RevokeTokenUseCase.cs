using BarberBilling.Domain.Repositories;
using BarberBilling.Domain.Repositories.Token;
using BarberBilling.Domain.Repositories.User;

namespace BarberBilling.Application.UseCases.Authentication.Revoke;

public class RevokeTokenUseCase : IRevokeTokenUseCase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ITokenWriteOnlyRepository _tokenRepository;
    private readonly IUserReadOnlyRepository _userReadOnlyRepository;
    private readonly IUserWriteOnlyRepository _userWriteOnlyRepository;


    public RevokeTokenUseCase(ITokenWriteOnlyRepository tokenRepository, IUserReadOnlyRepository userReadOnlyRepository, IUserWriteOnlyRepository userWriteOnlyRepository, IUnitOfWork unitOfWork)
    {
        _tokenRepository = tokenRepository;
        _userReadOnlyRepository = userReadOnlyRepository;
        _userWriteOnlyRepository = userWriteOnlyRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task Execute(Guid userId)
    {
        var user = await _userReadOnlyRepository.GetByIdentifier(userId);
        
        await _tokenRepository.DeleteAllByUserId(userId);
        
        user!.TokenVersion++;
        await _userWriteOnlyRepository.Update(user);

        await _unitOfWork.Commit();
    }
}