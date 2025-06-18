using InstantCodeLab.Application.DTOs;
using InstantCodeLab.Domain.Entities;
using InstantCodeLab.Domain.Repositories;
using InstantCodeLab.Infrastructure.Persistence.Repositories;
using Microsoft.AspNetCore.SignalR;

namespace InstantCodeLab.Api.Hubs;

public class MessageHub : Hub
{
    private readonly IUserRepository _userRepository;
    public MessageHub(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public override async Task OnConnectedAsync()
    {
        Console.WriteLine($"Client connected: {Context.ConnectionId}");
        try
        {
            await base.OnConnectedAsync();
        }
        catch (Exception ex)
        {
        }

    }

    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        User? user = _userRepository.Data.FirstOrDefault(e => e.ConnectionId == Context.ConnectionId);
        if(user is not null)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, user.LabRoomId); 
            _userRepository.Data.Remove(user);
            await Clients.Group(user.LabRoomId).SendAsync("UserLeft", user.Id);

        }

        await base.OnDisconnectedAsync(exception);
    }

    public async Task UserJoined(string roomId, string userId)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, roomId);
        User? user = _userRepository.Data.FirstOrDefault(e => e.Id == userId);
        if (user is null)
        {
            throw new Exception("User not found");
        }

        user.ConnectionId = Context.ConnectionId;

        var users = _userRepository.Data.Where(e => e.LabRoomId == roomId).Select(e => UserDto.ConvertToUser(e));
        await Clients.Group(roomId).SendAsync("UserJoined", users.ToList());
    }

    public async Task LeaveRoom(string roomId, string userId)
    {
        User? user = _userRepository.Data.FirstOrDefault(e => e.Id == userId);
        if (user is not null)
        {
            _userRepository.Data.Remove(user);  
            await Clients.Group(user.LabRoomId).SendAsync("UserLeft", userId);
            await Groups.RemoveFromGroupAsync(user.ConnectionId, roomId);
        }
    }

    public async Task DeleteRoom(string roomId)
    {
        List<User> allUsers = _userRepository.Data.Where(e => e.LabRoomId == roomId).ToList();
        _userRepository.Data.RemoveAll(e => e.LabRoomId == roomId);

        foreach (var user in allUsers)
        {
            await Clients.Group(roomId).SendAsync("UserLeft", user.Id);
            await Groups.RemoveFromGroupAsync(user.ConnectionId, roomId);
        }
    }

    public async Task SwitchToEditor(string targetUserId)
    {
        User? toUser = _userRepository.Data.FirstOrDefault(e => e.Id == targetUserId);
        User? currentUser = _userRepository.Data.FirstOrDefault(e => e.ConnectionId == Context.ConnectionId);
        if (toUser is null)
        {
            throw new Exception("User not found");
        }

        User? prevUser = _userRepository.Data.FirstOrDefault(e => e.ViewingOfConnectionId == Context.ConnectionId);

        prevUser?.ViewerConnectionIds.Remove(Context.ConnectionId);

        toUser.ViewerConnectionIds.Add(Context.ConnectionId);
        if(toUser.Id == currentUser?.Id)
        {
            currentUser.IsUserAtOwnEditor = true;
        }

        currentUser.ViewingOfConnectionId = toUser.ConnectionId;
        string emptyString = "";
        List<byte> byteList = System.Text.Encoding.UTF8.GetBytes(emptyString).ToList();

        await Groups.AddToGroupAsync(Context.ConnectionId, targetUserId);
        await Clients.Client(Context.ConnectionId).SendAsync("ReceiveCodeChange", byteList);
    }

    /// <summary>
    /// Update code in user variable and send to paired user
    /// </summary>
    /// <param name="userName"> </param>
    /// <param name="code"></param>
    /// <returns></returns>
    /// 

    public async Task SendCodeChange(string editorOwnerId, List<byte> newCode)
    {
        if (editorOwnerId is null)
        {
            throw new ArgumentNullException(nameof(editorOwnerId));
        }
        var user = _userRepository.Data.FirstOrDefault(e => e.Id == editorOwnerId);
        if(user is null)
        {
            throw new Exception("User not found");
        }
        //user.OwnCode = newCode;

        var allViewerConnectionIds = user.ViewerConnectionIds.ToList();
        if (user.IsUserAtOwnEditor)
        {
            allViewerConnectionIds.Add(user.ConnectionId);
        }

        allViewerConnectionIds.Remove(Context.ConnectionId);

        // Notify all viewers of this IDE
        await Clients.Clients(allViewerConnectionIds)
            .SendAsync("ReceiveCodeChange", newCode);
    }
}
