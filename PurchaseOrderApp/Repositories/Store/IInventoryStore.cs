using PurchaseOrderApp.Models;

namespace PurchaseOrderApp.Repositories.Store
{
    public interface IInventoryStore
    {
        Inventory All();

        bool Create(PurchaseOrderItem created);

        void Save();

        bool TryRemove(PurchaseOrderItem removed);

        bool TryUpdate(PurchaseOrderItem removed);
    }
}
