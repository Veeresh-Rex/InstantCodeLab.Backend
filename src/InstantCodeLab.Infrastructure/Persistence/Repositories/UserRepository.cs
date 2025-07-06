using InstantCodeLab.Domain.Entities;
using InstantCodeLab.Domain.Repositories;
using MongoDB.Driver;

namespace InstantCodeLab.Infrastructure.Persistence.Repositories;

public class UserRepository : GenericRepository<User>, IUserRepository
{
    public override string CollectionName => "User";

    public UserRepository(IMongoDatabase mongoDatabase) : base(mongoDatabase)
    {
    }
}
