using TODO.Dtos;

namespace TODO.Business.Interfaces;

public interface IStatusService
{
    // create a status
    Task<StatusDto> CreateStatusAsync(CreateStatusDto todo);
    
    // edit a status
    Task<StatusDto> UpdateStatusAsync(StatusDto todo);
    
    
    // get all statuses
    Task<IEnumerable<StatusDto>> GetAllStatusesWithUserIdAsync();
    
    // delete a status
    Task<StatusDto> DeleteStatusAsync(int id);
}