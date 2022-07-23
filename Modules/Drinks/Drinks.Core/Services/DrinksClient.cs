using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OnlineShop.DTOs.Actions;
using OnlineShop.Shared.Core.Entities;
using OnlineShop.Shared.Core.Interfaces;
using OnlineShop.Shared.Core.Wrapper;
using Drinks.Core.Interfaces;
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
        private readonly IMapper _mapper;
        private readonly IApplicationDbContext _context;

        /// <summary>
        /// DrinksClient
        /// </summary>
        /// <param name="mapper"></param>
        /// <param name="context"></param>
        public DrinksClient(IMapper mapper, IApplicationDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        /// <summary>
        /// Get created actions in outlook
        /// </summary>
        /// <returns></returns>
        public async Task<IResult<List<Action>>> GetActionsAsync()
        {
            List<Action> actions = await _context.Actions.Where(x => x.ModuleType == "Drinks").AsNoTracking().ToListAsync();

            return await Result<List<Action>>.SuccessAsync(actions);
        }

        /// <summary>
        /// Update action in outlook
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<IResult<System.Guid>> UpdateActionAsync(UpdateActionRequest request)
        {
            UpdateDrinksEvent(request);

            Action action = await _context.Actions.Where(b => b.Id == request.Id).AsNoTracking().FirstOrDefaultAsync();

            _mapper.Map(request, action);

            _context.Actions.Update(action);
            await _context.SaveChangesAsync();

            return await Result<System.Guid>.SuccessAsync(action.Id, "Action Updated");
        }

        /// <summary>
        /// Create action in outlook
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<IResult<System.Guid>> CreateActionAsync(CreateActionRequest request)
        {
            AddDrinksEvent(request);

            Action action = _mapper.Map<Action>(request);

            action.ModuleType = "Drinks";

            await _context.Actions.AddAsync(action);
            await _context.SaveChangesAsync();

            return await Result<System.Guid>.SuccessAsync(action.Id, "Action Added");
        }

        private void UpdateDrinksEvent(UpdateActionRequest request)
        {
            //Here you will connect and update the event in outlook
        }

        private void AddDrinksEvent(CreateActionRequest request)
        {
            //Here you will connect and add the event in outlook
        }
    }
}
