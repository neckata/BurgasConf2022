using Drinks.Core.Interfaces;
using Drinks.Core.Models;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Drinks.Core.Commands
{
    /// <summary>
    /// Get items command, created with reflection
    /// </summary>
    public class GetItemsCommand : IRequest<List<DrinkModel>>
    {

    }

    /// <summary>
    /// Drinks handler for get items command, called with Reflection
    /// </summary>
    public class GetItemsCommandHandler : IRequestHandler<GetItemsCommand, List<DrinkModel>>
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
        public async Task<List<DrinkModel>> Handle(GetItemsCommand command, CancellationToken cancellationToken)
        {
            return await _drinksClient.GetItemsAsync();
        }
    }
}
