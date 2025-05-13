using System.Collections.Concurrent;
using System.Collections.Generic;
using InstantCodeLab.Domain.Entities;
using InstantCodeLab.Domain.Repositories;

namespace InstantCodeLab.Infrastructure.Persistence.Repositories;

public class LabRoomRepository : ILabRoomRepository
{
    private readonly ConcurrentDictionary<string, List<LabRoom>> _userDb = new();

    public LabRoomRepository()
    {
        _userDb.TryAdd("LabRoom", new List<LabRoom>());
    }

    public List<LabRoom> Data => _userDb["LabRoom"];
}
