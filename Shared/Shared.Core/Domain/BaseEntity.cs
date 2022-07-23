using OnlineShop.Shared.Core.Contracts;
using System;

namespace OnlineShop.Shared.Core.Domain
{
    public abstract class BaseEntity : IEntity<Guid>
    {
        public Guid Id { get; set; }

        protected BaseEntity()
        {
            Id = Guid.NewGuid();
        }
    }
}