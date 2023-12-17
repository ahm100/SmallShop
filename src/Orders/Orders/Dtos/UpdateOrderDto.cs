using System;

namespace Orders.Dtos
{
    public class UpdateOrderDto
    {
        public int Id { get; set; }
        public DateTime? UpdateTime { get; set; }
        public string? UpdatedBy { get; set; }
        public string? CustomerName { get; set; } // hame fieldha dar update nullable
        public decimal? TotalAmount { get; set; } //hame fieldha dar update nullable 

        public UpdateOrderDto()
        {
            UpdateTime = DateTime.UtcNow;
        }
    }
}
