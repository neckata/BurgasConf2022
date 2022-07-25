using Food.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Shared.Core.Entities;
using OnlineShop.Shared.Core.Interfaces;
using OnlineShop.Shared.Core.Wrapper;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Food.Core.Services
{
    /// <summary>
    /// FoodClient extends IModuleClient
    /// </summary>
    public class FoodClient : IFoodClient
    {
        private readonly IApplicationDbContext _context;

        /// <summary>
        /// FoodClient
        /// </summary>
        /// <param name="context"></param>
        public FoodClient(IApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Get created items in food
        /// </summary>
        /// <returns></returns>
        public async Task<IResult<List<Item>>> GetItemsAsync()
        {
            List<Item> items = await _context.Items.Where(x => x.ModuleType == "Food").AsNoTracking().ToListAsync();

            return await Result<List<Item>>.SuccessAsync(items);
        }
    }
}
