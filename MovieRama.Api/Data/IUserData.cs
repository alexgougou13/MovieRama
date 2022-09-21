using MovieRama.Api.Models;

namespace MovieRama.Api.Data
{
    public interface IUserData
    {
        string[] Login(UserDto user);
        Task<User> AddUserAsync(UserDtoRegister user);
        User GetUser(Guid userID);

    }
}
