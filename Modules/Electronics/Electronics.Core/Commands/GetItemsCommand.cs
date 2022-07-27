using Electronics.Core.Interfaces;
using Electronics.Core.Models;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Electronics.Core.Commands
{
    /// <summary>
    /// Get items command, created with reflection
    /// </summary>
    public class GetItemsCommand : IRequest<List<ElectronicModel>>
    {

    }

    /// <summary>
    /// Food handler for get items command, called with Reflection
    /// </summary>
    public class GetItemsCommandHandler : IRequestHandler<GetItemsCommand, List<ElectronicModel>>
    {
        private readonly IElectronicClient _electronicClient;

        /// <summary>
        /// GetItemsCommandHandler
        /// </summary>
        /// <param name="slackClient"></param>
        public GetItemsCommandHandler(IElectronicClient  electronicClient)
        {
            _electronicClient = electronicClient;
        }

        /// <summary>
        /// Handle GetItemsCommand
        /// </summary>
        /// <param name="command"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<List<ElectronicModel>> Handle(GetItemsCommand command, CancellationToken cancellationToken)
        {
            return await _electronicClient.GetItemsAsync();
        }
    }
}
