using System;

namespace ProductAPI.Models
{
    public class ProductHistory
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string PropertyName { get; set; }
        public string OldValue { get; set; }
        public string NewValue { get; set; }
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    }
}
