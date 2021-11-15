using System;
namespace Domain.Contracts
{
    public abstract class Entity : IEntity {
        public Entity()
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; private set; }
    }
}
