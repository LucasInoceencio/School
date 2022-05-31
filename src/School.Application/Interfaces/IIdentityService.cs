namespace School.Application.Interfaces;

public interface IIdentityService
{
    Task<UserRegisterResponse> CadastrarUsuario(UserRegisterRequest userRegister);
    Task<UserLoginResponse> Login(UserLoginRequest userLogin);
}