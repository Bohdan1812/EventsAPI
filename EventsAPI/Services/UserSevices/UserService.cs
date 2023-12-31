
using EventsAPI.Data;
using Microsoft.EntityFrameworkCore;

namespace EventsAPI.Services.UserSevices
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly DataContext _context;

        public UserService(IMapper mapper, DataContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<ServiceResponse<User>> AddUser(AddUserDto newUser, Guid appUserId)
        {
            var serviceResponse = new ServiceResponse<User>();
            var user = _mapper.Map<User>(newUser);
            user.ApplicationUserId = appUserId;
            _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            serviceResponse.Data = _context.Users.OrderByDescending(u => u.Id).FirstOrDefault();

            return serviceResponse;
        }

        public async Task<ServiceResponse<bool>> DeleteUser(Guid appUserId)
        {
            var serviceResponse = new ServiceResponse<bool>();

            try
            {
                var user = await _context.Users.FirstOrDefaultAsync(u => u.ApplicationUserId == appUserId);

                if (user is null)
                    throw new Exception($"Application User doesn't contain a user!");

                _context.Users.Remove(user);
                serviceResponse.Data = await _context.SaveChangesAsync() > 0;


            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Data = false;
                serviceResponse.Message = ex.Message;
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<List<User>>> GetAllUsers()
        {
            var serviceResponse = new ServiceResponse<List<User>>();
            serviceResponse.Data = await _context.Users.ToListAsync();
            return serviceResponse;
        }

        public async Task<ServiceResponse<User>> GetUserById(Guid id) 
        {
            var serviceResponse = new ServiceResponse<User>();
            try
            {
                var user = await _context.Users.FirstOrDefaultAsync(c => c.Id == id);
                if (user is null)
                    throw new Exception($"User with id '{id}' not found");


                serviceResponse.Data = user;
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<User>> GetUserByAppUserId(Guid AppUserId)
        {
            var serviceResponce = new ServiceResponse<User>();
            try 
            {
                var user = await _context.Users.FirstOrDefaultAsync(u => u.ApplicationUserId == AppUserId);
                if (user is null)
                    throw new Exception("There is no such user!");

                serviceResponce.Data = user;
            }
            catch (Exception ex) 
            {
                serviceResponce.Message = ex.Message;
                serviceResponce.Success = false;
            }
            return serviceResponce;

        }
            

        public async Task<ServiceResponse<User>> UpdateUser(User updatedUser)
        {
            var serviceResponse = new ServiceResponse<User>();

            try
            {

                var user = await _context.Users.FirstOrDefaultAsync(c => c.Id == updatedUser.Id);

                if (user is null)
                    throw new Exception($"User with id '{updatedUser.Id}' not found");


                user.Name = updatedUser.Name;
                user.Surname = updatedUser.Surname;
                user.Friends = updatedUser.Friends;
                user.EventParticipations = updatedUser.EventParticipations;
                user.InvitationSendings = updatedUser.InvitationSendings;

                _context.Update(user);
                await _context.SaveChangesAsync();

                serviceResponse.Data = await _context.Users.FirstOrDefaultAsync(c => c.Id == updatedUser.Id);
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }

            return serviceResponse;
        }
    } 
}
