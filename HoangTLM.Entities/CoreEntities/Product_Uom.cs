using System.ComponentModel.DataAnnotations;

namespace HoangTLM.Entities.CoreEntities
{
    public class Product_Uom
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty; // Ví dụ: 'Hộp', 'Cái', 'Thùng'
        public string? Description { get; set; }
        public decimal ConversionRate { get; set; } // Quy đổi về đơn vị cơ bản
    }
} 