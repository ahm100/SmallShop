using BasicItems.Domain;
using System.ComponentModel.DataAnnotations;

namespace Orders.Domain
{
    public class Order : BaseEntity
    {
        public int Id { get; set; }

        [Required]
        public string CustomerName { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Total amount must be greater than or equal to 0")]
        public decimal TotalAmount { get; set; }
       
    }
}
