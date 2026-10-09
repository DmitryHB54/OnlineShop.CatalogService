using MediatR;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.CatalogService.Application.Common.Features.Brands.Commands.CreateBrand;
using OnlineShop.CatalogService.Application.Common.Features.Brands.Dtos;
using OnlineShop.CatalogService.Application.Common.Features.Brands.Queries.GetBrandById;
using OnlineShop.CatalogService.Application.Common.Features.Brands.Queries.GetBrands;

namespace OnlineShop.CatalogService.WebApi.Controllers;

[ApiController]
[Route("api/brands")]
public class BrandsController(ISender mediator) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(typeof(List<BrandDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        => Ok(await mediator.Send(new GetBrandsQuery(), cancellationToken));

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(BrandDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
        => Ok(await mediator.Send(new GetBrandByIdQuery(id), cancellationToken));

    [HttpPost]
    [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] CreateBrandCommand command, CancellationToken cancellatonToken)
    {
        var id = await mediator.Send(command, cancellatonToken);
        return CreatedAtAction(nameof(GetById), new { id }, id);
    }
}