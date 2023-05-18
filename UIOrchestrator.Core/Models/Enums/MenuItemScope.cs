using Code420.UIOrchestrator.Core.Models.Menus;

namespace Code420.UIOrchestrator.Core.Models.Enums
{
    /// <summary>
    /// Defines the scope of a menu item.
    /// Used when constructing the menu hierarchy from the raw menu definitions
    /// which are constructed from the <see cref="MenuItemDefinitionDto.MenuItemScope"/>
    /// property.
    /// </summary>
    public enum MenuItemScope
    {
        NoMenus = 0,
        AllMenus = 1,
        SidebarMenu = 2,
        HorizontalMenu = 3
    }
}
