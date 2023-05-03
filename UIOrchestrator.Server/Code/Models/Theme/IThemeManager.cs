using Code420.UIOrchestrator.Server.Code.Enums;

namespace Code420.UIOrchestrator.Server.Code.Models.Theme
{
    public interface IThemeManager
    {
        Dictionary<string, string> ActiveTheme { get; set; }

        void SetThemeType(ThemeType themeType);

        string GetThemeItem(string key);
    }
}