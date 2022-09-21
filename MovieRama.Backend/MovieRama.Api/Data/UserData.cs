using Microsoft.IdentityModel.Tokens;
using MovieRama.Api.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace MovieRama.Api.Data
{
    public class UserData : IUserData
    {
        private DataContext _context;
        private readonly IConfiguration _configuration;
        public UserData(DataContext userContext, IConfiguration configuration)
        {
            _context = userContext;
            _configuration = configuration;
        }
        //Implementing Interface
        public async Task<User> AddUserAsync(UserDtoRegister request)
        {
            var user = new User();
            user.Id=Guid.NewGuid();
            user.Username = request.Username;
            var existingUser=_context.Users.Where(x => x.Username==user.Username).FirstOrDefault();
            if (existingUser != null)
            {
                return null;
            }
            CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;
            user.FirstName = request.FirstName;
            user.LastName = request.LastName;
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public User GetUser(Guid userID)
        {
            var user = _context.Users.Find(userID);
            return user;
        }

        public string[] Login(UserDto request)
        {
            var existingUser = _context.Users.Where(x => x.Username == request.UserName).FirstOrDefault();

            if (existingUser == null || !VerifyPasswordHash(request.Password, existingUser))
            {
                return null;
            }
            string token = CreateToken(existingUser);
            string[] result = {token,existingUser.Id.ToString()};
            return result;
        }

        // Methods for creating and generating JWT //
        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }
        private bool VerifyPasswordHash(string password, User existingUser)
        {
            using (var hmac = new HMACSHA512(existingUser.PasswordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(existingUser.PasswordHash);
            }
        }
        private string CreateToken(User user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name,user.Username),
                new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
            };
            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return jwt;
        }

       
    }
}
