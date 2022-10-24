using Core.Entities;

namespace Data.Abstraction;

public interface IDataInitialiser
{
    EntitySet? EntitySet { get; }
}
