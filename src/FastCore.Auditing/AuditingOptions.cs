using System;
using System.Collections.Generic;
using System.Text;

namespace FastCore.Auditing
{
    public class AuditingOptions
    {
        /// <summary>
        /// If this value is true, auditing will not throw an exceptions and it will log it when an error occurred while saving AuditLog.
        /// Default: true.
        /// </summary>
        public bool HideErrors { get; set; }

        /// <summary>
        /// Default: true.
        /// </summary>
        public bool IsEnabled { get; set; }

        /// <summary>
        /// The name of the application or service writing audit logs.
        /// Default: null.
        /// </summary>
        public string ApplicationName { get; set; }

        /// <summary>
        /// Default: true.
        /// </summary>
        public bool IsEnabledForAnonymousUsers { get; set; }

        /// <summary>
        /// Audit log on exceptions.
        /// Default: true.
        /// </summary>
        public bool AlwaysLogOnException { get; set; }


        //TODO: Move this to asp.net core layer or convert it to a more dynamic strategy?
        /// <summary>
        /// Default: false.
        /// </summary>
        public bool IsEnabledForGetRequests { get; set; }

    }
}
