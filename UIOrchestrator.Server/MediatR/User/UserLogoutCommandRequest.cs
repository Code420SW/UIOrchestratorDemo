using Code420.StatusGeneric;
using MediatR;

namespace Code420.UIOrchestrator.Server.MediatR.User
{
     internal record UserLogoutCommandRequest : IRequest<StatusGenericHandler>;
}
