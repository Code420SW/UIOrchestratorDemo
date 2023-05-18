
namespace Code420.UIOrchestrator.Core.Models.UIOrchestratorConstants
{
    /// <summary>
    /// Various constants used by UIOrchestrator.Core and UIOrchestrator.Server projects. 
    /// </summary>
    public static class UIOrchestratorConstants
    {
        /// <summary>
        /// </summary>
        public const int EmailSize = 256;

        /// <summary>
        /// The error inserted in IStatusGeneric objects when an expired token is detected.
        /// </summary>
        public const string TokenExpiredErrorMessage = "The JWT token has expired.";

        /// <summary>
        /// The base namespace for all components injected as Orchestrator Tabs.
        /// Used to build the OrchestratorMenuItem.
        /// </summary>
        public const string OrchestratorTabBaseNamespace = "Code420.UIOrchestrator.Server.Components.UIOrchestratorComponents.UIOrchestratorTabManager.UIOrchestratorTabs.";
    }
}
