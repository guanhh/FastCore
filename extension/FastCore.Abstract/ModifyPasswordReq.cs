namespace FastCore.Abstract
{
    public class ModifyPasswordReq
    {

        /// <summary>
        /// 老密码
        /// </summary>
        public string OldPassword { get; set; }

        /// <summary>
        /// 新密码
        /// </summary>
        public string NewPassword { get; set; }
    }
}
