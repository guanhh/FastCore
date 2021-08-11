using FastCore.Model.Result;
using System.Threading.Tasks;

namespace FastCore.Abstract.Security
{
    public interface IAuthService
    {
        ResultMsg<TokenResp> Login(LoginReq loginReq);

        ResultMsg<TokenResp> Refresh(TokenReq tokenReq);

        ResultMsg<bool> Revoke(string userName);

        Task<ResultMsg<Data<UserInfoResp>>> GetUser(int userid);

    }
}
