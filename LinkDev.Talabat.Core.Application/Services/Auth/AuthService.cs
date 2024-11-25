using AutoMapper;
using LinkDev.Talabat.Core.Application.Abstraction.Models.Auth;
using LinkDev.Talabat.Core.Application.Abstraction.Models.Common;
using LinkDev.Talabat.Core.Application.Abstraction.Services.Auth;
using LinkDev.Talabat.Core.Application.Exceptions;
using LinkDev.Talabat.Core.Application.Extensions;
using LinkDev.Talabat.Core.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace LinkDev.Talabat.Core.Application.Services.Auth
{
    public class AuthService(
        IMapper mapper,
        IOptions<JwtSettings> jwtSettings,
        UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager) : IAuthService
    {
        private readonly JwtSettings _jwtSettings = jwtSettings.Value;



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
            //if (EmailExists(registerDto.Email).Result) throw new BadRequestException("this email is already exist");


            var user = new ApplicationUser
            {
                DisplayName = registerDto.DisplayName,
                Email = registerDto.Email,
                UserName = registerDto.UserName,
                PhoneNumber = registerDto.PhoneNumber,
            };
            var result = await userManager.CreateAsync(user, registerDto.Password);
            if (!result.Succeeded)
                throw new ValidationException("") { Errors = result.Errors.Select(E => E.Description).ToArray() };

            var response = new UserDto()
            {
                Id = user.Id,
                DisplayName = user.DisplayName,
                Email = user.Email!,
                Token = await GenerateTokenAsync(user)

            };
            return response;
        }
        public async Task<UserDto> GetCurrentUserAsync(ClaimsPrincipal claimsPrincipal)
        {
            var email = claimsPrincipal.FindFirstValue(ClaimTypes.Email);

            var user = await userManager.FindByEmailAsync(email);

            return new UserDto()
            {
                DisplayName = user.DisplayName,
                Email = email,
                Id = user.Id,
                Token = await GenerateTokenAsync(user)
            };
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

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));

            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var tokenObj = new JwtSecurityToken(

                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                expires: DateTime.UtcNow.AddMinutes(_jwtSettings.DurationTimeInMintues),
                claims: claims,
                signingCredentials: signingCredentials
                );

            return new JwtSecurityTokenHandler().WriteToken(tokenObj);
        }

        public async Task<AddressDto?> GetUserAddressAsync(ClaimsPrincipal claimsPrincipal)
        {

            var user = await userManager.FindUserWithAddress(claimsPrincipal);

            var address = mapper.Map<AddressDto>(user!.Address);

            return address;
        }

        public async Task<AddressDto?> UpdateUserAddressAsync(ClaimsPrincipal claim, AddressDto addressDto)
        {
            var user = await userManager.FindUserWithAddress(claim);

            var address = mapper.Map<Address>(addressDto);
            if (user?.Address is not null)
            {
                address.Id = user.Address.Id;
            }

            user.Address = address;

            var result = await userManager.UpdateAsync(user);

            if (!result.Succeeded) throw new BadRequestException(result.Errors.Select(error => error.Description).Aggregate((X, Y) => $"{X} {Y}"));

            return addressDto;

        }

        public async Task<bool> EmailExists(string email)
        {

            return await userManager.FindByEmailAsync(email) is not null;
        }
    }
}
