using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using VlackApi.Models;
using System.Linq;
using Microsoft.AspNetCore.SignalR;
using VlackApi.SignalR;
using System;

namespace VlackApi.Controllers
{
  [Route("api/[controller]")]
  public class MessageController : Controller
  {
    private readonly ApplicationContext _messageContext;
    private IHubContext<ChannelHub> _hubContext;

    public MessageController(ApplicationContext context, IHubContext<ChannelHub> hubContext)
    {
      _messageContext = context;
      _hubContext = hubContext;
    }

    [HttpGet]
    public IEnumerable<Message> GetAll()
    {
      return _messageContext.Messages.ToList();
    }

    [HttpGet("{id}", Name = "GetMessage")]
    public IActionResult GetById(long id)
    {
      var c = _messageContext.Messages.FirstOrDefault(t => t.Id == id);
      if (c == null)
      {
        return NotFound();
      }
      return new ObjectResult(c);
    }

    [HttpPost]
    public IActionResult Create([FromBody] Message c)
    {
      if (c == null)
      {
        return BadRequest();
      }
      
      c.CreatedAt = DateTime.Now.ToString("ddd h:mm tt");

      _messageContext.Messages.Add(c);
      _messageContext.SaveChanges();

      _hubContext.Clients.Group($"channel{c.ChannelId}").InvokeAsync("broadcastMessage", "hi", c);

      return CreatedAtRoute("GetMessage", new { id = c.Id }, c);
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(long id)
    {
      var Message = _messageContext.Messages.FirstOrDefault(t => t.Id == id);
      if (Message == null)
      {
        return NotFound();
      }

      _messageContext.Messages.Remove(Message);
      _messageContext.SaveChanges();
      return new NoContentResult();
    }

    [HttpPut("{id}")]
    public IActionResult Update(long id, [FromBody] Message c)
    {
      if (c == null || c.Id != id)
      {
        return BadRequest();
      }

      var Message = _messageContext.Messages.FirstOrDefault(t => t.Id == id);
      if (Message == null)
      {
        return NotFound();
      }

      Message.body = c.body;

      _messageContext.Messages.Update(Message);
      _messageContext.SaveChanges();
      return new NoContentResult();
    }
  }
  



  //   [HttpGet("{id}", Name = "GetMessage")]
  //   public IActionResult GetById(long id)
  //   {
  //     var c = _context.Messages.FirstOrDefault(t => t.Id == id);
  //     if (c == null)
  //     {
  //       return NotFound();
  //     }
  //     return new ObjectResult(c);
  //   }

  //   [HttpPost]
  //   public IActionResult Create([FromBody] Message c)
  //   {
  //     if (c == null)
  //     {
  //       return BadRequest();
  //     }

  //     _context.Messages.Add(c);
  //     _context.SaveChanges();

  //     return CreatedAtRoute("GetMessage", new { id = c.Id }, c);
  //   }

  //   [HttpPut("{id}")]
  //   public IActionResult Update(long id, [FromBody] Message c)
  //   {
  //     if (c == null || c.Id != id)
  //     {
  //       return BadRequest();
  //     }

  //     var Message = _context.Messages.FirstOrDefault(t => t.Id == id);
  //     if (Message == null)
  //     {
  //       return NotFound();
  //     }

  //     Message.body = c.body;

  //     _context.Messages.Update(Message);
  //     _context.SaveChanges();
  //     return new NoContentResult();
  //   }

  //   [HttpDelete("{id}")]
  //   public IActionResult Delete(long id)
  //   {
  //     var Message = _context.Messages.FirstOrDefault(t => t.Id == id);
  //     if (Message == null)
  //     {
  //       return NotFound();
  //     }

  //     _context.Messages.Remove(Message);
  //     _context.SaveChanges();
  //     return new NoContentResult();
  //   }
  // }
}