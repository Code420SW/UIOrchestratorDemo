using Code420.StatusGeneric;
using Code420.UIOrchestrator.Core.Models.Menus;
using MediatR;

namespace Code420.UIOrchestrator.Server.MediatR.Configuration.Menus
{
    /// <summary>
    /// Responsible for retrieving all menu definitions.
    /// </summary>
    /// <remarks>
    /// Typically this will invoke the orchestrator/service/broker responsible for
    /// retrieving the menu definitions from the persistence layer. For this demo we
    /// will hard-code the menu definitions here. Note that typical logging has
    /// been removed.
    /// </remarks>
    internal sealed class MenuItemDefinitionsQueryHandler 
        : IRequestHandler<MenuItemDefinitionsQueryRequest, StatusGenericHandler<List<MenuItemDefinitionDto>>>
    {
        // ReSharper disable once EmptyConstructor
        public MenuItemDefinitionsQueryHandler()
        {
            //  Inject orchestrator, logging
        }

        /// <summary>
        /// Responsible for retrieving the menu definitions from the repository.
        /// </summary>
        /// <param name="request">
        /// Reference to the <see cref="MenuItemDefinitionsQueryRequest"/> object that initiated the request.
        /// </param>
        /// <param name="cancellationToken">
        /// The passed <see cref="CancellationToken"/> object.
        /// </param>
        /// <returns>
        /// A <see cref="StatusGenericHandler{T}"/> object containing a <see cref="List{T}"/> of 
        /// <see cref="MenuItemDefinitionDto"/> objects in the Result property. The 
        /// <see cref="StatusGenericHandler{T}"/> object contains status information.
        /// </returns>
        public async Task<StatusGenericHandler<List<MenuItemDefinitionDto>>> Handle(MenuItemDefinitionsQueryRequest request, CancellationToken cancellationToken)
        {
            StatusGenericHandler<List<MenuItemDefinitionDto>> status = new();

            // Define the menu definitions here
            
            return await Task.FromResult(status);
        }
    }
}
