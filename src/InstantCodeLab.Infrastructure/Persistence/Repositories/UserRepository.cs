using System.Collections.Concurrent;
using System.Collections.Generic;
using InstantCodeLab.Domain.Entities;
using InstantCodeLab.Domain.Repositories;

namespace InstantCodeLab.Infrastructure.Persistence.Repositories;

public class UserRepository : IUserRepository
{
    private readonly ConcurrentDictionary<string, List<User>> _userDb = new();

    public UserRepository()
    {
        _userDb.TryAdd("User", new List<User>());
    }

    public List<User> Data => _userDb["User"];
}
