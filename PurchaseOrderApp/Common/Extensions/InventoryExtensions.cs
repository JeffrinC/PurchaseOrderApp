using PurchaseOrderApp.Controllers.Dtos;
using PurchaseOrderApp.Models;

namespace PurchaseOrderApp.Common.Extensions
{
    internal static class InventoryExtensions
    {
        public static bool IsValid(this PurchaseOrderCreateRequest request, PurchaseOrderItem inventory)
        {
            if (inventory == null || (inventory.Quantity < request.Quantity))
            {
                return false;
            }
            return true;
        }
    }
}
