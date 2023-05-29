using System.Diagnostics;
using Code420.UIOrchestrator.Server.Components.BaseComponents.ButtonBase;
using Code420.UIOrchestrator.Server.Components.CompositeComponents.HelpDialog;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Syncfusion.Blazor.Notifications;

namespace Code420.UIOrchestrator.Server.Components.CompositeComponents.HelpSystem.UserLogin.Email
{

    /// <summary>
    /// Composite component using the <see cref="HelpButton"/>  and <see cref="HelpDialog"/>
    /// components to provide the help system for the Email input on the <see cref="UserLogin"/>
    /// page. This component provides the base help icon, customized tooltip contents and dialog
    /// which is displayed when the Learn More button in the tooltip is clicked. 
    /// </summary>
    /// <remarks>
    /// The following parameters must be set:<br />
    /// <see cref="Target"/> -- The CSS id or class of the dialog box's parent.<br />
    /// <see cref="CssClassSuffix"/> -- The suffix to append to the CSS class name to provide styling segregation.
    /// </remarks>
    public partial class UserLoginEmailHelp : ComponentBase
    {

        #region Component Parameters

        #region Base Parameters

        /// <summary>
        /// Boolean value indicating if the help icon button is disabled.
        /// The default value is false.
        /// </summary>
        [Parameter]
        public bool HelpButtonDisabled { get; set; }

        /// <summary>
        /// String value containing the CSS Id of the dialog box's parent. 
        /// The dialog box will be rendered completely within the HTML element associated with the CSS Id.
        /// The Target parameter should be a proper CSS Id selector (e.g., #target-id).
        /// Default value is null indicating the target element is the HTML body element.
        /// </summary>
        [Parameter]
        [EditorRequired]
        public string Target { get; set; }

        #endregion
        
        #region CSS Parameters

        /// <summary>
        /// String value containing a suffix applied to the CSS class name to
        /// provide styling segregation.
        /// Default value is string.Empty.
        /// </summary>
        [Parameter]
        [EditorRequired]
        public string CssClassSuffix { get; set; } = string.Empty;
        
        #endregion

        #endregion


        #region Instance Variables

        private HelpButton.HelpButton helpButtonComponent;
        private ButtonBase buttonLearnMore;
        private HelpDialog.HelpDialog dialogLearnMore;

        #endregion


        #region Private Methods for Internal Use Only

        private async Task LearnMoreButtonClickAsync(MouseEventArgs args)
        {
            if (helpButtonComponent.IsTooltipSticky()) 
                await helpButtonComponent.CloseTooltipAsync();
            
            await dialogLearnMore.ShowAsync();
        }

        #endregion

    }
}
