using Code420.CanXtracServer.Pages.UIOrchestrator;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;
using Code420.CanXtracServer.Code.Models.Menus;

namespace Code420.CanXtracServer.Components.UIOrchestratorComponents.UIOrchestratorTabManager.UIOrchestratorTabs.FavoritesMenuTab
{
    /// <summary>
    /// Renders a rotating menu containing "favorites" menu items in icon for.
    /// <remarks>
    /// <para>
    /// Other than the <see cref="OrchestratorRef"/>parameter, a reference to the <see cref="UIOrchestrator"/> passed though
    /// a <see cref="CascadingParameterAttribute"/>, the component has no parameters.
    /// </para>
    /// <para>
    /// The component consumes the <see cref="UIOrchestrator.FavoritesMenu"/> object which contains the menu
    /// items displayed by the component as well as the parameters for each item.
    /// </para>
    /// </remarks>
    /// </summary>
    public partial class FavoritesMenu : ComponentBase
    {

        #region Component Parameters

        #region Base Parameters

        // ==================================================
        // Base Parameters
        // ==================================================

        /// <summary>
        /// Contains the reference to the <see cref="UIOrchestrator"/> parent.
        /// Used to subscribe to event handlers provided by the parent.
        /// </summary>
        [CascadingParameter(Name = "OrchestratorRef")]
        public UIOrchestrator OrchestratorRef { get; set; }

        /// <summary>
        /// String value containing the <see cref="OrchestratorMenuItem.ItemId"/> for this Tab.
        /// Used to identify the Tab when calling back to the <see cref="OrchestratorRef"/>.
        /// </summary>
        [Parameter]
        public string MenuItemId { get; set; }

        #endregion

        #endregion


        #region Callback Events Invoked from Underlying Components

        // ==================================================
        // Methods used as Callback Events from the underlying component(s)
        // ==================================================

        private async void MyButtonClickHandler(int id)
        {
            // Invoke the callback for the menu item
            if (OrchestratorRef.FavoritesMenu.FavoritesMenuItems[id].MenuItemCallback is not null)
            {
                OrchestratorRef.FavoritesMenu.FavoritesMenuItems[id].MenuItemCallback(OrchestratorRef.FavoritesMenu.FavoritesMenuItems[id].ItemId);
            }

            await Task.CompletedTask;
        }

        #endregion
    }
}
