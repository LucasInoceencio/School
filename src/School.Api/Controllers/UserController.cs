using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using School.Application.DTOs.Request;
using School.Application.DTOs.Response;
using School.Application.Interfaces;

namespace School.Api.Controllers;

public class UserController : ControllerBase
{
    private IIdentityService _identityService;

    public UserController(IIdentityService identityService) =>
        _identityService = identityService;

    [HttpPost("register")]
    public async Task<ActionResult<UserRegisterResponse>> Register(UserRegisterRequest userRegister)
    {
        if (!ModelState.IsValid)
            return BadRequest();

        var result = await _identityService.RegisterUser(userRegister);
        if (result.Success)
            return Ok(result);
        else if (result.Errors.Count > 0)
            return BadRequest(result);

        return StatusCode(StatusCodes.Status500InternalServerError);
    }

    [HttpPost("login")]
    public async Task<ActionResult<UserLoginResponse>> Login(UserLoginRequest userLogin)
    {
        if (!ModelState.IsValid)
            return BadRequest();

        var result = await _identityService.Login(userLogin);
        if (result.Success)
            return Ok(result);

        return Unauthorized(result);
    }

    [Authorize]
    [HttpPost("refresh-login")]
    public async Task<ActionResult<UserRegisterResponse>> RefreshLogin()
    {
        var identity = HttpContext.User.Identity as ClaimsIdentity;
        var userId = identity?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (userId == null)
            return BadRequest();

        var result = await _identityService.LoginWithoutPassword(userId);
        if (result.Success)
            return Ok(result);

        return Unauthorized(result);
    }
}