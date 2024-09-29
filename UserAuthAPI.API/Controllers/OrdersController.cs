using UserAuthAPI.API.DAL;
using UserAuthAPI.API.Domain.Dtos.Request;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace UserAuthAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private UserAuthAPIContext _context;
        public OrdersController(UserAuthAPIContext context) { 
            _context = context;
        }
        [HttpGet("all")]
        public async Task<IActionResult> List([FromQuery] OrderListRequestDto request)
        {
            var skip = (request.PageNo - 1) * request.PageSize;
            var orders = await _context.Order
                        .Skip(skip)
                        .Take(request.PageSize)
                        .ToListAsync();
            var totalRecords = await _context.Order.CountAsync();
            var totalPages = (int)Math.Ceiling((double) totalRecords / request.PageSize);
            return Ok(new OrderListResponeDto
            {
                Status = true,
                PageNo = request.PageNo,
                PageSize = request.PageSize,
                TotalRecords = totalRecords,
                TotalPages = totalPages,
                Orders = orders
            });
        }
    }
}
