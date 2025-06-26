using System.ComponentModel.DataAnnotations;
using HoangTLM.Core.Interfaces;

namespace HoangTLM.Entities.CoreEntities
{
    public class Loyalty_Tier : IAuditableFull
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int MinPoints { get; set; }
        public string? Description { get; set; }
        public string? Benefits { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public Guid? CreatedUserId { get; set; }
        public Guid? UpdatedUserId { get; set; }
    }
} 