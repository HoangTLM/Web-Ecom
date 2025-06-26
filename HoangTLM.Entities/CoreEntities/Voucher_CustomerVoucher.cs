using System.ComponentModel.DataAnnotations;

namespace HoangTLM.Entities.CoreEntities
{
    public class Voucher_CustomerVoucher
    {
        [Key]
        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }
        public Guid VoucherId { get; set; }
        public DateTime AssignedDate { get; set; }
        public bool IsUsed { get; set; }
        public DateTime? UsedDate { get; set; }
    }
} 