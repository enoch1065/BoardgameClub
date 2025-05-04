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
    public class MembersController : ControllerBase
    {
        private readonly IMemberRepository _memberRepository;

        public MembersController(IMemberRepository memberRepository)
        {
            _memberRepository = memberRepository;
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
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
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
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
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
    }
}
