using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BgClubApi.Models;
using Microsoft.AspNetCore.Http.HttpResults;

namespace BgClubApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LibraryController : ControllerBase
    {
        private readonly IGameRepository _gameRepository;

        public LibraryController(IGameRepository gameRepository)
        {
            _gameRepository = gameRepository;
        }

        // GET: api/Games
        [HttpGet("Games")]
        public async Task<ActionResult<IEnumerable<Game>>> GetGames()
        {
            var games = await _gameRepository.GetAllGames();
            return Ok(games);
        }

        // GET: api/Games/1
        [HttpGet("Games/{name}")]
        public async Task<ActionResult<Game>> GetGame(string name)
        {
            var game = await _gameRepository.GetGameByName(name);
            if (game == null)
            {
                return NotFound();
            }

            return Ok(game);
        }

        // POST: api/Games
        [HttpPost("Games")]
        public async Task<ActionResult<Game>> PostGame(Game game)
        {
            await _gameRepository.AddGame(game);
            return CreatedAtAction(nameof(GetGame), new { name = game.Name }, game);
        }
    }
}
