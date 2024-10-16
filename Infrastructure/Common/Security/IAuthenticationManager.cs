using System.Collections.Generic;
using Infrastructure.Common.Domain.Users;

namespace Infrastructure.Security
{
    public interface IAuthenticationManager
    {
        KeyValuePair<string, User> AuthenticateUser(string email, string token = null);
        KeyValuePair<string,User> AuthenticateUserByUsernamePassword(string username, string password);
        User AuthenticateUserByToken(string token);
        KeyValuePair<string, User> CreateNewUser(User user);
        void SignoutUser(string token);
    }
}
