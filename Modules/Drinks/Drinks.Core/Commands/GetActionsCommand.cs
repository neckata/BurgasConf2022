using MediatR;
using OnlineShop.Shared.Core.Entities;
using OnlineShop.Shared.Core.Wrapper;
using Drinks.Core.Interfaces;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Drinks.Core.Commands
{
    /// <summary>
    /// Get actions command, created with reflection
    /// </summary>
    public class GetActionsCommand : IRequest<IResult<List<Action>>>
    {

    }

    /// <summary>
    /// Drinks handler for get actions command, called with Reflection
    /// </summary>
    public class GetActionsCommandHandler : IRequestHandler<GetActionsCommand, IResult<List<Action>>>
    {
        private readonly IDrinksClient _outlookClient;

        /// <summary>
        /// GetActionsCommandHandler
        /// </summary>
        /// <param name="outlookClient"></param>
        public GetActionsCommandHandler(IDrinksClient outlookClient)
        {
            _outlookClient = outlookClient;
        }

        /// <summary>
        /// Handle GetActionsCommand
        /// </summary>
        /// <param name="command"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<IResult<List<Action>>> Handle(GetActionsCommand command, CancellationToken cancellationToken)
        {
            return await _outlookClient.GetActionsAsync();
        }
    }
}
