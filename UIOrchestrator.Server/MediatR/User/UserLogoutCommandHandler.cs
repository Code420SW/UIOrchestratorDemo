using Code420.StatusGeneric;
using MediatR;

namespace Code420.CanXtracServer.MediatR.User
{
    /// <summary>
    /// MediatR <see cref="IRequestHandler{TRequest, TResponse}"/> for the
    /// <see cref="UserLogoutCommandRequest"/>.
    /// The handler will return a <see cref="StatusGenericHandler"/> object.
    /// </summary>
    /// <remarks>
    /// The handler is invoked by the <see cref="UIOrchestrator.UserLogoutHandlerAsync"/> method.
    /// Typically this will invoke the orchestrator/service/broker responsible
    /// de-authenticating the user and and updating the <see cref="UserCredentials"/> object.
    /// For this demo we will update the UserCredential object here.
    /// Note that typical logging has been removed.
    /// </remarks>
    internal sealed class UserLogoutCommandHandler : IRequestHandler<UserLogoutCommandRequest, StatusGenericHandler>
    {
        public UserLogoutCommandHandler() {}

        public async Task<StatusGenericHandler> Handle(UserLogoutCommandRequest request, CancellationToken cancellationToken)
        {
            //  Typically this will contain the results of the call to the orchestrator
            //  responsible for de-authenticating the user. For the demo we will simply
            //  return a no-errors status.
            StatusGenericHandler status = new();

            // Update UserCredentials here versus invoking the orchestrator

            return await Task.FromResult(status);
        }
    }
}
