using System.Collections.Generic;
using System.Linq;

namespace PurchaseOrderApp.Models
{
    public class InventoryVm
    {
        public string SearchName;
        public string InventoryType { get; set; }
        public List<PurchaseOrderItem> PurchaseOrderItems { get; set; }

        public InventoryVm(in IEnumerable<PurchaseOrderItem> models, string name)
        {
            PurchaseOrderItems = models.ToList();
        }
    }
}
