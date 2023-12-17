using System;

namespace BasicItems.Domain
{
    public class BaseEntity
    {
        public int Id { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime? UpdateTime { get; set; }
        public string? CreatedBy { get; set; }
        public string? UpdatedBy { get; set; }

        public BaseEntity()
        {
            CreateTime = DateTime.UtcNow;
            UpdateTime = DateTime.UtcNow;
        }
    }
}
