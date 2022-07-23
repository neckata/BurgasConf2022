using OnlineShop.Shared.Core.Domain;

namespace OnlineShop.Shared.Core.Entities
{
    public class Module : BaseEntity
    {
        public string Name { get; set; }

        public bool IsUsed { get; set; }

        public Module()
        {

        }
    }
}
