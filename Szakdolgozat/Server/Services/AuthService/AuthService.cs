using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Szakdolgozat.Server.Services.AuthService
{

    public class AuthService : IAuthService
    {
        private readonly DataContext _context;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthService(DataContext context, IConfiguration configuration,IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor  = httpContextAccessor;
            _context = context;
            _configuration = configuration;
        }

        public int GetUserId()
        {
            return int.Parse(_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));
           /* var userIdString = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (!string.IsNullOrEmpty(userIdString))
            {
                // Az érték létezik, próbáljuk meg konvertálni int típusúvá
                if (int.TryParse(userIdString, out int userId))
                {
                    // A konverzió sikerült, a userId változó tartalmazza a konvertált értéket
                    // Tehát most meg lehet tenni azokat a lépéseket, amelyeket a helyes felhasználóazonosítóval kell végrehajtani
                    // Például:
                     return userId;
                    // vagy
                    // return true;
                }
                else
                {
                    // Nem sikerült az érték konvertálása, valószínűleg nem egész számot tartalmaz
                    // Ebben az esetben figyelmeztetést vagy naplóbejegyzést lehet készíteni a hibáról
                    throw new Exception("Nem sikerült az érték konvertálása, valószínűleg nem egész számot tartalmaz");
                }
            }
            else
            {
                // Az érték üres vagy null, valami probléma lehet a felhasználó azonosításával
                // Ebben az esetben figyelmeztetést vagy naplóbejegyzést lehet készíteni a hibáról
                throw new Exception("Az érték üres vagy null, valami probléma lehet a felhasználó azonosításával");
               /* int id = 4;
                return id;
            } */
        }

        public async Task<ServiceResponse<string>> Login(string email, string password)
        {
            var response = new ServiceResponse<string>();
            var user = await _context.User.FirstOrDefaultAsync(x => x.Email.ToLower().Equals(email.ToLower()));
            if (user == null)
            {
                response.Success = false;
                response.Message = "User not found.";
                return response;
            }
            else if (!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
            {
                response.Success = false;
                response.Message = "Wrong password.";
                return response;
            }
            else
            {
                response.Data = CreateWebToken(user);
                response.Message = "Login successful!";
            }



            return response;
        }
        public async Task<ServiceResponse<int>> Register(User user, string password)
        {
            if (await UserExists(user.Email))
            {
                return new ServiceResponse<int>
                {
                    Success = false,
                    Message = "User already exists."
                };
            }

            CreatePasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;
            _context.User.Add(user);
            await _context.SaveChangesAsync();

            return new ServiceResponse<int>
            {
                Data = user.Id,
                Message = "Registration successful!"
            };
        }

        public async Task<bool> UserExists(string email)
        {
            if (await _context.User.AnyAsync(user => user.Email.ToLower().Equals(email.ToLower())))
            {
                return true;
            }
            return false;
        }
        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }
        private string CreateWebToken(User user)
        {
            List<Claim> claims = new List<Claim>
           {
               new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
               new Claim(ClaimTypes.Name, user.Email)
           };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8
                   .GetBytes(_configuration.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

            var token = new JwtSecurityToken(

                             claims: claims,
                              expires: DateTime.Now.AddDays(1),
                                signingCredentials: creds
                                               );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return jwt;

        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));

                return computedHash.SequenceEqual(passwordHash);

            }

        }




       
    }
}
