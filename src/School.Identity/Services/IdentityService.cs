using Microsoft.AspNetCore.Identity;
using School.Application.DTOs.Request;
using School.Application.DTOs.Response;
using School.Application.Interfaces;

namespace School.Identity.Services;

public class IdentityService : IIdentityService
{
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly UserManager<IdentityUser> _userManager;
    // private readonly JwtOptions _jwtOptions;

    public IdentityService(
        SignInManager<IdentityUser> signInManager,
        UserManager<IdentityUser> userManager)
    // JwtOptions jwtOptions)
    {
        _signInManager = signInManager;
        _userManager = userManager;
        // _jwtOptions = jwtOptions;
    }

    public async Task<UserLoginResponse> Login(UserLoginRequest userLogin)
    {
        var result = await _signInManager.PasswordSignInAsync(userLogin.Email, userLogin.Password, false, true);
        // if(result.Succeeded)
        //     return await GerarToken(userLogin.Email);

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
}