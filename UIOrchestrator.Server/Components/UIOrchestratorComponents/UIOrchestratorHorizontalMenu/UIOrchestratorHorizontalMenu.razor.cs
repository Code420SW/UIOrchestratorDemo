using Microsoft.AspNetCore.Components;
using Syncfusion.Blazor.Navigations;
using Code420.UIOrchestrator.Server.Code.Models.Menus;
using Code420.UIOrchestrator.Server.Components.BaseComponents.MenuBase;

namespace Code420.UIOrchestrator.Server.Components.UIOrchestratorComponents.UIOrchestratorHorizontalMenu
{
    /// <summary>
    /// Provides the menu system used by the Orchestrator and containerizes the MenuBase component.
    /// Handles events (menu selections) by invoking the <see cref="OrchestratorMenuItem.MenuItemCallback"/>
    /// associated with the menu item.
    /// The menu is injected in the <see cref="UIOrchestrator"/> page__header-2 div.
    /// </summary>
    /// <remarks>
    /// <para>
    /// The component exposes one base parameter. 
    /// The <see cref="UIOrchestrator"/> is directly passed from the parent component.
    /// The parameter is not currently used.
    /// </para>
    /// <para>
    /// The component does not expose any event callback or CSS parameters.
    /// </para>
    /// <para>
    /// The component handles one event callback from the <see cref="MenuBase{TValue}"/> component.
    /// The <see cref="MenuBase{TValue}.ItemSelected"/> event is the only one handled and, if defined,
    /// invokes the method defined in <see cref="OrchestratorMenuItem.MenuItemCallback"/> method for 
    /// the selected menu item and passed the associated <see cref="OrchestratorMenuItem.ItemId"/>.
    /// </para>
    /// <para>
    /// The component does not expose any public methods.
    /// </para>
    /// </remarks>

    public partial class UIOrchestratorHorizontalMenu : ComponentBase
    {

        #region Component Parameters

        #region Base Parameters

        /// <summary>
        /// Contains the reference to the <see cref="UIOrchestrator"/> parent.
        /// Used to access methods provided by the parent.
        /// <remarks>
        /// Not currently used. Reserved for future use.
        /// </remarks>
        /// </summary>
        [Parameter]
        [EditorRequired]
        public UIOrchestrator OrchestratorRef { get; set; }
        
        /// <summary>
        /// String value containing the CSS class of the HTML div containing the
        /// component.
        /// <remarks>
        /// The parent will inject this component in a div. The CSS class of the
        /// div will be passed to child components.
        /// </remarks>
        /// </summary>
        [Parameter]
        [EditorRequired]
        public string TargetCssClass { get; set; }

        #endregion

        #endregion



        #region Callback Events Invoked from Underlying Components

        /// <summary>
        /// Responsible for invoking the callback handler for the selected menu item
        /// if defined. The <see cref="OrchestratorMenuItem.ItemId"/> is
        /// passed to the handler to identify which menu item is selected.
        /// <remarks>
        /// Every <see cref="OrchestratorMenuItem"/> has a callback handler but we are
        /// only interested in the menu items that do not have submenus (menu items with
        /// submenus are parent menu items).
        /// </remarks>
        /// </summary>
        /// <param name="args">
        /// A <see cref="MenuEventArgs{T}"/> object. The <see cref="MenuEventArgs{T}.Item"/>
        /// property contains the <see cref="OrchestratorMenuItem"/> for the selected menu item.
        /// </param>
        private void ItemSelectedHandler(MenuEventArgs<OrchestratorMenuItem> args)
        {
            if (args.Item.SubMenu is null)  
                args.Item.MenuItemCallback.Invoke(args.Item.ItemId);
        } 

        #endregion



        #region Instance Variables

        private const string menuCssClass = "page__main-horizontal-menu";
        private MenuBase<OrchestratorMenuItem> menubase;

        #endregion

    }
}
