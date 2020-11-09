using Microsoft.AspNetCore.Mvc;
using PurchaseOrderApp.Controllers.Dtos;
using PurchaseOrderApp.Models;
using PurchaseOrderApp.Repositories;
using System.Threading;
using System.Threading.Tasks;

namespace PurchaseOrderApp.Controllers
{
    [Route("api/orders")]
    public class PurchaseOrdersController : Controller
    {
        private readonly IPurchaseOrderRepository _purchaseOrderRepository;

        public PurchaseOrdersController(IPurchaseOrderRepository repo)
        {
            _purchaseOrderRepository = repo;
        }

        public async Task<IActionResult> Index(string name, CancellationToken token = default)
        {
            var inventory = await _purchaseOrderRepository.GetAvailableStocks(token);

            var vm = new InventoryVm(inventory.PurchaseOrderItems, name);

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> EditPurchaseOrder(PurchaseOrderItemUpdateRequest input)
        {
            if (ModelState.IsValid == false)
                return View(nameof(EditPurchaseOrder), input);

            if (await _purchaseOrderRepository.TryUpdate(new Models.PurchaseOrderItem
            {
                Id = input.ItemId,
                Quantity = input.Quantity,
            }))
                TempData["Success"] = "The changes were saved.";
            else
                TempData["Error"] = "An error occurred and the changes were NOT saved.";

            return RedirectToAction(nameof(EditPurchaseOrder), new { input.ItemId });
        }

        [HttpPost]
        public async Task<IActionResult> CreatePurchaseOrder(PurchaseOrderCreateRequest purchaseOrderCreateRequest, CancellationToken token)
        {
            if (ModelState.IsValid)
                return View(nameof(CreatePurchaseOrder), purchaseOrderCreateRequest);

            var itemId = await _purchaseOrderRepository.CreatePurchaseOrderAsync(purchaseOrderCreateRequest, token);

            return RedirectToAction(nameof(EditPurchaseOrder), new { Id = itemId });
        }

        [HttpGet]
        public async Task<IActionResult> GetAvailableStocks(CancellationToken token)
        {
            return Ok(await _purchaseOrderRepository.GetAvailableStocks(token));
        }

        public IActionResult Save()
        {
            _purchaseOrderRepository.Save();

            TempData["Success"] = "purchase orders were saved.";

            return RedirectToAction(nameof(CreatePurchaseOrder));
        }
    }
}
