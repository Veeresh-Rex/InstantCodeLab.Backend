using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace InstantCodeLab.Infrastructure.Utilities;

public class CodeStore
{
    private readonly ConcurrentDictionary<string, string> _userCode = new();

    public void SetCode(string userId, string code)
    {
        _userCode[userId] = code;
    }

    public string? GetCode(string userId)
    {
        return _userCode.TryGetValue(userId, out var code) ? code : null;
    }

    public void RemoveCode(string userId)
    {
        _userCode.TryRemove(userId, out _);
    }

    public Dictionary<string, string> GetAllCodes()
    {
        return _userCode.ToDictionary(kv => kv.Key, kv => kv.Value);
    }
}
