
using Application.Events.Commands.Create;
using Application.Events.Commands.Delete;
using Application.Events.Queries.GetEvent;
using Contracts.Event;
using Domain.EventAggregate.ValueObjects;
using Mapster;
using EventId = Domain.EventAggregate.ValueObjects.EventId;

namespace Api.Common.Mapping
{
    public class EventMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<(Guid, CreateEventRequestModel), CreateEventCommand>()
                .Map(dest => dest.appUserId, src => src.Item1)
                .Map(dest => dest.Name, src => src.Item2.Name)
                .Map(dest => dest.Description, src => src.Item2.Description)
                .Map(dest => dest.StartDateTime, src => src.Item2.StartDateTime)
                .Map(dest => dest.EndDateTime, src => src.Item2.EndDateTime)
                .Map(dest => dest.Address, src => src.Item2.AddressRequess)
                .Map(dest => dest.Link, src => new LinkCommand(src.Item2.Link))
                .Map(dest => dest.SubEvents, src => src.Item2.SubEvents);

            config.NewConfig<GetEventRequestModel, GetEventQuery>()
                .Map(dest => dest.EventId, src => src.EventId);

            config.NewConfig<(Guid appUserId, DeleteEventRequestModel request), DeleteEventCommand>()
                .Map(dest => dest.appUserId, src => src.appUserId)
                .Map(dest => dest.eventId, src => src.request.EventId);
        }
    }
}
