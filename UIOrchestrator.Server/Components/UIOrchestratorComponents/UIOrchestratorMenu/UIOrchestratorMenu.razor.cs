using Code420.UIOrchestrator.Server.Components.BaseComponents.MenuBase;
using Microsoft.AspNetCore.Components;
using Syncfusion.Blazor.Navigations;
using Code420.UIOrchestrator.Server.Code.Models.Menus;

namespace Code420.UIOrchestrator.Server.Components.UIOrchestratorComponents.UIOrchestratorMenu
{
    /// <summary>
    /// Provides the menu system used by the Orchestrator and containerizes the MenuBase component.
    /// Handles events (menu selections) by invoking the <see cref="OrchestratorMenuItem.MenuItemCallback"/>
    /// associated with the menu item.
    /// The menu is tied to the OrchestratorSidebar in that its SidebarCssClass is used to
    /// generate its masterCssSelector so that the menu popups are adjusted based on the
    /// state (opened/closed) of the Sidebar.
    /// </summary>
    /// <remarks>
    /// <para>
    /// The component exposes four base parameters:<br /> 
    /// The <see cref="UIOrchestrator"/> is directly passed from the parent component.
    /// The parameter is not currently used.<br />
    /// The <see cref="DockedIconFontSize"/> is directly passed to the component and sets the
    /// font size for the menu icons.<br />
    /// The <see cref="DockedMenuWidth"/> is directly passed to the component and sets the
    /// width of the menu when the Sidebar is in the closed state.<br />
    /// The <see cref="SidebarCssClass"/> is directly passed to the component and uis used
    /// to build the master CSS selector to address the menu for styling.
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
    public partial class UIOrchestratorMenu : ComponentBase
    {

        #region Component Parameters

        #region Base Parameters

        /// <summary>
        /// Contains the reference to the <see cref="UIOrchestrator"/> parent.
        /// Used to access methods provided by the parent.
        /// </summary>
        [Parameter]
        [EditorRequired]
        public Pages.UIOrchestrator.UIOrchestrator OrchestratorRef { get; set; }

        /// <summary>
        /// String value that specifies the CSS <a href="https://developer.mozilla.org/en-US/docs/Web/CSS/font-size">font-size</a>
        /// value used for the menu icons when the Sidebar is in the docked (closed) state.
        /// The font-size CSS property sets the size of the font.
        /// Default value is 28px.
        /// </summary>
        [Parameter]
        public string DockedIconFontSize { get; set; } = "28px";

        /// <summary>
        /// String value that specifies the CSS <a href="https://developer.mozilla.org/en-US/docs/Web/CSS/width">width</a>
        /// value used for the menu when the Sidebar is in the docked (closed) state.
        /// The width CSS property sets an element's width.
        /// This parameter is needed so that the popup sub-menus are displayed next to their menu item.
        /// Default value is 50px.
        /// </summary>
        [Parameter]
        public string DockedMenuWidth { get; set; } = "50px";

        /// <summary>
        /// String value containing CSS class of the Sidebar container.
        /// Used to build the master CSS selector needed to handle menu customizations.
        /// Default value is string.Empty.
        /// </summary>
        [Parameter]
        public string SidebarCssClass { get; set; } = string.Empty;

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

        private const string menuCssClass = "page__main-sidebar-menu";
        private MenuBase<OrchestratorMenuItem> menubase;
        private string masterCssSelector;

        #endregion


        #region Constructors

        // This method will be executed immediately after OnInitializedAsync if this is a new
        //  instance of a component. If it is an existing component that is being re-rendered because
        //  its parent is re-rendering then the OnInitialized* methods will not be executed, and this
        //  method will be executed immediately after SetParametersAsync instead
        protected override void OnParametersSet()
        {
            masterCssSelector = (SidebarCssClass == string.Empty) ?
                $".e-close .{ menuCssClass }.e-menu-container" : 
                $".{ SidebarCssClass }.e-close .{ menuCssClass }.e-menu-container";
        }

        #endregion
    }

}
