using Application.JoinRequests.Commands.Add;
using Application.JoinRequests.Commands.RemoveAsOrganizer;
using Application.JoinRequests.Commands.RemoveOwn;
using Application.JoinRequests.Queries.GetJoinRequest;
using Application.JoinRequests.Queries.GetOrganizerJoinRequests;
using Application.JoinRequests.Queries.GetOwnJoinRequests;
using Contracts.JoinRequest;
using Domain.JoinRequestAggregate;
using Mapster;

namespace Api.Common.Mapping
{
    public class JoinRequestMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<(Guid appUserId, CreateJoinRequestRequestModel request), AddJoinRequestCommand>()
                .Map(dest => dest.ApplicatioUserId, src => src.appUserId)
                .Map(dest => dest.EventId, src => src.request.EventId);

            config.NewConfig<Guid, GetOwnJoinRequestsQuery>()
                .Map(dest => dest.ApplicationUserId, src => src);

            config.NewConfig<Guid, GetOrganizerJoinRequestsQuery>()
                .Map(dest => dest.ApplicationUserId, src => src);

            config.NewConfig<(Guid appUserId, Guid joinRequestId), GetJoinRequestQuery>()
                .Map(dest => dest.ApplicationUserID, src => src.appUserId)
                .Map(dest => dest.JoinRequestId, src => src.joinRequestId);

            config.NewConfig<JoinRequest, JoinRequestResponse>()
                .Map(dest => dest.JoinRequestId, src => src.Id.Value)
                .Map(dest => dest.UserId, src => src.UserId.Value)
                .Map(dest => dest.EventId, src => src.EventId.Value);

            config.NewConfig<(Guid appUserId, RemoveJoinRequestRequestModel request), RemoveOwnJoinRequestCommand>()
                .Map(dest => dest.ApplicationUserId, src => src.appUserId)
                .Map(dest => dest.JoinRequestId, src => src.request.JoinRequestId);

            config.NewConfig<(Guid appUserId, RemoveJoinRequestRequestModel request), RemoveOrganizerJoinRequestCommand>()
                .Map(dest => dest.ApplicationUserId, src => src.appUserId)
                .Map(dest => dest.JoinRequestId, src => src.request.JoinRequestId);

            config.NewConfig<JoinRequest, JoinRequestFullInfoResponse>()
                .Map(dest => dest.Id, src => src.Id.Value)
                .Map(dest => dest.Event, src => src.Event)
                .Map(dest => dest.User, src => src.User);    
               



        }
    }
}
