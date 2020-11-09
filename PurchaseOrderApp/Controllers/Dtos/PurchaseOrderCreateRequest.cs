using PurchaseOrderApp.Common;
using System;

namespace PurchaseOrderApp.Controllers.Dtos
{
    public class PurchaseOrderCreateRequest
    {
        public Guid? ItemId { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public PurchaseOrderType OrderType { get; set; }
    }
}
