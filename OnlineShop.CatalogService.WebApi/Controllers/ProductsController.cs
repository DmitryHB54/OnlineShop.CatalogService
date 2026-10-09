using MediatR;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.CatalogService.Application.Common.Features.Products.Command.ActivateProduct;
using OnlineShop.CatalogService.Application.Common.Features.Products.Command.CreateProduct;
using OnlineShop.CatalogService.Application.Common.Features.Products.Command.DeactivateProduct;
using OnlineShop.CatalogService.Application.Common.Features.Products.Command.UpdateProduct;
using OnlineShop.CatalogService.Application.Common.Features.Products.Dtos;
using OnlineShop.CatalogService.Application.Common.Features.Products.Queries.GetProductById;
using OnlineShop.CatalogService.Application.Common.Features.Products.Queries.GetProducts;
using OnlineShop.CatalogService.Application.Models;

namespace OnlineShop.CatalogService.WebApi.Controllers;

[ApiController]
[Route("api/products")]
public class ProductsController(ISender mediator) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(typeof(PagedResult<ProductDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll([FromQuery] GetProductsQuery query, CancellationToken cancellationToken)
    {
        return Ok(await mediator.Send(query, cancellationToken));
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(ProductDetailsDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
    {
        return Ok(await mediator.Send(new GetProductByIdQuery(id), cancellationToken));
    }

    [HttpPost]
    [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] CreateProductCommand command,
        CancellationToken cancellationToken)
    {
        var id = await mediator.Send(command, cancellationToken);
        return CreatedAtAction(nameof(GetById), new { id }, id);
    }

    [HttpPut("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(Guid id,
        [FromBody] UpdateProductCommand command, CancellationToken cancellationToken)
    {
        if (id != command.Id)
            return BadRequest("Route id and body id must match");

        await mediator.Send(command, cancellationToken);
        return NoContent();
    }

    [HttpPatch("{id:guid}/deactivate")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Deactivate(Guid id, CancellationToken cancellationToken)
    {
        await mediator.Send(new DeactivateProductCommand(id), cancellationToken);
        return NoContent();
    }

    [HttpPatch("{id:guid}/activate")]
    public async Task<IActionResult> Activate(Guid id, CancellationToken cancellationToken)
    {
        await mediator.Send(new ActivateProductCommand(id), cancellationToken);
        return NoContent();
    }
}