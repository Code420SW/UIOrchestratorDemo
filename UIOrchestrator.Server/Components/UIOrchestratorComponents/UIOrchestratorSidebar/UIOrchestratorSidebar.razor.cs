using Code420.UIOrchestrator.Server.Components.BaseComponents.SidebarBase;
using Microsoft.AspNetCore.Components;

namespace Code420.UIOrchestrator.Server.Components.UIOrchestratorComponents.UIOrchestratorSidebar
{
    /// <summary>
    /// Provides a container for the OrchestratorMenu component.
    /// Responds to events from the OrchestratorSidebarButton (via the <see cref="UIOrchestrator"/>)
    /// to change the state (opened/closed) of the Sidebar.
    /// </summary>
    /// <remarks>
    /// <para>
    /// The component exposes two base parameters. 
    /// The <see cref="UIOrchestrator"/> is directly passed from the parent component.
    /// The parameter is not used by this component but is passed directly to its child component(s).
    /// The <see cref="InitialSidebarIsOpen"/> is directly passed to the component and indicates
    /// if the sidebar should be rendered in it opened/closed state when loaded.
    /// </para>
    /// <para>
    /// The component does not expose any event callback or CSS parameters.
    /// </para>
    /// <para>
    /// The component handles none of the event callbacks from the <see cref="SidebarBase"/> component.
    /// </para>
    /// <para>
    /// The icons used by the button are hard-coded.
    /// </para>
    /// <para>
    /// The <see cref="UIOrchestratorMenu.UIOrchestratorMenu"/> component is injected as a child of this
    /// component and passed the <see cref="OrchestratorRef"/> parameter. This component does
    /// interact with the menu component.
    /// </para>
    /// <para>
    /// The component exposes three public methods:<br />
    /// <see cref="ToggleSidebarAsync"/> provides a method for the parent component to open/close
    /// the sidebar.<br />
    /// <see cref="GetSidebarState"/> provides a method to report the current state of the sidebar.<br />
    /// <see cref="SetSidebarToggleState"/> provides a method to explicitly set the state of the sidebar
    /// (as opposed to simply toggling the state).
    /// </para>
    /// </remarks>
    public partial class UIOrchestratorSidebar : ComponentBase
    {

        #region Component Parameters

        #region Base Parameters

        /// <summary>
        /// Contains the reference to the <see cref="UIOrchestrator"/> parent.
        /// Used to access methods provided by the parent.
        /// </summary>
        [Parameter]
        [EditorRequired]
        public UIOrchestrator OrchestratorRef { get; set; }

        /// <summary>
        /// Boolean value setting the initial state of the
        /// <see cref="SidebarBase.IsOpen"/> parameter.
        /// Default value is <c>true</c>
        /// </summary>
        [Parameter]
        public bool InitialSidebarIsOpen { get; set; } = true;

        #endregion

        #endregion


        #region Instance Variables

        private const string sidebarCssClass = "page__main-sidebar";
        
        private SidebarBase sidebarbase;
        private UIOrchestratorMenu.UIOrchestratorMenu menu;
        private bool isOpen;
        private Dictionary<string, object> sidebarHtmlAttributes = new();

        #endregion


        #region Constructors

        // This method will be executed immediately after OnInitializedAsync if this is a new
        //  instance of a component. If it is an existing component that is being re-rendered because
        //  its parent is re-rendering then the OnInitialized* methods will not be executed, and this
        //  method will be executed immediately after SetParametersAsync instead
        protected override void OnParametersSet()
        {
            isOpen = InitialSidebarIsOpen;
        }

        #endregion


        #region Public Methods Providing Access to the Underlying Components to the Consumer

        /// <summary>
        /// Inverts the current state of the sidebar.
        /// </summary>
        public async Task ToggleSidebarAsync()
        {
            isOpen = !isOpen;
            await InvokeAsync(StateHasChanged);
        }

        /// <summary>
        /// Returns the current state of the sidebar
        /// </summary>
        /// <returns>
        /// Boolean value indicating if the sidebar is opened (true) or closed.
        /// </returns>
        public bool GetSidebarState() => isOpen;

        /// <summary>
        /// Explicitly set the state of the sidebar.
        /// </summary>
        /// <param name="state">
        /// Boolean value indicating if the sidebar state should be set to opened
        /// (true) or closed
        /// </param>
        public async Task SetSidebarToggleState(bool state)
        { 
            isOpen = state;
            await InvokeAsync(StateHasChanged);
        }

        #endregion

    }
}
