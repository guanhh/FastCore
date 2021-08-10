using System;

namespace FastCore.Auditing
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class DisableAuditingAttribute : Attribute
    {

    }
}
