using WebApplication2.Dto;
using WebApplication2.Repositories;

namespace WebApplication2.Services;


public interface ISystemService
{
    public Task<int> RegisterSubscriptionInDatabaseAsync(SubscriptionDTO subscriptionDTO);
}

public class SystemService : ISystemService
{
    private readonly ISystemRepository _systemRepository;

    public SystemService(ISystemRepository systemRepository)
    {
        _systemRepository = systemRepository;
    }
        
    
    public async Task<int> RegisterSubscriptionInDatabaseAsync(SubscriptionDTO subscriptionDTO)
    {
        var state = await _systemRepository.RegisterSubscriptionInDatabaseAsync(
            subscriptionDTO.IdUser,
            subscriptionDTO.IdService,
            subscriptionDTO.leftToPay);
        return state;
    }
}       