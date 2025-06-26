using System.ComponentModel.DataAnnotations;
using HoangTLM.Core.Interfaces;

namespace HoangTLM.Entities.CoreEntities
{
    public class System_ActionLog : IAuditableFull
    {
        [Key]
        public Guid Id { get; set; }
        public string ActionType { get; set; } = string.Empty; // e.g. Create, Update, Delete, Login, etc.
        public string? EntityName { get; set; } // Tên bảng hoặc entity bị tác động
        public Guid? EntityId { get; set; } // Id của entity bị tác động
        public string? Description { get; set; } // Mô tả chi tiết action
        public string? DataBefore { get; set; } // Dữ liệu trước khi thay đổi (nếu có)
        public string? DataAfter { get; set; } // Dữ liệu sau khi thay đổi (nếu có)
        public string? IpAddress { get; set; }
        public string? UserAgent { get; set; }
        public DateTime ActionTime { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public Guid? CreatedUserId { get; set; }
        public Guid? UpdatedUserId { get; set; }
    }
} 