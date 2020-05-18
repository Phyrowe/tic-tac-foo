using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TicTacFoo.Application.Common.Interfaces;

namespace TicTacFoo.Api.Controllers.v1
{
    [AllowAnonymous]
    [ApiController]
    [Route("api/v1/[controller]")]
    public class TestsController : ApiControllerBase
    {
        private readonly ISampleService _sample;

        public TestsController(ISampleService sample)
        {
            _sample = sample;
        }

        [AllowAnonymous]
        [HttpGet]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
        public async Task<IActionResult> Index()
        {
            try
            {
                return Ok(await Task.FromResult(new { Status = 200, Message = _sample.IsWorking() }));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}