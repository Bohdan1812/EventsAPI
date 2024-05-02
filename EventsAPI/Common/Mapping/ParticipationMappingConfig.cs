using Application.Participations.Commands.AddParticipationFromInvite;
using Application.Participations.Commands.AddParticipationFromJoinRequest;
using Application.Participations.Commands.RemoveOwnParticipation;
using Application.Participations.Commands.RemoveParticipationAsOrganizer;
using Application.Participations.Queries.GetOwnParticipations;
using Application.Participations.Queries.GetParticipation;
using Application.Participations.Queries.GetParticipationsByEvent;
using Contracts.Participation;
using Domain.ParticipationAggregate;
using Mapster;

namespace Api.Common.Mapping
{
    public class ParticipationMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<Guid, GetOwnParticipationsQuery>()
                 .Map(dest => dest.ApplicationUserId, src => src);

            config.NewConfig<(Guid appUserId, AddParticipationByInviteRequestModel request), AddParticipationFromInviteCommand>()
                .Map(dest => dest.ApplicatioUserId, src => src.appUserId)
                .Map(dest => dest.InviteId, src => src.request.InviteId);

            config.NewConfig<(Guid appUserId, AddParticipationByJoinRequestRequestModel request), AddParticipationFromJoinRequestCommand>()
                .Map(dest => dest.ApplicationUserId, src => src.appUserId)
                .Map(dest => dest.JoinRequestId, src => src.request.JoinRequestId);

            config.NewConfig<(Guid appUserId, GetParticipationsByEventRequestModel request), GetParticipationsByEventQuery>()
                .Map(dest => dest.ApplicationUserId, src => src.appUserId)
                .Map(dest => dest.EventId, src => src.request.EventId);

            config.NewConfig<(Guid appUserId, GetParticipationRequestModel request), GetParticipationQuery>()
                .Map(dest => dest.ApplicationUserId, src => src.appUserId)
                .Map(dest => dest.ParticipationId, src => src.request.ParticipationId);

            config.NewConfig<(Guid appUserId, RemoveParticipationRequestModel request), RemoveOwnParticipationCommand>()
                .Map(dest => dest.ApplicatioUserId, src => src.appUserId)
                .Map(dest => dest.ParticipationId, src => src.request.ParticipationId);

            config.NewConfig<(Guid appUserId, RemoveParticipationRequestModel request), RemoveParticipationAsOrganizerCommand>()
                .Map(dest => dest.ApplicationUserId, src => src.appUserId)
                .Map(dest => dest.ParticipationId, src => src.request.ParticipationId);

            config.NewConfig<Participation, ParticipationResponse>()
                .Map(dest => dest.Id, src => src.Id.Value)
                .Map(dest => dest.UserId, src => src.UserId.Value)
                .Map(dest => dest.EventId, src => src.EventId.Value);
        }
    }
}
