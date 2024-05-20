using Application.Events.Dto;
using Domain.EventAggregate;
using Domain.EventAggregate.Entities;
using Domain.EventAggregate.ValueObjects;
using Mapster;

namespace Application.Common.Mapping
{
    public class EventApplicationMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<SubEvent, SubEventInfo>()
                .Map(dest => dest.Id, src => src.Id.Value)
                .Map(dest => dest.Start, src => src.StartDateTime)
                .Map(dest => dest.End, src => src.EndDateTime);

            config.NewConfig<Address, AddressInfo>();

            config.NewConfig<Event, EventInfo>()
                .Map(dest => dest.Id, src => src.Id.Value)
                .Map(dest => dest.OrganizerId, src => src.OrganizerId.Value)
                .Map(dest => dest.SubEvents, src => src.SubEvents.Adapt<List<SubEventInfo>>())
                .Map(dest => dest.AddressInfo, src => src.Address.Adapt<AddressInfo>())
                .Map(dest => dest.ParticipationCount, src => src.Participations.Count)
                .Map(dest => dest.Start, src => src.StartDateTime)
                .Map(dest => dest.End, src => src.EndDateTime);
        }
    }
}
