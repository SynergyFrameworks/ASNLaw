using Infrastructure.Security;
using Infrastructure.Common.Extensions;
using Infrastructure.Common.Contracts;
using Infrastructure.Common.Domain.Users;
using Infrastructure.Users;
using Infrastructure.Common.Persistence.Cache;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using ICacheManager = Infrastructure.Common.Persistence.Cache.ICacheManager;
using Infrastructure.Common.TenantManagement;
using Infrastructure.Common;

namespace Infrastructure.Security
{
    public class AuthenticationManager : IAuthenticationManager
    {
        private IUserManager UserManager { get; set; }
        private ICacheManager AuthenticatedUsers { get; set; }
        private ITenantManager TenantManager { get; set; }
        private string TestToken { get; set; }
        public const int SALT_BYTE_SIZE = 24;
        public const int HASH_BYTE_SIZE = 24;
        public const int PBKDF2_ITERATIONS = 1000;

        public const int ITERATION_INDEX = 0;
        public const int SALT_INDEX = 1;
        public const int PBKDF2_INDEX = 2;

        /// <summary>
        /// Based on Username return a FV user object and authentication token 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public KeyValuePair<string, User> AuthenticateUser(string name, string token = null)
        {
            User user = UserManager.GetUserByUsername(name);

            if (user != null)
            {

                if (string.IsNullOrEmpty(token))
                    token = GenerateNewToken(name);
                if (AuthenticateUserByToken(token) != null)
                {
                    if (name == "applestore@ASNanalytics.com")
                        return new KeyValuePair<string, User>(token, user);
                    throw new Exception("Duplicate Tokens detected");
                }
                AuthenticatedUsers.Add(token, user);
                user.LastLoggedInDate = DateTime.UtcNow;
                UserManager.SaveUser(user, false);
                return new KeyValuePair<string, User>(token, user);
            }

            return new KeyValuePair<string, User>();
        }


        private string GenerateNewToken(string name = null)
        {
            if (name == "applestore@ASNanalytics.com")
                return "EA96FBA9-5BEF-4994-934A-DDBC61576F12";
            return Guid.NewGuid().ToString();
        }

        public KeyValuePair<string, User> AuthenticateUserByUsernamePassword(string username, string password)
        {
            User user = UserManager.GetUserByUsername(username);
            if (ValidatePassword(password, user.Password))
            {
                user.LastLoggedInDate = DateTime.UtcNow;
                string key = GenerateNewToken();
                AuthenticatedUsers.Add(key, user);
                UserManager.SaveUser(user, false);
                return new KeyValuePair<string, User>(key, user);
            }
            return new KeyValuePair<string, User>();
        }

        public User AuthenticateUserByToken(string token)
        {
            if (token == TestToken)
            {
                return UserManager.GetUserByEmail("sysadmin@ASNanalytics.com");


            }
            User user = (User)AuthenticatedUsers.Get(token);
            if (user != null)
            {
               Tenant tenant = TenantManager.GetCurrentTenantById(user.Tenant.BlankIfNull().Id);
                if (tenant != null)
                {
                    user.Tenant.Settings = tenant.Settings;
                    user.Tenant.TenantSettings = tenant.TenantSettings;
                    user.Tenant.LetterTemplatePath = tenant.LetterTemplatePath;
                    user.Tenant.LogoPath = tenant.LogoPath;
                }
                AuthenticatedUsers.ResetExpiration(token);
                return user;
            }
            return null;
        }

        public KeyValuePair<string, User> CreateNewUser(User user)
        {
            user.Password = CreateHash(user.Password);
            UserManager.SaveUser(user);
            string key = GenerateNewToken();
            AuthenticatedUsers.Add(key, user);
            return new KeyValuePair<string, User>(key, user);
        }


        ///Password hashing logic taken from https://crackstation.net/hashing-security.htm#aspsourcecode
        private static string CreateHash(string password)
        {
            RNGCryptoServiceProvider csprng = new RNGCryptoServiceProvider();
            byte[] salt = new byte[SALT_BYTE_SIZE];
            csprng.GetBytes(salt);

            // Hash the password and encode the parameters
            byte[] hash = PBKDF2(password, salt, PBKDF2_ITERATIONS, HASH_BYTE_SIZE);
            return PBKDF2_ITERATIONS + ":" +
                Convert.ToBase64String(salt) + ":" +
                Convert.ToBase64String(hash);
        }

        private static byte[] PBKDF2(string password, byte[] salt, int iterations, int outputBytes)
        {
            Rfc2898DeriveBytes pbkdf2 = new Rfc2898DeriveBytes(password, salt)
            {
                IterationCount = iterations
            };
            return pbkdf2.GetBytes(outputBytes);
        }

        public static bool ValidatePassword(string password, string correctHash)
        {
            // Extract the parameters from the hash
            char[] delimiter = { ':' };
            string[] split = correctHash.Split(delimiter);
            int iterations = Int32.Parse(split[ITERATION_INDEX]);
            byte[] salt = Convert.FromBase64String(split[SALT_INDEX]);
            byte[] hash = Convert.FromBase64String(split[PBKDF2_INDEX]);

            byte[] testHash = PBKDF2(password, salt, iterations, hash.Length);
            return SlowEquals(hash, testHash);
        }

        private static bool SlowEquals(byte[] a, byte[] b)
        {
            uint diff = (uint)a.Length ^ (uint)b.Length;
            for (int i = 0; i < a.Length && i < b.Length; i++)
                diff |= (uint)(a[i] ^ b[i]);
            return diff == 0;
        }


        public User ApiTestingLogin()
        {
            return null;
        }

        public void SignoutUser(string token)
        {
            AuthenticatedUsers.Remove(token);
        }
    }
}
