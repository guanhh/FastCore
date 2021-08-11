using FastCore.Base;
using FastCore.EFCore.SqlServer;
using FastCore.Model;
using System;
using System.Linq;

namespace FastCore.DbMigration
{
    public class DbInitializer
    {
        public static void Seed(FastCoreContext context)
        {
            if (context.FastUsers.Any())
            {
                return;
            }

            context.AddRange(
                new FastUser() { UserId = Guid.NewGuid(), UserName = "admin", Password = Md5Helper.Encrypt("1234qwer"), PhoneNumber = "10086", Status = 1 }
                );

            context.SaveChanges();
        }

    }
}
