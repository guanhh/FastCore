using FastCore.Abstract;
using FastCore.Auditing;
using FastCore.Model;
using FastCore.Model.Result;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FastCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ICurrentUser _currentUser;
        private readonly IUserService _userService;

        public UserController(IUserService userService, ICurrentUser currentUser)
        {
            _userService = userService;
            _currentUser = currentUser;
        }

        /// <summary>
        /// 更新用户信息
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("update")]
        public async Task<ResultMsg<int>> UserEditAsync(FastUser user)
        {
            return await _userService.EditUserAsync(user);
        }

        /// <summary>
        /// 新增用户
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("add")]
        public async Task<ResultMsg<int>> UserAddAsync(FastUser user)
        {
            return await _userService.AddUserAsync(user);
        }


        /// <summary>
        /// 修改用户密码
        /// </summary>
        /// <param name="modifyPasswordReq"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("modifypwd")]
        public async Task<ResultMsg<int>> ModifyPasswordAsync(ModifyPasswordReq modifyPasswordReq)
        {
            return await _userService.ModifyPasswordAsync(_currentUser.UserId, modifyPasswordReq);
        }

        /// <summary>
        /// 检查用户名
        /// </summary>
        /// <param name="username">用户名</param>
        /// <returns></returns>
        [HttpGet]
        [Route("check")]
        public async Task<ResultMsg<bool>> CheckUserAsync(string username)
        {
            return await _userService.CheckUserAsync(username);
        }

        /// <summary>
        /// 获取用户详情
        /// </summary>
        /// <param name="UserId">用户id</param>
        /// <returns></returns>
        [HttpGet]
        [Route("info")]
        public async Task<ResultMsg<UserInfoResp>> UserInfoAsync(string UserId)
        {
            return await _userService.UserInfoAsync(Guid.Parse(UserId));
        }


        [HttpGet]
        [Route("list")]
        public async Task<ResultMsg<Data<List<UserInfoListResp>>>> UserInfoListAsync(int page, int limit)
        {
            return await _userService.UserListAsync(page, limit);
        }


        [Audited]
        [HttpGet]
        [Route("enable/{UserId}")]
        public async Task<ResultMsg<int>> UsersStartUseAsync(string UserId)
        {
            return await _userService.EnableUserAsync(Guid.Parse(UserId));
        }


        [Audited]
        [HttpGet]
        [Route("disable/{UserId}")]
        public async Task<ResultMsg<int>> UsersStopUseAsync(string UserId)
        {
            return await _userService.DisableUserAsync(Guid.Parse(UserId));
        }

    }
}
