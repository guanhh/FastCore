using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FastCore.Model
{
    [Table("Base_AuditLogs")]
    public class AuditLog
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AuditLogId { get; set; }

        [MaxLength(96)]
        public string ApplicationName { get; set; }

        public int? TenantId { get; set; }

        [MaxLength(64)]
        public string TenantName { get; set; }

        public int? UserId { get; set; }

        [MaxLength(64)]
        public string UserName { get; set; }

        public DateTime ExecutionTime { get; set; }

        public int ExecutionDuration { get; set; }

        [MaxLength(64)]
        public string ClientIpAddress { get; set; }

        [MaxLength(128)]
        public string ClientName { get; set; }

        [MaxLength(64)]
        public string ClientId { get; set; }

        [MaxLength(64)]
        public string CorrelationId { get; set; }

        [MaxLength(512)]
        public string BrowserInfo { get; set; }

        [MaxLength(10)]
        public string HttpMethod { get; set; }

        [MaxLength(256)]
        public string Url { get; set; }

        [MaxLength]
        public string Exceptions { get; set; }

        [MaxLength]
        public string RequestContent { get; set; }

        [MaxLength]
        public string ResponseContent { get; set; }

        public int? HttpStatusCode { get; set; }

    }
}
