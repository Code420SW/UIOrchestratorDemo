using Code420.StatusGeneric;
using Code420.UIOrchestrator.Core.Models.AuthP;
using MediatR;

namespace Code420.UIOrchestrator.Server.MediatR.User
{
    internal record UserLoginCommandRequest(LoginUserModel loginUserModel) : IRequest<StatusGenericHandler>;
}
