using FastCore.Abstract.Security;
using FastCore.Base;
using FastCore.Model.Result;
using FastCore.TestBase;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FastCore.Test
{
    [TestClass]
    public class AuthServiceTest : FastCoreTest
    {
        private readonly IAuthService _authService;
        public AuthServiceTest() : base()
        {
            _authService = GetRequiredService<IAuthService>();
        }

        [TestMethod]
        public void GetToken_UserIsExist()
        {
            var tokenResult = _authService.GetTokenAsync(new LoginReq()
            {
                UserName = "admin",
                Password = "1234qwer"
            });

            Assert.IsTrue(tokenResult.Result.code == (int)StatusCode.Success);
            Assert.IsNotNull(tokenResult.Result.data);
        }

        [TestMethod]
        public void GetToken_UserNotExist()
        {
            var tokenResult = _authService.GetTokenAsync(new LoginReq()
            {
                UserName = "test",
                Password = "test"
            });

            Assert.IsTrue(tokenResult.Result.code == (int)StatusCode.Error);
            Assert.IsNull(tokenResult.Result.data);
        }

    }
}
