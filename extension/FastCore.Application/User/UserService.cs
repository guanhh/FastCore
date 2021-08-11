using FastCore.Abstract;
using FastCore.Base;
using FastCore.EFCore.SqlServer;
using FastCore.Model;
using FastCore.Model.Result;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FastCore.Application
{
    public class UserService : IUserService, IScopeService
    {
        private readonly FastCoreContext _dbContext;

        public UserService(FastCoreContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ResultMsg<int>> ModifyPasswordAsync(Guid userId, ModifyPasswordReq modifyPasswordReq)
        {
            throw new NotImplementedException();
        }

        public async Task<ResultMsg<int>> AddUserAsync(FastUser userinfo)
        {
            throw new NotImplementedException();
        }

        public async Task<ResultMsg<int>> EditUserAsync(FastUser userinfo)
        {

            throw new NotImplementedException();
        }

        private async Task<bool> UserExistAsync(string username)
        {
            bool Hasuser = await _dbContext.FastUsers.AnyAsync(m => m.UserName == username);

            if (Hasuser)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<ResultMsg<bool>> CheckUserAsync(string username)
        {
            var HasUser = await _dbContext.FastUsers.FirstOrDefaultAsync(x => x.UserName == username);
            if (HasUser == null)
            {
                return new ResultMsg<bool>
                {
                    code = (int)StatusCode.Success,
                    data = false
                };
            }
            else
            {
                return new ResultMsg<bool>
                {
                    code = (int)StatusCode.Success,
                    data = true
                };
            }
        }

        public async Task<ResultMsg<UserInfoResp>> UserInfoAsync(Guid UserId)
        {
            throw new NotImplementedException();
        }

        public async Task<ResultMsg<Data<List<UserInfoListResp>>>> UserListAsync(int page, int limit)
        {
            throw new NotImplementedException();
        }

        public async Task<ResultMsg<int>> EnableUserAsync(Guid UserId)
        {
            var User = await _dbContext.FastUsers.FindAsync(UserId);

            User.Status = 1;
            await _dbContext.SaveChangesAsync();

            return new ResultMsg<int>
            {
                code = (int)StatusCode.Success,
                message = $"用户启用成功！"
            };
        }

        public async Task<ResultMsg<int>> DisableUserAsync(Guid UserId)
        {
            var User = await _dbContext.FastUsers.FindAsync(UserId);

            User.Status = 0;
            await _dbContext.SaveChangesAsync();

            return new ResultMsg<int>
            {
                code = (int)StatusCode.Success,
                message = $"用户停用成功！"
            };
        }


    }
}
