using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Components;
using Syncfusion.Blazor.Notifications;

namespace Code420.UIOrchestrator.Server.Components.BaseComponents.ToastBase
{
    /// <summary>
    /// <para>
    /// Responsible for rendering a Toast element based on a minimal set of user-supplied parameters with
    /// the option to override the default settings of a wider array of parameters that provide
    /// fine-grained control over the look and feel of the Toast.
    /// </para>
    /// <para>
    /// Toast is a small, nonblocking notification pop-up and it is shown to users with readable message
    /// content at the bottom of the screen or at a specific target and disappears automatically after a f
    /// ew seconds (time-out) with different animation effects.
    /// </para>
    /// <para>
    /// The <see cref="OnClick"/> event callback handler is overridden by the component.
    /// If the consumer does not handle this event, the component will take care of closing the toast
    /// when clicked based on the <see cref="CloseToastOnClick"/> parameter
    /// </para>
    /// <para>
    /// The <see cref="OnOpen"/> event callback handler is overridden by the component.
    /// If the consumer does not handle this event, the component will use the markup in the
    /// <see cref="Content"/> or <see cref="ContentTemplate"/> parameter based on the
    /// <see cref="UseContentTemplate"/> parameter.
    /// </para>
    /// <remarks>
    /// The following parameters must be set:<br />
    /// <see cref="Content"/> or <see cref="ContentTemplate"/> -- Content of the toast<br />
    /// <see cref="Title"/> -- Title element of the toast<br />
    /// <see cref="CssClass"/> -- Provides CSS isolation for the toast<br />
    /// </remarks>
    /// <remarks>
    /// Consider setting the following parameters:<br />
    /// <see cref="Target"/> -- Specify the parent element for the toast,
    /// </remarks>
    /// </summary>
    [SuppressMessage("Usage", "BL0005:Component parameter should not be set outside of its component.")]
    public partial class ToastBase : ComponentBase
    {

        #region Component Parameters

        #region Base Parameters

        /// <summary>
        /// Integer value (in milliseconds) specifying the length of the animation when
        /// closing the toast.
        /// Default value is 500.
        /// </summary>
        [Parameter]
        public int AnimationHideDuration { get; set; } = 500;

        /// <summary>
        /// One of the <see cref="ToastEasing"/> enums that specifies the animation timing
        /// function applied while closing the toast.
        /// Default value is <see cref="ToastEasing.Ease"/>.
        /// </summary>
        [Parameter]
        public ToastEasing AnimationHideEasing { get; set; } = ToastEasing.Ease;

        /// <summary>
        /// One of the <see cref="ToastEffect"/> enums that specifies the animation name applied
        /// while closing the toast.
        /// Default value is <see cref="ToastEffect.FadeOut"/>.
        /// </summary>
        [Parameter]
        public ToastEffect AnimationHideEffect { get; set; } = ToastEffect.FadeOut;

        /// <summary>
        /// Integer value (in milliseconds) specifying the length of the animation when showing
        /// the toast.
        /// Default value is 500.
        /// </summary>
        [Parameter]
        public int AnimationShowDuration { get; set; } = 500;

        /// <summary>
        /// One of the <see cref="ToastEasing"/> enums that specifies the animation timing function
        /// applied while showing the toast.
        /// Default value is <see cref="ToastEasing.Ease"/>.
        /// </summary>
        [Parameter]
        public ToastEasing AnimationShowEasing { get; set; } = ToastEasing.Ease;

        /// <summary>
        /// One of the <see cref="ToastEffect"/> enums that specifies the animation name applied
        /// while showing the toast.
        /// Default value is <see cref="ToastEffect.FadeIn"/>.
        /// </summary>
        [Parameter]
        public ToastEffect AnimationShowEffect { get; set; } = ToastEffect.FadeIn;

        /// <summary>
        /// Boolean value indicating if the Toast should close when clicked by the user.
        /// Default value is false.
        /// </summary>
        [Parameter]
        public bool CloseToastOnClick { get; set; }

        /// <summary>
        /// String value that specifies the content to be displayed on the Toast. 
        /// Accepts selectors, string values and HTML elements.
        /// This parameter takes priority over the markup defined in the <see cref="ContentTemplate"/>.
        /// Use the <see cref="UseContentTemplate"/> parameter to override this prioritization.
        /// Default value is "No Content Defined".
        /// </summary>
        [Parameter]
        public string Content { get; set; } = "No Content Defined";

        /// <summary>
        /// A <see cref="RenderFragment"/> that specifies the content to be displayed on the Toast. 
        /// Accepts selectors, string values and HTML elements.
        /// This parameter subordinate to the markup defined in the <see cref="ContentTemplate"/>.
        /// Use the <see cref="UseContentTemplate"/> parameter to force use of the parameter's contents.
        /// Default value is a RenderFragment representing a "No Content Defined" message.
        /// </summary>
        [Parameter]
        public RenderFragment ContentTemplate { get; set; } = (builder) => { builder.AddMarkupContent(0, "No ContentTemplate Defined"); };

        /// <summary>
        /// Integer value specifying the Toast display time duration (in milliseconds) after interacting
        /// with the Toast. Note that this value is effective after any sort of user interaction, including
        /// hovering over the Toast.
        /// Default value is 0.
        /// </summary>
        [Parameter]
        public int ExtendedTimeout { get; set; }

        /// <summary>
        /// String value that specifies the CSS classes for an icon displayed at top left corner of the Toast.
        /// Default value is null.
        /// </summary>
        /// <remarks>
        /// Non-null value causes injection of e-toast-icon div
        /// </remarks>
        [Parameter]
        public string Icon { get; set; }

        /// <summary>
        /// Integer value containing the unique identifier for the Toast instance.
        /// Set when the toast is opened. 
        /// Can be used as parameter to HideKeyAsync method
        /// Default value is 0.
        /// </summary>
        [Parameter]
        public int Key { get; set; }

        /// <summary>
        /// Boolean value specifying the most recent Toast message is displayed above or below previous Toasts.
        /// Default value is false meaning newly added Toast will be added after old Toasts.
        /// Default value is <c>false</c>.
        /// </summary>
        [Parameter]
        public bool NewestOnTop { get; set; }

        /// <summary>
        /// String value specifying the horizontal position of the Toast message to be displayed in the
        /// target container. 
        /// X values are: Left , Right ,Center. 
        /// Default value is Right
        /// </summary>
        [Parameter]
        public string PositionX { get; set; } = "Right";

        /// <summary>
        /// String value specifying the vertical position of the Toast message to be displayed in the
        /// target container. 
        /// Y values are: Top , Bottom.
        /// Default value is Top
        /// </summary>
        [Parameter]
        public string PositionY { get; set; } = "Top";

        /// <summary>
        /// One of the <see cref="ProgressDirection"/> enums specifying the direction for the Toast
        /// progress bar. The parameter is applicable when <see cref="ShowProgressBar"/> is true and
        /// <see cref="Timeout"/> is non-zero.
        /// Default value is <see cref="ProgressDirection.LTR"/> (left to right).
        /// </summary>
        [Parameter]
        public ProgressDirection ProgressDirection { get; set; } = ProgressDirection.LTR;

        /// <summary>
        /// Boolean value specifying whether to show the close button in Toast message to close the Toast.
        /// Default value is false.
        /// </summary>
        [Parameter]
        public bool ShowCloseButton { get; set; }

        /// <summary>
        /// Boolean value specifying whether to show the progress bar to denote the Toast message
        /// display timeout. Displayed when the <see cref="Timeout"/> parameter is non-zero.
        /// Default value is false.
        /// </summary>
        /// <remarks>
        /// A value of <c>true</c> injects the e-toast-progress div
        /// </remarks>
        [Parameter]
        public bool ShowProgressBar { get; set; }

        /// <summary>
        /// String value specifying the CSS ID of the target container where the Toast to be displayed. 
        /// Based on the target, the positions such as Left, Top will be applied to the Toast.
        /// The ID must be prefixed with the '#' symbol (e.g., <c>#mybody</c>).
        /// The default value is null, which refers the <c>document.body</c> element.
        /// </summary>
        [Parameter]
        public string Target { get; set; }

        /// <summary>
        /// Integer value that specifies the Toast display time duration (in milliseconds) on the page.
        /// Once the time expires, Toast message will be removed.
        /// This parameter is in effect while no user interaction takes place, including hovering.
        /// The <see cref="ExtendedTimeout"/> comes into play as soon as the user interacts with the Toast.
        /// Setting 0 as a time out value displays the Toast on the page until the user closes it manually.
        /// Default value is 0;
        /// </summary>
        [Parameter]
        public int Timeout { get; set; }

        /// <summary>
        /// String value specifying the title to be displayed on the Toast. 
        /// Works only with string values.
        /// Default value is null.
        /// </summary>
        /// <remarks>
        /// Non-null value injects the <c>e-toast-title</c> div.
        /// </remarks>
        [Parameter]
        public string Title { get; set; }

        /// <summary>
        /// A <see cref="RenderFragment"/> containing one or more <see cref="ToastButton"/> definitions.
        /// Default value is null.
        /// </summary>
        [Parameter] 
        public RenderFragment ToastButtons { get; set; }

        /// <summary>
        /// Boolean value indicating the Toast should use the markup defined by the
        /// <see cref="ContentTemplate"/> parameter versus the markup defined by the
        /// <see cref="Content"/> parameter.
        /// The default value is false.
        /// </summary>
        [Parameter]
        public bool UseContentTemplate { get; set; }

        #endregion


        #region Event Callback Parameters

        /// <summary>
        /// Trigger the event after the Toast hides.
        /// </summary>
        [Parameter]
        public EventCallback<ToastCloseArgs> Closed { get; set; }

        /// <summary>
        /// An <see cref="EventCallback{TValue}"/> where TValue is an <see cref="object"/>.
        /// The consumer's method is invoked when the toast is created.
        /// </summary>
        [Parameter]
        public EventCallback<object> Created { get; set; }

        /// <summary>
        /// An <see cref="EventCallback{TValue}"/> where TValue is an <see cref="object"/>.
        /// The consumer's method is invoked when the toast is destroyed.
        /// </summary>
        [Parameter]
        public EventCallback<object> Destroyed { get; set; }

        /// <summary>
        /// The event will be fired while clicking on the Toast.
        /// </summary>
        [Parameter]
        public EventCallback<ToastClickEventArgs> OnClick { get; set; }

        /// <summary>
        /// Triggers the event before the toast close.
        /// </summary>
        [Parameter]
        public EventCallback<ToastBeforeCloseArgs> OnClose { get; set; }

        /// <summary>
        /// <see cref="EventCallback{TValue}"/> where TValue is <see cref="ToastBeforeOpenArgs"/>.
        /// The consumer's method is invoked before the Toast is opened.
        /// </summary>
        [Parameter]
        public EventCallback<ToastBeforeOpenArgs> OnOpen { get; set; }

        /// <summary>
        /// Triggers the event after the Toast shown on the target container.
        /// </summary>
        [Parameter]
        public EventCallback<ToastOpenArgs> Opened { get; set; }

        #endregion


        #region CSS Parameters

        /// <summary>
        /// String value containing CSS class definition(s) that will be injected in the root
        /// HTML div element of the listbox.
        /// Default value is string.Empty.
        /// </summary>
        /// <remarks>
        /// <c>e-toast-success</c>	Represents a positive toast<br />
        /// <c>e-toast-info</c> Represents an informative toast<br />
        /// <c>e-toast-warning</c> Represents a toast with caution<br />
        /// <c>e-toast-danger</c> Represents a negative toast
        /// </remarks>
        [Parameter]
        public string CssClass { get; set; } = string.Empty;

        /// <summary>
        /// String value containing CSS ID that will be injected in the root HTML div
        /// element (the container for all instances of this toast) of the Toast.
        /// Default value is string.Empty.
        /// </summary>
        [Parameter]
        public string CssId { get; set; } = string.Empty;

        /// <summary>
        /// String value that specifies the CSS <a href="https://developer.mozilla.org/en-US/docs/Web/CSS/height">height</a>
        /// value used for the toast.
        /// The height CSS property specifies the height of an element.
        /// Default value is auto
        /// </summary>
        [Parameter]
        public string Height { get; set; } = "auto";

        /// <summary>
        /// String value that specifies the CSS <a href="https://developer.mozilla.org/en-US/docs/Web/CSS/width">width</a>
        /// value used for the toast.
        /// The width CSS property sets an element's width.
        /// Default value is 300px
        /// </summary>
        [Parameter]
        public string Width { get; set; } = "300px";


        // ==================== Primary Toast Element Styles ====================


        /// <summary>
        /// String value that specifies the CSS <a href="https://developer.mozilla.org/en-US/docs/Web/CSS/font-size">font-size</a>
        /// value used for the base toast element.
        /// The font-size CSS property sets the size of the font.
        /// This is the base font size for the entire toast (=1em). Relative font sizes for child components are
        /// based on this font size.
        /// Default value is 14px.
        /// </summary>
        [Parameter]
        public string ToastFontSize { get; set; } = "14px";

        /// <summary>
        /// String value that specifies the CSS <a href="https://developer.mozilla.org/en-US/docs/Web/CSS/color">color</a>
        /// value used for the base toast element.
        /// The color CSS property sets the foreground color value of an element's text and
        /// text decorations.
        /// The color style is not directly applicable to the toast itself but is set here so
        /// child elements can inherit from it.
        /// Default value is #212529.
        /// </summary>
        [Parameter]
        public string ToastFontColor { get; set; } = "#212529";

        /// <summary>
        /// String value that specifies the CSS <a href="https://developer.mozilla.org/en-US/docs/Web/CSS/background">background</a>
        /// value used for the toast element.
        /// The background shorthand CSS property sets all background style properties at once, such
        /// as color, image, origin and size, or repeat method.
        /// This is the background for the entire toast element.
        /// Default value is: <c>rgba(255,255,255,0.85)</c> (background color).
        /// </summary>
        [Parameter]
        public string ToastBackground { get; set; } = "rgba(255,255,255,0.85)";

        /// <summary>
        /// String value that specifies the CSS <a href="https://developer.mozilla.org/en-US/docs/Web/CSS/border">border</a>
        /// value used for the toast element.
        /// The border shorthand CSS property sets an element's border. It sets the values of border-width,
        /// border-style, and border-color.
        /// This is the border around the entire toast element.
        /// Default value is: <c>1px solid rgba(0,0,0,0.1)</c>.
        /// </summary>
        [Parameter]
        public string ToastBorder { get; set; } = "1px solid rgba(0,0,0,0.1)";

        /// <summary>
        /// String value that specifies the CSS <a href="https://developer.mozilla.org/en-US/docs/Web/CSS/box-shadow">box-shadow</a>
        /// value used for the toast element.
        /// The box-shadow CSS property adds shadow effects around an element's frame. You can set
        /// multiple effects separated by commas. 
        /// A box shadow is described by X and Y offsets relative to the element, blur and spread radius,
        /// and color.
        /// This is the shadow around the entire toast element.
        /// Default value is: <c>0 4px 5px 0 rgba(0,0,0,0.1)</c>.
        /// </summary>
        [Parameter]
        public string ToastBoxShadow { get; set; } = "0 4px 5px 0 rgba(0,0,0,0.1)";

        /// <summary>
        /// String value that specifies the CSS <a href="https://developer.mozilla.org/en-US/docs/Web/CSS/border-radius">border-radius</a>
        /// value used for the toast element.
        /// The border-radius CSS property rounds the corners of an element's outer border edge.
        /// This is the border radius around the entire toast element.
        /// Default value is 4px.
        /// </summary>
        [Parameter]
        public string ToastBorderRadius { get; set; } = "4px";

        /// <summary>
        /// String value that specifies the CSS <a href="https://developer.mozilla.org/en-US/docs/Web/CSS/margin">margin</a>
        /// value used for the toast element.
        /// The margin CSS shorthand property sets the margin area on all four sides of an element.
        /// Typically the margin bottom is used to set the gap between toasts.
        /// Default value is: <c>0px 0px 10px 0px</c>.
        /// </summary>
        [Parameter]
        public string ToastMargin { get; set; } = "0px 0px 10px 0px";

        /// <summary>
        /// String value that specifies the CSS <a href="https://developer.mozilla.org/en-US/docs/Web/CSS/padding">padding</a>
        /// value used for the toast element.
        /// The padding CSS shorthand property sets the padding area on all four sides of an element at once.
        /// Default value is 12px.
        /// </summary>
        [Parameter]
        public string ToastPadding { get; set; } = "12px";


        // ==================== Progress Bar Styles ====================


        /// <summary>
        /// String value that specifies the CSS <a href="https://developer.mozilla.org/en-US/docs/Web/CSS/left">left</a>
        /// value used for the progress bar element.
        /// The left CSS property participates in specifying the horizontal position of a positioned element.
        /// Default value is 0px.
        /// </summary>
        [Parameter]
        public string ProgressBarLeft { get; set; } = "0px";

        /// <summary>
        /// String value that specifies the CSS <a href="https://developer.mozilla.org/en-US/docs/Web/CSS/bottom">bottom</a>
        /// value used for the progress bar element.
        /// The bottom CSS property participates in setting the vertical position of a positioned element.
        /// Default value is 0px.
        /// </summary>
        [Parameter]
        public string ProgressBarBottom { get; set; } = "0px";

        /// <summary>
        /// String value that specifies the CSS <a href="https://developer.mozilla.org/en-US/docs/Web/CSS/height">height</a>
        /// value used for the progress bar element.
        /// The height CSS property specifies the height of an element.
        /// Default value is 4px.
        /// </summary>
        [Parameter]
        public string ProgressBarHeight { get; set; } = "4px";

        /// <summary>
        /// String value that specifies the CSS <a href="https://developer.mozilla.org/en-US/docs/Web/CSS/background">background</a>
        /// value used for the progress bar element.
        /// The background shorthand CSS property sets all background style properties at once, such
        /// as color, image, origin and size, or repeat method.
        /// Default value is #007BFF (background color).
        /// </summary>
        [Parameter]
        public string ProgressBarBackground { get; set; } = "#007BFF";

        /// <summary>
        /// String value that specifies the CSS <a href="https://developer.mozilla.org/en-US/docs/Web/CSS/border-radius">border-radius</a>
        /// value used for the progress bar element.
        /// The border-radius CSS property rounds the corners of an element's outer border edge.
        /// This is the border radius around the entire progress bar element.
        /// Default value is 0px.
        /// </summary>
        [Parameter]
        public string ProgressBarBorderRadius { get; set; } = "0px";


        // ==================== Toast Icon Styles ====================


        /// <summary>
        /// String value that specifies the CSS <a href="https://developer.mozilla.org/en-US/docs/Web/CSS/font-size">font-size</a>
        /// value used for the icon element defined by the <see cref="Icon"/> parameter.
        /// The font-size CSS property sets the size of the font.
        /// Default value is 16px.
        /// </summary>
        [Parameter]
        public string ToastIconFontSize { get; set; } = "16px";

        /// <summary>
        /// String value that specifies the CSS <a href="https://developer.mozilla.org/en-US/docs/Web/CSS/color">color</a>
        /// value used for the icon element defined by the <see cref="Icon"/> parameter.
        /// The color CSS property sets the foreground color value of an element's text and text decorations.
        /// Default value is: <c>rgba(0,0,0,0.5)</c>.
        /// </summary>
        [Parameter]
        public string ToastIconFontColor { get; set; } = "rgba(0,0,0,0.5)";

        /// <summary>
        /// String value that specifies the CSS <a href="https://developer.mozilla.org/en-US/docs/Web/CSS/height">height</a>
        /// value used for the icon element defined by the <see cref="Icon"/> parameter.
        /// The height CSS property specifies the height of an element.
        /// This parameter is also used to set the width of the icon. 
        /// Note that actual size of the icon is dictated by the <see cref="ToastIconFontSize"/> parameter.
        /// Default value is 24px.
        /// </summary>
        [Parameter]
        public string ToastIconSize { get; set; } = "24px";

        /// <summary>
        /// String value that specifies the CSS <a href="https://developer.mozilla.org/en-US/docs/Web/CSS/margin">margin</a>
        /// value used for the icon element defined by the <see cref="Icon"/> parameter.
        /// The margin CSS shorthand property sets the margin area on all four sides of an element.
        /// Typically the margin right is used to set the gap between the icon and other toast elements.
        /// Default value is: <c>0px 8px 0px 0px</c>.
        /// </summary>
        [Parameter]
        public string ToastIconMargin { get; set; } = "0px 8px 0px 0px";


        // ==================== Toast Title Styles ====================


        /// <summary>
        /// String value that specifies the CSS <a href="https://developer.mozilla.org/en-US/docs/Web/CSS/font-size">font-size</a>
        /// value used for the title element defined by the <see cref="Title"/> parameter.
        /// The font-size CSS property sets the size of the font.
        /// Default value is 1em (based on <see cref="ToastFontSize"/> parameter).
        /// </summary>
        [Parameter]
        public string ToastTitleFontSize { get; set; } = "1em";

        /// <summary>
        /// String value that specifies the CSS <a href="https://developer.mozilla.org/en-US/docs/Web/CSS/color">color</a>
        /// value used for the title element defined by the <see cref="Title"/> parameter.
        /// The color CSS property sets the foreground color value of an element's text and text decorations.
        /// Default value is #6C757D.
        /// </summary>
        [Parameter]
        public string ToastTitleFontColor { get; set; } = "#6C757D";

        /// <summary>
        /// String value that specifies the CSS <a href="https://developer.mozilla.org/en-US/docs/Web/CSS/font-weight">font-weight</a>
        /// value used for the title element defined by the <see cref="Title"/> parameter.
        /// The font-weight CSS property sets the weight (or boldness) of the font.
        /// Default value is bold.
        /// </summary>
        [Parameter]
        public string ToastTitleFontWeight { get; set; } = "bold";

        /// <summary>
        /// String value that specifies the CSS <a href="https://developer.mozilla.org/en-US/docs/Web/CSS/letter-spacing">letter-spacing</a>
        /// value used for the title element defined by the <see cref="Title"/> parameter.
        /// The letter-spacing CSS property sets the horizontal spacing behavior between text characters.
        /// Default value is 0.5px.
        /// </summary>
        [Parameter]
        public string ToastTitleLetterSpacing { get; set; } = ".5px";

        /// <summary>
        /// String value that specifies the CSS <a href="https://developer.mozilla.org/en-US/docs/Web/CSS/padding">padding</a>
        /// value used for the first title element defined by the <see cref="Content"/> parameter.
        /// The padding CSS shorthand property sets the padding area on all four sides of an element at once.
        /// Default value is 0px.
        /// </summary>
        [Parameter]
        public string ToastTitlePaddingFirstChild { get; set; } = "0px";


        // ==================== Toast Content Styles ====================


        /// <summary>
        /// String value that specifies the CSS <a href="https://developer.mozilla.org/en-US/docs/Web/CSS/font-size">font-size</a>
        /// value used for the content element defined by the <see cref="Content"/> parameter.
        /// The font-size CSS property sets the size of the font.
        /// Default value is 1em (based on <see cref="ToastFontSize"/> parameter).
        /// </summary>
        [Parameter]
        public string ToastContentFontSize { get; set; } = "1em";

        /// <summary>
        /// String value that specifies the CSS <a href="https://developer.mozilla.org/en-US/docs/Web/CSS/color">color</a>
        /// value used for the content element defined by the <see cref="Content"/> parameter.
        /// The color CSS property sets the foreground color value of an element's text and text decorations.
        /// Default value is #212529.
        /// </summary>
        [Parameter]
        public string ToastContentFontColor { get; set; } = "#212529";

        /// <summary>
        /// String value that specifies the CSS <a href="https://developer.mozilla.org/en-US/docs/Web/CSS/font-weight">font-weight</a>
        /// value used for the content element defined by the <see cref="Content"/> parameter.
        /// The font-weight CSS property sets the weight (or boldness) of the font.
        /// Default value is normal.
        /// </summary>
        [Parameter]
        public string ToastContentFontWeight { get; set; } = "normal";

        /// <summary>
        /// String value that specifies the CSS <a href="https://developer.mozilla.org/en-US/docs/Web/CSS/letter-spacing">letter-spacing</a>
        /// value used for the content element defined by the <see cref="Content"/> parameter.
        /// The letter-spacing CSS property sets the horizontal spacing behavior between text characters.
        /// Default value is 0px.
        /// </summary>
        [Parameter]
        public string ToastContentLetterSpacing { get; set; } = "0px";

        /// <summary>
        /// String value that specifies the CSS <a href="https://developer.mozilla.org/en-US/docs/Web/CSS/padding">padding</a>
        /// value used for the content element defined (between the first and last) by the
        /// <see cref="Content"/> parameter.
        /// The padding CSS shorthand property sets the padding area on all four sides of an element at once.
        /// Default value is: <c>10px 0px 5px 0px</c>.
        /// </summary>
        [Parameter]
        public string ToastContentPadding { get; set; } = "10px 0px 5px 0px";

        /// <summary>
        /// String value that specifies the CSS <a href="https://developer.mozilla.org/en-US/docs/Web/CSS/padding">padding</a>
        /// value used for the first content element defined by the <see cref="Content"/> parameter.
        /// The padding CSS shorthand property sets the padding area on all four sides of an element at once.
        /// Default value is 0px.
        /// </summary>
        [Parameter]
        public string ToastContentPaddingFirstChild { get; set; } = "0px";

        /// <summary>
        /// String value that specifies the CSS <a href="https://developer.mozilla.org/en-US/docs/Web/CSS/padding">padding</a>
        /// value used for the last content element defined by the <see cref="Content"/> parameter.
        /// The padding CSS shorthand property sets the padding area on all four sides of an element at once.
        /// Default value is: <c>10px 0px 5px 0px</c>.
        /// </summary>
        [Parameter]
        public string ToastContentPaddingLastChild { get; set; } = "10px 0px 5px 0px";


        // ==================== Toast Close Button Styles ====================


        /// <summary>
        /// String value that specifies the CSS <a href="https://developer.mozilla.org/en-US/docs/Web/CSS/font-size">font-size</a>
        /// value used for the close button element (if enabled by the <see cref="ShowCloseButton"/> parameter).
        /// The font-size CSS property sets the size of the font.
        /// Default value is 10px.
        /// </summary>
        [Parameter]
        public string ToastCloseButtonFontSize { get; set; } = "10px";

        /// <summary>
        /// String value that specifies the CSS <a href="https://developer.mozilla.org/en-US/docs/Web/CSS/color">color</a>
        /// value used for the close button element (if enabled by the <see cref="ShowCloseButton"/> parameter).
        /// The color CSS property sets the foreground color value of an element's text and text decorations.
        /// Default value is: <c>rgba(0,0,0,0.5)</c>.
        /// </summary>
        [Parameter]
        public string ToastCloseButtonFontColor { get; set; } = "rgba(0,0,0,0.5)";

        /// <summary>
        /// String value that specifies the CSS <a href="https://developer.mozilla.org/en-US/docs/Web/CSS/height">height</a>
        /// value used for the close button element (if enabled by the <see cref="ShowCloseButton"/> parameter).
        /// The height CSS property specifies the height of an element.
        /// This parameter is also used to set the width of the close button. 
        /// Default value is 24px.
        /// </summary>
        [Parameter]
        public string ToastCloseButtonSize { get; set; } = "24px";

        /// <summary>
        /// String value that specifies the CSS <a href="https://developer.mozilla.org/en-US/docs/Web/CSS/margin">margin</a>
        /// value used for the close button element (if enabled by the <see cref="ShowCloseButton"/> parameter).
        /// The margin CSS shorthand property sets the margin area on all four sides of an element.
        /// Typically the margin left is used to set the gap between the close button and other toast elements.
        /// Default value is: <c>0px 0px 0px auto</c>.
        /// </summary>
        [Parameter]
        public string ToastCloseMargin { get; set; } = "0px 0px 0px auto";

        /// <summary>
        /// String value that specifies the CSS <a href="https://developer.mozilla.org/en-US/docs/Web/CSS/background">background</a>
        /// value used for the close button element (if enabled by the <see cref="ShowCloseButton"/> parameter).
        /// The background shorthand CSS property sets all background style properties at once, such as color,
        /// image, origin and size, or repeat method.
        /// Default value is transparent (background color).
        /// </summary>
        [Parameter]
        public string ToastCloseButtonBackground { get; set; } = "transparent";

        /// <summary>
        /// String value that specifies the CSS <a href="https://developer.mozilla.org/en-US/docs/Web/CSS/border">border</a>
        /// value used for the close button element (if enabled by the <see cref="ShowCloseButton"/> parameter).
        /// The border shorthand CSS property sets an element's border. It sets the values of border-width,
        /// border-style, and border-color.
        /// Default value is none.
        /// </summary>
        [Parameter]
        public string ToastCloseButtonBorder { get; set; } = "none";

        /// <summary>
        /// String value that specifies the CSS <a href="https://developer.mozilla.org/en-US/docs/Web/CSS/box-shadow">box-shadow</a>
        /// value used for the close button element (if enabled by the <see cref="ShowCloseButton"/> parameter).
        /// The box-shadow CSS property adds shadow effects around an element's frame. You can set
        /// multiple effects separated by commas. 
        /// A box shadow is described by X and Y offsets relative to the element, blur and spread radius, and color.
        /// Default value is none.
        /// </summary>
        [Parameter]
        public string ToastCloseButtonBoxShadow { get; set; } = "none";

        /// <summary>
        /// String value that specifies the CSS <a href="https://developer.mozilla.org/en-US/docs/Web/CSS/background">background</a>
        /// value used for the close button element when hovered (if enabled by the <see cref="ShowCloseButton"/> parameter).
        /// The background shorthand CSS property sets all background style properties at once, such as
        /// color, image, origin and size, or repeat method.
        /// Default value is transparent (background color).
        /// </summary>
        [Parameter]
        public string ToastCloseButtonHoverBackground { get; set; } = "transparent";

        /// <summary>
        /// String value that specifies the CSS <a href="https://developer.mozilla.org/en-US/docs/Web/CSS/border">border</a>
        /// value used for the close button element when hovered (if enabled by the <see cref="ShowCloseButton"/> parameter).
        /// The border shorthand CSS property sets an element's border. It sets the values of border-width,
        /// border-style, and border-color.
        /// Default value is none.
        /// </summary>
        [Parameter]
        public string ToastCloseButtonHoverBorder { get; set; } = "none";

        /// <summary>
        /// String value that specifies the CSS <a href="https://developer.mozilla.org/en-US/docs/Web/CSS/box-shadow">box-shadow</a>
        /// value used for the close button element when hovered (if enabled by the <see cref="ShowCloseButton"/> parameter).
        /// The box-shadow CSS property adds shadow effects around an element's frame. You can set multiple effects separated by commas. 
        /// A box shadow is described by X and Y offsets relative to the element, blur and spread radius, and color.
        /// Default value is none.
        /// </summary>
        [Parameter]
        public string ToastCloseButtonHoverBoxShadow { get; set; } = "none";

        #endregion

        #endregion


        #region Callback Events Invoked from Underlying Components

        private async Task OnClickHandler(ToastClickEventArgs args)
        {
            // If the user defined the OnClick callback, defer all actions to them.
            // Otherwise, set the ClickToClose field in args to CloseToastOnClick parameter.
            if (OnClick.HasDelegate) await OnClick.InvokeAsync(args);
            else args.ClickToClose = CloseToastOnClick;
        }

        private async Task OnOpenHandler(ToastBeforeOpenArgs args)
        {
            // If the user defined the OnOpen callback, defer all actions to them.
            if (OnOpen.HasDelegate)
            {
                await OnOpen.InvokeAsync(args);
                return;
            }

            // Otherwise...
            // Prioritize use of Content/ContentTemplate markup
            if ((UseContentTemplate) && (ContentTemplate is not null))
            {
                args.Options.ContentTemplate = ContentTemplate;
                args.Options.Content = null;
            }
            else
            {
                args.Options.ContentTemplate = null;
                args.Options.Content = Content;
            }

            // Set the Key value
            args.Options.Key = Key;
        }

        #endregion


        #region Instance Variables

        private SfToast toast;
        private string masterCssSelector = string.Empty;
        private string masterCssIdSelector = string.Empty;
        private ToastAnimationSettings animationSettings;

        #endregion


        #region Constructors

        protected override void OnParametersSet()
        {
            // Build the master selectors
            masterCssSelector = (CssClass == string.Empty) ? ".e-toast" : $".{ CssClass }.e-toast";
            masterCssIdSelector = (CssId == string.Empty) ? ".e-toast-container" : $"#{ CssId }.e-toast-container";

            // Set the ToastAnimationSettings model
            animationSettings = new ToastAnimationSettings
            {
                HideSettings = new ToastHideAnimationSettings()
                {
                    Duration = AnimationHideDuration,
                    Easing = AnimationHideEasing,
                    Effect = AnimationHideEffect
                },

                ShowSettings = new ToastShowAnimationSettings()
                {
                    Duration = AnimationShowDuration,
                    Easing = AnimationShowEasing,
                    Effect = AnimationShowEffect
                }
            };
        }

        #endregion


        #region Public Methods Providing Access to the Underlying Components to the Consumer

        /// <summary>
        /// Responsible for displaying the Toast element.
        /// </summary>
        /// <param name="toastModel">
        /// Optionally accepts a <see cref="ToastModel"/> parameter in which all Toast parameters are defined.
        /// </param>
        /// <remarks>
        /// Using the ToastModel is for clean-slate approaches. The base model seems to be pre-populated
        /// with values that are not reflective of this component's parameters. 
        /// If you need to dynamically update parameters it is better to override the 
        /// OnOpenHandler whose args.Options properties are reflective of this component's
        /// parameters, and then update the ones needed.
        /// </remarks>
        public async Task ShowAsync(ToastModel toastModel = null) => 
            await toast.ShowAsync(toastModel);

        /// <summary>
        /// Responsible for hiding the Toast element.
        /// </summary>
        public async Task HideAsync() =>await toast.HideAsync();

        /// <summary>
        /// Responsible for hiding the Toast element associated with the key parameter.
        /// </summary>
        /// <param name="toastKey">
        /// Integer value identifying the Toast to be hidden.
        /// </param>
        public async Task HideKeyAsync(int toastKey) => await toast.HideAsync(key: toastKey);

        /// <summary>
        /// Responsible for hiding all open Toast elements.
        /// </summary>
        public async Task HideAllAsync() => await toast.HideAsync("All");

        #endregion

    }
}
