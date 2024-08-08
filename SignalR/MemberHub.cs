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
    
    public static void AddActiveMember(Guid activeMember)
    {
        ActiveMembers.Add(activeMember);
    }

    public static void DeleteActiveMember(Guid memberId)
    {
        if (!ActiveMembers.Contains(memberId))
            throw new KeyNotFoundException("memberId not found in active members!");

        ActiveMembers.Remove(memberId);
    }

    public static void DeleteAllActiveMembers()
    {
        ActiveMembers.Clear();
    }

    public static List<Guid> GetActiveMemberList()
    {
        return ActiveMembers;
    }
}