using Code420.StatusGeneric;
using Code420.UIOrchestrator.Core.Models.Menus;
using MediatR;

namespace Code420.UIOrchestrator.Server.MediatR.Configuration.Menus
{
    internal record MenuItemDefinitionsQueryRequest : IRequest<StatusGenericHandler<List<MenuItemDefinitionDto>>>;
}
