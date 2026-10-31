using MediatR;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.CatalogService.Application.Common.Features.Products.Commands.ActivateProduct;
using OnlineShop.CatalogService.Application.Common.Features.Products.Commands.AddSpecification;
using OnlineShop.CatalogService.Application.Common.Features.Products.Commands.CreateProduct;
using OnlineShop.CatalogService.Application.Common.Features.Products.Commands.DeactivateProduct;
using OnlineShop.CatalogService.Application.Common.Features.Products.Commands.RemoveSpecification;
using OnlineShop.CatalogService.Application.Common.Features.Products.Commands.ReplaceSpecifications;
using OnlineShop.CatalogService.Application.Common.Features.Products.Commands.UpdateProduct;
using OnlineShop.CatalogService.Application.Common.Features.Products.Commands.UpdateSpecification;
using OnlineShop.CatalogService.Application.Common.Features.Products.Dtos;
using OnlineShop.CatalogService.Application.Common.Features.Products.Queries.GetProductById;
using OnlineShop.CatalogService.Application.Common.Features.Products.Queries.GetProducts;
using OnlineShop.CatalogService.Application.Common.Features.Products.Queries.GetProductSpecifiations;
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

    // SPECIFICATIONS

    [HttpGet("{id:guid}/specifications")]
    [ProducesResponseType(typeof(ProductSpecificationDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetSpecifications(Guid id, CancellationToken cancellationToken)
        => Ok(await mediator.Send(new GetProductSpecificationsQuery(id), cancellationToken));

    [HttpPost("{id:guid}/specifications")]
    [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> AddSpecification(
        Guid id, [FromBody] AddSpecificationCommand command, CancellationToken cancellationToken)
    {
        if (id != command.ProductId)
            return BadRequest("Route id and body id must match");

        var specId = await mediator.Send(command, cancellationToken);

        return CreatedAtAction(nameof(GetSpecifications), new { id }, id);
    }

    [HttpPut("{productId:guid}/specifications/{specId:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateSpecification(
        Guid productId,
        Guid specId,
        [FromBody] UpdateSpecificationCommand command,
        CancellationToken cancellationToken)
    {
        if(productId != command.ProductId)
            return BadRequest("Route id and body id must match");
        await mediator.Send(command, cancellationToken);

        return NoContent();
    }

    [HttpDelete("{productId:guid}/specifications/{specId:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> RemoveSpecification(Guid productId, Guid specId, CancellationToken cancellationToken)
    {
        await mediator.Send(new RemoveSpecificationCommand(productId, specId), cancellationToken);
        return NoContent();
    }

    [HttpPut("{productId:guid}/specifications")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> ReplaceSpecifications(
        Guid productId, [FromBody] ReplaceSpecificationsBody body, CancellationToken cancellationToken)
    {
        var inputs = body.Specifications
            .Select(s => new SpecificationInput(s.Key, s.Value, s.DisplayOrder))
            .ToList();

        await mediator.Send(new ReplaceSpecificationsCommand(productId, inputs), cancellationToken);

        return NoContent();
    }

    public record ReplaceSpecificationsBody(List<SpecificationInput> Specifications);
}