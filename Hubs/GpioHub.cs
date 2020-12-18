using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
namespace aspnetcoreapp.Hubs
{
    public class GpioHub : Hub
    {
        public async Task SendMessage(bool output1, bool output2, bool output3)
        {
            await Clients.All.SendAsync("ReceiveMessage", output1, output2,output3);
        }
    }
}
