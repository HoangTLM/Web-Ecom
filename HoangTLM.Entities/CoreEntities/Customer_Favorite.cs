using System.ComponentModel.DataAnnotations;
using HoangTLM.Core.Interfaces;

namespace HoangTLM.Entities.CoreEntities
{
    public class Customer_Favorite : IAuditableFull
    {
        [Key]
        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }
        public Guid ProductId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public Guid? CreatedUserId { get; set; }
        public Guid? UpdatedUserId { get; set; }
    }
} 