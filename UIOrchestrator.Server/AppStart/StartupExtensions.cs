using Code420.UIOrchestrator.Core.Models.UserCredentials;
using Code420.UIOrchestrator.Server.Code.Models.CssUtilities;
using Code420.UIOrchestrator.Server.Code.Models.Menus;
using Code420.UIOrchestrator.Server.Code.Models.Theme;

namespace Code420.UIOrchestrator.Server.AppStart
{
    /// <summary>
    /// Responsible for registering all non third-party UIOrchestrator
    /// dependencies in DI
    /// </summary>
    public static class StartupExtensions
    {
        /// <summary>
        /// Extension method to the <see cref="IServiceCollection"/> for registering
        /// dependencies.
        /// </summary>
        /// <param name="services">
        /// Reference to the <see cref="IServiceCollection"/> object.
        /// </param>
        /// <param name="configuration">
        /// Reference to the <see cref="IConfiguration"/> object.
        /// </param>
        /// <returns>
        /// The <see cref="IServiceCollection"/> object.
        /// </returns>
        public static IServiceCollection RegisterUIOrchestratorClasses(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IUserCredentials, UserCredentials>();
            services.AddTransient<ICssUtilities, CssUtilities>();
            services.AddScoped<IThemeManager, ThemeManager>();
            services.AddScoped<IMenuManager, MenuManager>();
            return services;
        }
    }
}