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
        [Route("login")]
        public ResultMsg<TokenResp> Login([FromBody] LoginReq loginReq)
        {
            return _authService.Login(loginReq);
        }

        /// <summary>
        /// 刷新token
        /// </summary>
        /// <param name="tokenReq"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("refresh")]
        public ResultMsg<TokenResp> Refresh(TokenReq tokenReq)
        {
            return _authService.Refresh(tokenReq);
        }

        /// <summary>
        /// 撤销refreshtoken
        /// </summary>
        /// <returns></returns>
        [HttpPost, Authorize]
        [Route("revoke")]
        public ResultMsg<bool> Revoke()
        {
            return _authService.Revoke(User.Identity.Name);
        }

        /// <summary>
        /// 查询人员信息
        /// </summary>
        /// <returns></returns>
        [HttpGet, Authorize]
        [Route("userinfo")]
        public async Task<ResultMsg<Data<UserInfoResp>>> GetUser()
        {
            return await _authService.GetUser(int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value));
        }


    }
}
