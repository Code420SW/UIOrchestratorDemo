using System.Net;
using Code420.StatusGeneric;
using Code420.UIOrchestrator.Core.Models.Enums;

namespace Code420.UIOrchestrator.Core.Models.UserCredentials
{
    /// <summary>
    /// Responsible for managing the state of the user's credentials.
    /// Maintains the current user's JWT token and refresh token.
    /// All properties are read-only and are available to all consumers.
    /// </summary>
    public class UserCredentials : IUserCredentials
    {
        private readonly List<string> userRoles = new();
        private readonly List<string> userPermissions = new();
        private readonly List<string> errors = new();


        /// <summary>
        /// String value containing the JWT token.
        /// </summary>
        public string Token { get; private set; } = string.Empty;

        /// <summary>
        /// String value containing the refresh token.
        /// </summary>
        public string RefreshToken { get; private set; } = string.Empty;

        /// <summary>
        /// Boolean value indicating is the user's JWT token needs to be refresh.
        /// </summary>
        /// <remarks>
        /// This property is updated by the <see cref="CheckIfTokenRefreshNeeded"/> method
        /// called by orchestrators after, directly or indirectly, invokes an [Authorize] API endpoint.
        /// </remarks>
        public bool TokenRefreshNeeded { get; private set; } 

        /// <summary>
        /// String value containing the Tenant assigned to the user.
        /// Will be an empty string when no Tenant is assigned.
        /// </summary>
        public string Tenant { get; private set; } = string.Empty;

        /// <summary>
        /// Boolean value indicating if the user is authenticated.
        /// Determined by presence of a token (not null or white space).
        /// </summary>
        public bool IsAuthenticated => string.IsNullOrWhiteSpace(Token) is false;

        /// <summary>
        /// Boolean value indicating if any errors exist. 
        /// </summary>
        public bool IsValid => errors.Any() is false;

        /// <summary>
        /// Boolean value indicating if any errors exist. 
        /// </summary>
        public bool HasErrors => errors.Any();

        /// <summary>
        /// Read-only list of string values containing the roles assigned to the user.
        /// Use UpdateUserRoles() method to modify.
        /// </summary>
        public IReadOnlyList<string> UserRoles => userRoles.AsReadOnly();

        /// <summary>
        /// Read-only list of string values containing the permissions assigned to the user.
        /// Use UpdateUserPermissions() method to modify.
        /// </summary>
        public IReadOnlyList<string> UserPermissions => userPermissions.AsReadOnly();

        /// <summary>
        /// Read-only list of string values containing the errors encountered.
        /// Use AddError() method to modify.
        /// </summary>
        public IReadOnlyList<string> Errors => errors.AsReadOnly();



        /// <summary>
        /// Adds/Updates the JWT and Refresh tokens including any validation.
        /// A null parameter will clear the current token.
        /// </summary>
        /// <param name="token">
        /// String value containing the JWT token. Can be null to clear the existing
        /// token.
        /// </param>
        /// <param name="refreshToken">
        /// String value containing the refresh token. Can be null to clear the existing
        /// refresh token.
        /// </param>
        public void UpdateTokens(string token, string refreshToken)
        {
            Token = token ?? string.Empty;
            RefreshToken = refreshToken ?? string.Empty;
            TokenRefreshNeeded = false;
        }

        /// <summary>
        /// Updates the list of user roles.
        /// <remarks>
        /// The passed roles must be defined wherever/however the roles (mapping
        /// one or more permissions to a single name) are defined. This is out of
        /// scope for this demo.
        /// </remarks>
        /// </summary>
        /// <param name="roles">
        /// A <see cref="List{T}"/> of string values containing the
        /// roles to which the user is assigned.
        /// </param>
        public void UpdateUserRoles(List<string> roles)
        {
            userRoles.Clear();
            userRoles.AddRange(roles);
        }

        /// <summary>
        /// Updates the list of user permissions.
        /// <remarks>
        /// The passed permissions must be present in <see cref="AppPermissions"/>.
        /// </remarks>
        /// </summary>
        /// <param name="permissions">
        /// A <see cref="List{T}"/> of string values containing the
        /// permission to which the user is assigned.
        /// </param>
        public void UpdateUserPermissions(List<string> permissions)
        {
            userPermissions.Clear();
            userPermissions.AddRange(permissions);
        }

        /// <summary>
        /// Returns a boolean value indicating if the user has the passed permission.
        /// <remarks>
        /// The passed permission must be present in <see cref="AppPermissions"/>.
        /// </remarks>
        /// </summary>
        /// <param name="permission">
        /// String value containing the permission to be verified against the
        /// user's permission.
        /// </param>
        /// <returns>
        /// Returns <c>true</c> if <paramref name="permission"></paramref> is assigned to the
        /// user, <c>false</c> otherwise.
        /// </returns>
        public bool HasPermission(string permission) => userPermissions.Contains(permission);

        /// <summary>
        /// Adds the passed error message to the list of errors.
        /// <remarks>
        /// Null error messages will be ignore.
        /// </remarks>
        /// </summary>
        /// <param name="errorMessage">
        /// String value containing the message to be added to the list of errors.
        /// </param>
        public void AddError(string errorMessage)
        {
            if (errorMessage is not null) errors.Add(errorMessage);
        }

        /// <summary>
        /// Clears the list of errors.
        /// </summary>
        public void ClearErrors() => errors.Clear();

        /// <summary>
        /// Updates the Tenant assigned to the user.
        /// </summary>
        /// <param name="tenant">
        /// String value containing the name of the Tenant to which the user is assigned.
        /// A null parameter is allowed and will remove the current Tenant assignment.
        /// </param>
        public void UpdateTenant(string tenant = null) => Tenant = tenant ?? string.Empty;

        /// <summary>
        /// Processes the result of a call to an [Authorize] API endpoint and updates the properties
        /// indicating any errors and if the JWT token needs to be refreshed.
        /// </summary>
        /// <param name="status">
        /// An <see cref="IStatusGeneric"/> object encapsulating the result of an invocation
        /// of an [Authorize] API endpoint.
        /// </param>
        public void CheckIfTokenRefreshNeeded(IStatusGeneric status)
        {
            //  Focus on the status.HttpStatusCode property.
            //  There are two conditions when it is set to Unauthorized:
            //      1. A call was made to a broker that invokes an [Authorize] API endpoint and the user is not authenticated
            //      2. An [Authorize] API endpoint returned the Unauthorized message--presumably due to token expiring
            //  Ignore the first, other that maintaining internal consistency
            //  Deal with the second
            if (status.HttpStatusCode is not HttpStatusCode.Unauthorized) return;

            // Handle second condition.
            if (status.GetAllErrors().Contains(UIOrchestratorConstants.UIOrchestratorConstants.TokenExpiredErrorMessage))
            {
                TokenRefreshNeeded = true;
                return;
            }

            //  Handle the first condition--maintain internal consistency.
            //  The TokenRefreshNeeded property should not be true when IsAuthenticated is false
            if (IsAuthenticated is false && TokenRefreshNeeded)
            {
                AddError("The CheckIfTokenRefreshNeeded method was invoked with the state inconsistency of IsAuthenticated = false and TokenRefreshNeeded = true.");
                TokenRefreshNeeded = false;
            }
        }

        /// <summary>
        /// Resets all affected properties as a result of removing user authentication.
        /// </summary>
        public void LogoutUser()
        {
            Token = string.Empty;
            RefreshToken = string.Empty;
            Tenant = string.Empty;
            userRoles.Clear();
            userPermissions.Clear();
            errors.Clear();
            TokenRefreshNeeded = false;
        }
    }
}
