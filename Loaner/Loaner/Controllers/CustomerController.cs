using AutoMapper;
using Loaner.Data;
using Loaner.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Loaner.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly LoanerDbContext _context;
        private readonly IMapper _mapper;

        public CustomerController(LoanerDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var customer = _context.customers.FirstOrDefault();
            return Ok(_mapper.Map<CustomerDto>(customer));
        }
    }
}
