using System.Collections.Concurrent;
using InstantCodeLab.Domain.Entities;
using InstantCodeLab.Domain.Repositories;
using MongoDB.Driver;

namespace InstantCodeLab.Infrastructure.Persistence.Repositories;

public class LabRoomRepository : GenericRepository<LabRoom>, ILabRoomRepository
{
    public override string CollectionName => "LabRoom";

    public LabRoomRepository(IMongoDatabase mongoDatabase) : base(mongoDatabase)
    {
    }
}
