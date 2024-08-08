using System.Collections.Concurrent;
using Microsoft.AspNetCore.SignalR;

namespace SignalR;

public class MemberHub : Hub
{
    public static readonly ConcurrentDictionary<string, Guid> ConnectedClients = new ConcurrentDictionary<string, Guid>();
    public static readonly List<Guid> ActiveMembers = new List<Guid>();
    
    public void SubscribeMember(Guid memberId)
    {
        ConnectedClients[Context.ConnectionId] = memberId;
    }

    public override async Task OnDisconnectedAsync(Exception exception)
    {
        ConnectedClients.TryRemove(Context.ConnectionId, out _);
        await base.OnDisconnectedAsync(exception);
    }
    
    public static void UpdateActiveMembersList(List<Guid> newActiveMembers)
    {
        ActiveMembers.Clear();
        ActiveMembers.AddRange(newActiveMembers);
    }
}