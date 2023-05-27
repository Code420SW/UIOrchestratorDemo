using Code420.UIOrchestrator.Server.Components.BaseComponents.IconButtonBase;
using Code420.UIOrchestrator.Server.Components.BaseComponents.ToolTipBase;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Syncfusion.Blazor.Popups;

namespace Code420.UIOrchestrator.Server.Components.CompositeComponents.HelpButton
{
    /// <summary>
    /// <para>
    /// Composite component consisting of an <see cref="IconButtonBase"/> component
    /// and a <see cref="ToolTipBase"/> component.
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

    public partial class HelpButton : ComponentBase
    {

        #region Component Parameters
        
        #region Base Parameters

        /// <summary>
        /// Boolean value indicating if the help button is disabled.
        /// Default value is false.
        /// </summary>
        [Parameter]
        public bool ButtonDisabled { get; set; }

        /// <summary>
        /// Contains the <see cref="RenderFragment" /> composing the tooltip contents.
        /// This parameter is required.
        /// </summary>
        [Parameter]
        [EditorRequired]
        public RenderFragment TooltipContent { get; set; }

        #endregion

        
        #region CSS Parameters
        
        /// <summary>
        /// String value containing CSS class definition(s) that will be injected in the HTML. 
        /// Injecting a class will provide styling segregation when multiple icon buttons are
        /// on the same page.
        /// Default value is <c>help-button</c>.
        /// </summary>
        /// <remarks>
        /// The parameter is provided to mitigate styling conflicts in the situation when the
        /// consuming component also contains one of this component's children
        /// (e.g., <see cref="IconButtonBase"/>). The suffixes <c>__button</c> and
        /// <c>__tooltip</c> are added to the CSSClass parameters for the <see cref="IconButtonBase"/>
        /// and <see cref="ToolTipBase"/> child components, respectively.
        /// </remarks>
        [Parameter]
        public string CssClass { get; set; } = "help-button";
        
        /// <summary>
        /// String containing the CSS <a href="https://developer.mozilla.org/en-US/docs/Web/CSS/margin">margin</a>
        /// value used for the help button.
        /// The margin CSS shorthand property sets the margin area on all four sides of an element.
        /// Default value is: <c>0px</c> on all sides.
        /// </summary>
        [Parameter]
        public string ButtonMargin { get; set; } = "0px";

        /// <summary>
        /// String containing the CSS <a href="https://developer.mozilla.org/en-US/docs/Web/CSS/padding">padding</a>
        /// value used for the help button.
        /// The padding CSS shorthand property sets the padding area on all four sides of an element at once.
        /// Default value is: <c>0px</c> on all sides.
        /// </summary>
        [Parameter]
        public string ButtonPadding { get; set; } = "0px";

        /// <summary>
        /// String value specifying the CSS height value of the Tooltip.
        /// When the Tooltip content gets overflowed due to the height value, 
        /// then the scroll mode will be enabled. When set to auto, the Tooltip 
        /// height gets auto adjusted to display its content within the viewable screen.
        /// Default value is <c>auto</c>.
        /// </summary>
        [Parameter]
        public string TooltipHeight { get; set; } = "auto";

        /// <summary>
        /// String value specifying the CSS width value of the Tooltip.
        /// When set to <c>auto</c>, the Tooltip width gets auto adjusted to display its content
        /// within the viewable screen.
        /// Default value is <c>auto</c>.
        /// </summary>
        [Parameter]
        public string TooltipWidth { get; set; } = "auto";

        #endregion
        
        #endregion


        #region Instance Variables

        private IconButtonBase helpIconButton;
        private ToolTipBase tooltipBase;

        #endregion


        #region Public Methods Providing Access to the Underlying Components to the Consumer

            #region Public Methods for the TooltipBase

            /// <summary>
            /// Used to hide the Tooltip with a specific animation effect.
            /// </summary>
            /// <param name="animation">
            /// <see cref="Syncfusion.Blazor.Popups.TooltipAnimationSettings"/> settings for
            /// tooltip close action.
            /// </param>
            public async Task CloseTooltipAsync(TooltipAnimationSettings animation = null) =>
                await tooltipBase.CloseAsync(animation);

            /// <summary>
            /// Used to show the Tooltip on the specified target with specific animation settings. 
            /// You can also pass the additional arguments like target element in which the tooltip
            /// should appear and animation settings for the tooltip open action.
            /// </summary>
            /// <param name="element">
            /// Target element in which the tooltip should appear.
            /// </param>
            /// <param name="animation">
            /// <see cref="Syncfusion.Blazor.Popups.AnimationModel"/> settings for the tooltip open action.
            /// </param>
            public async Task OpenTooltipAsync(ElementReference? element = null, TooltipAnimationSettings animation = null) =>
                await tooltipBase.OpenAsync(element, animation);

            /// <summary>
            /// Refresh the tooltip component when the target element is dynamically used.
            /// </summary>
            public async Task RefreshTooltipAsync() => await tooltipBase.RefreshAsync();

            /// <summary>
            /// Dynamically refreshes the tooltip element position based on the target element.
            /// </summary>
            /// <param name="target">
            /// The target element.
            /// </param>
            public async Task RefreshTooltipPositionAsync(ElementReference? target = null) => 
                await tooltipBase.RefreshPositionAsync(target);

            #endregion


            #region Public Methods for IconButtonBase

            /// <summary>
            /// Set icon button state to enabled.
            /// </summary>
            public async Task EnableIconButtonAsync() => 
                await helpIconButton.EnableAsync();

            /// <summary>
            /// Set icon button state to disabled.
            /// </summary>
            public async Task DisableIconButtonAsync() => 
                await helpIconButton.DisableAsync();

            /// <summary>
            /// Set focus to the icon button.
            /// </summary>
            public async Task FocusIconButtonAsync() => 
                await helpIconButton.FocusAsync();

            #endregion

        #endregion
    }
}
