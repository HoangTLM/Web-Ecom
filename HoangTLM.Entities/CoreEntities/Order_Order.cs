using HoangTLM.Core.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace HoangTLM.Entities.CoreEntities
{
    public class Order_Order : IAuditableFull
    {
        [Key]
        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }
        public DateTime OrderDate { get; set; }
        [Precision(18,2)]
        public decimal TotalAmount { get; set; }
        public string Status { get; set; } = string.Empty;
        public string? Note { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public Guid? CreatedUserId { get; set; }
        public Guid? UpdatedUserId { get; set; }
    }
} 