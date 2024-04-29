using Application.JoinRequests.Commands.Add;
using Contracts.JoinRequest;
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
        }
    }
}
