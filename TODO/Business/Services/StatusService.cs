using TODO.Business.Interfaces;
using TODO.Dtos;

namespace TODO.Business.Services;

public class StatusService : IStatusService
{
    public Task<StatusDto> CreateStatusAsync(CreateStatusDto todo)
    {
        throw new NotImplementedException();
    }

    public Task<StatusDto> UpdateStatusAsync(StatusDto todo)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<StatusDto>> GetAllStatusesWithUserIdAsync()
    {
        throw new NotImplementedException();
    }

    public Task<StatusDto> DeleteStatusAsync(int id)
    {
        throw new NotImplementedException();
    }
}