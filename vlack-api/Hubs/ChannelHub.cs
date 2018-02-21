using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace VlackApi.SignalR
{
  public class ChannelHub : Hub
  {
    public override Task OnConnectedAsync()
    {
      var id = this.Context.Connection.GetHttpContext().Request.Query["channel_id"].SingleOrDefault();
      this.Groups.AddAsync(this.Context.ConnectionId, $"channel{id}");
      return base.OnConnectedAsync();
    }
    public void Send(string name, string message)
    {
      Clients.All.InvokeAsync("broadcastMessage", name, message);
    }
    public void Double(string name, string message)
    {
      Clients.All.InvokeAsync("broadcastMessage", name, $"{message} {message}");
    }
  }
}