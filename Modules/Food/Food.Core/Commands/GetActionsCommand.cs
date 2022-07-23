using MediatR;
using OnlineShop.Shared.Core.Entities;
using OnlineShop.Shared.Core.Wrapper;
using Food.Core.Interfaces;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Food.Core.Commands
{
    /// <summary>
    /// Get actions command, created with reflection
    /// </summary>
    public class GetActionsCommand : IRequest<IResult<List<Action>>>
    {

    }

    /// <summary>
    /// Food handler for get actions command, called with Reflection
    /// </summary>
    public class GetActionsCommandHandler : IRequestHandler<GetActionsCommand, IResult<List<Action>>>
    {
        private readonly IFoodClient _slackClient;

        /// <summary>
        /// GetActionsCommandHandler
        /// </summary>
        /// <param name="slackClient"></param>
        public GetActionsCommandHandler(IFoodClient slackClient)
        {
            _slackClient = slackClient;
        }

        /// <summary>
        /// Handle GetActionsCommand
        /// </summary>
        /// <param name="command"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<IResult<List<Action>>> Handle(GetActionsCommand command, CancellationToken cancellationToken)
        {
            return await _slackClient.GetActionsAsync();
        }
    }
}
