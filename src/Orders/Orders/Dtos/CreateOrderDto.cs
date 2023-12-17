using System;
using System.ComponentModel.DataAnnotations;

namespace Orders.Dtos
{
    public class CreateOrderDto
    {
        public DateTime? CreateTime { get; set; }
        public string? CreatedBy { get; set; }
        public string CustomerName { get; set; }

        // if dto is called in another classlibrary or project cant catch validation with data annotation
        //[Range(0, double.MaxValue, ErrorMessage = "Total amount must be greater than or equal to 0")]
        public decimal TotalAmount { get; set; }

        public CreateOrderDto()
        {
            CreateTime = DateTime.UtcNow;
        }
    }
}
