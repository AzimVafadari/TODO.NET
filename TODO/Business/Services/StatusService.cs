using Microsoft.EntityFrameworkCore.ChangeTracking;
using TODO.Business.Exceptions;
using TODO.Business.Interfaces;
using TODO.Data;
using TODO.Dtos;
using TODO.Models;

namespace TODO.Business.Services;

public class StatusService(AppDbContext appDbContext) : IStatusService
{
    public async Task<StatusDto> CreateStatusAsync(CreateStatusDto status)
    {
        EntityEntry<Status> createdStatusEntity = await appDbContext.Statuses.AddAsync(new Status(status.StatusName));
        Status createdStatus = createdStatusEntity.Entity;
        return new StatusDto(createdStatus.StatusId, createdStatus.StatusName);
    }

    public async Task<StatusDto> UpdateStatusAsync(StatusDto status)
    {
        Status? foundStatus = await appDbContext.Statuses.FindAsync(status.StatusId);
        if (foundStatus == null)
        {
            throw new StatusNotFoundException("Status not found");
        }
        appDbContext.Statuses.Update(new Status(foundStatus.StatusId, foundStatus.StatusName));
        Status? updatedStatus = await appDbContext.Statuses.FindAsync(status.StatusId);
        return new StatusDto(updatedStatus.StatusId, updatedStatus.StatusName);
    }

    public async Task<IEnumerable<StatusDto>> GetAllStatusesWithUserIdAsync()
    {
        IAsyncEnumerable<Status> statuses = appDbContext.Statuses.AsAsyncEnumerable();
        IEnumerable<StatusDto> statusDtos = new List<StatusDto>();
        await foreach (var s in statuses)
        {
            if (!s.IsDeleted)
                statusDtos.Append(new StatusDto(s.StatusId, s.StatusName));
        }
        return statusDtos;
    }

    public async Task<StatusDto> DeleteStatusAsync(int id)
    {
        Status? foundStatus = await appDbContext.Statuses.FindAsync(id);
        if (foundStatus == null)
        {
            throw new StatusNotFoundException();
        }

        if (foundStatus.IsDeleted)
        {
            throw new StatusAlreadyDeletedException("Status already deleted");
        }
        foundStatus.IsDeleted = true;
        EntityEntry<Status> updatedStatusEntityEntry = appDbContext.Statuses.Update(foundStatus);
        Status updatedStatus = updatedStatusEntityEntry.Entity;
        return new StatusDto(updatedStatus.StatusId, updatedStatus.StatusName);
    }
}