using Code420.UIOrchestrator.Server.Components.BaseComponents.TabBase;
using Microsoft.AspNetCore.Components;
using Syncfusion.Blazor;
using Syncfusion.Blazor.Navigations;
using Code420.UIOrchestrator.Server.Code.Models.Menus;
using Code420.UIOrchestrator.Core.Models.UIOrchestratorConstants;

namespace Code420.UIOrchestrator.Server.Components.UIOrchestratorComponents.UIOrchestratorTabManager.UIOrchestratorTabs.ConfigurationTab
{
    /// <summary>
    /// This is a proof of concept showing how to use the <see cref="TabBase"/>
    /// component as a child tab. There is a lot of crappy code here that
    /// needs to be cleaned up -- another day.
    /// </summary>
    /// <remarks>
    /// <para>
    /// The real point of this component show/test CSS isolation for a TabBase component
    /// that is a child of another TabBase component. Note the styling of this component
    /// is isolated from its parent.
    /// </para>
    /// <para>
    /// This component <b>must</b> exist in the namespace defined by
    /// <see cref="UIOrchestratorConstants.OrchestratorTabBaseNamespace"/> otherwise
    /// an exception will be thrown when the application constructs the <see cref="RenderFragment"/>.
    /// </para>
    /// <para>
    /// Though not demonstrated, the idea is this component's child tabs should be in the
    /// Code420.UIOrchestrator.Server.Components.UIOrchestratorComponents.UIOrchestratorTabManager.UIOrchestratorTabs.ConfigurationTab.ConfigurationChildTabs
    /// namespace. This component would add the child components to the <see cref="tabItem"/> property and load
    /// them all at once. If needed, child tabs can be disabled/hidden as needed by the workflow.
    /// Another approach is to have a "master" child tab that is responsible for loading/unloading
    /// additional child tabs as needed by the workflow.
    /// </para>
    /// </remarks>
    public partial class Configuration : ComponentBase
    {

        #region Component Parameters

        #region Base Parameters

        /// <summary>
        /// Contains the reference to the <see cref="UIOrchestrator"/> parent.
        /// <remarks>
        /// Note that this is a [CascadingParameter] passed down through the
        /// <see cref="UIOrchestratorTabManager"/>.
        /// </remarks>
        /// </summary>
        [CascadingParameter(Name = "OrchestratorRef")]
        public Pages.UIOrchestrator.UIOrchestrator OrchestratorRef { get; set; }


        /// <summary>
        /// String value containing the <see cref="OrchestratorMenuItem.ItemId"/> for this Tab.
        /// Used to identify the Tab when calling back to the <see cref="OrchestratorRef"/>.
        /// </summary>
        [Parameter]
        public string MenuItemId { get; set; }

        #endregion

        #endregion



        #region Callback Events Invoked from Underlying Components

        private void MyOnSelectedItemChangedHandler(int index)
        {
            // Update the active tab index tracker
            selectedItem = index;
        }

        #endregion


        #region Instance Variables

        private TabBase tabbase;
        private List<TabItem> tabItem;
        private int selectedItem;

        #endregion


        #region Constructors

        protected override void OnParametersSet()
        {
            //  Get the Tab Item associated with the passed InitialMenuItemId parameter
            var initialTabItem = OrchestratorRef.GetTabDefinitionFromMenuItemId("4000");

            //  If the element was not found initialize the tabIem as an empty list.
            //  If the element was found, build the tabItem list (used to initialize the TabBase component)
            tabItem = (initialTabItem is null) ?
                new List<TabItem>() :
                new List<TabItem>() { initialTabItem };
        }

        #endregion

        
        #region Public Methods Providing Access to the Underlying Components to the Consumer

        /// <summary>
        /// Adds a Tab into the list of active Tabs.
        /// </summary>
        /// <param name="addedTabItem">A <see cref="TabItem"/> object to add to the Tab.</param>
        /// <param name="indexTabItems">
        /// Integer value containing the index in the <see cref="TabBase.TabItems"/> list of active Tabs.
        /// </param>
        /// <remarks>
        /// The method adds a <see cref="TabItem"/> object to the list of active Tabs, <see cref="TabBase.TabItems"/>
        /// at the position specified by the indexTabItems parameter.
        /// </remarks>
        public async Task AddTabAsync(TabItem addedTabItem, int indexTabItems)
        {
            // Add the passed Tab Item(s) to the tabbase.TabItems
            await this.tabbase.AddTabAsync(new List<TabItem> { addedTabItem }, indexTabItems);
        }

        /// <summary>
        /// Disables/enables the TabBase component.
        /// </summary>
        /// <param name="disable">Boolean value specifying if the TabBase component is disabled (true) or enabled.</param>
        /// <remarks>
        /// Provides a method for enabling or disabling the entire <see cref="TabBase"/> component.
        /// Use the <see cref="EnableTabAsync(int, bool)"/> method to enable/disable a specific active Tab.
        /// </remarks>
        public async Task DisableAsync(bool disable) => await this.tabbase.DisableAsync(disable);

        /// <summary>
        /// Enables or disables a specific active Tab.
        /// </summary>
        /// <param name="indexTabItems">
        /// Integer value containing the index in the <see cref="TabBase.TabItems"/> list of active Tabs.
        /// </param>
        /// <param name="isEnable">Boolean value specifying is the tab is enabled (true) or disabled.</param>
        /// <returns></returns>
        /// <remarks>
        /// Provides a method for enabling or disabling one of the active Tabs.
        /// When the isEnabled parameter is false, the specified Tab remains visible but does not respond to user interactions.
        /// Use the <see cref="DisableAsync(bool)"/> method to enable/disable the entire <see cref="TabBase"/> component.
        /// </remarks>
        public async Task EnableTabAsync(int indexTabItems, bool isEnable = true) => await this.tabbase.EnableTabAsync(indexTabItems, isEnable);

        /// <summary>
        /// Returns the content element of the active Tab with the specified index.
        /// </summary>
        /// <param name="indexTabItems">
        /// Integer value containing the index in the <see cref="TabBase.TabItems"/> list of active Tabs.
        /// </param>
        /// <returns>A <see cref="DOM"/> object containing the contents of the tab.</returns>
        /// <remarks>
        /// The method is provided since it exposed by the <see cref="TabBase"/> component.
        /// Experimentation shows that the <see cref="DOM"/> object is not actually available for consumption.
        /// </remarks>
        public async Task<DOM> GetTabContentAsync(int indexTabItems)
        {
            return await this.tabbase.GetTabContentAsync(indexTabItems);
        }

        /// <summary>
        /// Returns the header element of the active Tab with the specified index.
        /// </summary>
        /// <param name="indexTabItems">
        /// Integer value containing the index in the <see cref="TabBase.TabItems"/> list of active Tabs.
        /// </param>
        /// <returns>A <see cref="DOM"/> object containing the header element of the tab.</returns>
        /// <remarks>
        /// The method is provided since it exposed by the <see cref="TabBase"/> component.
        /// Experimentation shows that the <see cref="DOM"/> object is not actually available for consumption.
        /// </remarks>
        public async Task<DOM> GetTabItemAsync(int indexTabItems)
        {
            return await this.tabbase.GetTabItemAsync(indexTabItems);
        }

        /// <summary>
        /// Show/hides an active Tab based on the specified index.
        /// </summary>
        /// <param name="indexTabItems">
        /// Integer value containing the index in the <see cref="TabBase.TabItems"/> list of active Tabs.
        /// </param>
        /// <param name="show">Boolean value specifying if the tab is shown (true) or hidden.</param>
        /// <returns></returns>
        /// <remarks>
        /// Provides a method for hiding/showing one of the active Tabs.
        /// When the show parameter is false, the specified Tab is hidden in the Tab Manager but remains in the DOM.
        /// </remarks>
        public async Task HideTabAsync(int indexTabItems, bool show) => await this.tabbase.HideTabAsync(indexTabItems, show);

        /// <summary>
        /// Refresh the entire <see cref="TabBase"/> component.
        /// </summary>
        /// <remarks>
        /// Provides a method for forcing the entire <see cref="TabBase"/> component to be re-rendered.
        /// </remarks>
        public async Task RefreshAsync() => await this.tabbase.RefreshAsync();

        /// <summary>
        /// Removes an active Tab from the <see cref="TabBase.TabItems"/> list.
        /// </summary>
        /// <param name="indexTabItems">
        /// Integer value containing the index in the <see cref="TabBase.TabItems"/> list of active Tabs.
        /// </param>
        /// <remarks>
        /// The method removes a <see cref="TabItem"/> object from the list of active Tabs, (<see cref="TabBase.TabItems"/>).
        /// </remarks>
        private async Task RemoveTabAsync(int indexTabItems)
        {
            // Just to be safe, bail if the TabItems list is empty.
            if ((tabbase.TabItems is null) || (tabbase.TabItems.Count == 0)) return;

            //  Close the tab
            await this.tabbase.RemoveTabAsync(indexTabItems);
        }

        /// <summary>
        /// Activates a Tab based on the specified index.
        /// </summary>
        /// <param name="indexTabItems">
        /// Integer value containing the index in the <see cref="TabBase.TabItems"/> list of active Tabs.
        /// </param>
        /// <remarks>
        /// The method will activate the Tab in the <see cref="TabBase.TabItems"/> list specified by 
        /// the indexTabItems parameter.
        /// </remarks>
        public async Task SelectAsync(int indexTabItems)
        {
            // Just to be safe, bail if the TabItems list is empty.
            if ((tabbase.TabItems is null) || (tabbase.TabItems.Count == 0)) return;

            // Change the active tab.
            selectedItem = indexTabItems;
            await tabbase.SelectAsync(indexTabItems);

            //  Need to tell Blazor something has changed or the Tab won't be activated.
            await InvokeAsync(StateHasChanged);
        }

        /// <summary>
        /// Gets the index of the last item in the <see cref="TabBase.TabItems"/> list.
        /// </summary>
        /// <returns>
        /// Integer value representing the highest index of the TabItems list, or -1 if there are no items.
        /// </returns>
        /// <remarks>
        /// The method finds the index of the last item in the <see cref="TabBase.TabItems"/> list.
        /// Useful when the caller want to add a new Tab to the end of the <see cref="TabBase.TabItems"/> list.
        /// </remarks>
        public int GetLastTabItemsIndex()
        {
            return (tabbase.TabItems is not null) ? tabbase.TabItems.Count - 1 : -1;
        }

        /// <summary>
        /// Get the index of the passed Orchestrator Tab in the <see cref="TabBase.TabItems"/> list.
        /// </summary>
        /// <param name="menuItemId">
        /// String value containing the <see cref="OrchestratorMenuItem.ItemId"/> of the Tab to find.
        /// </param>
        /// <returns>
        /// Integer value containing the index in the <see cref="TabBase.TabItems"/> list for the
        /// Tab whose <see cref="TabItem.CssClass"/> matches the menuItemId parameter.
        /// NOTE: The <see cref="TabItem.CssClass"/> parameter will match the <see cref="OrchestratorMenuItem.ItemId"/>
        /// so the CssClass value can be used to find the appropriate Tab element.
        /// </returns>
        private int GetTabItemIndexByMenuItemId(string menuItemId)
        {
            return tabbase.TabItems?.FindIndex(x => x.CssClass == menuItemId) ?? -1;
        }

        /// <summary>
        /// Closes all active tabs not in the exceptions list.
        /// Accepts an options list of <see cref="OrchestratorMenuItem.ItemId"/> values
        /// for Tabs that should not be closed.
        /// </summary>
        /// <param name="exceptions">
        /// List of <see cref="OrchestratorMenuItem.ItemId"/> values for the Tabs
        /// that should not be closed.
        /// </param>
        /// <returns></returns>
        public async Task CloseAllActiveTabs(List<string> exceptions = null)
        {
            //  Get the number of active Tabs
            //  Nothing to do if no open Tabs
            int countActiveTabs = tabbase.TabItems.Count;
            if (countActiveTabs == 0) return;

            int tabIndex;
            List<string> menuItemIdOfTabsToClose = new();

            //  Iterate through the active Tabs...
            for (tabIndex = 0; tabIndex < countActiveTabs; tabIndex++)
            {
                //  Look for the Menu Item Id in the passed exceptions list
                var tabIsInExceptions = (exceptions is not null) && exceptions.Contains(tabbase.TabItems[tabIndex].CssClass);

                //  If not found in the exceptions list, add it to the list of tabs to close
                if (tabIsInExceptions is false) menuItemIdOfTabsToClose.Add(tabbase.TabItems[tabIndex].CssClass);
            }

            //  Nothing to do is we didn't find any Tabs to close
            if (menuItemIdOfTabsToClose.Count == 0) return;

            //  Iterate through the list of Tabs to close...
            foreach (string menuItemId in menuItemIdOfTabsToClose)
            {
                //  Get the index for the TabItems list for the Tab
                tabIndex = GetTabItemIndexByMenuItemId(menuItemId);

                //  Remove the Tab
                if (tabIndex != -1) await RemoveTabAsync(tabIndex);
            }
        }

        #endregion
    }
}
