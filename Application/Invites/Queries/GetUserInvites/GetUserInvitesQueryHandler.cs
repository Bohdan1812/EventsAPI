using Application.Common.Errors;
using Application.Persistence.Repositories;
using Domain.InviteAggregate;
using ErrorOr;
using MediatR;

namespace Application.Invites.Queries.GetUserInvites
{
    public class GetUserInvitesQueryHandler
        : IRequestHandler<GetUserInvitesQuery, ErrorOr<List<Invite>>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IInviteRepository _inviteRepository;

        public GetUserInvitesQueryHandler(IUserRepository userRepository, IInviteRepository inviteRepository)
        {
            _userRepository = userRepository;
            _inviteRepository = inviteRepository;
        }

        public async Task<ErrorOr<List<Invite>>> Handle(GetUserInvitesQuery request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetUser(request.ApplicatioUserId);

            if (user is null)
                return UserError.UserNotFound;

            var invites = await _inviteRepository.GetInvitesByUser(user.Id);

            return invites;
        }
    }}
