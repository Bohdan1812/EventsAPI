

namespace EventsAPI.Services.UserSevices
{
    public interface IUserService
    {
        Task<ServiceResponse<List<User>>> GetAllUsers();
        Task<ServiceResponse<User>> GetUserById(Guid id);
        Task<ServiceResponse<User>> AddUser(AddUserDto newUser, Guid appUserId);
        Task<ServiceResponse<User>> UpdateUser(User updatedUser);
        Task<ServiceResponse<bool>> DeleteUser(Guid appUserId);
        Task<ServiceResponse<User>> GetUserByAppUserId(Guid appUserId); 
    }
}
