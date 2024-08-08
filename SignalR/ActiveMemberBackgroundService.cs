using Microsoft.AspNetCore.SignalR;

namespace SignalR;

public class ActiveMemberBackgroundService : BackgroundService
{
    private readonly IHubContext<MemberHub> _hubContext;

    public ActiveMemberBackgroundService(IHubContext<MemberHub> hubContext)
    {
        _hubContext = hubContext;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            foreach (var client in MemberHub.ConnectedClients)
            {
                bool isActive = MemberHub.ActiveMembers.Contains(client.Value);
                await _hubContext.Clients.Client(client.Key).SendAsync("MemberStatusUpdate", isActive);
            }

            await Task.Delay(TimeSpan.FromSeconds(10), stoppingToken);
        }
    }
}