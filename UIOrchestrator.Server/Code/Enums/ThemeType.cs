using Code420.UIOrchestrator.Server.Code.Models.Theme;

namespace Code420.UIOrchestrator.Server.Code.Enums
{
    /// <summary>
    /// Defines a set of standard themes used with the <see cref="ThemeManager"/>
    /// class to set the <see cref="ThemeManager.ActiveTheme"/> property.
    /// </summary>
    public enum ThemeType
    {
        NoTheme = 0,
        Light = 1,
        Dark = 2
    }
}
