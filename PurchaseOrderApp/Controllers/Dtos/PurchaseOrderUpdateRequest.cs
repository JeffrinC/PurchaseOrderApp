using PurchaseOrderApp.Common;
using System;

namespace PurchaseOrderApp.Controllers.Dtos
{
    public class PurchaseOrderItemUpdateRequest
    {
        public Guid ItemId { get; set; }
        public int Quantity { get; set; }
        public PurchaseOrderType OrderType { get; set; }
    }
}
