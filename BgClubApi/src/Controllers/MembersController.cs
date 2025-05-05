using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BgClubApi.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore.Metadata;

namespace BgClubApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MembersController : ControllerBase
    {
        private readonly IMemberRepository _memberRepository;
        private readonly IGameRepository _gameRepository;

        public MembersController(IMemberRepository memberRepository, IGameRepository gameRepository)
        {
            _memberRepository = memberRepository;
            _gameRepository = gameRepository;
        }

        // GET: api/Members
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Member>>> GetMembers()
        {
            var members = await _memberRepository.GetAllMembers();
            return Ok(members);
        }

        // GET: api/Members/1
        [HttpGet("{id}")]
        public async Task<ActionResult<Member>> GetMember(int id)
        {
            var member = await _memberRepository.GetMemberById(id);
            if (member == null)
            {
                return NotFound();
            }

            return Ok(member);
        }

        // PUT: api/Members/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMember(int id, Member member)
        {
            if (id != member.Id)
            {
                return BadRequest("Property 'id' in body does not match query string parameter 'id'.");
            }

            if (!await _memberRepository.MemberExists(id))
            {
                return NotFound();
            }

            await _memberRepository.UpdateMember(member);
            return NoContent();
        }

        // POST: api/Members
        [HttpPost]
        public async Task<ActionResult<Member>> PostMember(Member member)
        {
            await _memberRepository.AddMember(member);
            return CreatedAtAction(nameof(GetMember), new { id = member.Id }, member);
        }

        // DELETE: api/Members/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMember(int id)
        {
            var member = await _memberRepository.GetMemberById(id);
            if (member == null)
            {
                return NotFound();
            }

            await _memberRepository.RemoveMember(member);
            return NoContent();
        }

        [HttpPost("{memberId}/BorrowedGames/{gameName}")]
        public async Task<ActionResult<Member>> PostBorrowedGame(int memberId, string gameName)
        {
            // Get the member who is borrowing a game
            var member = await _memberRepository.GetMemberById(memberId);
            if (member == null)
            {
                return NotFound("Member not found");
            }

            // Get the game they want to borrow
            var game = await _gameRepository.GetGameByName(gameName);
            if (game == null)
            {
                return NotFound("Game Not Found");
            }

            // If the game is already borrowed, return error
            if (game.Borrower != null)
            {
                return BadRequest("That game is already check out.");
            }

            game.BorrowerId = memberId;
            await _gameRepository.UpdateGame(game);
            return CreatedAtAction(nameof(GetMember), new { id = member.Id }, member);
        }
    }
}
