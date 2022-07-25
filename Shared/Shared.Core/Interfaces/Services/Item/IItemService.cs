using OnlineShop.Shared.Core.Wrapper;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnlineShop.Shared.Core.Interfaces.Services.Item
{
    /// <summary>
    /// Base interface to be implemented by all Modules
    /// </summary>
    public interface IItemService
    {
        /// <summary>
        ///  Get created items in Module
        /// </summary>
        /// <returns></returns>
        Task<IResult<List<Entities.Item>>> GetItemsAsync();
    }
}
