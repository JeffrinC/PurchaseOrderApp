using Microsoft.Extensions.Options;
using PurchaseOrderApp.Common;
using PurchaseOrderApp.Controllers.Dtos;
using PurchaseOrderApp.Models;
using PurchaseOrderApp.Repositories.Store;
using System.Threading;
using System.Threading.Tasks;

namespace PurchaseOrderApp.Repositories
{
    public class PurchaseOrderRepository : IPurchaseOrderRepository
    {
        private readonly StockRuleSetConfiguration _stockConfig;
        private readonly IInventoryStore _inventoryStore;


        public PurchaseOrderRepository(IInventoryStore inventoryStore, IOptions<StockRuleSetConfiguration> stockConfig)
        {
            _stockConfig = stockConfig?.Value;

            _inventoryStore = inventoryStore;
        }

        public async Task<Inventory> GetAvailableStocks(CancellationToken token)
        {
            return await Task.Run(() => _inventoryStore.All());
        }

        public async Task<bool> TryUpdate(PurchaseOrderItem item)
        {
            return await Task.Run(() => _inventoryStore.TryUpdate(item));
        }

        // create purchase order
        public async Task<bool> CreatePurchaseOrderAsync(PurchaseOrderCreateRequest createRequest, CancellationToken token = default)
        {
            if (createRequest.OrderType == PurchaseOrderType.Buy)
                return await Buy(createRequest, token);
            else
                return await Sell(createRequest, token);
        }

        // buy value // no need as we will be doing it in ui
        public async Task<bool> Buy(PurchaseOrderCreateRequest buyRequest, CancellationToken token = default)
        {

            return await Task.Run(() =>
            {
                return _inventoryStore.Create(new PurchaseOrderItem
                {
                    Name = buyRequest.Name,
                    Quantity = buyRequest.Quantity,
                });
            });
        }

        // sell value
        public async Task<bool> Sell(PurchaseOrderCreateRequest sellRequest, CancellationToken token = default)
        {

            return await Task.Run(() =>
            {
                return _inventoryStore.TryRemove(new PurchaseOrderItem
                {
                    Id = sellRequest.ItemId,
                    Name = sellRequest.Name,
                    Quantity = sellRequest.Quantity,
                });
            });
        }

        public async Task Save()
        {
            await Task.Run(() => _inventoryStore.Save());
        }

        //// mock data
        //private Inventory GetInventory()
        //{
        //    return new Inventory
        //    {
        //        Id = 1,
        //        InventoryType = "items",
        //        PurchaseOrderItems = new List<PurchaseOrderItem>()
        //        {
        //            new PurchaseOrderItem
        //            {
        //                Id = 1,
        //                Name = "ItemA",
        //                Price = 100,
        //                Quantity = 10,
        //            },

        //            new PurchaseOrderItem
        //            {
        //                Id = 2,
        //                Name = "ItemB",
        //                Price = 150,
        //                Quantity = 10,
        //            }
        //        }
        //    };
        //}
    }
}
