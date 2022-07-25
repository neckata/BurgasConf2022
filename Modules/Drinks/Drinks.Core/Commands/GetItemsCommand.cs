using Drinks.Core.Interfaces;
using MediatR;
using OnlineShop.Shared.Core.Entities;
using OnlineShop.Shared.Core.Wrapper;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Drinks.Core.Commands
{
    /// <summary>
    /// Get items command, created with reflection
    /// </summary>
    public class GetItemsCommand : IRequest<IResult<List<Item>>>
    {

    }

    /// <summary>
    /// Drinks handler for get items command, called with Reflection
    /// </summary>
    public class GetItemsCommandHandler : IRequestHandler<GetItemsCommand, IResult<List<Item>>>
    {
        private readonly IDrinksClient _drinksClient;

        /// <summary>
        /// GetItemsCommandHandler
        /// </summary>
        /// <param name="outlookClient"></param>
        public GetItemsCommandHandler(IDrinksClient drinksClient)
        {
            _drinksClient = drinksClient;
        }

        /// <summary>
        /// Handle GetItemsCommand
        /// </summary>
        /// <param name="command"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<IResult<List<Item>>> Handle(GetItemsCommand command, CancellationToken cancellationToken)
        {
            return await _drinksClient.GetItemsAsync();
        }
    }
}
