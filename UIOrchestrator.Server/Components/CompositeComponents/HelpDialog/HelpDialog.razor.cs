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
    /// The help button is displayed in the parent container and when hovered,
    /// the tooltip is displayed. The component can be used as-is but is typically
    /// used by topic-specific Help System components.
    /// </para>
    /// <para>
    /// Styling for the child components should be consistent across the application.
    /// For this reason the styling is hard-coded in the component though should
    /// reference theming elements as appropriate.
    /// </para>
    /// <para>
    /// The styling for the <see cref="TooltipContent"/> should be defined in the
    /// consuming component since the content is specific to the consuming component.
    /// </para>
    /// <para>
    /// The <see cref="CssClass"/> parameter is provided to mitigate styling conflicts
    /// in the situation when the consuming component also contains one of this component's
    /// children (e.g., <see cref="IconButtonBase"/>). The suffixes <c>__button</c> and
    /// <c>__tooltip</c> are added to the CSSClass parameters for the <see cref="IconButtonBase"/>
    /// and <see cref="ToolTipBase"/> child components, respectively.
    /// </para>
    /// <para>
    /// The component does not provide access to any event handlers for the child component.
    /// </para>
    /// <para>
    /// Methods are provided for basic manipulation of the child components
    /// (e.g., <see cref="OpenTooltipAsync"/> and <see cref="EnableIconButtonAsync"/>).
    /// </para>
    /// </summary>
    /// <remarks>
    /// The following parameters must be set:<br />
    /// <see cref="TooltipContent"/> -- Define the content displayed in the tooltip<br />
    /// </remarks>
    /// <remarks>
    /// Consider setting the following parameters:<br />
    /// <see cref="CssClass"/> -- Provides CSS isolation for the toast.
    /// </remarks>

    public partial class HelpDialog : ComponentBase
    {

        #region Component Parameters

        #region Base Parameters

        /// <summary>
        /// String value containing the CSS Id of the dialog box's parent. 
        /// The dialog box will be rendered completely within the HTML element associated with
        /// the CSS Id. The Target parameter should be a proper CSS Id selector (e.g., #target-id).
        /// Default value is <c>null</c> indicating the target element is the HTML <c>body</c> element.
        /// </summary>
        [Parameter]
        public string Target { get; set; }

        /// <summary>
        /// A <see cref="RenderFragment" /> composing the content section of the dialog box.
        /// The default value is null.
        /// </summary>
        [Parameter]
        public RenderFragment ContentFragment { get; set; }

        /// <summary>
        /// A <see cref="RenderFragment" /> composing the footer section of the dialog box. 
        /// The default value is null.
        /// </summary>
        [Parameter]
        public RenderFragment FooterFragment { get; set; }

        /// <summary>
        /// A <see cref="RenderFragment" /> composing the header section of the dialog box. 
        /// The default value is null.
        /// </summary>
        [Parameter]
        public RenderFragment HeaderFragment { get; set; }

        #endregion


        #region CSS Parameters


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