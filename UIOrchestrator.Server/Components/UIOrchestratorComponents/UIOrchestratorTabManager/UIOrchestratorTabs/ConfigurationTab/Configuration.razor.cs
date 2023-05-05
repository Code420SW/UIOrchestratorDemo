﻿using Code420.CanXtracServer.Pages.UIOrchestrator;
using Microsoft.AspNetCore.Components;
using Syncfusion.Blazor.Navigations;
using System.Collections.Generic;
using System.Threading.Tasks;
using Syncfusion.Blazor;
using Code420.CanXtracServer.Code.Models.Menus;
using Code420.UIOrchestrator.Server.Components.BaseComponents.TabBase;

namespace Code420.CanXtracServer.Components.UIOrchestratorComponents.UIOrchestratorTabManager.UIOrchestratorTabs.ConfigurationTab
{
    /// <summary>
    /// Responsible for managing all Orchestrator Tab Items.
    /// </summary>
    /// <remarks>
    /// <para>
    /// 1.  X
    /// </para>
    /// <para>
    /// 2.  X
    /// </para>
    /// <para>
    /// 3.  X
    /// </para>
    /// <para>
    /// 4.  X
    /// </para>
    /// <para>
    /// 5.  X
    /// </para>
    /// <para>
    /// 6.  X
    /// </para>
    /// </remarks>
    public partial class Configuration : ComponentBase
    {

        #region Component Parameters

        #region Base Parameters

        // ==================================================
        // Base Parameters
        // ==================================================

        /// <summary>
        /// Contains the reference to the <see cref="UIOrchestrator"/> parent.
        /// </summary>
        [CascadingParameter(Name = "OrchestratorRef")]
        public UIOrchestrator OrchestratorRef { get; set; }


        /// <summary>
        /// String value containing the <see cref="OrchestratorMenuItem.ItemId"/> for this Tab.
        /// Used to identify the Tab when calling back to the <see cref="OrchestratorRef"/>.
        /// </summary>
        [Parameter]
        public string MenuItemId { get; set; }

        #endregion


        #region Event Callback Parameters

        // ==================================================
        // Event Callback Parameters
        // ==================================================


        #endregion


        #region CSS Parameters

        // ==================================================
        // CSS Styling Parameters
        // ==================================================


        #endregion

        #endregion



        #region Callback Events Invoked from Underlying Components

        // ==================================================
        // Methods used as Callback Events from the underlying component(s)
        // ==================================================

        //private void MyAddedHandler(AddEventArgs args)
        //{
        //    //Debug.WriteLine("MyAddedHandler method invoked.");
        //}

        //private void MyAddingHandler(AddEventArgs args)
        //{
        //    //Debug.WriteLine("MyAddingHandler method invoked.");
        //}

        //private void MyCreatedHandler(object args)
        //{
        //    //Debug.WriteLine("MyCreatedHandler method invoked.");
        //}

        //private void MyDestroyedHandler(object args)
        //{
        //    //Debug.WriteLine("MyDestroyedHandler method invoked.");
        //}

        //private void MyDraggedHandler(DragEventArgs args)
        //{
        //}

        //private void MyOnDragStartHandler(DragEventArgs args)
        //{
        //}

        //private void MyOnRemovedHandler(RemoveEventArgs args)
        //{
        //}

        //private void MyOnRemovingHandler(RemoveEventArgs args)
        //{
        //}

        //private void MyOnSelectedHandler(SelectEventArgs args)
        //{
        //}

        //private void MyOnSelectingHandler(SelectingEventArgs args)
        //{
        //}

        private void MyOnSelectedItemChangedHandler(int index)
        {
            // Update the active tab index tracker
            selectedItem = index;
        }



        #endregion



        #region Instance Variables

        // ==================================================
        // Instance variables
        // ==================================================

        private TabBase tabbase;                                // Reference to the TabBase component
        private List<TabItem> tabItem;                          // List of Orchestrator Tabs initially loaded by the TabBase component
        private int selectedItem;                           // Tracks the index (in tabbase.TabItems) of the active tab
        //private int initialMenuItemOrchestratorTabsIndex = -1;  // Contains index on OrchestratorTabs list for the InitialMenuItemId Orchestrator Tab

        #endregion



        #region Injected Dependencies

        // Injected Dependencies



        // Dependency Injection


        #endregion



        #region Constructors


        // ==================================================
        // Constructors
        // ==================================================

        // This method is executed whenever the parent renders.
        // Parameters that were passed into the component are contained in a ParameterView.
        //  This is a good point at which to make asynchronous calls to a server (for example)
        //  based on the state passed into the component.
        // The component’s [Parameter] properties are assigned their values when you call
        //  base.SetParametersAsync(parameters) inside your override.
        // It is also the correct place to assign default parameter values.
        public override async Task SetParametersAsync(ParameterView parameters)
        {
            await base.SetParametersAsync(parameters);
        }

        // Once the state from the ParameterCollection has been assigned to the component’s
        //  [Parameter] properties, these methods are executed. This is useful in the same way
        //  as SetParametersAsync, except it is possible to use the component’s state.
        // This method is only executed once when the component is first created.If the parent
        //  changes the component’s parameters at a later time, this method is skipped.
        // When the component is a @page, and our Blazor app navigates to a new URL that renders
        //  the same page, Blazor will reuse the current object instance for that page.Because the
        //  object is the same instance, Blazor does not call IDisposable.Dispose on the object,
        //  nor does it execute its OnInitialized method again.
        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();

            //  Get the Tab Item associated with the passed InitialMenuItemId parameter
            var initialTabItem = OrchestratorRef.GetTabDefinitionFromMenuItemId("4000");

            //  If the element was not found initialize the tabIem as an empty list.
            //  If the element was found, build the tabItem list (used to initialize the TabBase component)
            tabItem = (initialTabItem is null) ?
                new List<TabItem>() :
                new List<TabItem>() { initialTabItem };
        }

        // This method will be executed immediately after OnInitializedAsync if this is a new
        //  instance of a component. If it is an existing component that is being re-rendered because
        //  its parent is re-rendering then the OnInitialized* methods will not be executed, and this
        //  method will be executed immediately after SetParametersAsync instead
        protected override async Task OnParametersSetAsync()
        {
            await base.OnParametersSetAsync();
        }

        // This is the first place that the State should be changed
        //
        // This method is executed every time Blazor has re-generated the component’s RenderTree.
        //  This can be as a result of the component’s parent re-rendering, the user interacting with the component
        //  (e.g. a mouse-click), or if the component executes its StateHasChanged method to invoke a re-render.
        // This method has a single parameter named firstRender. This parameter is true only the first time the
        //  method is called on the current component, from there onwards it will always be false. In cases where
        //  additional component hook-up is required (for example, via JavaScript) it is useful to know this is the
        //  first render.
        // It is not until after the OnAfterRender method have executed that it is safe to use any references to
        //  components set via the @ref directive. And it is not until after the OnAfterRender method have been
        //  executed with firstRender set to true that it is safe to use any references to HTML elements set via
        //  the @ref directive
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await base.OnAfterRenderAsync(firstRender);
        }

        #endregion



        #region Public Methods Providing Access to the Underlying Components to the Consumer

        // ==================================================
        // Public Methods providing access to the underlying component to the consumer
        // ==================================================

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



        #region Private Methods for Internal Use Only


        #endregion

    }
}
