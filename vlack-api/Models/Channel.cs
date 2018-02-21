using System.Collections.Generic;

namespace VlackApi.Models
{
  public class Channel
  {
    public long Id { get; set; }
    public string Name { get; set; }
    public ICollection<Message> Messages { get; set; }
  }
}