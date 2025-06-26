using System.ComponentModel.DataAnnotations;
using HoangTLM.Core.Interfaces;

namespace HoangTLM.Entities.CoreEntities
{
    public class Point_PointTransaction : IAuditableFull
    {
        [Key]
        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }
        public int PointsChanged { get; set; }
        public string Type { get; set; } = string.Empty; // Earn, Redeem, Adjust
        public string? Description { get; set; }
        public DateTime TransactionDate { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public Guid? CreatedUserId { get; set; }
        public Guid? UpdatedUserId { get; set; }
    }
} 