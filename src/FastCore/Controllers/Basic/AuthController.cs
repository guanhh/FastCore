using FastCore.Abstract;
using FastCore.Abstract.Security;
using FastCore.Model.Result;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;

namespace FastCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="loginReq"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("gettoken")]
        public async Task<ResultMsg<TokenResp>> GetTokenAsync([FromBody] LoginReq loginReq)
        {
            return await _authService.GetTokenAsync(loginReq);
        }

        /// <summary>
        /// 刷新token
        /// </summary>
        /// <param name="tokenReq"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("refresh")]
        public async Task<ResultMsg<TokenResp>> RefreshAsync(TokenReq tokenReq)
        {
            return await _authService.RefreshAsync(tokenReq);
        }

        /// <summary>
        /// 撤销refreshtoken
        /// </summary>
        /// <returns></returns>
        [HttpPost, Authorize]
        [Route("revoke")]
        public async Task<ResultMsg<bool>> RevokeAsync()
        {
            return await _authService.RevokeAsync(User.Identity.Name);
        }

        /// <summary>
        /// 查询人员信息
        /// </summary>
        /// <returns></returns>
        [HttpGet, Authorize]
        [Route("userinfo")]
        public async Task<ResultMsg<Data<UserInfoResp>>> GetUserAsync()
        {
            return await _authService.GetUserAsync(int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value));
        }


    }
}
