using Code420.StatusGeneric;
using Code420.UIOrchestrator.Core.Models.Enums;
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
            //  Typically this handler would invoke and orchestrator responsible for coordinating
            //  the services required to get the menu items (e.g., status = await menuOrchestrator.GetMenuItemDefinitionsAsync();)
            //  and then returning status directly. For he demo we will define the StatusGenericHandler object,
            //  build the list of menu items, and return the list through the status object.
            
            StatusGenericHandler<List<MenuItemDefinitionDto>> status = new();
            status.SetResult(BuildMenuItems());
            return await Task.FromResult(status);
        }

        private List<MenuItemDefinitionDto> BuildMenuItems()
        {
            //  Note that the MenuItemPackedPermissions property of the MenuItemDefinitionDto
            //  object is not used. This property is used to obfuscate the user permissions
            //  transmitted over-the-wire from an authentication/authorization provider
            //  and subsequently converted to human-readable form by one of the services
            //  invoked (indirectly) by the handler above.
            //
            //  For this demo we will skip MenuItemPackedPermissions and build the
            //  MenuItemRequiredPermissions property directly.
            
            List<MenuItemDefinitionDto> output = new()
            {
                new MenuItemDefinitionDto()
                {
                    MenuItemCode = "Favorites",
                    MenuItemText = "Favorites",
                    MenuItemId = "1000",
                    MenuItemComponentName = "FavoritesMenuTab.FavoritesMenu",
                    MenuItemScope = MenuItemScope.AllMenus,
                    MenuItemRequiredPermissions = new List<string>()
                    {
                        "Permission_1",
                        "AccessAll"
                    },
                    MenuItemIconCss = "oi oi-badge",
                    MenuItemParentId = null,
                    MenuItemIsSeparator = false,
                    MenuItemUrl = string.Empty
                },
                
                new MenuItemDefinitionDto()
                {
                    MenuItemCode = "DummyTab",
                    MenuItemText = "Dummy Tab",
                    MenuItemId = "4000",
                    MenuItemComponentName = "DummyTab.DummyTab",
                    MenuItemScope = MenuItemScope.AllMenus,
                    MenuItemRequiredPermissions = new List<string>()
                    {
                        "Permission_1",
                        "Permission_2",
                        "AccessAll"
                    },
                    MenuItemIconCss = "oi oi-bug",
                    MenuItemParentId = null,
                    MenuItemIsSeparator = false,
                    MenuItemUrl = string.Empty
                },
                
                new MenuItemDefinitionDto()
                {
                    MenuItemCode = "Settings",
                    MenuItemText = "Settings",
                    MenuItemId = "5000",
                    MenuItemComponentName = null,
                    MenuItemScope = MenuItemScope.HorizontalMenu,
                    MenuItemRequiredPermissions = new List<string>()
                    {
                        "Permission_1",
                        "Permission_2",
                        "AccessAll"
                    },
                    MenuItemIconCss = "oi oi-cog",
                    MenuItemParentId = null,
                    MenuItemIsSeparator = false,
                    MenuItemUrl = string.Empty
                },
                
                new MenuItemDefinitionDto()
                {
                    MenuItemCode = "Settings_Configuration",
                    MenuItemText = "Configuration",
                    MenuItemId = "5500",
                    MenuItemComponentName = null,
                    MenuItemScope = MenuItemScope.HorizontalMenu,
                    MenuItemRequiredPermissions = new List<string>()
                    {
                        "Permission_1",
                        "AccessAll"
                    },
                    MenuItemIconCss = string.Empty,
                    MenuItemParentId = "5000",
                    MenuItemIsSeparator = false,
                    MenuItemUrl = string.Empty
                },
                
                new MenuItemDefinitionDto()
                {
                    MenuItemCode = "Settings_Configuration_Application",
                    MenuItemText = "Application",
                    MenuItemId = "5510",
                    MenuItemComponentName = "ConfigurationTab.Configuration",
                    MenuItemScope = MenuItemScope.HorizontalMenu,
                    MenuItemRequiredPermissions = new List<string>()
                    {
                        "Permission_1",
                        "AccessAll"
                    },
                    MenuItemIconCss = string.Empty,
                    MenuItemParentId = "5500",
                    MenuItemIsSeparator = false,
                    MenuItemUrl = string.Empty
                },
                
                new MenuItemDefinitionDto()
                {
                    MenuItemCode = "Settings_User",
                    MenuItemText = "User",
                    MenuItemId = "5700",
                    MenuItemComponentName = null,
                    MenuItemScope = MenuItemScope.HorizontalMenu,
                    MenuItemRequiredPermissions = new List<string>()
                    {
                        "Permission_1",
                        "Permission_2",
                        "AccessAll"
                    },
                    MenuItemIconCss = string.Empty,
                    MenuItemParentId = "5000",
                    MenuItemIsSeparator = false,
                    MenuItemUrl = string.Empty
                },
                
                new MenuItemDefinitionDto()
                {
                    MenuItemCode = "Settings_User_Login",
                    MenuItemText = "Login",
                    MenuItemId = "5710",
                    MenuItemComponentName = "UserLoginTab.UserLogin",
                    MenuItemScope = MenuItemScope.HorizontalMenu,
                    MenuItemRequiredPermissions = new List<string>()
                    {
                        "Permission_1",
                        "Permission_2",
                        "AccessAll"
                    },
                    MenuItemIconCss = string.Empty,
                    MenuItemParentId = "5700",
                    MenuItemIsSeparator = false,
                    MenuItemUrl = string.Empty
                },
                
                new MenuItemDefinitionDto()
                {
                    MenuItemCode = "Settings_User_Logout",
                    MenuItemText = "Logout",
                    MenuItemId = "5720",
                    MenuItemComponentName = null,
                    MenuItemScope = MenuItemScope.HorizontalMenu,
                    MenuItemRequiredPermissions = new List<string>()
                    {
                        "Permission_1",
                        "Permission_2",
                        "AccessAll"
                    },
                    MenuItemIconCss = string.Empty,
                    MenuItemParentId = "5700",
                    MenuItemIsSeparator = false,
                    MenuItemUrl = string.Empty
                },
                
                new MenuItemDefinitionDto()
                {
                    MenuItemCode = "Settings_SwitchTheme",
                    MenuItemText = "Switch Theme",
                    MenuItemId = "5800",
                    MenuItemComponentName = null,
                    MenuItemScope = MenuItemScope.HorizontalMenu,
                    MenuItemRequiredPermissions = new List<string>()
                    {
                        "Permission_1",
                        "AccessAll"
                    },
                    MenuItemIconCss = string.Empty,
                    MenuItemParentId = "5000",
                    MenuItemIsSeparator = false,
                    MenuItemUrl = string.Empty
                },
                
                new MenuItemDefinitionDto()
                {
                    MenuItemCode = "Settings_SwitchTheme_Light",
                    MenuItemText = "Light",
                    MenuItemId = "5810",
                    MenuItemComponentName = null,
                    MenuItemScope = MenuItemScope.HorizontalMenu,
                    MenuItemRequiredPermissions = new List<string>()
                    {
                        "Permission_1",
                        "AccessAll"
                    },
                    MenuItemIconCss = string.Empty,
                    MenuItemParentId = "5800",
                    MenuItemIsSeparator = false,
                    MenuItemUrl = string.Empty
                },
                
                new MenuItemDefinitionDto()
                {
                    MenuItemCode = "Settings_SwitchTheme_Dark",
                    MenuItemText = "Dark",
                    MenuItemId = "5820",
                    MenuItemComponentName = null,
                    MenuItemScope = MenuItemScope.HorizontalMenu,
                    MenuItemRequiredPermissions = new List<string>()
                    {
                        "Permission_1",
                        "AccessAll"
                    },
                    MenuItemIconCss = string.Empty,
                    MenuItemParentId = "5800",
                    MenuItemIsSeparator = false,
                    MenuItemUrl = string.Empty
                },
                
                new MenuItemDefinitionDto()
                {
                    MenuItemCode = "Settings_DoNothing",
                    MenuItemText = "Do Nothing",
                    MenuItemId = "5900",
                    MenuItemComponentName = null,
                    MenuItemScope = MenuItemScope.HorizontalMenu,
                    MenuItemRequiredPermissions = new List<string>()
                    {
                        "Permission_2",
                        "AccessAll"
                    },
                    MenuItemIconCss = string.Empty,
                    MenuItemParentId = "5000",
                    MenuItemIsSeparator = false,
                    MenuItemUrl = string.Empty
                }
            };

            return output;
        }
    }
}
