using System.ComponentModel.DataAnnotations;

namespace HoangTLM.Entities.CoreEntities
{
    public class Product_ProductUom
    {
        [Key]
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public Guid UomId { get; set; }
        public int Stock { get; set; } // Tồn kho riêng cho UOM này của sản phẩm
        public decimal Price { get; set; } // Giá bán cho UOM này của sản phẩm
    }
} 