using BusinessLogic.DTOs;
using BusinessLogic.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Presentation.APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShippingTypesController : ControllerBase
    {
        private readonly IShippingTypeService _service;

        public ShippingTypesController(IShippingTypeService service)
        {
            _service = service;
        }

        // GET: api/<ShippingTypesController>
        [HttpGet]
        public async Task<ActionResult<ApiResponse>> Get()
        {
            try
            {
                var allData = await _service.GetAllAsync();

                var response = new ApiResponse
                {
                    Success = true,
                    Content = allData!
                };

                return Ok(response);
            }
            catch
            {
                var response = new ApiResponse
                {
                    Success = false,
                    Content = "Unexpected error occured"
                };

                return StatusCode(500, response);
            }

        }

        // GET api/<ShippingTypesController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse>> Get(Guid id)
        {
            try
            {
                var data = await _service.GetByIdAsync(id);

                ApiResponse response;

                if (data is not null)
                {
                    response = new ApiResponse
                    {
                        Success = true,
                        Content = data!
                    };
                }

                else
                {
                    response = new ApiResponse
                    {
                        Success = false,
                        Content = "no resource exists with the given id."!
                    };
                }

                return Ok(response);
            }
            catch
            {
                var response = new ApiResponse
                {
                    Success = false,
                    Content = "Unexpected error occured"
                };

                return StatusCode(500, response);
            }
        }
    }
}
