

using System;
using System.ComponentModel.DataAnnotations;

namespace WarehouseManager.Models
{
    public enum MovementType
    {
        In,
        Out
    }

    public class StockMovement
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }

        public MovementType Type { get; set; }

        public DateTime Date { get; set; }
    }
}
