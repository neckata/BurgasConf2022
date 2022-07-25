using Drinks.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Shared.Core.Entities;
using OnlineShop.Shared.Core.Interfaces;
using OnlineShop.Shared.Core.Wrapper;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Drinks.Core.Services
{
    /// <summary>
    /// DrinksClient extends IModuleClient
    /// </summary>
    public class DrinksClient : IDrinksClient
    {
        private readonly IApplicationDbContext _context;

        /// <summary>
        /// DrinksClient
        /// </summary>
        /// <param name="context"></param>
        public DrinksClient(IApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Get created items in drinks
        /// </summary>
        /// <returns></returns>
        public async Task<IResult<List<Item>>> GetItemsAsync()
        {
            List<Item> items = await _context.Items.Where(x => x.ModuleType == "Drinks").AsNoTracking().ToListAsync();

            return await Result<List<Item>>.SuccessAsync(items);
        }
    }
}
