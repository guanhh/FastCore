using System;
using System.Collections.Generic;
using System.Text;

namespace FastCore.Abstract
{
    public class UserInfoListResp
    {
        /// <summary>
        /// 用户Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 用户类型
        /// </summary>
        public int UIType { get; set; }

        /// <summary>
        /// 用户类型名称(0集团内部用户,1合作伙伴用户)
        /// </summary>
        public string UITypeName { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        public string UIName { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string UIPassword { get; set; }

        /// <summary>
        /// 用户状态(0 注销,1 启用)
        /// </summary>
        public string UIStatusInfo { get; set; }

        /// <summary>
        /// 所属公司Id(当用户类型为0 1 时查询内部分部Id,当用户类型为2 PartnerType为0 时查询客户Id,当用户类型为2 PartnerType为1时查询供应商Id,当用户类型为2 PartnerType为2时查询承运商Id)
        /// </summary>
        public int UICompId { get; set; }

        /// <summary>
        /// 所属公司名称(当用户类型为0 1 时查询内部分部名称,当用户类型为2 PartnerType为0 时查询客户名称,当用户类型为2 PartnerType为1时查询供应商名称,当用户类型为2 PartnerType为2时查询承运商名称)
        /// </summary>
        public string UICompName { get; set; }

        /// <summary>
        /// 用户使用人员Id(当用户类型为0 1 时查询内部人员Id,当用户类型为2 PartnerType为0 时查询客户联系人Id,当用户类型为2 PartnerType为1时查询供应商联系人Id,当用户类型为2 PartnerType为2时查询承运商联系人Id)
        /// </summary>
        public int StaffId { get; set; }

        /// <summary>
        /// 用户使用人员名称(当用户类型为0 1 时查询内部人员名称,当用户类型为2 PartnerType为0 时查询客户联系人名称,当用户类型为2 PartnerType为1时查询供应商联系人名称,当用户类型为2 PartnerType为2时查询承运商联系人名称)
        /// </summary>
        public string StaffName { get; set; }

        /// <summary>
        /// 人员编号
        /// </summary>
        public string StaffCode { get; set; }

        /// <summary>
        /// 部门名称
        /// </summary>
        public string DepName { get; set; }
    }
}
