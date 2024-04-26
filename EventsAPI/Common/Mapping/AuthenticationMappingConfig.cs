using Application.Authentication.Commands.Register;
using Application.Users.Commands.Delete;
using Contracts.Authentication;
using Domain.Common.Models;
using Mapster;
using Microsoft.Build.Framework;

namespace Api.Common.Mapping
{
    public class AuthenticationMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<RegisterRequestModel, RegisterCommand>();

            config.NewConfig<(Guid, DeleteAccountRequestModel),DeleteAccountCommand>()
                .Map(dest => dest.AppUserId, src => src.Item1)
                .Map(dest => dest.Password, src => src.Item2.Password);


        }
    }
}
