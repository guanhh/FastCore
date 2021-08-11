using FastCore.Model;
using FastCore.Model.Result;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FastCore.Abstract
{
    public interface IUserService
    {

        Task<ResultMsg<int>> AddUserAsync(FastUser userinfo);

        Task<ResultMsg<int>> EditUserAsync(FastUser userinfo);

        Task<ResultMsg<int>> ModifyPasswordAsync(Guid userId, ModifyPasswordReq modifyPasswordReq);

        Task<ResultMsg<Data<List<UserInfoListResp>>>> UserListAsync(int page, int limit);

        Task<ResultMsg<int>> EnableUserAsync(Guid UserId);

        Task<ResultMsg<int>> DisableUserAsync(Guid UserId);

        Task<ResultMsg<bool>> CheckUserAsync(string username);

        Task<ResultMsg<UserInfoResp>> UserInfoAsync(Guid UserId);

    }
}
