using Code420.StatusGeneric;
using Syncfusion.Blazor.Navigations;

namespace Code420.UIOrchestrator.Server.Code.Models.Menus
{
    internal interface IMenuManager
    {
        List<OrchestratorMenuItem> SidebarMenu { get; }
        List<OrchestratorMenuItem> HorizontalMenu { get; }
        FavoritesMenuDefinition FavoritesMenu { get; }
        IStatusGeneric Status { get; }
        string LoginTabItemId { get; }
        public bool MenuSystemInitialized { get; }

        Task InitializeMenusAsync(bool forceReload = false);
        void RegisterMenuItemCallbackHandler(Action<object> callbackHandler);
        string GetMenuItemTextFromMenuItemId(string menuItemId);
        string GetMenuItemCodeFromMenuItemId(string menuItemId);
        string GetMenuItemIdFromMenuItemCode(string menuItemCode);
        TabItem GetTabDefinitionFromMenuItemId(string menuItemId);
    }
}