using System;

namespace VlackApi.Models
{
  public class Message
  {
    public long Id { get; set; }
    public string body { get; set; }
    public long ChannelId { get; set; }
    public string CreatedAt { get; set; }
    public string User { get; set; }
    public bool starred { get; set; }
  }
}