using Code420.UIOrchestrator.Server.Components.BaseComponents.DialogBoxBase;
using Code420.UIOrchestrator.Server.Components.BaseComponents.IconButtonBase;
using Code420.UIOrchestrator.Server.Components.BaseComponents.ToolTipBase;
using Microsoft.AspNetCore.Components;

namespace Code420.UIOrchestrator.Server.Components.CompositeComponents.HelpDialog
{
    /// <summary>
    /// <para>
    /// Provides a standardized <see cref="DialogBoxBase"/> component with styling
    /// applies so all Help System components have a consistent look and feel.
    /// </para>
    /// <para>
    /// The dialog is displayed in the container specified by the <see cref="Target"/>
    /// parameter.
    /// </para>
    /// <para>
    /// Styling for the dialog should be consistent across the application.
    /// For this reason the styling is hard-coded in the component though should
    /// reference theming elements as appropriate.
    /// </para>
    /// <para>
    /// The component does not provide access to any event handlers for the child component.
    /// </para>
    /// <para>
    /// Methods are provided for basic manipulation of the child components
    /// (e.g., <see cref="ShowAsync"/> and <see cref="HideAsync"/>).
    /// </para>
    /// </summary>
    /// <remarks>
    /// The following parameters must be set:<br />
    /// <see cref="Target"/> -- The CSS id or class of the dialog box's parent.<br />
    /// <see cref="HeaderFragment"/> -- Define the content displayed in the header section of the dialog.<br />
    /// <see cref="ContentFragment"/> -- Define the content displayed in the content section of the dialog.<br />
    /// <see cref="FooterFragment"/> -- Define the content displayed in the footer section of the dialog.<br />
    /// <see cref="CssClassSuffix"/> -- The suffix to append to the CSS class name to provide styling segregation.
    /// </remarks>

    public partial class HelpDialog : ComponentBase
    {

        #region Component Parameters

        #region Base Parameters

        /// <summary>
        /// String value containing the CSS id or class of the dialog box's parent. 
        /// The dialog box will be rendered completely within the HTML element associated with
        /// the CSS Id. The Target parameter should be a proper CSS Id selector (e.g., #target-id).
        /// Default value is <c>null</c> indicating the target element is the HTML <c>body</c> element.
        /// </summary>
        [Parameter]
        [EditorRequired]
        public string Target { get; set; }

        /// <summary>
        /// A <see cref="RenderFragment" /> composing the content section of the dialog box.
        /// The default value is null.
        /// </summary>
        [Parameter]
        [EditorRequired]
        public RenderFragment ContentFragment { get; set; }

        /// <summary>
        /// A <see cref="RenderFragment" /> composing the footer section of the dialog box. 
        /// The default value is null.
        /// </summary>
        [Parameter]
        [EditorRequired]
        public RenderFragment FooterFragment { get; set; }

        /// <summary>
        /// A <see cref="RenderFragment" /> composing the header section of the dialog box. 
        /// The default value is null.
        /// </summary>
        [Parameter]
        [EditorRequired]
        public RenderFragment HeaderFragment { get; set; }

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

        private DialogBoxBase helpDialog;

        #endregion


        #region Public Methods Providing Access to the Underlying Components to the Consumer

        /// <summary>
        /// Opens the dialog if it is in a hidden state.
        /// </summary>
        /// <param name="isFullScreen">
        /// Boolean value specifying if the dialog is rendered full screen.
        /// Default value is false.
        /// </param>
        public async Task ShowAsync(bool isFullScreen = false) => 
            await helpDialog.ShowAsync(isFullScreen);

        /// <summary>
        /// Closes the dialog if it is in a visible state.
        /// </summary>
        public async Task HideAsync() => 
            await helpDialog.HideAsync();

        /// <summary>
        /// Refreshes the dialog's position when the user changes its height and width dynamically.
        /// </summary>
        public async Task RefreshPositionAsync() => 
            await helpDialog.RefreshPositionAsync();

        #endregion
    }
}