using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using Srv_Sale.Models;
using Srv_Sale.DTOs;
using Srv_Sale.Data;

using Microsoft.Extensions.Logging;
using AutoMapper.QueryableExtensions;
using MassTransit;
using Transit;
using Microsoft.AspNetCore.Authorization;

namespace Srv_Sale.Controllers;

[ApiController]
[Route("api/sales")]
public class SalesController : ControllerBase
{
    private readonly SaleDbContext _context;
    private readonly IMapper _mapper;
    private readonly IPublishEndpoint _publishEndpoint;
    private readonly ILogger<SalesController> _logger;

    public SalesController(SaleDbContext context, IMapper mapper, ILogger<SalesController> logger, IPublishEndpoint publishEndpoint)
    {
        _context = context;
        _mapper = mapper;
        _publishEndpoint = publishEndpoint;
        _logger = logger;
    }

    [HttpGet]
    public async Task<ActionResult<List<SaleDto>>> GetAllSales()
    {
        var sales = await _context.Sales
            .Include(s => s.Item)
            .OrderBy(x => x.Item.Brand)
            .ToListAsync();

        var saleDtos = _mapper.Map<List<SaleDto>>(sales);

        return saleDtos;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<SaleDto>> GetSaleById(Guid id)
    {
        var sale = await _context.Sales
            .Include(s => s.Item)
            .SingleOrDefaultAsync(s => s.Id == id);

        if (sale == null)
        {
            return NotFound();
        }

        var saleDto = _mapper.Map<SaleDto>(sale);
        return Ok(saleDto);
    }

    [Authorize]
    [HttpPost]
    public async Task<ActionResult<SaleDto>> CreateSale(NewSaleDto saleDto)
    {
        var sale = _mapper.Map<Sale>(saleDto);

        sale.Author = User.Identity.Name;
        
        _context.Sales.Add(sale);
        
        var newSale = _mapper.Map<SaleDto>(sale);

        await _publishEndpoint.Publish(_mapper.Map<SaleCreated>(newSale));
        
        var result = await _context.SaveChangesAsync() > 0;

        if (!result)
        {
            return BadRequest("Problem after Pub(result)");
        }

        return CreatedAtAction(nameof(GetSaleById),
            new { sale.Id }, newSale);
    }

    [Authorize]
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateSale(Guid id, UpdateSaleDto updateSaleDto)
    {
        var sale = await _context.Sales.Include(x => x.Item)
                   .FirstOrDefaultAsync(x => x.Id == id);

        if (sale == null) return NotFound();

        if (sale.Author != User.Identity.Name) return Forbid();

        sale.Author = updateSaleDto.Author ?? sale.Author;
        sale.Status = Enum.TryParse(updateSaleDto.Status, out Status status) ? status : sale.Status;
        sale.PublicationDate = updateSaleDto.PublicationDate ?? sale.PublicationDate;

        sale.Item.Brand = updateSaleDto.Brand ?? sale.Item.Brand;
        sale.Item.Model = updateSaleDto.Model ?? sale.Item.Model;
        sale.Item.Year = updateSaleDto.Year != 0 ? updateSaleDto.Year : sale.Item.Year;
        sale.Item.ImageUrl = updateSaleDto.ImageUrl ?? sale.Item.ImageUrl;


        sale.Item.AdditionalProperties.Frame = updateSaleDto.Frame ?? sale.Item.AdditionalProperties.Frame;
        sale.Item.AdditionalProperties.Handlebar = updateSaleDto.Handlebar ?? sale.Item.AdditionalProperties.Handlebar;
        sale.Item.AdditionalProperties.Brakes = updateSaleDto.Brakes ?? sale.Item.AdditionalProperties.Brakes;
        sale.Item.AdditionalProperties.WheelsTires = updateSaleDto.WheelsTires ?? sale.Item.AdditionalProperties.WheelsTires;
        sale.Item.AdditionalProperties.Seat = updateSaleDto.Seat ?? sale.Item.AdditionalProperties.Seat;
        sale.Item.AdditionalProperties.DerailleursDrive = updateSaleDto.DerailleursDrive ?? sale.Item.AdditionalProperties.DerailleursDrive;
        sale.Item.AdditionalProperties.AdditionalAccessories = updateSaleDto.AdditionalAccessories ?? sale.Item.AdditionalProperties.AdditionalAccessories;

        await _publishEndpoint.Publish(_mapper.Map<SaleUpdated>(sale));

        var result = await _context.SaveChangesAsync() > 0;

        if (result) return Ok();

        return BadRequest("Problem saving changes");
    }
    [Authorize]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteSale(Guid id)
    {
        var sale = await _context.Sales.FindAsync(id);

        if (sale == null) return NotFound();

        if (sale.Author != User.Identity.Name) return Forbid();

        _context.Sales.Remove(sale);

        await _publishEndpoint.Publish<SaleDeleted>(new { Id = sale.Id.ToString() });

        var result = await _context.SaveChangesAsync() > 0;

        if (!result) return BadRequest("Could not delete DB item");

        return Ok();
    }
}
