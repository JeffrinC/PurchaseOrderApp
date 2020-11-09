using System;

namespace PurchaseOrderApp.Models
{
    public class PurchaseOrderItem
    {
        public Guid? Id { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public string Description { get; set; }
    }
}
