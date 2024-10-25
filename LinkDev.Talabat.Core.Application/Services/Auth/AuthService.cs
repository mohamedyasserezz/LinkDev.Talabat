using LinkDev.Talabat.Core.Application.Abstraction.Models.Auth;
using LinkDev.Talabat.Core.Application.Abstraction.Services.Auth;
using LinkDev.Talabat.Core.Application.Exceptions;
using LinkDev.Talabat.Core.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace LinkDev.Talabat.Core.Application.Services.Auth
{
    public class AuthService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager) : IAuthService
    {
        public async Task<UserDto> LoginAsync(LoginDto loginDto)
        {
            var user = await userManager.FindByEmailAsync(loginDto.Email);
            if (user is null) throw new UnAuthorizedException("InValid Login");

            var result = await signInManager.CheckPasswordSignInAsync(user, loginDto.Password, lockoutOnFailure: true);

            if (result.IsNotAllowed) throw new UnAuthorizedException("account not confirmed yet.");

            if (result.IsLockedOut) throw new UnAuthorizedException("account is LockedOut.");

            if (!result.Succeeded) throw new UnAuthorizedException("InValid Login");

            var response = new UserDto()
            {
                Id = user.Id,
                DisplayName = user.DisplayName,
                Email = user.Email!,
                Token = await GenerateTokenAsync(user)

            };
            return response;
        }

        public async Task<UserDto> RegisterAsync(RegisterDto registerDto)
        {
            var user = new ApplicationUser
            {
                DisplayName = registerDto.DisplayName,
                Email = registerDto.Email,
                UserName = registerDto.UserName,
                PhoneNumber = registerDto.PhoneNumber,
            };
            var result = await userManager.CreateAsync(user, registerDto.Password);
            if (!result.Succeeded)
                throw new ValidationException("") { Errors = result.Errors.Select(E => E.Description) };

            var response = new UserDto()
            {
                Id = user.Id,
                DisplayName = user.DisplayName,
                Email = user.Email!,
                Token = await GenerateTokenAsync(user)

            };
            return response;
        }

        private async Task<string> GenerateTokenAsync(ApplicationUser user)
        {
            var userClaims = await userManager.GetClaimsAsync(user);
            var roles = await userManager.GetRolesAsync(user);

            var rolesClaims = new List<Claim>();

            foreach (var role in roles)
                rolesClaims.Add(new Claim(ClaimTypes.Role, role.ToString()));
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.PrimarySid, user.Id),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.GivenName, user.DisplayName),
                new Claim("secret","Ay 7aga serry"),

            }
            .Union(rolesClaims)
            .Union(userClaims);

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("your-256-bit-secret"));

            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var tokenObj = new JwtSecurityToken(

                issuer: "TalabatIdentity",
                audience: "TalabatUsers",
                expires: DateTime.UtcNow.AddMinutes(10),
                claims: claims,
                signingCredentials: signingCredentials
                );

            return new JwtSecurityTokenHandler().WriteToken(tokenObj);
        }

    }
}
