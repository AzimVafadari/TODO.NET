using TODO.Dtos;

namespace TODO.Business.Interfaces;

public interface IStatusService
{
    // create a status
    Task<StatusDto> CreateStatusAsync(CreateStatusDto status);
    
    // edit a status
    Task<StatusDto> UpdateStatusAsync(StatusDto status);
    
    
    // get all statuses
    Task<IEnumerable<StatusDto>> GetAllStatusesWithUserIdAsync();
    
    // delete a status
    Task<StatusDto> DeleteStatusAsync(int id);
}