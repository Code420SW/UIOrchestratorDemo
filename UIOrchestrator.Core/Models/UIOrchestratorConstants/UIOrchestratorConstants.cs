using System.Net;

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
        /// Default value passed to caller when the Http call throws.
        /// </summary>
        // public const HttpStatusCode BrokerFailureStatusCode = HttpStatusCode.ServiceUnavailable;


        /// <summary>
        /// The error inserted in IStatusGeneric objects when an expired token is detected.
        /// </summary>
        public const string TokenExpiredErrorMessage = "The JWT token has expired.";


        /// <summary>
        /// The error inserted in IStatusGeneric objects when an expired token is detected.
        /// </summary>
        // public const string TokenRefreshFailureMessage = "Attempt to refresh the user's tokens failed.";


        /// <summary>
        /// The base namespace for all components injected as Orchestrator Tabs.
        /// Used to build the OrchestratorMenuItem.
        /// </summary>
        public const string OrchestratorTabBaseNamespace = "Code420.UIOrchestrator.Server.Components.UIOrchestratorComponents.UIOrchestratorTabManager.UIOrchestratorTabs.";




        /// <summary>
        /// Set the length of a company name field in the <see cref="TenantCompany"/> class.
        /// </summary>
        // public const int CompanyNameLength = 256;


        /// <summary>
        /// Set the length of a company address field (used for Address1 and Address2) in the
        /// <see cref="TenantCompany"/> class.
        /// </summary>
        // public const int CompanyAddressLength = 50;


        /// <summary>
        /// Set the length of a company city field in the <see cref="TenantCompany"/> class.
        /// </summary>
        // public const int CompanyCityLength = 25;


        /// <summary>
        /// Set the length of a company state field in the <see cref="TenantCompany"/> class.
        /// </summary>
        // public const int CompanyStateLength = 2;


        /// <summary>
        /// Set the length of a company zip code field in the <see cref="TenantCompany"/> class.
        /// </summary>
        // public const int CompanyZipCodeLength = 10;

    }
}
