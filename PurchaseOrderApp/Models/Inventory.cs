using System.Collections.Generic;

namespace PurchaseOrderApp.Models
{
    public class Inventory
    {
        public int Id { get; set; }
        public string InventoryType { get; set; }
        public List<PurchaseOrderItem> PurchaseOrderItems { get; set; }
    }
}
