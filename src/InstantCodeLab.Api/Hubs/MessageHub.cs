using InstantCodeLab.Application.DTOs;
using InstantCodeLab.Domain.Repositories;
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

    public override async Task OnDisconnectedAsync(Exception exception)
    {
        _userRepository.Data.RemoveAll(e => e.ConnectionId == Context.ConnectionId);
        await base.OnDisconnectedAsync(exception);
    }

    /// <summary>
    /// Join a specific lab room
    /// </summary>
    /// <param name="connection"></param>
    /// <returns></returns>
    // TODO: This should handle thru api
    public async Task JoinSpecificlabelRoom(UserDto userDto)
    {
        var currentUser = _userRepository.Data.FirstOrDefault(e => e.UserName == userDto.UserName);

        if(currentUser is null)
        {
            throw new Exception("No User found");
        }

        currentUser.PairedWithConnectionId = Context.ConnectionId;
        currentUser.ConnectionId = Context.ConnectionId;
        currentUser.IsConnected = true;


        foreach (var user in _userRepository.Data)
        {
            await Clients.Client(user.ConnectionId).SendAsync("UserJoinedRoom", _userRepository.Data);
        }
    }

    /// <summary>
    /// Click on to see code of a user
    /// </summary>
    /// <param name="toUserName"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public async Task JoinPairCoding(string toUserName)
    {
        var toUser = _userRepository.Data.FirstOrDefault(e => e.Id == toUserName);
        var currentUser = _userRepository.Data.FirstOrDefault(e => e.ConnectionId == Context.ConnectionId);

        if (toUser is null)
        {
            throw new Exception("No User found");
        }

        toUser.PairedWithConnectionId = Context.ConnectionId;
        currentUser.PairedWithConnectionId = toUser.ConnectionId;

        await Clients.Client(currentUser.ConnectionId).SendAsync("ReceiveMessage", toUser.OwnCode);
    }

    /// <summary>
    /// Update code in user variable and send to paired user
    /// </summary>
    /// <param name="userName"> </param>
    /// <param name="code"></param>
    /// <returns></returns>
    public async Task SendCode(string code)
    {
        var user = _userRepository.Data.FirstOrDefault(e => e.ConnectionId == Context.ConnectionId);
        if (user is null)
        {
            throw new Exception("No User found");
        }

        var userConnectedWith = user.IsAtOwnIde ? user : _userRepository.Data.FirstOrDefault(e => e.PairedWithConnectionId == Context.ConnectionId);  
        if(userConnectedWith is null)
        {
            throw new Exception("Not connected with anyone");
        }

        userConnectedWith.OwnCode = code;

        if (!string.IsNullOrWhiteSpace(user.PairedWithConnectionId) && !user.IsAtOwnIde)
        {
            await Clients.Client(user.PairedWithConnectionId).SendAsync("ReceiveMessage", code);
        }
    }
}
