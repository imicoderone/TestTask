using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using TestTask.Application.UseCases.Products.Command;
using TestTask.Application.UseCases.Products.Query;
using TestTask.Configurations;
using TestTask.Models;

namespace TestTask.Controllers
{
    public class ProductController : Controller
    {
        private readonly IMediator _mediator;
        private readonly ProductConfigurations _productConfigurations;
        public ProductController(IMediator mediator, IOptions<ProductConfigurations> productConfigurations)
        {
            _mediator = mediator;
            _productConfigurations = productConfigurations.Value;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Index()
        {
            var products = await _mediator.Send(new GetProductsQuery());

            return View(products.Select(p => new ProductViewModel
            {
                Id = p.Id, 
                Name = p.Name, 
                Price = p.Price, 
                Quantity = p.Quantity, 
                TotalPriceWithVAT = p.Quantity * p.Price * (1 + _productConfigurations.VAT) // Move into AutoMapper to avoid code duplication
            }));
        }

        [HttpGet]
        [Authorize(Policy = "ProductChanges")]
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Policy = "ProductChanges")]
        public async Task<IActionResult> Create(ProductViewModel model)
        {
            var products = await _mediator.Send(new CreateProductCommand()
                { Name = model.Name, Quantity = model.Quantity, Price = model.Price });

            return RedirectToAction("Index");
        }

        [HttpGet("Remove/{id}")]
        [Authorize(Policy = "ProductChanges")]
        public async Task<IActionResult> Remove([FromRoute]Guid id)
        {
            var product = await _mediator.Send(new GetProductQuery() { Id = id });

            if (product == null)
            {
                return NotFound();
            }

            return View(new ProductViewModel()
            {
                Id = product.Id, 
                Name = product.Name, 
                Quantity = product.Quantity, 
                Price = product.Price,
                TotalPriceWithVAT = product.Quantity * product.Price * (1 + _productConfigurations.VAT) // Move into AutoMapper to avoid code duplication
            });
        }

        [HttpPost]
        [Authorize(Policy = "ProductChanges")]
        public async Task<IActionResult> ConfirmRemove(Guid id)
        {
            var products = await _mediator.Send(new RemoveProductCommand()
                { Id = id });

            return RedirectToAction("Index");
        }
    }
}
