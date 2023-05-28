using Code420.UIOrchestrator.Core.Models.Enums;
using Syncfusion.Blazor.Navigations;

namespace Code420.UIOrchestrator.Server.Code.Models.Menus
{
    public interface IOrchestratorMenuItem
    {
        Action<object> MenuItemCallback { get; set; }
        
        List<string> RequiredPermissions { get; set; }
        
        public MenuItemScope MenuItemScope { get; set; }
        
        string MenuText { get; set; }
        
        bool IsDisabled { get; set; }
        
        bool IsHidden { get; set; }
        
        Dictionary<string, object> HtmlAttributes { get; set; }
        
        string IconCss { get; set; }
        
        string ItemId { get; set; }
        
        string ParentId { get; set; }
        
        bool IsSeparator { get; set; }
        
        List<OrchestratorMenuItem> SubMenu { get; set; }
        
        string Url { get; set; }
        
        TabItem TabDefinition { get; set; }
    }
}