using Mapster;
using Application.Users.Commands.Update;
using Contracts.User;
using Application.Users.Queries.GetCurrentUserInfo;
using Application.Users.Dto;
using Application.Users.Queries.GetUserInfo;
namespace Api.Common.Mapping
{
    public class UserMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<(Guid, UpdateUserRequestModel), UpdateUserCommand>()
                .Map(dest => dest.appUserId, src => src.Item1)
                .Map(dest => dest.FirstName, src => src.Item2.FirstName)
                .Map(dest => dest.LastName, src => src.Item2.LastName);

            config.NewConfig<Guid, GetCurrentUserInfoQuery>()
                .Map(dest => dest.ApplicationUserId, src => src);

            config.NewConfig<GetUserInfoRequestModel, GetUserInfoQuery>()
                .Map(dest => dest.UserId, src => src.UserId);
        }
    }
}
