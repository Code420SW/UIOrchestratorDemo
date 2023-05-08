using Code420.StatusGeneric;
using FluentValidation;
using MediatR;
using Code420.UIOrchestrator.Core.Models.AuthP;
using Code420.UIOrchestrator.Core.Models.UserCredentials;
using Code420.UIOrchestrator.Server.Components.UIOrchestratorComponents.UIOrchestratorTabManager.UIOrchestratorTabs.UserLoginTab;

namespace Code420.UIOrchestrator.Server.MediatR.User
{
    /// <summary>
    /// MediatR <see cref="IRequestHandler{TRequest, TResponse}"/> for the
    /// <see cref="LoginUserModel"/>.
    /// The handler will return a <see cref="StatusGenericHandler"/> object.
    /// </summary>
    /// <remarks>
    /// The handler is invoked from <see cref="UserLogin.OnSubmit"/>.
    /// Typically this will invoke the orchestrator/service/broker responsible
    /// authenticating the user and and creating/updating the
    /// <see cref="UserCredentials"/> object.
    /// For this demo we will create/update the UserCredential object here.
    /// Note that typical logging has been removed.
    /// </remarks>
    internal sealed class UserLoginCommandHandler : IRequestHandler<UserLoginCommandRequest, StatusGenericHandler>
    {
        private readonly IEnumerable<IValidator<LoginUserModel>> validators;
        private readonly IUserCredentials userCredentials;  // For demo only

        public UserLoginCommandHandler(IEnumerable<IValidator<LoginUserModel>> validators,
            IUserCredentials userCredentials)
        {
            this.validators = validators;
            this.userCredentials = userCredentials; // For demo only
            
            //  Inject orchestrator, logging
        }

        /// <summary>
        /// Responsible for validating the passed <see cref="LoginUserModel"/> and, if valid,
        /// invoking the appropriate orchestrator method.
        /// </summary>
        /// <param name="request">
        /// </param>
        /// <param name="cancellationToken">
        /// Accepts a <see cref="CancellationToken"/>. If omitted, a default token is used.
        /// </param>
        /// <returns></returns>
        /// <exception cref="Exception">
        /// An exception will be thrown if no validators can be found. This is a developer error.
        /// </exception>
        public async Task<StatusGenericHandler> Handle(UserLoginCommandRequest request, CancellationToken cancellationToken = default)
        {
            //  Ensure that at least one validator has been discovered
            //  If not, throw since this is a developer error
            if (validators.Any() is false)
            {
                string message = $"Required validators for LoginUserModel not found.";
                throw new Exception(message);
            }

            //  Execute all registered validation rules
            var context = new ValidationContext<LoginUserModel>(request.loginUserModel);
            var validationResults = await Task.WhenAll(validators
                .Select(v => v.ValidateAsync(context, cancellationToken)));
            var errors = validationResults.SelectMany(r => r.Errors)
                .Where(f => f != null).ToList();

            //  Capture any errors and bail
            if (errors.Any())
            {
                StatusGenericHandler validationStatus = new() { HasValidationErrors = true };
                foreach (var error in errors)
                {
                    validationStatus.AddError(error.ErrorMessage);
                }
                return validationStatus;
            }

            //  Typically this will contain the results of the call to the orchestrator
            //  responsible for authenticating the user. For example:
            //  status = await userAuthenticationOrchestrator.LoginUser(request.loginUserModel);
            //  For the demo we will simply return a no-errors status.
            StatusGenericHandler status = new();
            
            // For demo only...See comments below
            BuildUserCredentials(request.loginUserModel);
            
            return status;
        }

        private void BuildUserCredentials(LoginUserModel loginUserModel)
        {
            //  To make this simple we will define three users based on the email address:
            //  1.  accessall@test.com -- Receive AccessAll permission
            //  2.  permission1@test.com -- Receive Permission_1 permission
            //  3.  anything else -- Receive Permission_2 permission

            var permissions = loginUserModel.Email.ToLower() switch
            {
                "accessall@test.com" => new List<string> { "AllAccess" },
                "permission1@test.com" => new List<string> { "Permission_1" },
                _ => new List<string> { "Permission_2" }
            };
            
            userCredentials.UpdateTokens("DummyJWT", "DummyRefreshToken");
            userCredentials.UpdateUserPermissions(permissions);
        }
    }
}
