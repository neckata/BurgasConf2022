using OnlineShop.Shared.Core.Domain;

namespace OnlineShop.Shared.Core.Entities
{
    public class Item : BaseEntity
    {
        public string Name { get; set; }

        public decimal Price { get; set; }

        public Item()
        {

        }
    }
}
