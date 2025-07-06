using InstantCodeLab.Application.DTOs;
using InstantCodeLab.Domain.Entities;
using InstantCodeLab.Domain.Enums;
using InstantCodeLab.Domain.Repositories;
using Microsoft.AspNetCore.SignalR;
using MongoDB.Driver.Linq;

namespace InstantCodeLab.Api.Hubs;

public class MessageHub : Hub
{
    private readonly IUserRepository _userRepository;
    private readonly ILabRoomRepository _labRoomRepository;

    public MessageHub(IUserRepository userRepository, ILabRoomRepository labRoomRepository)
    {
        _userRepository = userRepository;
        _labRoomRepository = labRoomRepository;
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
        User? user = await _userRepository.FindFirstOrDefaultAsync(e => e.ConnectionId == Context.ConnectionId);
        if (user is not null)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, user.LabRoomId);
            await _userRepository.DeleteAsync(user._id);
            await Clients.Group(user.LabRoomId).SendAsync("UserLeft", user._id);

        }

        await base.OnDisconnectedAsync(exception);
    }

    public async Task UserJoined(string roomId, string userId)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, roomId);
        await Groups.AddToGroupAsync(Context.ConnectionId, userId);

        User? user = await _userRepository.GetByIdAsync(userId);
        if (user is null)
        {
            throw new Exception("User not found");
        }

        user.ConnectionId = Context.ConnectionId;
        user.AddedToGroup = Context.ConnectionId;

        await _userRepository.UpdateAsync(user._id, user);

        var users = (await _userRepository.Query
            .Where(e => e.LabRoomId == roomId)
            .ToListAsync())
            .Select(UserDto.ConvertToUser)
            .ToList();

        await Clients.Group(roomId).SendAsync("UserJoined", users.ToList());
    }

    public async Task LeaveRoom(string roomId, string userId)
    {
        User? user = await _userRepository.GetByIdAsync(userId);
        if (user is not null)
        {
            await _userRepository.DeleteAsync(user._id);
            await Clients.Group(user.LabRoomId).SendAsync("UserLeft", userId);
            await Groups.RemoveFromGroupAsync(user.ConnectionId, roomId);
        }
    }

    public async Task DeleteRoom(string roomId)
    {
        List<User> allUsers = await _userRepository.Query.Where(e => e.LabRoomId == roomId).ToListAsync();
        await _userRepository.DeleteManyAsync(e => e.LabRoomId == roomId);

        await Clients.Group(roomId).SendAsync("RoomIsDeleted");

        foreach (var user in allUsers)
        {
            await Groups.RemoveFromGroupAsync(user.ConnectionId, roomId);
        }
    }

    public async Task SwitchToEditor(string targetUserId)
    {
        User? toUser = await _userRepository.GetByIdAsync(targetUserId);
        User? currentUser = await _userRepository.FindFirstOrDefaultAsync(e => e.ConnectionId == Context.ConnectionId);
        if (toUser is null)
        {
            throw new Exception("User not found");
        }
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, currentUser.AddedToGroup);
        await Groups.AddToGroupAsync(Context.ConnectionId, targetUserId);
        currentUser.AddedToGroup = targetUserId;

        await _userRepository.UpdateAsync(currentUser._id, currentUser);

        await Clients.Client(Context.ConnectionId).SendAsync("ReceiveCodeChange", toUser.OwnCode);
    }

    /// <summary>
    /// Update code in user variable and send to paired user
    /// </summary>
    /// <param name="userName"> </param>
    /// <param name="code"></param>
    /// <returns></returns>
    /// 

    public async Task SendCodeChange(string editorOwnerId, string newCode)
    {
       if (editorOwnerId is null)
        {
            throw new ArgumentNullException(nameof(editorOwnerId));
        }
        var user = await _userRepository.GetByIdAsync(editorOwnerId);
        if (user is null)
        {
            throw new Exception("User not found");
        }
        user.OwnCode = newCode;
        await _userRepository.UpdateAsync(user._id, user);

        // Notify all viewers of this IDE
        await Clients.GroupExcept(editorOwnerId, Context.ConnectionId).SendAsync("ReceiveCodeChange", newCode);
    }

    public async Task ChangeLanguage(LanguageCode languageCode, string roomId)
    {
        var room = await _labRoomRepository.GetByIdAsync(roomId);
        if (room is null)
        {
            throw new Exception("Room not found");
        }
        room.LanguageCode = languageCode;

        await _labRoomRepository.UpdateAsync(room._id, room);

        await Clients.Group(roomId).SendAsync("LanguageChanged", languageCode);
    }
}
