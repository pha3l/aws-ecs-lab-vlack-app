using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using VlackApi.Models;

namespace VlackApi.Controllers
{
    [Route("api/[controller]")]
    public class ChannelController : Controller
    {
      private readonly ApplicationContext _context;
      public ChannelController(ApplicationContext context)
      {
        _context = context;
        if (_context.Channels.Count() == 0)
        {
          _context.Channels.Add(new Channel{ Name = "First Channel" });
          _context.SaveChanges();
        }
      }

      [HttpGet]
      public IEnumerable<Channel> GetAll()
      {
        return _context.Channels.ToList();
      }

      [HttpGet("{id}", Name = "GetChannel")]
      public IActionResult GetById(long id)
      {
        var c = _context.Channels
          .Include(t => t.Messages)
          .AsNoTracking()
          .FirstOrDefault(t => t.Id == id);
        if (c == null)
        {
          return NotFound();
        }
        return new ObjectResult(c);
      }

      [HttpPost]
      public IActionResult Create([FromBody] Channel c)
      {
        if (c == null)
        {
          return BadRequest();
        }

        _context.Channels.Add(c);
        _context.SaveChanges();

        return CreatedAtRoute("GetChannel", new { id = c.Id }, c);
      }

      [HttpPut("{id}")]
      public IActionResult Update(long id, [FromBody] Channel c)
      {
          if (c == null || c.Id != id)
          {
            return BadRequest();
          }

          var channel = _context.Channels.FirstOrDefault(t => t.Id == id);
          if (channel == null) {
            return NotFound();
          }

          channel.Name = c.Name;

          _context.Channels.Update(channel);
          _context.SaveChanges();
          return new NoContentResult();
      }

      [HttpDelete("{id}")]
      public IActionResult Delete(long id)
      {
        var channel = _context.Channels.FirstOrDefault(t => t.Id == id);
        if (channel == null)
        {
          return NotFound();
        }

        _context.Channels.Remove(channel);
        _context.SaveChanges();
        return new NoContentResult();
      }
    }
}