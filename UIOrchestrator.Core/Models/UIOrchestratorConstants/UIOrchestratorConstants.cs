using System.Net;

namespace Code420.UIOrchestrator.Core.Models.UIOrchestratorConstants
{
    /// <summary>
    /// Various constants used by CanXtracAPI and/or CanXtracServer projects. 
    /// </summary>
    public static class UIOrchestratorConstants
    {
        /// <summary>
        /// </summary>
        public const int EmailSize = 256;
        
        /// <summary>
        /// Default value passed to caller when the Http call throws.
        /// </summary>
        public const HttpStatusCode BrokerFailureStatusCode = HttpStatusCode.ServiceUnavailable;


        /// <summary>
        /// The error inserted in IStatusGeneric objects when an expired token is detected.
        /// </summary>
        public const string TokenExpiredErrorMessage = "The JWT token has expired.";


        /// <summary>
        /// The error inserted in IStatusGeneric objects when an expired token is detected.
        /// </summary>
        public const string TokenRefreshFailureMessage = "Attempt to refresh the user's tokens failed.";


        /// <summary>
        /// The base namespace for all components injected as Orchestrator Tabs.
        /// Used to build the OrchestratorMenuItem.
        /// </summary>
        public const string OrchestratorTabBaseNamespace = "Code420.CanXtracServer.Components.UIOrchestratorComponents.UIOrchestratorTabManager.UIOrchestratorTabs.";


        /// <summary>
        /// Sets the precision (the maximum total number of decimal digits that will be stored, 
        /// both to the left and to the right of the decimal point) for decimal values in the database.
        /// </summary>
        /// <remarks>
        /// Used by the <see cref="CanxtracDbContext"/> and <see cref="ConfigurationDbContext"/>.
        /// </remarks>
        public const int DatabaseDecimalPrecision = 9;


        /// <summary>
        /// Sets the scale (the number of decimal digits that will be stored to the right of the 
        /// decimal point) for decimal values stored in the database.
        /// </summary>
        /// <remarks>
        /// Used by the <see cref="CanxtracDbContext"/> and <see cref="ConfigurationDbContext"/>.
        /// </remarks>
        public const int DatabaseDecimalScale = 2;


        /// <summary>
        /// Set the length of a company name field in the <see cref="TenantCompany"/> class.
        /// </summary>
        public const int CompanyNameLength = 256;


        /// <summary>
        /// Set the length of a company address field (used for Address1 and Address2) in the
        /// <see cref="TenantCompany"/> class.
        /// </summary>
        public const int CompanyAddressLength = 50;


        /// <summary>
        /// Set the length of a company city field in the <see cref="TenantCompany"/> class.
        /// </summary>
        public const int CompanyCityLength = 25;


        /// <summary>
        /// Set the length of a company state field in the <see cref="TenantCompany"/> class.
        /// </summary>
        public const int CompanyStateLength = 2;


        /// <summary>
        /// Set the length of a company zip code field in the <see cref="TenantCompany"/> class.
        /// </summary>
        public const int CompanyZipCodeLength = 10;


        /// <summary>
        /// Set the length of a base portion of the Metrc Tag in the <see cref="TenantCompany"/> class.
        /// </summary>
        public const int MetrcTagBaseLength = 15;


        /// <summary>
        /// Set the length of a Metrc Tag in the <see cref="TenantCompany"/> class.
        /// </summary>
        public const int MetrcTagLength = 24;


        /// <summary>
        /// Set the length of the Metrc entity name in the <see cref="TenantCompany"/> class.
        /// </summary>
        public const int MetrcEntityNameLength = 50;


        /// <summary>
        /// Set the length of a Metrc license number in the <see cref="TenantCompany"/> class.
        /// </summary>
        public const int MetrcEntityLicenseLength = 24;


        /// <summary>
        /// Set the length of the access key assigned to the Metrc entity in the <see cref="TenantCompany"/> class.
        /// </summary>
        public const int MetrcUserKeyLength = 50;


        /// <summary>
        /// Set the length of the access key assigned to the vendor in the <see cref="TenantCompany"/> class.
        /// </summary>
        public const int MetrcVendorKeyLength = 50;
        
        /// <summary>
        /// Contains the route for the GetMenuItemsAsync API endpoint.
        /// Used by the <see cref="Code420.CanXtracServer.Brokers.ConfigurationApiBroker.GetMenuItemDefinitionsAsync"/> method.
        /// </summary>
        public const string GetMenusEndpointRoute = "getmenus";
        
        /// <summary>
        /// Contains the route for the Login API endpoint.
        /// Used by the <see cref="Code420.CanXtracServer.Brokers.HomeApiBroker.Login"/> method.
        /// </summary>
        public const string LoginEndpointRoute = "login";
        
        /// <summary>
        /// Contains the route for the Logout API endpoint
        /// Used by the <see cref="Code420.CanXtracServer.Brokers.HomeApiBroker.Logout"/> method.
        /// </summary>
        public const string LogoutEndpointRoute = "logout";
        
        /// <summary>
        /// Contains the route for the RefreshAuthenticationToken API endpoint
        /// Used by the <see cref="Code420.CanXtracServer.Brokers.HomeApiBroker.RefreshAuthenticationToken"/> method.
        /// </summary>
        public const string TokenRefreshEndpointRoute = "tokenrefresh";
        
        /// <summary>
        /// Contains the route for the GetUsersPermissions API endpoint
        /// Used by the <see cref="Code420.CanXtracServer.Brokers.HomeApiBroker.GetUsersPermissions"/> method.
        /// </summary>
        public const string UserPermissionsEndpointRoute = "getuserpermissions";
        
        /// <summary>
        /// Contains the route for the GetUserInformation API endpoint
        /// Used by the <see cref="Code420.CanXtracServer.Brokers.HomeApiBroker.GetUserInformation"/> method.
        /// </summary>
        public const string UserInformationEndpointRoute = "getuserinformation";
        
        /// <summary>
        /// Contains the route for the GetUserInformation API endpoint
        /// Used by the <see cref="TBD"/> method.
        /// </summary>
        public const string AddSingleTenantEndpointRoute = "addsingletenant";
        
        /// <summary>
        /// Contains the route for the GetUserInformation API endpoint
        /// Used by the <see cref="TBD"/> method.
        /// </summary>
        public const string UpdateTenantNameEndpointRoute = "updatetenantname";
        
        /// <summary>
        /// Contains the route for the GetUserInformation API endpoint
        /// Used by the <see cref="TBD"/> method.
        /// </summary>
        public const string GetAllTenantsEndpointRoute = "getalltenants";
        
        /// <summary>
        /// Contains the route for the GetUserInformation API endpoint
        /// Used by the <see cref="TBD"/> method.
        /// </summary>
        public const string RegisterNewUserEndpointRoute = "registernewuser";
        
        /// <summary>
        /// Contains the route for the GetUserInformation API endpoint
        /// Used by the <see cref="TBD"/> method.
        /// </summary>
        public const string RemoveUserEndpointRoute = "removeuser";
        
        /// <summary>
        /// Contains the route for the GetUserInformation API endpoint
        /// Used by the <see cref="TBD"/> method.
        /// </summary>
        public const string GetUsersEndpointRoute = "getusers";
        
        /// <summary>
        /// Contains the route for the GetUserInformation API endpoint
        /// Used by the <see cref="TBD"/> method.
        /// </summary>
        public const string GetUserEndpointRoute = "getuser";
        
        /// <summary>
        /// Contains the route for the GetUserInformation API endpoint
        /// Used by the <see cref="TBD"/> method.
        /// </summary>
        public const string PrepareForUpdateUserEndpointRoute = "prepareforupdateuser";
        
        /// <summary>
        /// Contains the route for the GetUserInformation API endpoint
        /// Used by the <see cref="TBD"/> method.
        /// </summary>
        public const string UpdateUserEndpointRoute = "updateuser";
        
        /// <summary>
        /// Contains the route for the GetUserInformation API endpoint
        /// Used by the <see cref="TBD"/> method.
        /// </summary>
        public const string UpdateUserDisbledEndpointRoute = "updateuserdisabled";
    }
}
