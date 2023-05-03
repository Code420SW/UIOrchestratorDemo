using Code420.StatusGeneric;

namespace Code420.UIOrchestrator.Core.Models.UserCredentials;

public interface IUserCredentials
{
    //  Directed properties
    string Token { get; }
    string RefreshToken { get; }
    bool TokenRefreshNeeded { get; }
    string Tenant { get; }

    //  Derived properties
    bool IsAuthenticated { get; }
    bool IsValid { get; }
    bool HasErrors { get; }
    IReadOnlyList<string> UserRoles { get; }
    IReadOnlyList<string> UserPermissions { get; }
    IReadOnlyList<string> Errors { get; }

    //  Methods
    void UpdateTokens(string token, string refreshToken);
    void UpdateUserRoles(List<string> roles);
    void UpdateUserPermissions(List<string> permissions);
    bool HasPermission(string permission);
    void AddError(string errorMessage);
    void ClearErrors();
    void UpdateTenant(string? tenant = null);
    void CheckIfTokenRefreshNeeded(IStatusGeneric status);
    void LogoutUser();
}