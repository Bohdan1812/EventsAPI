using Mapster;
using Application.Users.Commands.Update;
using Domain.UserAggregate.ValueObjects;
using Contracts.User;
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
                
        }
    }
}
