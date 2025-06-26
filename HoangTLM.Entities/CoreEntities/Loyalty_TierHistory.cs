using System.ComponentModel.DataAnnotations;
using HoangTLM.Core.Interfaces;

namespace HoangTLM.Entities.CoreEntities
{
    public class Loyalty_TierHistory : IAuditableFull
    {
        [Key]
        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }
        public Guid TierId { get; set; }
        public DateTime ChangedDate { get; set; }
        public string ChangeType { get; set; } = string.Empty; // Up, Down, Initial
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public Guid? CreatedUserId { get; set; }
        public Guid? UpdatedUserId { get; set; }
    }
} 