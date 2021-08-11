using FastCore.Abstract;
using FastCore.Abstract.Security;
using FastCore.Base;
using FastCore.EFCore.SqlServer;
using FastCore.Model.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace FastCore.Application.Security
{
    public class AuthService : IAuthService, IScopeService
    {

        private readonly ICache _cache;
        private readonly FastCoreContext _dbContext;
        private readonly ITokenService _tokenService;

        private TimeSpan _ts = new(24, 0, 0, 0);

        public AuthService(FastCoreContext dbContext, ITokenService tokenService, ICache cahce)
        {
            _dbContext = dbContext;
            _tokenService = tokenService;
            _cache = cahce;
        }

        public async Task<ResultMsg<Data<UserInfoResp>>> GetUserAsync(Guid userid)
        {
            throw new NotImplementedException();
        }

        public async Task<ResultMsg<TokenResp>> GetTokenAsync(LoginReq loginReq)
        {
            if (loginReq == null)
                return new ResultMsg<TokenResp>()
                {
                    code = (int)StatusCode.Error,
                    message = $"无效请求"
                };

            //加密密码
            var user = _dbContext.FastUsers
                .FirstOrDefault(u => (u.UserName == loginReq.UserName) &&
                                        (u.Password == Md5Helper.Encrypt(loginReq.Password)));

            return await GetTokenByUserAsync(user);

        }

        private async Task<ResultMsg<TokenResp>> GetTokenByUserAsync(Model.FastUser user)
        {
            if (user == null)
                return new ResultMsg<TokenResp>()
                {
                    code = (int)StatusCode.Error,
                    message = $"用户名或密码错误"
                };

            var claims = new List<Claim>
            {
                new Claim(StringConstants.UserId, user.UserId.ToString()),
                new Claim(StringConstants.UserName, user.UserName),
                //其它需要保存的信息

            };

            var accessToken = _tokenService.GenerateAccessToken(claims);
            var refreshToken = _tokenService.GenerateRefreshToken();

            await _cache.SetAsync($"{StringConstants.RefreshToken}:{refreshToken}", user.UserId, _ts, _ts);

            return new ResultMsg<TokenResp>()
            {
                code = (int)StatusCode.Success,
                data = new TokenResp()
                {
                    accesstoken = accessToken,
                    refreshtoken = refreshToken
                }
            };
        }

        public async Task<ResultMsg<TokenResp>> RefreshAsync(TokenReq tokenReq)
        {
            var userId = await _cache.GetOrDefaultAsync($"{StringConstants.RefreshToken}:{tokenReq.refreshtoken}");

            if (userId == null)
                return new ResultMsg<TokenResp>()
                {
                    code = (int)StatusCode.Error,
                    message = $"RefreshToken已过期"
                };

            await _cache.RemoveAsync($"{StringConstants.RefreshToken}:{tokenReq.refreshtoken}");
            var user = await _dbContext.FastUsers.FindAsync(Guid.Parse(userId.ToString()));

            return await GetTokenByUserAsync(user);
        }

        public async Task<ResultMsg<bool>> RevokeAsync(string userName)
        {
            var user = _dbContext.FastUsers.FirstOrDefault(u => u.UserName == userName);

            if (user == null)
                return new ResultMsg<bool>()
                {
                    code = (int)StatusCode.InternalError,
                    message = "不存在该用户"
                };

            //
            await _cache.RemoveAsync($"{StringConstants.RefreshToken}:{user.UserId}");

            return new ResultMsg<bool>()
            {
                code = (int)StatusCode.Success,
                data = true
            };
        }

    }
}
