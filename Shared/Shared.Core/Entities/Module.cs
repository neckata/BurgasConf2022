using OnlineShop.Shared.Core.Domain;

namespace OnlineShop.Shared.Core.Entities
{
    public class Module : BaseEntity
    {
        public string Name { get; set; }

        public bool IsInSolution { get; set; }

        public bool IsActive { get; set; }

        public Module()
        {

        }
    }
}
