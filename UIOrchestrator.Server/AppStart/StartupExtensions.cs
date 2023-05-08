using Code420.UIOrchestrator.Core.Models.UserCredentials;
using Code420.UIOrchestrator.Server.Code.Models.CssUtilities;
using Code420.UIOrchestrator.Server.Code.Models.Menus;
using Code420.UIOrchestrator.Server.Code.Models.Theme;

namespace Code420.UIOrchestrator.Server.AppStart
{
    public static class StartupExtensions
    {
        public static IServiceCollection RegisterCanxtracServerClasses(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IUserCredentials, UserCredentials>();
            services.AddTransient<ICssUtilities, CssUtilities>();
            services.AddScoped<IThemeManager, ThemeManager>();
            services.AddScoped<IMenuManager, MenuManager>();
            return services;
        }
    }
}