using Microsoft.AspNetCore.Components;
using Syncfusion.Blazor.Popups;

namespace Code420.UIOrchestrator.Server.Components.BaseComponents.ToolTipBase
{
    /// <summary>
    /// <para>
    /// Encapsulates the SfTooltip component with the goal of exposing various CSS styling options,
    /// and to overload tooltip events. 
    /// </para>
    ///<para>
    /// The contents of the tooltip can be defined using either the <see cref="Content"/>
    /// or the <see cref="ContentFragment"/> parameters. <b>Only one of the parameters can
    /// be used.</b>
    /// </para>
    ///<para>
    /// Note that the <see cref="Content"/> parameter takes priority over the
    /// <see cref="ContentFragment"/> parameter.
    /// </para>
    /// <para>
    /// 
    /// </para>
    /// </summary>
    /// <remarks>
    /// The following parameters must be set:<br />
    /// <see cref="Content"/> -- A string value containing the content displayed
    /// in the tooltip.<br />
    /// <b>OR</b><br />
    /// <see cref="ContentFragment"/> -- A <see cref="RenderFragment"/> containing
    /// the content displayed in the tooltip.<br />
    /// </remarks>
    /// <remarks>
    /// Consider setting the following parameters:<br />
    /// <see cref="CssClass"/> -- Provides CSS isolation.
    /// </remarks>
    public partial class ToolTipBase : ComponentBase
    {
        
        #region Component Parameters
        
        #region Base Parameters

        /// <summary>
        /// Used to customize the animation of the Tooltip while opening and closing. 
        /// The animation property also allows you to set delay, duration, and various other effects of your choice. 
        /// You can set the same or different animation options to the Tooltip when it is in the open or close state. 
        /// The <see cref="Syncfusion.Blazor.Popups.AnimationModel"/> exposes the base
        /// Open and Close properties for which the <see cref="Syncfusion.Blazor.Popups.TooltipAnimationSettings"/> can be set.
        /// The default value is: Delay =  0.0ms, Duration = 0.0ms, Effect = None for both Open and Close.
        /// </summary>
        [Parameter]
        public AnimationModel Animation { get; set; }

        /// <summary>
        /// Double value specifying the number of millisecond to wait before closing the Tooltip.
        /// Default value is 0 seconds.
        /// </summary>
        [Parameter]
        public double CloseDelay { get; set; }

        /// <summary>
        /// String value specifying the container element in which the Tooltip pop-up will be appended.
        /// Default value <c>body</c>.
        /// </summary>
        [Parameter]
        public string Container { get; set; } = "body";

        /// <summary>
        /// String value containing the content to be displayed in the Tooltip.
        /// This property is the primary source for the tooltip content. 
        /// If this property is not set, the <see cref="ContentFragment"/> parameter
        /// will be used.
        /// Default value is string.Empty.
        /// </summary>
        [Parameter]
        public string Content { get; set; } = string.Empty;

        /// <summary>
        /// Contains the <see cref="RenderFragment" /> composing the tooltip contents.
        /// This parameter will be used <b>only</b> if the <see cref="Content"/> parameter
        /// is not set.
        /// The default value is null.
        /// </summary>
        [Parameter]
        public RenderFragment ContentFragment { get; set; }

        /// <summary>
        /// Boolean value indicating if right-to-left formatting is enabled for the button.
        /// Default value is false.
        /// </summary>
        [Parameter]
        public bool EnableRtl { get; set; }

        /// <summary>
        /// String value specifying the CSS height value of the Tooltip.
        /// When the Tooltip content gets overflowed due to the height value, 
        /// then the scroll mode will be enabled. When set to auto, the Tooltip 
        /// height gets auto adjusted to display its content within the viewable screen.
        /// Default value is <c>auto</c>.
        /// </summary>
        [Parameter]
        public string Height { get; set; } = "auto";

        /// <summary>
        /// Boolean value indicating if the Tooltip is displayed in an open state until it is
        /// closed manually.
        /// Default value is false.
        /// </summary>
        [Parameter]
        public bool IsSticky { get; set; }

        /// <summary>
        /// Boolean value indicating if the Tooltip will follow the mouse pointer moves over the
        /// element specified by the <see cref="Target"/> parameter.
        /// Default value is false.
        /// </summary>
        [Parameter]
        public bool MouseTrail { get; set; }

        /// <summary>
        /// Double value that sets the space between the element specified by the
        /// <see cref="Target"/> parameter and Tooltip element in X-axis.
        /// Default value is 0.0.
        /// </summary>
        [Parameter]
        public double OffsetX { get; set; }

        /// <summary>
        /// Double value that sets the space between the element specified by the
        /// <see cref="Target"/> parameter and Tooltip element in Y-axis.
        /// Default value is 0.0.
        /// </summary>
        [Parameter]
        public double OffsetY { get; set; }

        /// <summary>
        /// Double value specifying the number of millisecond to wait before opening the Tooltip.
        /// Default value is 0.5 seconds (500ms).
        /// </summary>
        [Parameter]
        public double OpenDelay { get; set; } = 500.0;

        /// <summary>
        /// String value specifying the type of open mode to display the Tooltip content. 
        /// The available open modes are Auto, Hover, Click, Focus, and Custom. 
        /// To open the Tooltip for multiple actions, say while hovering over or clicking on a 
        /// target element, the OpensOn property can be assigned with multiple values, 
        /// separated by space such as <c>Hover Click</c>. 
        /// The Auto value cannot be used with any combination for multiple values.
        /// Default value is Auto.
        /// </summary>
        /// <remarks>
        /// Mode: {Desktop} [Mobile]<br/>
        /// Auto:<br/>
        /// {Tooltip appears when you hover over the target or when the target element receives the focus.}<br/>
        /// [Tooltip opens on tap and hold of the target element.]<br/>
        /// Hover:<br/>
        /// {Tooltip appears when you hover over the target.}<br/>
        /// [Tooltip opens on tap and hold of the target element]<br/>
        /// Click:<br/>
        /// {Tooltip appears when you click a target element.}<br/>
        /// [Tooltip appears when you single tap the target element.]<br/>
        /// Focus:<br/>
        /// {Tooltip appears when you focus (say through tab key) on a target element.}<br/>
        /// [Tooltip appears with a single tap on the target element.]<br/>
        /// Custom:<br/>
        /// {Tooltip is not triggered by any default action. So, you have to bind your own events<br/>
        /// and use either open or close public methods.}
        /// [Same as Desktop.]<br/>
        /// </remarks>
        [Parameter]
        public string OpensOn { get; set; } = "Auto";

        /// <summary>
        /// <see cref="Syncfusion.Blazor.Popups.Position"/> member specifying the position of the Tooltip element with respect to the target element.
        /// Default value is <see cref="Position.TopCenter"/>.
        /// </summary>
        [Parameter]
        public Position Position { get; set; } = Position.TopCenter;

        /// <summary>
        /// Boolean value indicating if the tip pointer is shown with the Tooltip.
        /// Default value is true.
        /// </summary>
        [Parameter]
        public bool ShowTipPointer { get; set; } = true;

        /// <summary>
        /// String value specifying the target selector where the Tooltip needs to be displayed. 
        /// The target element is considered as the parent container.
        /// Must use full CSS target designator (e.g., <c>#target</c> for HTML Id or
        /// <c>.target</c> for HTML class).
        /// Default value is string.Empty.
        /// </summary>
        [Parameter]
        public string Target { get; set; } = string.Empty;

        /// <summary>
        /// <see cref="Syncfusion.Blazor.Popups.TipPointerPosition"/> member specifying the position 
        /// of the tip pointer on the tooltip. The available options are Auto, Start, Middle, and End. 
        /// Default value is <see cref="TipPointerPosition.Auto"/>.
        /// </summary>
        /// <remarks>
        /// When set to Auto, the tip pointer gets auto adjusted within the space of the target's
        /// length and does not point outside.
        /// </remarks>
        [Parameter]
        public TipPointerPosition TipPointerPosition { get; set; } = TipPointerPosition.Auto;

        /// <summary>
        /// String value specifying the CSS width value of the Tooltip.
        /// When set to <c>auto</c>, the Tooltip width gets auto adjusted to display its content
        /// within the viewable screen.
        /// Default value is <c>auto</c>.
        /// </summary>
        [Parameter]
        public string Width { get; set; } = "auto";

        /// <summary>
        /// Boolean value used to set the collision target element as page viewport (window) or
        /// Tooltip element, when using the target. 
        /// If this property is enabled, tooltip will perform the collision calculation between the
        /// target elements. and viewport (window) instead of Tooltip element.
        /// Default value is true.
        /// </summary>
        [Parameter]
        public bool WindowCollision { get; set; } = true;

        #endregion


        #region Event Callback Parameters
        // ==================================================
        // Event Callback Parameters
        // ==================================================


        /// <summary>
        /// An <see cref="EventCallback"/> containing the consumer's method invoked when the
        /// Tooltip is closed.
        /// </summary>
        [Parameter]
        public EventCallback<TooltipEventArgs> Closed { get; set; }

        /// <summary>
        /// An <see cref="EventCallback"/> containing the consumer's method invoked when the
        /// Tooltip is created.
        /// </summary>
        [Parameter]
        public EventCallback<object> Created { get; set; }

        /// <summary>
        /// An <see cref="EventCallback"/> containing the consumer's method invoked when the
        /// Tooltip is destroyed.
        /// </summary>
        [Parameter]
        public EventCallback<object> Destroyed { get; set; }

        /// <summary>
        /// An <see cref="EventCallback"/> containing the consumer's method called before the
        /// Tooltip hides from the screen. 
        /// </summary>
        /// <remarks>
        /// Closing the Tooltip can be prevented by setting the <see cref="TooltipEventArgs.Cancel"/>
        /// argument to <c>true</c>. 
        /// </remarks>
        [Parameter]
        public EventCallback<TooltipEventArgs> OnClose { get; set; }

        /// <summary>
        /// An <see cref="EventCallback"/> containing the consumer's method invoked for every
        /// collision fit calculation.
        /// </summary>
        [Parameter]
        public EventCallback<TooltipEventArgs> OnCollision { get; set; }

        /// <summary>
        /// An <see cref="EventCallback"/> containing the consumer's method invoked before the
        /// Tooltip is displayed over the target element. 
        /// </summary>
        /// <remarks>
        /// The Tooltip display can be prevented by setting the <see cref="TooltipEventArgs.Cancel"/>
        /// argument to <c>true</c>. 
        /// This event is mainly used to refresh the Tooltip positions dynamically or to set
        /// customized styles.
        /// </remarks>
        [Parameter]
        public EventCallback<TooltipEventArgs> OnOpen { get; set; }

        /// <summary>
        /// An <see cref="EventCallback"/> containing the consumer's method invoked before the
        /// Tooltip and its contents will be added to the DOM. 
        /// </summary>
        /// <remarks>
        /// The Tooltip render can be prevented by setting the <see cref="TooltipEventArgs.Cancel"/>
        /// argument to <c>true</c>. 
        /// This event is mainly used to customize the Tooltip before it shows up on the screen. 
        /// For example, to set new animation effects on the Tooltip.
        /// </remarks>
        [Parameter]
        public EventCallback<TooltipEventArgs> OnRender { get; set; }

        /// <summary>
        /// An <see cref="EventCallback"/> containing the consumer's method invoked after the
        /// Tooltip component is opened.
        /// </summary>
        [Parameter]
        public EventCallback<TooltipEventArgs> Opened { get; set; }

        #endregion


        #region CSS Parameters

        // ==================================================
        // CSS Styling Parameters
        // ==================================================


        /// <summary>
        /// String value containing CSS class definition(s) that will be injected in the root
        /// HTML div element of the Tooltip.
        /// Default value is string.Empty.
        /// </summary>
        [Parameter]
        public string CssClass { get; set; } = string.Empty;

        /// <summary>
        /// String value that specifies the CSS ID in the root HTML div element of the Tooltip.
        /// Default value is string.Empty.
        /// </summary>
        [Parameter]
        public string CssId { get; set; } = string.Empty;

        /// <summary>
        /// String value that specifies the CSS <a href="https://developer.mozilla.org/en-US/docs/Web/CSS/background-color">background-color</a>
        /// value used for the tooltip.
        /// The background-color CSS property sets the background color of an element.
        /// Default value is <c>rgba(0,0,0,0.9)</c>.
        /// </summary>
        [Parameter]
        public string TooltipBackgroundColor { get; set; } = "rgba(0,0,0,0.9)";

        /// <summary>
        /// String value that specifies the CSS <a href="https://developer.mozilla.org/en-US/docs/Web/CSS/border">border</a>
        /// value set used for the tooltip border.
        /// The border shorthand CSS property sets an element's border. It sets the values of border-width,
        /// border-style, and border-color.
        /// Default value is <c>1px solid #000</c>.
        /// </summary>
        [Parameter]
        public string TooltipBorder { get; set; } = "1px solid #000";

        /// <summary>
        /// String value that specifies the CSS <a href="https://developer.mozilla.org/en-US/docs/Web/CSS/border-radius">border-radius</a>
        /// value used for the tooltip border radius.
        /// The border-radius CSS property rounds the corners of an element's outer border edge.
        /// Default value is <c>4px</c>.
        /// </summary>
        [Parameter]
        public string TooltipBorderRadius { get; set; } = "4px";

        /// <summary>
        /// String value that specifies the CSS <a href="https://developer.mozilla.org/en-US/docs/Web/CSS/filter">filter</a>
        /// value applied to the tooltip HTML element.
        /// The filter CSS property applies graphical effects like blur or color shift to an element. 
        /// Filters are commonly used to adjust the rendering of images, backgrounds, and borders.
        /// Default value is <c>none</c>.
        /// </summary>
        [Parameter]
        public string TooltipFilter { get; set; } = "none";

        /// <summary>
        /// String value that specifies the CSS <a href="https://developer.mozilla.org/en-US/docs/Web/CSS/color">color</a>
        /// value used for the tooltip content font color.
        /// The color CSS property sets the foreground color value of an element's text and text decorations,
        /// and sets the currentcolor value.
        /// Default value is <c>#FFF</c>.
        /// </summary>
        [Parameter]
        public string ContentFontColor { get; set; } = "#FFF";

        /// <summary>
        /// String value that specifies the CSS <a href="https://developer.mozilla.org/en-US/docs/Web/CSS/font-size">font-size</a>
        /// value used for the tooltip content.
        /// The font-size CSS property sets the size of the font. Changing the font size also updates the sizes
        /// of the font size-relative length units, such as em, ex, and so forth.
        /// This becomes the base em unit (=1em) for the tooltip contents and can be used to size child elements
        /// proportionally.
        /// Default value is <c>12px</c>.
        /// </summary>
        [Parameter]
        public string ContentFontSize { get; set; } = "12px";

        /// <summary>
        /// String value that specifies the CSS <a href="https://developer.mozilla.org/en-US/docs/Web/CSS/line-height">line-height</a>
        /// value used for the tooltip content.
        /// The line-height CSS property sets the height of a line box. 
        /// It's commonly used to set the distance between lines of text.
        /// Default value is <c>1.5</c>.
        /// </summary>
        [Parameter]
        public string ContentLineHeight { get; set; } = "1.5";

        /// <summary>
        /// String containing the CSS <a href="https://developer.mozilla.org/en-US/docs/Web/CSS/padding">padding</a>
        /// value used for the tooltip content.
        /// The padding CSS shorthand property sets the padding area on all four sides of an element at once.
        /// Specify the padding in top, right, bottom, left order.
        /// Default value is: <c>4px 8px 4px 8px</c>.
        /// </summary>
        [Parameter]
        public string ContentPadding { get; set; } = "4px 8px 4px 8px";

        /// <summary>
        /// String value that specifies the CSS <a href="https://developer.mozilla.org/en-US/docs/Web/CSS/color">color</a>
        /// value used for the tooltip tip color.
        /// The color CSS property sets the foreground color value of an element's text and text decorations,
        /// and sets the currentcolor value.
        /// Default value is <c>rgba(0,0,0,0.9)</c>.
        /// </summary>
        [Parameter]
        public string TipColor { get; set; } = "rgba(0,0,0,0.9)";

        /// <summary>
        /// String value that specifies the CSS <a href="https://developer.mozilla.org/en-US/docs/Web/CSS/background-color">background-color</a>
        /// value used for the close button.
        /// The background-color CSS property sets the background color of an element.
        /// Default value is <c>#FFF</c>.
        /// </summary>
        [Parameter]
        public string CloseButtonBackgroundColor { get; set; } = "#FFF";

        /// <summary>
        /// String value that specifies the CSS <a href="https://developer.mozilla.org/en-US/docs/Web/CSS/color">color</a>
        /// value used for the close button icon color.
        /// The color CSS property sets the foreground color value of an element's text and text decorations,
        /// and sets the currentcolor value.
        /// Default value is <c>rgba(0,0,0,0.9)</c>.
        /// </summary>
        [Parameter]
        public string CloseButtonIconColor { get; set; } = "rgba(0,0,0,0.9)";

        /// <summary>
        /// String value that specifies the CSS <a href="https://developer.mozilla.org/en-US/docs/Web/CSS/border">border</a>
        /// value set used for the close button border.
        /// The border shorthand CSS property sets an element's border. It sets the values of border-width,
        /// border-style, and border-color.
        /// Default value is <c>1px solid #FFF</c>.
        /// </summary>
        [Parameter]
        public string CloseButtonBorder { get; set; } = "1px solid #FFF";

        /// <summary>
        /// String value that specifies the CSS <a href="https://developer.mozilla.org/en-US/docs/Web/CSS/font-size">font-size</a>
        /// value used for the close button icon size.
        /// The font-size CSS property sets the size of the font.
        /// Default value is <c>16px</c>.
        /// </summary>
        [Parameter]
        public string CloseButtonIconSize { get; set; } = "16px";

        /// <summary>
        /// String value that specifies the CSS <a href="https://developer.mozilla.org/en-US/docs/Web/CSS/border">border</a>
        /// value set used for the close button icon border.
        /// The border shorthand CSS property sets an element's border. It sets the values of border-width,
        /// border-style, and border-color.
        /// Default value is <c>1px solid #FFF</c>.
        /// </summary>
        [Parameter]
        public string CloseButtonIconBorder { get; set; } = "1px solid #FFF";

        /// <summary>
        /// String value that specifies the CSS <a href="https://developer.mozilla.org/en-US/docs/Web/CSS/background-color">background-color</a>
        /// value used for the close button when hovered.
        /// The background-color CSS property sets the background color of an element.
        /// Default value is <c>#FFF</c>.
        /// </summary>
        [Parameter]
        public string CloseButtonHoverBackgroundColor { get; set; } = "#FFF";

        /// <summary>
        /// String value that specifies the CSS <a href="https://developer.mozilla.org/en-US/docs/Web/CSS/color">color</a>
        /// value used for the close button icon when hovered.
        /// The color CSS property sets the foreground color value of an element's text and text decorations,
        /// and sets the currentcolor value.
        /// Default value is <c>rgba(0,0,0,0.85)</c>.
        /// </summary>
        [Parameter]
        public string CloseButtonHoverIconColor { get; set; } = "rgba(0,0,0,0.85)";

        #endregion
        
        #endregion


        #region Instance Variables

        private SfTooltip sfTooltip;
        private string masterCssSelector;

        #endregion


        #region Constructors

        protected override void OnParametersSet()
        {
            // Need to ensure a non-null Animation exists so SfTooltip doesn't throw an exception
            Animation ??= new()
            {
                Open = new()
                {
                    Delay = 0.0,
                    Duration = 0.0,
                    Effect = Effect.None
                },
                Close = new()
                {
                    Delay = 0.0,
                    Duration = 0.0,
                    Effect = Effect.None
                }
            };

            // Build the master selector
            masterCssSelector = ((CssClass == String.Empty) ? ".e-tooltip-wrap" : $".{ CssClass }.e-tooltip-wrap");
        }

        #endregion


        #region Public Methods Providing Access to the Underlying Components to the Consumer

        /// <summary>
        /// Used to hide the Tooltip with a specific animation effect.
        /// </summary>
        /// <param name="animation">
        /// <see cref="Syncfusion.Blazor.Popups.TooltipAnimationSettings"/> settings for tooltip close action.
        /// </param>
        public async Task CloseAsync(TooltipAnimationSettings animation = null)
        {
            animation ??= new()
            {
                Delay = 0.0,
                Duration = 0.0,
                Effect = Effect.None
            };

            await sfTooltip.CloseAsync(animation);
        }

        /// <summary>
        /// Used to show the Tooltip on the specified target with the specified animation settings. 
        /// You can also pass the additional arguments like target element in which the tooltip should
        /// appear and animation settings for the tooltip open action.
        /// </summary>
        /// <param name="element">
        /// Target element in which the tooltip should appear.
        /// </param>
        /// <param name="animation">
        /// <see cref="Syncfusion.Blazor.Popups.AnimationModel"/> settings for the tooltip open action.
        /// </param>
        public async Task OpenAsync(ElementReference? element = null, TooltipAnimationSettings animation = null) => 
            await sfTooltip.OpenAsync(element, animation);

        /// <summary>
        /// Refresh the tooltip component when the target element is dynamically used.
        /// </summary>
        public async Task RefreshAsync() => await sfTooltip.RefreshAsync();

        /// <summary>
        /// Dynamically refreshes the tooltip element position based on the target element.
        /// </summary>
        /// <param name="target">
        /// The target element.
        /// </param>
        public async Task RefreshPositionAsync(ElementReference? target = null) => 
            await sfTooltip.RefreshPositionAsync(target);

        #endregion
    }
}
