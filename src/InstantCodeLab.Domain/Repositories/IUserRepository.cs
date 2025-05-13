using System.Collections.Generic;
using InstantCodeLab.Domain.Entities;

namespace InstantCodeLab.Domain.Repositories;

public interface IUserRepository
{
    List<User> Data { get; }
}
