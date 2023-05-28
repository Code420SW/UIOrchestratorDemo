using Code420.UIOrchestrator.Server.Components.BaseComponents.IconButtonBase;
using Microsoft.AspNetCore.Components;

namespace Code420.UIOrchestrator.Server.Components.UIOrchestratorComponents.UIOrchestratorSidebarButton
{
    /// <summary>
    /// Provides the mechanism to change the state (opened/closed) of the <see cref="UIOrchestratorSidebar.UIOrchestratorSidebar"/>.
    /// Will handle events (button clicks) and make appropriate calls through the <see cref="UIOrchestrator"/> to 
    /// coordinate events.
    /// All queries regarding the state/status of the Sidebar are made through <see cref="UIOrchestrator"/> methods.
    /// </summary>
    /// <remarks>
    /// <para>
    /// The component exposes a base parameter for the <see cref="UIOrchestrator"/> which is
    /// passed down from the parent component. Provides ability to callback to the parent
    /// component to query the state of the <see cref="UIOrchestratorSidebar.UIOrchestratorSidebar"/> component
    /// and report changes in state resulting from a button click event.
    /// </para>
    /// <para>
    /// The component does not expose any event callback or CSS parameters.
    /// </para>
    /// <para>
    /// When the button is clicked, the component will trigger a state change for the
    /// <see cref="UIOrchestratorSidebar.UIOrchestratorSidebar"/> component. The button's icon is 
    /// updated based on the current state of the sidebar.
    /// </para>
    /// <para>
    /// The icons used by the button are hard-coded.
    /// </para>
    /// <para>
    /// The component exposes one public method.
    /// <see cref="UpdateSidebarButtonState"/> provides a method for causing the component to update 
    /// the button's icon due to an (external) change in the sidebar state.
    /// </para>
    /// </remarks>

    public partial class UIOrchestratorSidebarButton : ComponentBase
    {

        #region Component Parameters

        #region Base Parameters

        // ==================================================
        // Base Parameters
        // ==================================================

        /// <summary>
        /// Contains the reference to the <see cref="UIOrchestrator"/> parent.
        /// Used to access methods provided by the parent.
        /// </summary>
        [Parameter]
        [EditorRequired]
        public Pages.UIOrchestrator.UIOrchestrator OrchestratorRef { get; set; }

        #endregion

        #endregion


        #region Callback Events Invoked from Underlying Components

        /// <summary>
        /// Responds to a button click event and reports the event to the
        /// <see cref="UIOrchestrator"/> component.
        /// </summary>
        /// <remarks>
        /// In response to a button click, the component will callback to the
        /// <see cref="UIOrchestrator"/> (parent) component to trigger a change
        /// in state of the <see cref="UIOrchestratorSidebar.UIOrchestratorSidebar"/>.
        /// The component will then change the button's icon to reflect the action
        /// performed when the button is next clicked based on the current state
        /// of the <see cref="UIOrchestratorSidebar.UIOrchestratorSidebar"/> 
        /// (e.g., close icon if the sidebar is open).
        /// </remarks>
        private async void  ButtonClickHandler()
        {
            await OrchestratorRef.ToggleSidebarAsync();

            currentIconCss = (OrchestratorRef.IsSidebarOpen()) ? iconCssClose : iconCssOpen;
            await InvokeAsync(StateHasChanged);
        }

        #endregion


        #region Instance Variables

        private const string iconCssOpen = "oi oi-expand-left";
        private const string iconCssClose = "oi oi-expand-right";

        private IconButtonBase iconButton;
        private string currentIconCss;

        #endregion


        #region Constructors

        protected override void OnParametersSet()
        {
            // Set the button's icon
            currentIconCss = (OrchestratorRef.IsSidebarOpen()) ? iconCssClose : iconCssOpen;
        }

        #endregion


        #region Public Methods Providing Access to the Underlying Components to the Consumer

        /// <summary>
        /// Method that causes the component to update the button icon due to a (external) change 
        /// in the sidebar state.
        /// </summary>
        public async Task  UpdateSidebarButtonState()
        {
            currentIconCss = (OrchestratorRef.IsSidebarOpen()) ? iconCssClose : iconCssOpen;
            await InvokeAsync(StateHasChanged);
        }

        #endregion

    }
}
