using Code420.UIOrchestrator.Server.Code.Enums;

namespace Code420.UIOrchestrator.Server.Code.Models.Theme
{
    public interface IThemeManager
    {
        void SetThemeType(ThemeType themeType);

        string GetThemeItem(string key);
    }
}