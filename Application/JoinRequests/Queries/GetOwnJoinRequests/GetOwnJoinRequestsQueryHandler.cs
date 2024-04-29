
using Application.Common.Errors;
using Application.Persistence.Repositories;
using Domain.JoinRequestAggregate;
using ErrorOr;
using MediatR;

namespace Application.JoinRequests.Queries.GetOwnJoinRequests
{
    public class GetOwnJoinRequestsQueryHandler
        : IRequestHandler<GetOwnJoinRequestsQuery, ErrorOr<List<JoinRequest>>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IJoinRequestRepository _joinRequestRepository;

        public GetOwnJoinRequestsQueryHandler(IUserRepository userRepository, IJoinRequestRepository joinRequestRepository)
        {
            _userRepository = userRepository;
            _joinRequestRepository = joinRequestRepository;
        }

        public async Task<ErrorOr<List<JoinRequest>>> Handle(GetOwnJoinRequestsQuery query, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetUser(query.ApplicationUserId);

            if (user is null)
                return UserError.UserNotFound;

            return await _joinRequestRepository.GetJoinRequestsByUser(user.Id);
        }
    }

}
