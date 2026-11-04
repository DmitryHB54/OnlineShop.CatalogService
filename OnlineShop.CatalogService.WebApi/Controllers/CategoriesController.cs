using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.CatalogService.Application.Common.Features.Categories.Commands.CreateCategory;
using OnlineShop.CatalogService.Application.Common.Features.Categories.Dtos;
using OnlineShop.CatalogService.Application.Common.Features.Categories.Queries.GetCategories;

namespace OnlineShop.CatalogService.WebApi.Controllers;

[ApiController]
[Route("api/categories")]
public class CategoriesController(ISender mediator) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(typeof(List<CategoryDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        return Ok(await mediator.Send(new GetCategoreisQuery(), cancellationToken));
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] CreateCategoryCommand command, CancellationToken cancellationToken)
    {
        var id = await mediator.Send(command, cancellationToken);
        return CreatedAtAction(nameof(GetAll), new { id }, id);
    }
}