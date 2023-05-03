using Code420.StatusGeneric;
using Code420.UIOrchestrator.Core.Models.Enums;
using Code420.UIOrchestrator.Core.Models.Menus;
using Code420.UIOrchestrator.Core.Models.UIOrchestratorConstants;
using Code420.UIOrchestrator.Core.Models.UserCredentials;
using Code420.UIOrchestrator.Server.MediatR.Configuration.Menus;
using MediatR;
using Microsoft.AspNetCore.Components;
using Syncfusion.Blazor.Navigations;

namespace Code420.UIOrchestrator.Server.Code.Models.Menus
{
    /// <summary>
    /// Responsible for managing the application's menus.
    /// The menus are exposed through public properties and are security-trimmed
    /// based on the user's permissions.
    /// Typically this will be injected into the <see cref="UIOrchestrator"/> component
    /// by the DI with a scoped lifetime.
    /// </summary>
    /// <remarks>
    /// <para>
    /// The menu definitions are loaded from a repository and stored as a
    ///  <see cref="List{T}"/> of <see cref="MenuItemDefinitionDto"/> objects.
    /// </para>
    /// <para>
    /// The <see cref="MenuManager.rawMenu"/> is a non-hierarchical <see cref="List{T}"/>
    /// of <see cref="OrchestratorMenuItem"/> built from the <see cref="List{T}"/> of
    /// <see cref="MenuItemDefinition"/> objects.
    /// </para>
    /// <para>
    /// The raw menu list is a flat structure that is converted to a hierarchical
    /// structure and stored in the sidebar and horizontal menu properties which are 
    /// accessible through <see cref="SidebarMenu"/> and <see cref="HorizontalMenu"/>
    /// </para>
    /// <para>
    /// The <see cref="SidebarMenu"/> and <see cref="HorizontalMenu"/> properties contain only 
    /// the menu items allowed by the user's permissions.
    /// </para>
    /// <para>
    /// The <see cref="IUserCredentials"/> object is injected from DI and contains the user's
    /// permissions which are compared to the <see cref="OrchestratorMenuItem.RequiredPermissions"/>
    /// property to set <see cref="OrchestratorMenuItem.IsHidden"/> property.
    /// </para>
    /// <para>
    /// Placeholder for Favorites menu.
    /// </para>
    /// </remarks>

    internal sealed class MenuManager : IMenuManager
    {
        //  Make sure this menu item code matches the one for the login menu item
        private const string LoginMenuItemCode = "Settings_User_Login";
        
        private readonly IUserCredentials userCredentials;
        private readonly IMediator mediator;

        private List<OrchestratorMenuItem> rawMenu;
        private List<MenuItemDefinitionDto> menuItemDefinitions;

        private Action<object> menuItemCallbackHandler;


        public MenuManager(IUserCredentials userCredentials, IMediator mediator)
        {
            this.userCredentials = userCredentials;
            this.mediator = mediator;
        }



        /// <summary>
        /// Contains a hierarchical list of <see cref="IOrchestratorMenuItem"/> elements used for the
        /// sidebar menu. Consumed by the <see cref="UIOrchestratorMenu"/> component.
        /// </summary>
        public List<OrchestratorMenuItem> SidebarMenu { get; private set; }

        /// <summary>
        /// Contains a hierarchical list of <see cref="IOrchestratorMenuItem"/> elements used for the
        /// horizontal menu. Consumed by the <see cref="UIOrchestratorHorizontalMenu"/> component.
        /// </summary>
        public List<OrchestratorMenuItem> HorizontalMenu { get; private set; }

        /// <summary>
        /// Contains the <see cref="FavoritesMenuDefinition"/> used by the FavoritesMenu Orchestrator Tab.
        /// </summary>
        public FavoritesMenuDefinition FavoritesMenu { get; private set; }

        /// <summary>
        /// An <see cref="IStatusGeneric"/> object containing the current operational status
        /// of the <see cref="MenuManager"/>.
        /// </summary>
        public IStatusGeneric Status { get; private set; }

        /// <summary>
        /// String value containing the <see cref="OrchestratorMenuItem.ItemId"/> assigned to the
        /// Login Tab.
        /// </summary>
        public string LoginTabItemId => GetMenuItemIdFromMenuItemCode(LoginMenuItemCode);

        /// <summary>
        /// Boolean value indicating if the menu system is initialized.
        /// </summary>
        public bool MenuSystemInitialized { get; private set; }


        /// <summary>
        /// Primary entry point invoked by the consumer to initialize the application's
        /// menu system.
        /// </summary>
        /// <param name="forceReload">
        /// Boolean value that indicates if the menu system should be reloaded from the
        /// repository (true).
        /// </param>
        /// <returns></returns>
        /// <remarks>
        /// The consumer must invoke this method prior to using the menu system.
        /// The method will use the currently loaded menu system unless the forceReload
        /// parameter is set to true in which case, the menu system is reloaded from
        /// the database. 
        /// All subordinate methods are invoked to configure the menu system
        /// such that, upon return, the menu system is ready to use.
        /// </remarks>
        public async Task InitializeMenusAsync(bool forceReload = false)
        {
            //  If needed (menus have not been previously loaded or a force-reload
            //  is requested)...
            if (forceReload || rawMenu is null)
            {
                //  Load the list of MenuItemDefinitionDto objects from the db
                var loadMenuItemDefinitionsStatus = await LoadMenuItemDefinitionsAsync();
                if (loadMenuItemDefinitionsStatus.HasErrors)
                {
                    Status = loadMenuItemDefinitionsStatus;
                    SidebarMenu = new();
                    HorizontalMenu = new();
                    MenuSystemInitialized = false;
                    return;
                }
                
                //  Save the list of MenuItemDefinitionDto objects
                menuItemDefinitions = loadMenuItemDefinitionsStatus.Result;
                
                //  Convert the MenuItemDefinitionDto objects into OrchestratorMenuItem objects.
                //  This is a flat (non-hierarchical) list.
                var buildRawMenuStatus = BuildRawMenu();
                rawMenu = buildRawMenuStatus.Result;
            }
            
            //  Create the "no errors" object
            Status = new StatusGenericHandler();
            
            //  Security-trim the menu items by apply the user permissions to the raw menu
            ApplyPermissionsToRawMenu();
            
            //  Convert the flat menus into hierarchical menus.
            SidebarMenu = BuildMenuHierarchy(MenuItemScope.SidebarMenu);
            HorizontalMenu = BuildMenuHierarchy(MenuItemScope.HorizontalMenu);
            BuildFavoritesMenu();
            MenuSystemInitialized = true;
        }

        /// <summary>
        /// Method used by the consumer to register an <see cref="Action{T}"/> method where
        /// T is an <see cref="object"/>. 
        /// </summary>
        /// <param name="callbackHandler">
        /// Reference to an <see cref="Action{T}"/> method.
        /// </param>
        /// <remarks>
        /// The registered method will be invoked when a menu item is clicked and the 
        /// <see cref="OrchestratorMenuItem.ItemId"/> string value is passed (cast an on object).
        /// </remarks>
        public void RegisterMenuItemCallbackHandler(Action<object> callbackHandler) => 
            menuItemCallbackHandler = callbackHandler;

        /// <summary>
        /// Returns the <see cref="MenuItemDefinition.MenuItemText"/> associated with the
        /// passed <see cref="OrchestratorMenuItem.ItemId"/> which is the same as the
        /// <see cref="MenuItemDefinition.MenuItemId"/>.
        /// </summary>
        /// <param name="menuItemId">
        /// String value containing the <see cref="MenuItemDefinition.MenuItemId"/>.
        /// </param>
        /// <returns>
        /// String value containing the <see cref="MenuItemDefinition.MenuItemText"/>,
        /// or string.Empty if the menuItemId is not valid.
        /// </returns>
        public string GetMenuItemTextFromMenuItemId(string menuItemId)
        {
            var item = menuItemDefinitions.FirstOrDefault(x => x.MenuItemId == menuItemId);
            return (item is null) ? string.Empty : item.MenuItemText;
        }

        /// <summary>
        /// Returns the <see cref="MenuItemDefinition.MenuItemCode"/> associated with the
        /// passed <see cref="OrchestratorMenuItem.ItemId"/> which is the same as the
        /// <see cref="MenuItemDefinition.MenuItemId"/>.
        /// </summary>
        /// <param name="menuItemId">
        /// String value containing the <see cref="MenuItemDefinition.MenuItemId"/>.
        /// </param>
        /// <returns>
        /// String value containing the <see cref="MenuItemDefinition.MenuItemCode"/>,
        /// or string.Empty if the menuItemId is not valid.
        /// </returns>
        public string GetMenuItemCodeFromMenuItemId(string menuItemId)
        {
            var item = menuItemDefinitions.FirstOrDefault(x => x.MenuItemId == menuItemId);
            return (item is null) ? string.Empty : item.MenuItemCode;
        }

        /// <summary>
        /// Returns the <see cref="MenuItemDefinition.MenuItemId"/> for the passed 
        /// <see cref="MenuItemDefinition.MenuItemCode"/>.
        /// </summary>
        /// <param name="menuItemCode">
        /// String value containing the <see cref="MenuItemDefinition.MenuItemCode"/> to search for.
        /// </param>
        /// <returns>
        /// String value containing the <see cref="MenuItemDefinition.MenuItemId"/> associated with
        /// the passed <see cref="MenuItemDefinition.MenuItemCode"/>,
        /// or string.Empty if the menuItemCode is not valid.
        /// </returns>
        public string GetMenuItemIdFromMenuItemCode(string menuItemCode)
        {
            var item = menuItemDefinitions?.FirstOrDefault(x => x.MenuItemCode == menuItemCode);
            return (item is null) ? string.Empty : item.MenuItemId;
        }

        /// <summary>
        /// Returns the <see cref="OrchestratorMenuItem.TabDefinition"/> for the menu item
        /// associated with the passed <see cref="OrchestratorMenuItem.ItemId"/>.
        /// </summary>
        /// <param name="menuItemId">
        /// String value containing the <see cref="OrchestratorMenuItem.ItemId"/>.
        /// </param>
        /// <returns>
        /// The <see cref="OrchestratorMenuItem.TabDefinition"/> for the menu item
        /// associated with the passed <see cref="OrchestratorMenuItem.ItemId"/>,
        /// or null if the menuItemId is not valid.
        /// </returns>
        public TabItem GetTabDefinitionFromMenuItemId(string menuItemId)
        {
            var index = rawMenu.FindIndex(x => x.ItemId == menuItemId);
            return (index is -1) ? null : rawMenu[index].TabDefinition;
        }


        /// <summary>
        /// Load the list of <see cref="MenuItemDefinitionDto"/> objects from the repository.
        /// </summary>
        /// <returns>
        /// A <see cref="StatusGenericHandler{T}"/> object containing the results of the
        /// operation. The <see cref="StatusGenericHandler{T}.Result"/> property contains
        /// the list of <see cref="MenuItemDefinitionDto"/> objects.
        /// </returns>
        private async Task<IStatusGeneric<List<MenuItemDefinitionDto>>> LoadMenuItemDefinitionsAsync()
        {
            var returnedStatus = await mediator.Send(new MenuItemDefinitionsQueryRequest());
            return returnedStatus;
        }
        
        /// <summary>
        /// Builds a flat (single-level) <see cref="List{T}"/> of <see cref="OrchestratorMenuItem"/> objects.
        /// An <see cref="OrchestratorMenuItem"/> item is created for each element in the 
        /// <see cref="menuItemDefinitions"/> (a list of <see cref="MenuItemDefinitionDto"/> objects).
        /// </summary>
        /// <returns>
        /// A <see cref="StatusGenericHandler{T}"/> object containing the result of the operation.
        /// The <see cref="StatusGenericHandler{T}.Result"/> property contains the <see cref="List{T}"/>
        /// of <see cref="OrchestratorMenuItem"/> objects.
        /// </returns>
        /// <exception cref="ArgumentException">
        /// The method is responsible for ensuring the <see cref="MenuItemDefinitionDto.MenuItemComponentName"/>
        /// property can be correctly resolved. If this validation fails, it is a developer error so
        /// an exception is thrown.
        /// </exception>
        private StatusGenericHandler<List<OrchestratorMenuItem>> BuildRawMenu()
        {
            List<OrchestratorMenuItem> menu = new();
            StatusGenericHandler<List<OrchestratorMenuItem>> returnedStatus = new();

            //  Iterate over the menuItemDefinitions list...
            foreach (var menuItemDefinition in menuItemDefinitions)
            {
                //  If an Orchestrator Tab is associated with the menu item...
                //  Otherwise, set the TabDefinition property to null
                TabItem tabItem = null;
                if (menuItemDefinition.MenuItemComponentName is not null)
                {
                    //  Try to resolve the MenuItemComponentName
                    //  If it can't be resolves, this is developer error so throw.
                    var componentNameType = Type.GetType($"{UIOrchestratorConstants.OrchestratorTabBaseNamespace}{menuItemDefinition.MenuItemComponentName}");
                    if (componentNameType is null)
                    {
                        throw new ArgumentException($"The component {UIOrchestratorConstants.OrchestratorTabBaseNamespace}{menuItemDefinition.MenuItemComponentName} can not be resolved.");
                    }

                    //  Build the TabDefinition
                    tabItem = new TabItem()
                    {
                        CssClass = menuItemDefinition.MenuItemId,
                        ContentTemplate = RenderComponent(componentNameType, menuItemDefinition.MenuItemId),
                        Disabled = false,
                        Header = new TabHeader()
                        {
                            IconCss = menuItemDefinition.MenuItemIconCss,
                            IconPosition = "left",
                            Text = menuItemDefinition.MenuItemText
                        },
                        Visible = true
                    };
                }

                //  Create the OrchestratorMenuItem object
                menu.Add(new OrchestratorMenuItem
                {
                    MenuItemCallback = MenuItemCallbackHandler,
                    RequiredPermissions = menuItemDefinition.MenuItemRequiredPermissions,
                    MenuItemScope = menuItemDefinition.MenuItemScope,
                    MenuText = menuItemDefinition.MenuItemText,
                    IsDisabled = false,
                    IsHidden = false,
                    HtmlAttributes = new(),
                    IconCss = menuItemDefinition.MenuItemIconCss,
                    ItemId = menuItemDefinition.MenuItemId,
                    ParentId = menuItemDefinition.MenuItemParentId,
                    IsSeparator = menuItemDefinition.MenuItemIsSeparator,
                    SubMenu = default,
                    Url = menuItemDefinition.MenuItemUrl,
                    TabDefinition = tabItem
                });
            }

            returnedStatus.SetResult(menu);
            return returnedStatus;
        }
        
        /// <summary>
        /// Security-trims the menu system.
        /// <remarks>
        /// <para>
        /// Compares the <see cref="UserCredentials.UserPermissions"/> list to the <see cref="OrchestratorMenuItem.RequiredPermissions"/>
        /// list and, if the user does not have the proper permissions, sets the <see cref="OrchestratorMenuItem.IsHidden"/> property
        /// to true.
        /// </para>
        /// <para>
        /// The BuildRawMenu method must be invoked prior to invoking this method.
        /// </para>
        /// </remarks>
        /// </summary>
        private void ApplyPermissionsToRawMenu()
        {
            //  Bail if the base menu system has no menu items
            if (rawMenu.Any() is false) return;

            //  Set the IsHidden property based on user permissions set
            foreach (var menuItem in rawMenu)
            {
                //  Note to self:
                //  It is tempting to use the MenuItemScope property to control if a menu item is
                //  applicable to a user's permissions. This would make the filtering in BuildMenuHierarchy()
                //  easier but has a side effect that should be avoided.
                //  The base methodology dictates that MenuItemDefinitions should be immutable even after they
                //  are transferred to OrchestratorMenuItem. To do otherwise would require a reload of
                //  MenuItemDefinitions after mutation.
                //  The OrchestratorMenuItem IsHidden property is a good candidate for indicating the
                //  application of a menu item based on user permissions since its mutation has no side
                //  effects. This does require additional filtering in BuildMenuHierarchy().
                menuItem.IsHidden = (menuItem.RequiredPermissions.Any(x => userCredentials.UserPermissions.Contains(x)) is false);
            }

            //  Set the IsHidden property of the Login and Logout menu items based on user authentication state
            var temp = rawMenu.FindIndex(x => x.MenuText == "Login");
            if (temp is not -1) rawMenu[temp].IsHidden = userCredentials.IsAuthenticated;
            temp = rawMenu.FindIndex(x => x.MenuText == "Logout");
            if (temp is not-1) rawMenu[temp].IsHidden = (userCredentials.IsAuthenticated is false);
        }

        /// <summary>
        /// Converts the flat rawMenu menu system into the hierarchical menu structure
        /// required by the <see cref="MenuBase{TValue}"/> component.
        /// <remarks>
        /// The ApplyPermissionsToRawMenu method must be invoked prior to invoking this method.
        /// </remarks>
        /// </summary>
        /// <param name="menuItemScope">
        /// One of the <see cref="MenuItemScope"/> enum elements that specify which menu is
        /// to be build.
        /// </param>
        /// <returns>
        /// A <see cref="List{T}"/> of <see cref="OrchestratorMenuItem"/> objects in a hierarchical
        /// format (parent-child relationship).
        /// </returns>
        private List<OrchestratorMenuItem> BuildMenuHierarchy(MenuItemScope menuItemScope)
        {
            //  https://stackoverflow.com/questions/25532262/mapping-a-flat-list-to-a-hierarchical-list-with-parent-ids-c-sharp
            //  Build a new list of OrchestratorMenuItems from the rawMenu list
            //  Customize element properties as needed (e.g., set the MenuItemCallback property)
            var categories = 
                (from menuItem in rawMenu
                 select new OrchestratorMenuItem()
                 {
                     MenuItemCallback = MenuItemCallbackHandler,
                     RequiredPermissions = menuItem.RequiredPermissions,
                     MenuItemScope = menuItem.MenuItemScope,
                     MenuText = menuItem.MenuText,
                     IsDisabled = menuItem.IsDisabled,
                     IsHidden = menuItem.IsHidden,
                     IconCss = menuItem.IconCss,
                     ItemId = menuItem.ItemId,
                     ParentId = menuItem.ParentId,
                     IsSeparator = menuItem.IsSeparator,
                     Url = menuItem.Url
                 })
                 .Where(x => 
                            (x.MenuItemScope == MenuItemScope.AllMenus || 
                             x.MenuItemScope == menuItemScope) &&
                            (x.IsHidden is false)
                        )
                .ToList();

            //  Create a Lookup table of key-value pairs using the ParentId as the key
            //  For each ParentId, a series of entries will be created--one for each menu item
            var lookup = categories.ToLookup(c => c.ParentId);

            //  Iterate over the list of menu items and populate the SubMenu property with a list containing
            //  all the key-value pairs matching the ItemId (find all the children)
            foreach (var c in categories)
            {
                //  This check ensures an empty SubMenu list is not created.
                if (lookup.Contains(c.ItemId)) c.SubMenu = lookup[c.ItemId].ToList();
            }

            //  Finally, remove all top-level menu items that have a ParentId and are really children.
            categories.RemoveAll(c => c.ParentId is not null);

            return categories;
        }

        /// <summary>
        /// The method is injected in each menu item's <see cref="OrchestratorMenuItem.MenuItemCallback"/> property
        /// and invoked when the menu item is clicked.
        /// </summary>
        /// <param name="o">
        /// A generic <see cref="object"/>.
        /// </param>
        /// <remarks>
        /// <para>
        /// The <see cref="UIOrchestratorMenu.MyItemSelectedHandler(Syncfusion.Blazor.Navigations.MenuEventArgs{OrchestratorMenuItem})"/> and
        /// <see cref="UIOrchestratorHorizontalMenu.MyItemSelectedHandler(Syncfusion.Blazor.Navigations.MenuEventArgs{OrchestratorMenuItem})"/>
        /// methods will invoke this method when a menu item is clicked and will extract the <see cref="OrchestratorMenuItem.ItemId"/> property
        /// for the selected menu item and pass it to this method as an <see cref="object"/>.
        /// </para>
        /// <para>
        /// The <see cref="UIOrchestrator"/> processes all menu item selections and will register its own
        /// menu handler via the <see cref="RegisterMenuItemCallbackHandler(Action{object})"/> method.
        /// </para>
        /// <para>
        /// This method will invoke that callback handler if it is defined.
        /// <b>Note that UIOrchestrator registers an async delegate:<br />
        /// <c>async (s) => await MenuItemCallbackHandlerAsync((string)s)</c><br />
        /// which means the Invoke method is really a fire-and-forget.</b>
        /// </para>
        /// </remarks>
        private void MenuItemCallbackHandler(object o)
        {
            menuItemCallbackHandler?.Invoke(o);
        }

        /// <summary>
        /// Builds the <see cref="RenderFragment"/> for the passed component and
        /// adds a MenuItemId attribute for the passed menuItemId.
        /// </summary>
        /// <param name="componentType">
        /// A <see cref="Type"/> object containing the component for which the <see cref="RenderFragment"/>
        /// is built. It is the caller's responsibility to ensure the component resolves correctly to
        /// avoid having an exception thrown when the <see cref="RenderFragment"/> is first used.
        /// </param>
        /// <param name="menuItemId">
        /// String value containing the Menu Item Id that, when selected, causes the component to
        /// load using the <see cref="RenderFragment"/>.
        /// </param>
        /// <returns>
        /// The <see cref="RenderFragment"/> object.
        /// </returns>
        private RenderFragment RenderComponent(Type componentType, string menuItemId) =>
            builder =>
            {
                builder.OpenComponent(0, componentType);
                builder.AddAttribute(1, "MenuItemId", menuItemId);
                builder.CloseComponent();
            };

        private void BuildFavoritesMenu()
        {
            //
            // Here is how this works.
            // The basic elements for the favorites menu are defined in the FavoritesMenuDefinition.
            // The info for each menu item is defined in a FavoritesMenuItem list element.
            // 
            // The program will calculate the OffsetX and OffsetY elements for each menu item...no need for you to do it.
            // The program enforces a maximum number of menu items to 8.
            // If more than 8 menu items are in the FavoriteMenuItems list, the MenuItemCount will be set to 8.
            // If less than 8 menu items are defined, the MenuItemCount will reflect the actual count but
            //  additional menu items will be added to FavoriteMenuItems so 8 elements are defined in the list.
            //  The additional elements are added with default values and will not be displayed.
            //  This is needed by the component's CSS styling to prevent a bunch of awkward code.
            //

            FavoritesMenu = new FavoritesMenuDefinition()
            {
                MenuItemCount = 0,
                MenuRadius = 225,
                PrimaryCenterImage = "./images/code420-white.jpg",
                SecondaryCenterImage = "./images/code420-black.jpg",
                ThrobScale = "1.05",
                MenuMinimumHeight = "700px",
                CenterImageSize = "200px",
                MenuItemRotationSpeed = "30s",
                CenterImageTransitionSpeed = "500ms",
                MenuItemSize = "80px",
                MenuItemTransitionSpeed = "500ms",

                FavoritesMenuItems = new()
                {
                    new()
                    {
                        ItemId = "4000",
                        MenuItemCallback = MenuItemCallbackHandler,
                        OffsetX = "",
                        OffsetY = "",
                        IconCss = "oi oi-basket",
                        IconFontSize = "24px",
                        IconColor = "hsl(0deg, 0%, 100%)",
                        HoverText = "Dummy Tab",
                        HoverTextFontSize = "12px",
                        HoverTextColor = "hsl(0deg, 0%, 100%)",
                        MenuItemIconBackgroundColor = "hsl(123deg, 47%, 74%)",
                        MenuItemTextBackgroundColor = "#2e7f32"
                    },

                    new()
                    {
                        ItemId = "10001",
                        MenuItemCallback = MenuItemCallbackHandler,
                        OffsetX = "",
                        OffsetY = "",
                        IconCss = "oi oi-bell",
                        IconFontSize = "24px",
                        IconColor = "hsl(0deg, 0%, 100%)",
                        HoverText = "Time & Attendance",
                        HoverTextFontSize = "12px",
                        HoverTextColor = "hsl(0deg, 0%, 100%)",
                        MenuItemIconBackgroundColor = "hsl(210deg, 47%, 74%)",
                        MenuItemTextBackgroundColor = "#2e577f"
                    },

                    new()
                    {
                        ItemId = "10002",
                        MenuItemCallback = MenuItemCallbackHandler,
                        OffsetX = "",
                        OffsetY = "",
                        IconCss = "oi oi-calendar",
                        IconFontSize = "24px",
                        IconColor = "hsl(0deg, 0%, 100%)",
                        HoverText = "Onboarding",
                        HoverTextFontSize = "12px",
                        HoverTextColor = "hsl(0deg, 0%, 100%)",
                        MenuItemIconBackgroundColor = "hsl(34deg, 93%, 77%)",
                        MenuItemTextBackgroundColor = "#b66a07"
                    },

                    new()
                    {
                        ItemId = "10003",
                        MenuItemCallback = MenuItemCallbackHandler,
                        OffsetX = "",
                        OffsetY = "",
                        IconCss = "oi oi-clock",
                        IconFontSize = "24px",
                        IconColor = "hsl(0deg, 0%, 100%)",
                        HoverText = "HR Information System",
                        HoverTextFontSize = "12px",
                        HoverTextColor = "hsl(0deg, 0%, 100%)",
                        MenuItemIconBackgroundColor = "hsl(231deg, 44%, 76%)",
                        MenuItemTextBackgroundColor = "#334084"
                    },

                    new()
                    {
                        ItemId = "10004",
                        MenuItemCallback = MenuItemCallbackHandler,
                        OffsetX = "",
                        OffsetY = "",
                        IconCss = "oi oi-envelope-open",
                        IconFontSize = "24px",
                        IconColor = "hsl(0deg, 0%, 100%)",
                        HoverText = "Leaves & Holidays",
                        HoverTextFontSize = "12px",
                        HoverTextColor = "hsl(0deg, 0%, 100%)",
                        MenuItemIconBackgroundColor = "hsl(54deg, 76%, 79%)",
                        MenuItemTextBackgroundColor = "#afa018"
                    },

                    new()
                    {
                        ItemId = "10005",
                        MenuItemCallback = MenuItemCallbackHandler,
                        OffsetX = "",
                        OffsetY = "",
                        IconCss = "oi oi-fire",
                        IconFontSize = "24px",
                        IconColor = "hsl(0deg, 0%, 100%)",
                        HoverText = "Exit & Off-boarding",
                        HoverTextFontSize = "12px",
                        HoverTextColor = "hsl(0deg, 0%, 100%)",
                        MenuItemIconBackgroundColor = "hsl(20deg, 41%, 69%)",
                        MenuItemTextBackgroundColor = "#68402c"
                    },

                    new()
                    {
                        ItemId = "10006",
                        MenuItemCallback = MenuItemCallbackHandler,
                        OffsetX = "",
                        OffsetY = "",
                        IconCss = "oi oi-image",
                        IconFontSize = "24px",
                        IconColor = "hsl(0deg, 0%, 100%)",
                        HoverText = "Travel & Requisitions",
                        HoverTextFontSize = "12px",
                        HoverTextColor = "hsl(0deg, 0%, 100%)",
                        MenuItemIconBackgroundColor = "hsl(0deg, 59%, 76%)",
                        MenuItemTextBackgroundColor = "#922626"
                    },

                    new()
                    {
                        ItemId = "10007",
                        MenuItemCallback = MenuItemCallbackHandler,
                        OffsetX = "",
                        OffsetY = "",
                        IconCss = "oi oi-pie-chart",
                        IconFontSize = "24px",
                        IconColor = "hsl(0deg, 0%, 100%)",
                        HoverText = "Disciplinary",
                        HoverTextFontSize = "12px",
                        HoverTextColor = "hsl(0deg, 0%, 100%)",
                        MenuItemIconBackgroundColor = "hsl(295deg, 42%, 72%)",
                        MenuItemTextBackgroundColor = "#6e2f74"
                    }
                }
            };

            InitialzeFavoritesMenu();
            FinalizeFavoritesMenu();
        }

        private void InitialzeFavoritesMenu()
        {
            // Capture the number of menu items in the FavoritesMenuItems list
            FavoritesMenu.MenuItemCount = FavoritesMenu.FavoritesMenuItems.Count;
            switch (FavoritesMenu.MenuItemCount)
            {
                case 0:
                    return;
                case > 8:
                    FavoritesMenu.MenuItemCount = 8;
                    break;
            }


            // Calculate the angular spacing between menu items
            double angularSpacing = (2 * Math.PI) / FavoritesMenu.MenuItemCount;

            // Calculate the X- and Y-offsets for each menu item
            // Store results in the menu item's record as a CSS value
            for (int i = 0; i < FavoritesMenu.MenuItemCount; i++)
            {
                var tempY = Math.Sin(i * angularSpacing) * FavoritesMenu.MenuRadius;
                var tempX = Math.Cos(i * angularSpacing) * FavoritesMenu.MenuRadius;
                FavoritesMenu.FavoritesMenuItems[i].OffsetX = Convert.ToInt16(tempX).ToString() + "px";
                FavoritesMenu.FavoritesMenuItems[i].OffsetY = Convert.ToInt16(tempY).ToString() + "px";
            }
        }

        private void FinalizeFavoritesMenu()
        {
            if (FavoritesMenu.MenuItemCount == 8) return;

            // Back-fill the FavoritesMenuItems list with default menu items until it has eight elements
            for (int i = FavoritesMenu.MenuItemCount + 1; i <= 8; i++)
            {
                FavoritesMenu.FavoritesMenuItems.Add(new());
            }
        }
    }
}
