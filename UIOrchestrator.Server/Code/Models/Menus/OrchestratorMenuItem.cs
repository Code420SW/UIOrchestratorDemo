using Code420.UIOrchestrator.Core.Models.Enums;
using Code420.UIOrchestrator.Core.Models.Menus;
using Syncfusion.Blazor.Navigations;

namespace Code420.UIOrchestrator.Server.Code.Models.Menus
{
    /// <summary>
    /// Defines a menu item available for use in any of the menus.
    /// <remarks>
    /// <para>
    /// Note that this is the template for a menu item which is built
    /// from <see cref="MenuItemDefinitionDto"/> objects by the
    /// <see cref="MenuManager.BuildRawMenu"/> method.
    /// </para>
    /// <para>
    /// The applicable elements are copied to each type of menu (based
    /// on the <see cref="MenuItemScope"/> property) by the
    /// <see cref="MenuManager.BuildMenuHierarchy"/> method.
    /// </para>
    /// </remarks>
    /// </summary>
    public class OrchestratorMenuItem : IOrchestratorMenuItem
    {
        /// <summary>
        /// An <see cref="Action{T}"/> accepting an <see cref="object"/> parameter.
        /// The delegate will be invoked when the menu item is selected.
        /// Default value is null.
        /// </summary>
        public Action<object> MenuItemCallback { get; set; }

        /// <summary>
        /// A <see cref="List{T}"/> of string objects containing the human-readable
        /// permissions required for the menu item. The user authorization must contain
        /// one of these permissions in order for the menu item to be available.
        /// Default value is null.
        /// <remarks>
        /// This list is derived from <see cref="MenuItemDefinitionDto.MenuItemPackedPermissions"/>
        /// and would typically be populated by the service responsible for retrieving the
        /// menu definitions from a repository. 
        /// method.
        /// </remarks>
        /// </summary>
        public List<string> RequiredPermissions { get; set; }

        /// <summary>
        /// One of the <see cref="MenuItemScope"/> enum members specifying
        /// which menu(s) in which the menu item is available.
        /// Default value is <see cref="MenuItemScope.AllMenus"/>.
        /// </summary>
        public MenuItemScope MenuItemScope { get; set; } = MenuItemScope.AllMenus;

        /// <summary>
        /// String value containing the test displayed in the menu item.
        /// Default value is null.
        /// </summary>
        public string MenuText { get; set; }

        /// <summary>
        /// Boolean value indicating if the menu item is disabled.
        /// Default value is false.
        /// </summary>
        public bool IsDisabled { get; set; }

        /// <summary>
        /// Boolean value indicating if the menu item is hidden in a menu.
        /// Default value is false.
        /// <remarks>
        /// In reality, hidden menu items are never populated in a menu. The
        /// property is used to security-trim the menu items by the
        /// <see cref="MenuManager.ApplyPermissionsToRawMenu"/> method and
        /// then filtered out by the <see cref="MenuManager.BuildMenuHierarchy"/> method.
        /// </remarks>
        /// </summary>
        public bool IsHidden { get; set; }

        /// <summary>
        /// A <see cref="Dictionary{TKey,TValue}"/> where TKey is a string and TValue is
        /// an object that contains CSS classes/attributes injected in the Menu Item's DOM
        /// element.
        /// Default value is null.
        /// <remarks>
        /// <para>
        /// This property is not currently used since there is no analog in
        /// <see cref="MenuItemDefinitionDto"/> from which this object is constructed.
        /// </para>
        /// <para>
        /// The property is present for future use.
        /// </para>
        /// </remarks>
        /// </summary>
        public Dictionary<string, object> HtmlAttributes { get; set; }

        /// <summary>
        /// String value containing CSS icon definition. The icon is
        /// displayed to the left of the menu item text.
        /// Default value is null
        /// </summary>
        public string IconCss { get; set; }

        /// <summary>
        /// String value containing the unique identifier for this menu item.
        /// Default value is null.
        /// </summary>
        public string ItemId { get; set; }

        /// <summary>
        /// String value containing the unique identifier for this menu item's parent.
        /// Default value is null.
        /// <remarks>
        /// The <see cref="MenuManager.BuildRawMenu"/> method build a flat (single-level)
        /// <see cref="List{T}"/> of <see cref="OrchestratorMenuItem"/> objects.
        /// The <see cref="MenuManager.BuildMenuHierarchy"/> method converts the flat menu
        /// system into the hierarchical menu structure. The hierarchy is based on
        /// relationship between a menu item's ItemId and ParentId.
        /// </remarks>
        /// </summary>
        public string ParentId { get; set; }

        /// <summary>
        /// Boolean value indicating if the menu item is rendered as a separator
        /// in a menu.
        /// Default value is false.
        /// </summary>
        public bool IsSeparator { get; set; }

        /// <summary>
        /// A <see cref="List{T}"/> of <see cref="OrchestratorMenuItem"/> objects
        /// representing the child menu items associated with this menu item.
        /// Default value is null.
        /// <remarks>
        /// </remarks>
        /// This property is built by the <see cref="MenuManager.BuildMenuHierarchy"/> method.
        /// </summary>
        public List<OrchestratorMenuItem> SubMenu { get; set; }

        /// <summary>
        /// String value containing the URL associated with the menu item.
        /// Default value is null.
        /// <remarks>
        /// At this point, none of the menu systems use this property.
        /// </remarks>
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// A <see cref="TabItem"/> object representing the tab that will be opened when the
        /// menu item is selected.
        /// Default value is null.
        /// <remarks>
        /// This property is populated by the <see cref="MenuManager.BuildRawMenu"/> method
        /// when a <see cref="MenuItemDefinitionDto.MenuItemComponentName"/> property is not
        /// null. 
        /// </remarks>
        /// </summary>
        public TabItem TabDefinition { get; set; }

    }
}
