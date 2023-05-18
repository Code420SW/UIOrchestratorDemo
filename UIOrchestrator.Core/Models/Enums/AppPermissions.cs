using System.ComponentModel.DataAnnotations;

namespace Code420.UIOrchestrator.Core.Models.Enums
{
    /// <summary>
    /// This is an example of how you might build a real application.
    /// Notice that there are lots of permissions - the idea is to have very
    /// detailed control over your application
    /// These permissions are combined to create a Role, which will be more human-focused
    /// </summary>
    /// <remarks>
    /// The idea is to define fine-grained permissions that specify an access level in the
    /// application. These permissions are assigned to a user either directly or combining
    /// into a role which is then assigned to a user. The permissions ultimately determine
    /// which menu items the user can see and which actions they can perform.
    /// </remarks>
    public enum AppPermissions : ushort
    {
        NotSet = 0, //error condition

        AccessAll = ushort.MaxValue
    }
}
