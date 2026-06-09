using Application.Authentication.Commands.Login;
using Application.Authentication.Commands.Register;
using MediatR;

namespace EventsApi.Endpoints
{
    public static class AuthEndpoints
    {
        public static void MapAuthEndpoints(this IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("/auth").WithTags("Authentication");

            group.MapPost("/login", async (LoginCommand command, ISender sender) =>
            {
                var result = await sender.Send(command);

                return result.Match(
                    token => Results.Ok(token),
                    errors => Results.BadRequest(errors)
                );
            });

            group.MapPost("/register", async (RegisterCommand command, ISender sender) =>
            {
                var result = await sender.Send(command);

                return result.Match(
                    message => Results.Ok(message),
                    errors => Results.BadRequest(errors)
                );
            });
        }
    }
}