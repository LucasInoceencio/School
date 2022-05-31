using School.Application.DTOs.Request;
using School.Application.DTOs.Response;

namespace School.Application.Interfaces;

public interface IIdentityService
{
    Task<UserRegisterResponse> RegisterUser(UserRegisterRequest userRegister);
    Task<UserLoginResponse> Login(UserLoginRequest userLogin);
}