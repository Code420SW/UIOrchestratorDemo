using System.Diagnostics.CodeAnalysis;
using MediatR;
using Microsoft.AspNetCore.Components;
using Syncfusion.Blazor.Navigations;
using Code420.UIOrchestrator.Server.Code.Models.Theme;
using Code420.UIOrchestrator.Server.Code.Models.Menus;
using Code420.UIOrchestrator.Core.Models.UserCredentials;
using Code420.UIOrchestrator.Server.Code.Enums;
using Code420.UIOrchestrator.Server.Components.CustomComponents.CustomToasts.GenericErrors;
using Code420.UIOrchestrator.Server.Components.UIOrchestratorComponents.UIOrchestratorHorizontalMenu;
using Code420.UIOrchestrator.Server.Components.UIOrchestratorComponents.UIOrchestratorMenu;
using Code420.UIOrchestrator.Server.Components.UIOrchestratorComponents.UIOrchestratorSidebar;
using Code420.UIOrchestrator.Server.Components.UIOrchestratorComponents.UIOrchestratorSidebarButton;
using Code420.UIOrchestrator.Server.Components.UIOrchestratorComponents.UIOrchestratorTabManager;
using Code420.UIOrchestrator.Server.MediatR.User;

namespace Code420.UIOrchestrator.Server.Pages.UIOrchestrator
{
    /// <summary>
    /// Responsible for containerizing all UI elements (e.g., OrchestratorSidebar and OrchestratorTabManager)
    /// and the data structures used by these components.
    /// </summary>
    /// <remarks>
    /// <para>
    /// The Orchestrator component is the primary application page. It lives at the root URL
    /// and is, therefore, the first page loaded. It is the only URL available in the application.
    /// </para>
    /// <para>
    /// The Orchestrator component is injected into MainLayout through the @Body element.
    /// </para>
    /// <para>
    /// The Orchestrator exposes public methods to child components that allow the child to notify
    /// the Orchestrator that an event has occurred (user clicked a menu item).
    /// </para>
    /// <para>
    /// The component does not expose any base, event callback or CSS parameters.
    /// </para>
    /// <para>
    /// The following objects are injected from the DI:<br />
    /// <see cref="IThemeManager"/> -- Responsible for UI theming.<br />
    ///  <see cref="IMenuManager"/> -- Responsible for managing the application's menu system.<br />
    ///  <see cref="IUserCredentials"/> -- Responsible for managing the user authentication data.<br />
    ///  <see cref="IMediator"/> -- Command handler required to perform actions related to the user
    /// authorization, permissions, etc.
    /// </para>
    /// </remarks>

    // ReSharper disable once ClassNeverInstantiated.Global
    [SuppressMessage("Usage", "BL0005:Component parameter should not be set outside of its component.")]
    public partial class UIOrchestrator : ComponentBase
    {

        #region Instance Variables

        //  This is the CSS class for the div into which the UIOrchestratorHorizontalMenu
        //  component is injected and is passed down to the component
        private const string orchestratorMenuTargetClass = "header2__menu";
        
        private UIOrchestratorSidebar sidebar;
        private UIOrchestratorSidebarButton buttonSidebarToggle;
        private UIOrchestratorTabManager tabmanager;
        private UIOrchestratorHorizontalMenu horizontalMenu;
        private GenericError toastGenericError;

        private bool initialSidebarIsOpen;
        private bool showPageHeader1;
        
        // ReSharper disable once NotAccessedField.Local
        // Reserved for future use
        private bool showPageHeader2;
        
        private bool startupComplete;

        #endregion


        #region Public Properties
        
        /// <summary>
        /// Provides access to the Sidebar menu structure maintained by the IMenuManager.
        /// </summary>
        public List<OrchestratorMenuItem> SidebarMenuItems => menuManager.SidebarMenu;
        
        /// <summary>
        /// Provides access to the Horizontal menu structure maintained by the IMenuManager.
        /// </summary>
        public List<OrchestratorMenuItem> MainMenuItems => menuManager.HorizontalMenu;
        
        /// <summary>
        /// Provides access to the <see cref="OrchestratorMenuItem.ItemId"/> associated
        /// with the LoginUser tab.
        /// </summary>
        public string LoginTabItemId => menuManager.LoginTabItemId;
        
        /// <summary>
        /// Provides access to the Favorites menu structure maintained by the IMenuManager.
        /// </summary>
        //  NOTE: The FavoritesMenuDefinition class is internal
        internal FavoritesMenuDefinition FavoritesMenu => menuManager.FavoritesMenu;

        #endregion


        #region Injected Dependencies

        [Inject] private IThemeManager themeManager { get; set; }

        [Inject] private IMenuManager menuManager { get; set; }

        [Inject] private IUserCredentials userCredentials { get; set; }

        [Inject] private IMediator mediator { get; set; }

        #endregion



        #region Constructors

        protected override async Task OnInitializedAsync()
        {
            //  Execute the following if the menu system has not been initialized
            if (menuManager.MenuSystemInitialized is false)
            {
                //  Initialize the Menu Manager
                //  1.  Register our callback method for handling menu item selections
                //      (must be done before initializing the menus)
                //  2.  Initialize the menu system
                // ReSharper disable once AsyncVoidLambda
                menuManager.RegisterMenuItemCallbackHandler(async (s) => await MenuItemCallbackHandlerAsync((string)s));
                await menuManager.InitializeMenusAsync();

                //  Set the showPageHeader1 and showPageHeader2 variables to false to suppress
                //  display of the header rows (hidden prior to user authentication).
                showPageHeader1 = false;
                showPageHeader2 = false;
                
                //  Set the initialSidebarIsOpen variable to false to suppress display of the
                //  Sidebar prior to user authentication.
                initialSidebarIsOpen = false;

                //  Set the initial theming
                themeManager.SetThemeType(ThemeType.Light);
            }
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            //  This is a result of the component lifecycle
            //  The second (and additional) invocation of OnAfterRenderAsync() will occur AFTER
            //      the menu system has had a chance to load (see OnInitializedAsync()).
            //  The startupComplete flag will ensure the following is executed only once.
            // if ((firstRender is false) && (startupComplete is false))
            if (startupComplete is false)
            {
                await LoadLoginTabAsync();
                startupComplete = true;
            }
        }

        #endregion



        #region Public Methods Providing Access to the Underlying Components to the Consumer

        #region Methods Invoked by Child Components

        /// <summary>
        /// Toggles the open/close state of the Sidebar.
        /// </summary>
        /// <remarks>
        /// Invoked by <see cref="UIOrchestratorSidebarButton"/>.
        /// Invokes the <see cref="UIOrchestratorSidebar.ToggleSidebarAsync"/> method.
        /// </remarks>
        public async Task ToggleSidebarAsync() => await sidebar.ToggleSidebarAsync();

        /// <summary>
        /// Retrieves the current value of the Sidebar IsOpen parameter.
        /// </summary>
        /// <returns>Boolean value indicating if the Sidebar is open.</returns>
        /// <remarks>
        /// Invoked by <see cref="UIOrchestratorSidebarButton"/>.
        /// Invokes the <see cref="UIOrchestratorSidebar.GetSidebarState"/> method if the <see cref="UIOrchestratorSidebar"/> 
        /// has been created. Otherwise, return the initialSidebarIsOpen variable which will be passed to the Sidebar component
        /// when instantiated.
        /// </remarks>
        public bool IsSidebarOpen() => sidebar?.GetSidebarState() ?? initialSidebarIsOpen;

        #endregion


        #region Methods Invoked by Menu Items and Tabs

        /// <summary>
        /// Responsible for completing the user login process after the user has been authenticated.
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        /// This method is invoked after the user authentication process (performed by the UserLogin Tab) is complete. 
        /// The method finds the reference to the UserLogin Tab and invokes the 
        /// <see cref="UIOrchestratorTabManager.RemoveTabAsync(int)"/> method to close the Tab. 
        /// The <see cref="UserAuthenticationStateChangedAsync"/> method is invoked to reconfigure the 
        /// application based on the user's permissions.
        /// </remarks>
        public async Task UserLoginComplete()
        {
            //  If the UserLogin Tab is active, close it
            var tabItemIndex = tabmanager.GetTabItemIndexByMenuItemId(LoginTabItemId);
            if (tabItemIndex is not -1) await tabmanager.RemoveTabAsync(tabItemIndex);

            //  Make necessary application changes based on the change in authentication
            await UserAuthenticationStateChangedAsync();
        }

        /// <summary>
        /// Determine if the user is authenticated.
        /// </summary>
        /// <returns>
        /// Boolean value indicating if the user is authenticated.
        /// </returns>
        /// <remarks>
        /// Invoked by the <see cref="UIOrchestratorTabManager"/> to determine if the user
        /// can close the UserLogin Tab.
        /// </remarks>
        public bool UserIsAuthenticated() => userCredentials.IsAuthenticated;

        /// <summary>
        /// Returns the <see cref="OrchestratorMenuItem.TabDefinition"/> object (which is
        /// analogous to the <see cref="TabItem"/> object) for the menu item associated
        /// with the passed menuItemId parameter.
        /// </summary>
        /// <param name="menuItemId">
        /// String value containing the <see cref="OrchestratorMenuItem.ItemId"/> of the
        /// menu item for which the <see cref="OrchestratorMenuItem.TabDefinition"/> is returned.
        /// </param>
        /// <returns>
        /// The <see cref="OrchestratorMenuItem.TabDefinition"/> associated with the passed menuItemId.
        /// May be null if the menu item is not associated with a <see cref="TabItem"/>.
        /// </returns>
        public TabItem GetTabDefinitionFromMenuItemId(string menuItemId)
        {
            return menuManager.GetTabDefinitionFromMenuItemId(menuItemId);
        }

        /// <summary>
        /// Retrieves the ItemId for the menu item associated with the Login command.
        /// </summary>
        /// <returns>
        /// String value containing the <see cref="OrchestratorMenuItem.ItemId"/> for the
        /// menu item associated with the Login command.
        /// </returns>
        private string GetLoginTabItemId() => LoginTabItemId;

        #endregion

        #endregion



        #region Private Methods for Internal Use Only

        /// <summary>
        /// Processes selected menu item by invoking the appropriate method.
        /// </summary>
        /// <param name="menuItemId">
        /// String value containing the <see cref="OrchestratorMenuItem.ItemId"/>
        /// for the selected menu item.
        /// </param>
        /// <returns></returns>
        /// <remarks>
        /// The <see cref="MenuManager"/> injects its <see cref="MenuManager.MenuItemCallbackHandler(object)"/>
        /// in every <see cref="OrchestratorMenuItem.MenuItemCallback"/> property it builds.
        /// The <see cref="UIOrchestratorMenu"/> and <see cref="UIOrchestratorHorizontalMenu"/> components
        /// will invoke the method defined in the <see cref="OrchestratorMenuItem.MenuItemCallback"/> for
        /// the selected menu item.
        /// The <see cref="UIOrchestrator.OnInitializedAsync"/> method invokes the 
        /// <see cref="MenuManager.RegisterMenuItemCallbackHandler(System.Action{object})"/> method to
        /// register this method.
        /// The <see cref="MenuManager.MenuItemCallbackHandler(object)"/> will invoke the registered
        /// method.
        /// </remarks>
        private async Task MenuItemCallbackHandlerAsync(string menuItemId)
        {
            //  TODO: This can be generalized better so the switch statement doesn't mutate
            //  every time a non-tab-based menu item is added.
            
            //  Determine how to handle the selected menu item...
            //  The case statements handle menu items that DO NOT load an Orchestrator Tab
            //  The default case handles menu items that DO load an Orchestrator Tab
            switch (menuManager.GetMenuItemCodeFromMenuItemId(menuItemId))
            {
                case "Settings_User_Logout":
                    await UserLogoutHandlerAsync();
                    break;
                case "Settings_SwitchTheme_Light":
                    await ChangeThemeAsync(ThemeType.Light);
                    break;
                case "Settings_SwitchTheme_Dark":
                    await ChangeThemeAsync(ThemeType.Dark);
                    break;
                case "Settings_DoNothing":
                    await DoNothingMenuHandlerAsync();
                    break;
                default:
                    await MenuItemSelectedAsync(menuItemId);
                    break;
            }
        }

        /// <summary>
        /// Responds to a menu item selected event by loading/activating the tab associated with the 
        /// passed <see cref="OrchestratorMenuItem.ItemId"/>.
        /// </summary>
        /// <param name="menuItemId">
        /// String value containing the <see cref="OrchestratorMenuItem.ItemId"/> of the tab to load/activate.
        /// </param>
        /// <remarks>
        /// Invoked by the <see cref="MenuItemCallbackHandlerAsync(string)"/> method for any menu item
        /// that loads an Orchestrator Tab.
        /// </remarks>
        private async Task MenuItemSelectedAsync(string menuItemId)
        {
            //  Get the index of the menuItemId record in the list of Tabs managed by the tab manager
            var tabItemsIndex = tabmanager.GetTabItemIndexByMenuItemId(menuItemId);

            //  If the Tab exists in the list, activate it and bail
            if (tabItemsIndex is not -1)
            {
                await tabmanager.SelectTabAsync(tabItemsIndex);
                return;
            }

            // Determine the index in the list of Tabs managed by the tab manager where the new
            // tab will be inserted.
            var newTabItemsIndex = tabmanager.GetLastTabItemsIndex() + 1;

            //  Get the TabItem object associated with the menuItemId in OrchestratorTabs
            //  If the menu does not have an associated tab definition, display a message and bail
            var tabItem = GetTabDefinitionFromMenuItemId(menuItemId);
            if (tabItem is null)
            {
                await DisplayExecutionErrors("Tab Not Found", $"The definition for a Tab with Menu Item ID = { menuItemId } not found.");
                return;
            }

            // Have the Tab Manager add the Tab Item
            await tabmanager.AddTabAsync(tabItem, newTabItemsIndex);

            // Find the index for the new Tab and activate it.
            tabItemsIndex = tabmanager.GetTabItemIndexByMenuItemId(menuItemId);
            if (tabItemsIndex is not -1) await tabmanager.SelectTabAsync(tabItemsIndex);
        }

        /// <summary>
        /// Switches the current theme and causes the Orchestrator Manager to re-render.
        /// </summary>
        /// <param name="themeType">
        /// One of the <see cref="ThemeType"/> enums.
        /// </param>
        /// <returns></returns>
        /// <remarks>
        /// Invoked by the <see cref="MenuItemCallbackHandlerAsync(string)"/> method.
        /// </remarks>
        private async Task ChangeThemeAsync(ThemeType themeType)
        {
            themeManager.SetThemeType(themeType);
            await InvokeAsync(StateHasChanged);
        }

        /// <summary>
        /// Executes the necessary API call to logout the user.
        /// Note that the API will invalidate the user's refresh token but the
        /// authentication token will be valid until the authentication token expires.
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        /// The method is directly invoked by the Logout menu item.
        /// An API call is made that will invalidate the user's refresh token.
        /// The <see cref="UserAuthenticationStateChangedAsync"/> method is invoked to reconfigure 
        /// the application based on the user's permissions.
        /// </remarks>
        // ReSharper disable once UnusedParameter.Local
        private async Task UserLogoutHandlerAsync()
        {
            var result = await mediator.Send(new UserLogoutCommandRequest());
            if (result.HasErrors) await DisplayExecutionErrors("Logout Error", result.BuildStatusErrorMessage());

            //  At this point the user is no longer authenticated.
            await UserAuthenticationStateChangedAsync();
        }

        /// <summary>
        /// Configures and displays a Toast object to display an execution error message.
        /// </summary>
        /// <param name="title">
        /// String value containing the title displayed in the Toast.
        /// </param>
        /// <param name="errorMessage">
        /// String value containing the error message displayed in the Toast.
        /// </param>
        private async Task DisplayExecutionErrors(string title, string errorMessage)
        {
            //  Make sure all previous Toasts are closed
            await toastGenericError.HideAllAsync();
            
            toastGenericError.Title = title;
            toastGenericError.Content = errorMessage;
            await toastGenericError.ShowAsync();
        }

        /// <summary>
        /// Responsible for reconfiguring the application due to a change in the user's
        /// authentication state.
        /// </summary>
        /// <remarks>
        /// This should only be invoked by the <see cref="UserLoginComplete"/> or 
        /// <see cref="UserLogoutHandlerAsync"/> methods,
        /// When invoked by the <see cref="UserLoginComplete"/> method, the user will 
        /// be authenticated and the UserLogin Tab will be closed.
        /// When invoked by the <see cref="UserLogoutHandlerAsync"/> method, the 
        /// user will not be authenticated.
        /// </remarks>
        private async Task UserAuthenticationStateChangedAsync()
        {
            //  If the user is not authenticated...
            if (userCredentials.IsAuthenticated is false)
            {
                //  Close all active Tabs except for the Login Tab (if open)
                await tabmanager.CloseAllActiveTabs(new List<string>() { LoginTabItemId });

                //  If the UserLogin Tab is not active...
                if (tabmanager.GetTabItemIndexByMenuItemId(LoginTabItemId) is -1)
                {
                    //  Find the TabItem for the Tab associated with the LoginTabItemId.
                    //  If not found, display a message and bail
                    var tabItem = GetTabDefinitionFromMenuItemId(LoginTabItemId);
                    if (tabItem is null)
                    {
                        await DisplayExecutionErrors("Tab Not Found", $"The definition for a Tab with Menu Item ID = { LoginTabItemId } not found.");
                        return;
                    }

                    // Find the highest index in the list of Tabs managed by the tab manager
                    var newTabItemsIndex = tabmanager.GetLastTabItemsIndex() + 1;

                    // Have the Tab Manager add the Tab Item
                    await tabmanager.AddTabAsync(tabItem, newTabItemsIndex);
                }

                // Find the index for the Login Tab and activate it.
                var tabItemsIndex = tabmanager.GetTabItemIndexByMenuItemId(LoginTabItemId);
                if (tabItemsIndex is not -1) await tabmanager.SelectTabAsync(tabItemsIndex);
            }

            //  Reconfigure the application menus
            await menuManager.InitializeMenusAsync();

            //  Display of the header_1 and header_2 elements only if user is authenticated
            showPageHeader1 = showPageHeader2 = userCredentials.IsAuthenticated;
            await InvokeAsync(StateHasChanged);

            //  Update the Sidebar and have the Sidebar Button update its state.
            //  The Sidebar is closed if the user is NOT authenticated and open if it is.
            // TODO: Set the Sidebar state based on authenticated user preferences.
            await sidebar.SetSidebarToggleState(userCredentials.IsAuthenticated);
            await buttonSidebarToggle.UpdateSidebarButtonState();
        }

        /// <summary>
        /// Responsible for loading the Login tab in the UIOrchestratorTabManager.
        /// </summary>
        /// <returns></returns>
        private async Task LoadLoginTabAsync()
        {
            //  Find the index for the Tab associated with the LoginTabItemId.
            //  TabItem object for the Login tab
            //  If not found, display a message and bail
            var loginTabItemId = GetLoginTabItemId();
            var tabItem = GetTabDefinitionFromMenuItemId(loginTabItemId);
            if (tabItem is null)
            {
                await DisplayExecutionErrors("Tab Not Found", $"The definition for a Tab with Menu Item ID = { loginTabItemId } not found.");
                return;
            }

            // Find the highest index in the list of Tabs managed by the tab manager
            var newTabItemsIndex = tabmanager.GetLastTabItemsIndex() + 1;

            // Have the Tab Manager add the Tab Item
            await tabmanager.AddTabAsync(tabItem, newTabItemsIndex);
            await InvokeAsync(StateHasChanged);
        }

        private async Task DoNothingMenuHandlerAsync() => await Task.CompletedTask;

        #endregion

    }
}
