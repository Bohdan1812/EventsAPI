using Domain.OrganizerAggregate;
using Domain.OrganizerAggregate.ValueObjects;

namespace Application.Persistence.Repositories
{
    public interface IOrganizerRepository
    {
        Task<Organizer?> GetOrganizer(Guid appUserId);
        Task<Organizer?> GetOrganizer(OrganizerId organizerId);

        Task Add(Organizer organizer);

        Task Remove(OrganizerId organizerId);

        Task Update(Organizer organizer);
    }
}
