using System.ComponentModel.DataAnnotations;
using HoangTLM.Core.Interfaces;

namespace HoangTLM.Entities.CoreEntities
{
    public class System_EventLog : IAuditableFull
    {
        [Key]
        public Guid Id { get; set; }
        public string EventType { get; set; } = string.Empty; // Tên loại event (ví dụ: OrderCreated, UserRegistered, ...)
        public string? EventData { get; set; } // Dữ liệu event (JSON)
        public string? Source { get; set; } // Nguồn phát sinh event (service, module, ...)
        public string? Status { get; set; } // Trạng thái event (Pending, Processed, Failed, ...)
        public string? QueueName { get; set; } // Tên queue nếu có
        public string? CacheKey { get; set; } // Cache key nếu có
        public DateTime EventTime { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public Guid? CreatedUserId { get; set; }
        public Guid? UpdatedUserId { get; set; }
    }
} 