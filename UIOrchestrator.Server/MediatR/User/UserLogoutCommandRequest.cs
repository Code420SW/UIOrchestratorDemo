using Code420.StatusGeneric;
using MediatR;

namespace Code420.CanXtracServer.MediatR.User
{
     internal record UserLogoutCommandRequest : IRequest<StatusGenericHandler>;
}
