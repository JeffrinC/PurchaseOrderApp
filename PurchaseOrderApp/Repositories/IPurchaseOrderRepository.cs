using PurchaseOrderApp.Controllers.Dtos;
using PurchaseOrderApp.Models;
using System.Threading;
using System.Threading.Tasks;

namespace PurchaseOrderApp.Repositories
{
    public interface IPurchaseOrderRepository
    {
        Task<bool> CreatePurchaseOrderAsync(PurchaseOrderCreateRequest purchaseOrderCreateRequest, CancellationToken token);

        Task<bool> TryUpdate(PurchaseOrderItem item);

        Task<Inventory> GetAvailableStocks(CancellationToken token);

        Task Save();

    }
}
