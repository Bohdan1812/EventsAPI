
using Application.Events.Commands.Create;
using Application.Events.Commands.Delete;
using Application.Events.Commands.SubEventCommands.AddSubEvent;
using Application.Events.Commands.SubEventCommands.RemoveSubEvent;
using Application.Events.Commands.SubEventCommands.UpdateSubEvent;
using Application.Events.Commands.Update;
using Application.Events.Queries.FindEvents;
using Application.Events.Queries.GetAllUserEvents;
using Application.Events.Queries.GetEvent;
using Contracts.Event;
using Contracts.Event.SubEvent;
using Domain.EventAggregate;
using Domain.EventAggregate.Entities;
using Domain.EventAggregate.ValueObjects;
using Mapster;

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
                .Map(dest => dest.Address, src => src.Item2.AddressRequest)
                .Map(dest => dest.Link, src => new Application.Events.Commands.
                    Create.LinkCommand(src.Item2.Link))
                .Map(dest => dest.SubEvents, src => src.Item2.SubEvents)
                .Map(dest => dest.IsPrivate, src => src.Item2.IsPrivate)
                .Map(dest => dest.AllowParticipantsInvite, src => src.Item2.AllowParticipantsInvite);

            config.NewConfig<GetEventRequestModel, GetEventQuery>()
                .Map(dest => dest.EventId, src => src.EventId);

            config.NewConfig<(Guid appUserId, DeleteEventRequestModel request), DeleteEventCommand>()
                .Map(dest => dest.appUserId, src => src.appUserId)
                .Map(dest => dest.eventId, src => src.request.EventId);

            config.NewConfig<(Guid appUserId, UpdateEventRequestModel request), UpdateEventCommand>()
                .Map(dest => dest.EventId, src => src.request.eventId)
                .Map(dest => dest.ApplicationUserId, src => src.appUserId)
                .Map(dest => dest.Name, src => src.Item2.Name)
                .Map(dest => dest.Description, src => src.Item2.Description)
                .Map(dest => dest.StartDateTime, src => src.Item2.StartDateTime)
                .Map(dest => dest.EndDateTime, src => src.Item2.EndDateTime)
                .Map(dest => dest.Address, src => src.Item2.Address)
                .Map(dest => dest.Link, src => new Application.Events.Commands.
                    Update.LinkCommand(src.Item2.Link))
                .Map(dest => dest.IsPrivate, src => src.Item2.IsPrivate)
                .Map(dest => dest.AllowParticipantsInvite, src => src.Item2.AllowParticipantsInvite);

            config.NewConfig<(Guid appUserId, AddSubEventRequestModel request), AddSubEventCommand>()
                .Map(dest => dest.ApplicationUserId, src => src.appUserId)
                .Map(dest => dest.EventId, src => src.request.EventId)
                .Map(dest => dest.Name, src => src.request.Name)
                .Map(dest => dest.Description, src => src.request.Description)
                .Map(dest => dest.StartDateTime, src => src.request.StartDateTime)
                .Map(dest => dest.EndDateTime, src => src.request.EndDateTime);

            config.NewConfig<(Guid appUserId, UpdateSubEventRequestModel request), UpdateSubEventCommand>()
                .Map(dest => dest.ApplicationUserId, src => src.appUserId)
                .Map(dest => dest.EventId, src => src.request.EventId)
                .Map(dest => dest.SubEventId, src => src.request.SubEventId)
                .Map(dest => dest.Name, src => src.request.Name)
                .Map(dest => dest.Description, src => src.request.Description)
                .Map(dest => dest.StartDateTime, src => src.request.StartDateTime)
                .Map(dest => dest.EndDateTime, src => src.request.EndDateTime);

            config.NewConfig<(Guid appUserId, RemoveSubEventRequestModel request), RemoveSubEventCommand>()
                .Map(dest => dest.ApplicationUserId, src => src.appUserId)
                .Map(dest => dest.EventId, src => src.request.EventId)
                .Map(dest => dest.SubEventId, src => src.request.SubEventId);

            config.NewConfig<Event, EventResponse>()
                .Map(dest => dest.Id, src => src.Id.Value)
                .Map(dest => dest.Name, src => src.Name)
                .Map(dest => dest.Description, src => src.Description)
                .Map(dest => dest.SubEvents, src => src.SubEvents.Adapt<List<SubEventResponse>>())
                .Map(dest => dest.Link, src => src.Link)
                .Map(dest => dest.Address, src => src.Address)
                .Map(dest => dest.OrganizerId, src => src.OrganizerId.Value)
                .Map(dest => dest.StartDateTime, src => src.StartDateTime)
                .Map(dest => dest.EndDateTime, src => src.EndDateTime)
                .Map(dest => dest.IsPrivate, src => src.IsPrivate)
                .Map(dest => dest.AllowParticipantsInvite, src => src.AllowParticipantsInvite)
                .Map(dest => dest.PhotoPath, src => src.PhotoPath);

            config.NewConfig<Link, LinkResponse>()
                .Map(dest => dest.Link, src => src.Value);

            config.NewConfig<SubEvent, SubEventResponse>()
                .Map(dest => dest.SubEventId, src => src.Id.Value);

            config.NewConfig<(Guid ApplicationUserId, GetUserEventsRequestModel request), GetAllUserEventsQuery>()
                .Map(dest => dest.ApplicationUserId, src => src.ApplicationUserId)
                .Map(dest => dest.StartDateTime, src => src.request.DateTime);

            config.NewConfig<FindEventsRequestModel, FindEventsQuery>()
                .Map(dest => dest.EventSearchQuery, src => src.EventSearchQuery)
                .Map(dest => dest.StartDateTime, src => src.StartDateTime)
                .Map(dest => dest.EndDateTime, src => src.EndDateTime);
        }
    }
}
