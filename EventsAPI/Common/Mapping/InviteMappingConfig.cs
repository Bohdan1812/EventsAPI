using Application.Invites.Commands.AddInvite;
using Application.Invites.Commands.DeleteOwnInvite;
using Application.Invites.Commands.RemoveAsOrganizer;
using Application.Invites.Queries.GetEventInvites;
using Application.Invites.Queries.GetInvite;
using Application.Invites.Queries.GetUserInvites;
using Contracts.Invite;
using Domain.InviteAggregate;
using Mapster;

namespace Api.Common.Mapping
{
    public class InviteMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<(Guid appUserId, AddInviteRequestModel request), AddInviteCommand>()
                .Map(dest => dest.ApplicationUserId, src => src.appUserId)
                .Map(dest => dest.EventId, src => src.request.EventId)
                .Map(dest => dest.UserId, src => src.request.UserId);

            config.NewConfig<(Guid appUserId, RemoveInviteRequestModel request), RemoveOwnInviteCommand>()
                .Map(dest => dest.ApplicatonUserId, src => src.appUserId)
                .Map(dest => dest.InviteId, src => src.request.InviteId);
            
            config.NewConfig<(Guid appUserId, RemoveInviteRequestModel request), RemoveOrganizerInviteCommand>()
               .Map(dest => dest.ApplicationUserId, src => src.appUserId)
               .Map(dest => dest.InviteId, src => src.request.InviteId);

            config.NewConfig<(Guid appUserId, GetInviteRequestModel request), GetInviteQuery>()
               .Map(dest => dest.ApplicatioUserId, src => src.appUserId)
               .Map(dest => dest.InviteId, src => src.request.InviteId);

            config.NewConfig<Invite, GetInviteResponse>()
                .Map(dest => dest.Id, src => src.Id.Value)
                .Map(dest => dest.EventId, src => src.EventId.Value)
                .Map(dest => dest.UserId, src => src.UserId.Value);

            config.NewConfig<Guid, GetUserInvitesQuery>()
                .Map(dest => dest.ApplicatioUserId, src => src);

            config.NewConfig<(Guid appUserId, GetEventInvitesRequestModel request), GetEventInvitesQuery>()
                .Map(dest => dest.ApplicationUserId, src => src.appUserId)
                .Map(dest => dest.EventId, src => src.request.EventId);
        }
    }
}
