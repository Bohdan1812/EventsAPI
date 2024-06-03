using Application.Persistence.Repositories;
using Application.Users.Dto;
using Domain.UserAggregate;
using ErrorOr;
using Mapster;
using MediatR;

namespace Application.Users.Queries.FindUsers
{
    public class FindUsersQueryHandler : IRequestHandler<FindUsersQuery, ErrorOr<List<User>>>
    {
        private readonly IUserRepository _userRepository;

        public FindUsersQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<ErrorOr<List<User>>> Handle(FindUsersQuery request, CancellationToken cancellationToken)
        {
            return await _userRepository.FindUsers(request.Email, request.FirstName, request.LastName);
            
        }
    }
}
