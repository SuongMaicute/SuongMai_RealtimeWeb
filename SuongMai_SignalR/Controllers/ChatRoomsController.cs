using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SuongMai_SignalR.Data;
using SuongMai_SignalR.Models;

namespace SuongMai_SignalR.Controllers
{
    [Route("/[controller]")]
    [ApiController]
    public class ChatRoomsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ChatRoomsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/ChatRooms
        [HttpGet]
        [Route("/[controller]/GetChatRoom")]
        public async Task<ActionResult<IEnumerable<ChatRoom>>> GetChatRooms()
        {
          if (_context.ChatRooms == null)
          {
              return NotFound();
          }
            return await _context.ChatRooms.ToListAsync();
        }

        // GET: api/ChatRooms
        [HttpGet]
        [Route("/[controller]/GetChatUser")]
        public async Task<ActionResult<Object>> GetChatUser()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var users = await _context.Users.ToListAsync();

            if (users == null)
            {
                return NotFound();
            }

            return  users.Where(u => u.Id != userId).Select(u => new { u.Id, u.UserName }).ToList();
        }

        // GET: api/ChatRooms/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ChatRoom>> GetChatRoom(int id)
        {
          if (_context.ChatRooms == null)
          {
              return NotFound();
          }
            var chatRoom = await _context.ChatRooms.FindAsync(id);

            if (chatRoom == null)
            {
                return NotFound();
            }

            return chatRoom;
        }

        // PUT: api/ChatRooms/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutChatRoom(int id, ChatRoom chatRoom)
        {
            if (id != chatRoom.Id)
            {
                return BadRequest();
            }

            _context.Entry(chatRoom).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
               
            }

            return NoContent();
        }

        // POST: api/ChatRooms
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Route("/[controller]/PostChatRoom")]
        public async Task<ActionResult<ChatRoom>> PostChatRoom(ChatRoom chatRoom)
        {
          if (_context.ChatRooms == null)
          {
              return Problem("Entity set 'ApplicationDbContext.ChatRooms'  is null.");
          }
            _context.ChatRooms.Add(chatRoom);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetChatRoom", new { id = chatRoom.Id }, chatRoom);
        }

        // DELETE: api/ChatRooms/5
        [HttpDelete("{id}")]
        [Route("/[controller]/DeleteChatRoom/{id}")]
        public async Task<IActionResult> DeleteChatRoom(int id)
        {
            if (_context.ChatRooms == null)
            {
                return NotFound();
            }
            var chatRoom = await _context.ChatRooms.FindAsync(id);
            if (chatRoom == null)
            {
                return NotFound();
            }

            _context.ChatRooms.Remove(chatRoom);
            await _context.SaveChangesAsync();

            return NoContent();
        }

    }
}
