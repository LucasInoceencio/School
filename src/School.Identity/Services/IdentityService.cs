using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using School.Application.DTOs.Request;
using School.Application.DTOs.Response;
using School.Application.Interfaces;
using School.Identity.Configurations;

namespace School.Identity.Services;

public class IdentityService : IIdentityService
{
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly UserManager<IdentityUser> _userManager;
    private readonly JwtOptions _jwtOptions;

    public IdentityService(
        SignInManager<IdentityUser> signInManager,
        UserManager<IdentityUser> userManager,
        IOptions<JwtOptions> jwtOptions)
    {
        _signInManager = signInManager;
        _userManager = userManager;
        _jwtOptions = jwtOptions.Value;
    }

    public async Task<UserLoginResponse> Login(UserLoginRequest userLogin)
    {
        var result = await _signInManager.PasswordSignInAsync(userLogin.Email, userLogin.Password, false, true);
        if (result.Succeeded)
            return await GenerateCredentials(userLogin.Email);

        var userLoginResponse = new UserLoginResponse(result.Succeeded);
        if (!result.Succeeded)
        {
            if (result.IsLockedOut)
                userLoginResponse.AddError("This account is locked");
            else if (result.IsNotAllowed)
                userLoginResponse.AddError("This account doesn't have permission to sign-in");
            else if (result.RequiresTwoFactor)
                userLoginResponse.AddError("Confirm your sign-in");
            else
                userLoginResponse.AddError("Username or password is incorrect");
        }

        return userLoginResponse;
    }

    private string GenerateToken(IEnumerable<Claim> claims, DateTime dataExpiracao)
    {
        var jwt = new JwtSecurityToken(
            issuer: _jwtOptions.Issuer,
            audience: _jwtOptions.Audience,
            claims: claims,
            notBefore: DateTime.Now,
            expires: dataExpiracao,
            signingCredentials: _jwtOptions.SigningCredentials);

        return new JwtSecurityTokenHandler().WriteToken(jwt);
    }

    private async Task<IList<Claim>> GetClaimsAndRoles(IdentityUser user, bool addUserClaims)
    {
        var claims = new List<Claim>();

        claims.Add(new Claim(JwtRegisteredClaimNames.Sub, user.Id));
        claims.Add(new Claim(JwtRegisteredClaimNames.Email, user.Email));
        claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
        claims.Add(new Claim(JwtRegisteredClaimNames.Nbf, DateTime.Now.ToString()));
        claims.Add(new Claim(JwtRegisteredClaimNames.Iat, DateTime.Now.ToString()));

        if (addUserClaims)
        {
            var userClaims = await _userManager.GetClaimsAsync(user);
            var roles = await _userManager.GetRolesAsync(user);

            claims.AddRange(userClaims);

            foreach (var role in roles)
                claims.Add(new Claim("role", role));
        }

        return claims;
    }

    public async Task<UserRegisterResponse> RegisterUser(UserRegisterRequest userRegister)
    {
        var identityUser = new IdentityUser
        {
            UserName = userRegister.Email,
            Email = userRegister.Email,
            EmailConfirmed = true
        };

        var result = await _userManager.CreateAsync(identityUser, userRegister.Password);
        if (result.Succeeded)
            await _userManager.SetLockoutEnabledAsync(identityUser, false);

        var userRegisterResponse = new UserRegisterResponse(result.Succeeded);
        if (!result.Succeeded && result.Errors.Count() > 0)
            userRegisterResponse.AddErrors(result.Errors.Select(f => f.Description));

        return userRegisterResponse;
    }

    public async Task<UserLoginResponse> LoginWithoutPassword(string usuarioId)
    {
        var userLoginResponse = new UserLoginResponse();
        var user = await _userManager.FindByIdAsync(usuarioId);

        if (await _userManager.IsLockedOutAsync(user))
            userLoginResponse.AddError("Essa conta está bloqueada");
        else if (!await _userManager.IsEmailConfirmedAsync(user))
            userLoginResponse.AddError("Essa conta precisa confirmar seu e-mail antes de realizar o login");

        if (userLoginResponse.Success)
            return await GenerateCredentials(user.Email);

        return userLoginResponse;
    }

    private async Task<UserLoginResponse> GenerateCredentials(string email)
    {
        var user = await _userManager.FindByEmailAsync(email);
        var accessTokenClaims = await GetClaimsAndRoles(user, addUserClaims: true);
        var refreshTokenClaims = await GetClaimsAndRoles(user, addUserClaims: false);

        var expirationDateAccessToken = DateTime.Now.AddSeconds(_jwtOptions.AccessTokenExpiration);
        var expirationDateRefreshToken = DateTime.Now.AddSeconds(_jwtOptions.RefreshTokenExpiration);

        var accessToken = GenerateToken(accessTokenClaims, expirationDateAccessToken);
        var refreshToken = GenerateToken(refreshTokenClaims, expirationDateRefreshToken);

        return new UserLoginResponse
        (
            success: true,
            accessToken: accessToken,
            refreshToken: refreshToken
        );
    }
}