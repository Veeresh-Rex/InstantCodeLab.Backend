using System.Collections.Generic;
using InstantCodeLab.Domain.Entities;

namespace InstantCodeLab.Domain.Repositories;

public interface ILabRoomRepository
{
    List<LabRoom> Data { get; }
}
