using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WarehouseManager.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }


        [Required]
        public string Name { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        [Range(minimum:0,maximum:100000)] 
        public decimal Price { get; set; }

        [Required]
        public string Category { get; set; }


        [Range(minimum: 0, maximum: 100000)]
        public int Quantity { get; set; }
    }
}