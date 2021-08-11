using FastCore.Model.Result;
using System.Threading.Tasks;

namespace FastCore.Abstract.Security
{
    public interface IAuthService
    {
        Task<ResultMsg<TokenResp>> GetTokenAsync(LoginReq loginReq);

        Task<ResultMsg<TokenResp>> RefreshAsync(TokenReq tokenReq);

        Task<ResultMsg<bool>> RevokeAsync(string userName);

        Task<ResultMsg<Data<UserInfoResp>>> GetUserAsync(int userid);

    }
}
