using System;

namespace ProductAPI.Models
{
    public class ProductHistory
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string ChangeType { get; set; }
        public DateTime ChangeDate { get; set; }
        public string Details { get; set; }
    }
}
