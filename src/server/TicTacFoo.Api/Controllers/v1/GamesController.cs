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
    public class GamesController : ApiControllerBase
    {
        private readonly IGameService _gameService;
        private readonly IGameHub _gameHub;

        public GamesController(IGameService gameService, IGameHub gameHub)
        {
            _gameService = gameService;
            _gameHub = gameHub;
        }


        [HttpGet]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
        public async Task<IActionResult> Index()
        {
            try
            {
                return Ok(await Task.FromResult(new { Status = 200, Message = new string[3,3] }));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}