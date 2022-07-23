using MediatR;
using OnlineShop.DTOs.Actions;
using OnlineShop.Shared.Core.Wrapper;
using Drinks.Core.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Drinks.Core.Commands
{
    /// <summary>
    /// Update action command, created with reflection
    /// </summary>
    public class UpdateActionCommand : IRequest<IResult<Guid>>
    {
        /// <summary>
        /// Request
        /// </summary>
        public UpdateActionRequest Request { get; set; }

        /// <summary>
        /// UpdateActionCommand
        /// </summary>
        /// <param name="UpdateAction"></param>
        public UpdateActionCommand(UpdateActionRequest UpdateAction)
        {
            Request = UpdateAction;
        }
    }

    /// <summary>
    /// Drinks handler for update action command, called with Reflection
    /// </summary>
    public class UpdateActionCommandHandler : IRequestHandler<UpdateActionCommand, IResult<Guid>>
    {
        private readonly IDrinksClient _outlookClient;

        /// <summary>
        /// UpdateActionCommandHandler
        /// </summary>
        /// <param name="outlookClient"></param>
        public UpdateActionCommandHandler(IDrinksClient outlookClient)
        {
            _outlookClient = outlookClient;
        }

        /// <summary>
        /// Handle UpdateActionCommand
        /// </summary>
        /// <param name="command"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<IResult<Guid>> Handle(UpdateActionCommand command, CancellationToken cancellationToken)
        {
            return await _outlookClient.UpdateActionAsync(command.Request);
        }
    }
}
