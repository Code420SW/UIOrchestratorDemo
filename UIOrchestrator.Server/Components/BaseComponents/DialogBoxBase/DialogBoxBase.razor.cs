using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Syncfusion.Blazor.Popups;
using DragEventArgs = Syncfusion.Blazor.Popups.DragEventArgs;

namespace Code420.UIOrchestrator.Server.Components.BaseComponents.DialogBoxBase
{
    /// <summary>
    /// <para>
    /// Responsible for rendering a Dialog Box element based on a minimal set of user-supplied 
    /// parameters with the option to override the default settings using a wider array of parameters 
    /// that provide fine-grained control over the look and feel of the element.
    /// </para>
    /// <para>
    /// The two types of Dialog Box elements available are modal and modeless. Modeless dialog boxes prevent the user
    /// from interacting with other application elements while the dialog box is rendered.
    /// </para>
    /// <para>
    /// The Dialog Box provides three primary display elements: the header, body and footer. All elements
    /// are optional. The contents of each element is defined by the <see cref="HeaderFragment"/>,
    /// <see cref="ContentFragment"/> and <see cref="FooterFragment"/> parameters. These parameters can be true
    /// <see cref="RenderFragment"/> objects or simple string values.
    /// </para>
    /// <para>
    /// The <see cref="ShowCloseIcon"/> parameter specified if a close icon is displayed in the header element
    /// which, when clicked, will dismiss the dialog box.
    /// </para>
    /// <para>
    /// The <see cref="AnimationEffect"/>, <see cref="AnimationDelay"/> and <see cref="AnimationDuration"/>
    /// parameters allow you to select one of the preset animation effect (<see cref="DialogEffect"/> and
    /// specify the delay and duration associated with the effect.
    /// </para>
    /// <para>
    /// The <see cref="DialogButtons"/> allows you to define one or more buttons displayed in the footer
    /// element of the dialog box. Note that <see cref="FooterFragment"/> and <see cref="DialogButtons"/>
    /// parameters are mutually exclusive with the FooterFragment having priority over the DialogButtons.
    /// </para>
    /// <remarks>
    /// The following parameters must be set:<br />
    /// <see cref="CssClass"/> -- Provides CSS isolation for the dialog box<br />
    /// <see cref="HeaderFragment"/> -- Defines the content of the dialog box header element<br />
    /// <see cref="ContentFragment"/> -- Defines the content of the dialog box body element<br />
    /// <see cref="FooterFragment"/> -- Defines the content of the dialog box footer element
    /// </remarks>
    /// </summary>
    
    public partial class DialogBoxBase : ComponentBase
    {
        // TODO: May be able to improve the overlay functionality
        //          by using box-shadow with 100vh and 100vw
        //          https://www.youtube.com/watch?v=TZRSXNc0T1k

        #region Component Parameters
        
        #region Base Parameters

        /// <summary>
        /// Boolean value specifying whether the dialog component can be dragged by the user. 
        /// The dialog allows a user to drag by selecting the header and dragging it for
        /// re-positioning the dialog.
        /// Default value is <c>true</c>.
        /// </summary>
        [Parameter]
        public bool AllowDragging { get; set; } = true;

        /// <summary>
        /// Boolean value specifying whether the Dialog element re-renders or not when the
        /// Dialog gets open. When disabled (<c>false</c>), the Dialog component DOM element
        /// will be destroyed when closing and re-rendered when the dialog DOM element is opened. 
        /// Otherwise, the dialog will be shown when opening it and remain hidden while closed.
        /// Default value is false.
        /// </summary>
        [Parameter]
        public bool AllowPrerender { get; set; }

        /// <summary>
        /// A <see cref="DialogEffect" /> enum member that specifies the type of animation effects
        /// used when opening and closing the dialog box.
        /// Default value is <see cref="DialogEffect.FadeZoom" />.
        /// </summary>
        [Parameter]
        public DialogEffect AnimationEffect { get; set; } = DialogEffect.FadeZoom;

        /// <summary>
        /// Double value specifying the delay (in milliseconds) before the animation effect begins.
        /// Default value is <c>0</c>.
        /// </summary>
        [Parameter]
        public double AnimationDelay { get; set; }

        /// <summary>
        /// Double value specifying the amount of time (in millisecond) in which the animation
        /// effect will take place.
        /// The default value is <c>400</c>.
        /// </summary>
        [Parameter]
        public double AnimationDuration { get; set; } = 400.0;

        /// <summary>
        /// A <see cref="RenderFragment" /> composing the content section of the dialog box.
        /// The default value is null.
        /// </summary>
        [Parameter]
        public RenderFragment ContentFragment { get; set; }

        /// <summary>
        /// Boolean value specifying if the dialog box is closed when the user presses the Escape key. 
        /// Default value is <c>true</c>.
        /// </summary>
        [Parameter]
        public bool CloseOnEscape { get; set; } = true;
        
        /// <summary>
        /// A <see cref="RenderFragment"/> containing one or more <see cref="DialogButton"/> definitions.
        /// All dialog buttons are rendered in the footer section of the dialog box.
        /// Default value is null.
        /// <remarks>
        /// Note that use of this parameter and <see cref="FooterFragment"/> are mutually exclusive.
        /// If the FooterFragment parameter is non-null the dialog buttons will not be rendered.
        /// </remarks>
        /// </summary>
        [Parameter] 
        public RenderFragment DialogButtons { get; set; }

        /// <summary>
        /// Boolean value specifying whether the dialog component can be resized by the user. 
        /// If <c>true</c>, the dialog component creates a grip to resize it in a diagonal direction.
        /// In addition, the borders specified in the <see cref="ResizeHandles"/> parameter
        /// can be individually resized.
        /// Default value is <c>false</c>.
        /// </summary>
        [Parameter]
        public bool EnableResize { get; set; }
        
        /// <summary>
        /// Boolean value indicating of the dialog box contents are rendered right-to-left
        /// direction (<c>true</c>).
        /// Default value is <c>false</c>.
        /// </summary>
        [Parameter]
        public bool EnableRtl { get; set; }

        /// <summary>
        /// A <see cref="RenderFragment" /> composing the footer section of the dialog box. 
        /// The default value is null.
        /// <remarks>
        /// Note that use of this parameter and <see cref="DialogButtons"/> are mutually exclusive.
        /// If this parameter is non-null the dialog buttons will not be rendered.
        /// </remarks>
        /// </summary>
        [Parameter]
        public RenderFragment FooterFragment { get; set; }

        /// <summary>
        /// A <see cref="RenderFragment" /> composing the header section of the dialog box. 
        /// The default value is null.
        /// </summary>
        [Parameter]
        public RenderFragment HeaderFragment { get; set; }

        /// <summary>
        /// Boolean value specifying whether the dialog can be displayed as modal or non-modal.<br /> 
        /// Modal: Creates an overlay that disables interaction with the parent application and
        /// the user must close the dialog before continuing with other applications.<br /> 
        /// Modeless: Does not prevent user interaction with the parent application.<br />
        /// Default value is <c>false</c>.
        /// </summary>
        [Parameter]
        public bool IsModal { get; set; }

        /// <summary>
        /// Boolean value indicating if the dialog box's position is maintained when the
        /// user scrolls the main window.
        /// Has no effect if <see cref="IsModal"/> parameter is set to <c>true</c>.
        /// Default value is <c>false</c>.
        /// </summary>
        [Parameter]
        public bool MaintainPositionOnScroll { get; set; }

        /// <summary>
        /// String value specifying the left offset of the dialog box. Can either be a
        /// numeric value specifying the number of pixels the left edge of the dialog box
        /// is offset from the left side of the parent element (see <see cref="Target"/>).
        /// One of the predefined positioning values can be used. Valid values are: <c>left</c>,
        /// <c>center</c> or <c>right</c>. 
        /// The default value is <c>center</c>.
        /// </summary>
        [Parameter]
        public string PositionX { get; set; } = "center";

        /// <summary>
        /// String value specifying the top offset of the dialog box. Can either be a
        /// numeric value specifying the number of pixels the top edge of the dialog box
        /// is offset from the top of the parent element (see <see cref="Target"/>).
        /// One of the predefined positioning values can be used. Valid values are: <c>top</c>,
        /// <c>center</c> or <c>bottom</c>. 
        /// The default value is <c>center</c>.
        /// </summary>
        [Parameter]
        public string PositionY { get; set; } = "center";

        /// <summary>
        /// An array containing one or more <see cref="ResizeDirection"/> enum members specifying
        /// the available resize handles for the dialog box. Applicable when <see cref="EnableResize"/>
        /// parameter is set to <c>true</c>.
        /// The default value is <see cref="ResizeDirection.All"/>.
        /// </summary>
        [Parameter]
        public ResizeDirection[] ResizeHandles { get; set; }

        /// <summary>
        /// Boolean value indicating if the close (X) icon is shown in the upper right corner of
        /// header section in the dialog box.
        /// Default value is <c>true</c>.
        /// </summary>
        [Parameter]
        public bool ShowCloseIcon { get; set; } = true;

        /// <summary>
        /// String value containing the CSS Id of the dialog box's parent. 
        /// The dialog box will be rendered completely within the HTML element associated with
        /// the CSS Id. The Target parameter should be a proper CSS Id selector (e.g., #target-id).
        /// Default value is <c>null</c> indicating the target element is the HTML <c>body</c> element.
        /// </summary>
        [Parameter]
        public string Target { get; set; }

        /// <summary>
        /// Boolean value indicating if the dialog box is displayed.
        /// Default value is <c>false</c>.
        /// </summary>
        [Parameter]
        public bool Visible { get; set; }
        
        /// <summary>
        /// Double value specifying the z-order for rendering that determines whether the
        /// dialog is displayed in front or behind of another component.
        /// Default value is <c>1000.0</c>.
        /// </summary>
        [Parameter]
        public double ZIndex { get; set; } = 1000.0;

        #endregion

        #region Event Callback Parameters

        /// <summary>
        /// An <see cref="EventCallback"/> method invoked when the dialog box is closed.
        /// </summary>
        [Parameter]
        public EventCallback<CloseEventArgs> Closed { get; set; }

        /// <summary>
        /// An <see cref="EventCallback"/> method invoked when the dialog box is created.
        /// </summary>
        [Parameter]
        public EventCallback<object> Created { get; set; }

        /// <summary>
        /// An <see cref="EventCallback"/> method invoked when the dialog box is destroyed.
        /// </summary>
        [Parameter]
        public EventCallback<object> Destroyed { get; set; }

        /// <summary>
        /// An <see cref="EventCallback"/> method invoked before the dialog is closed. 
        /// If you cancel this event, the dialog remains opened.
        /// </summary>
        [Parameter]
        public EventCallback<BeforeCloseEventArgs> OnClose { get; set; }

        /// <summary>
        /// An <see cref="EventCallback"/> method invoked when the user drags the dialog box.
        /// </summary>
        [Parameter]
        public EventCallback<DragEventArgs> OnDrag { get; set; }

        /// <summary>
        /// An <see cref="EventCallback"/> method invoked when the user starts dragging the dialog box.
        /// </summary>
        [Parameter]
        public EventCallback<DragStartEventArgs> OnDragStart { get; set; }

        /// <summary>
        /// An <see cref="EventCallback"/> method invoked when the user stops dragging the dialog box.
        /// </summary>
        [Parameter]
        public EventCallback<DragStopEventArgs> OnDragStop { get; set; }

        /// <summary>
        /// An <see cref="EventCallback"/> method invoked when the dialog box is about to open.
        /// If you cancel this event, the dialog remains closed.
        /// </summary>
        [Parameter]
        public EventCallback<BeforeOpenEventArgs> OnOpen { get; set; }

        /// <summary>
        /// An <see cref="EventCallback"/> method called when the dialog box's overlay is clicked.
        /// </summary>
        [Parameter]
        public EventCallback<OverlayModalClickEventArgs> OnOverlayModalClick { get; set; }

        /// <summary>
        /// An <see cref="EventCallback"/> method invoked when the user starts resizing the dialog box.
        /// </summary>
        [Parameter]
        public EventCallback<MouseEventArgs> OnResizeStart { get; set; }

        /// <summary>
        /// An <see cref="EventCallback"/> method invoked when the user stops resizing the dialog box.
        /// </summary>
        [Parameter]
        public EventCallback<MouseEventArgs> OnResizeStop { get; set; }

        /// <summary>
        /// An <see cref="EventCallback"/> method invoked when the dialog box is opened.
        /// </summary>
        [Parameter]
        public EventCallback<OpenEventArgs> Opened { get; set; }

        /// <summary>
        /// An <see cref="EventCallback"/> method invoked when the user resizes the dialog.
        /// </summary>
        [Parameter]
        public EventCallback<MouseEventArgs> Resizing { get; set; }
        
        /// <summary>
        /// An <see cref="EventCallback"/> method invoked when the visibility of the dialog
        /// box changes..
        /// </summary>
        [Parameter]
        public EventCallback<bool> VisibleChanged { get; set; }

        #endregion

        #region CSS Parameters

        // TODO: (MEDIUM) Add support for CSS max-height and min/max-width since the user may be allowed to resize the dialog box.

        /// <summary>
        /// String value containing the CSS ID that will be injected in the HTML.
        /// Default value is string.Empty.
        /// </summary>
        [Parameter]
        public string CssId { get; set; } = string.Empty;

        /// <summary>
        /// String value containing CSS class definition(s) that will be injected in the HTML.
        /// Default value is string.Empty.
        /// </summary>
        [Parameter]
        public string CssClass { get; set; } = string.Empty;

        /// <summary>
        /// String value that specifies the CSS <a href="https://developer.mozilla.org/en-US/docs/Web/CSS/height">height</a> of the dialog box.
        /// The height CSS property specifies the height of an element.
        /// Default value is <c>300px</c>.
        /// </summary>
        [Parameter]
        public string Height { get; set; } = "300px";

        /// <summary>
        /// String value that specifies the CSS <a href="https://developer.mozilla.org/en-US/docs/Web/CSS/min-height">min-height</a> value of the dialog box.
        /// The min-height CSS property sets the minimum height of an element. It prevents the used value of the height property from becoming smaller than 
        /// the value specified for min-height.
        /// Default value is <c>100px</c>.
        /// </summary>
        [Parameter]
        public string MinHeight { get; set; } = "100px";

        /// <summary>
        /// String value that specifies the CSS <a href="https://developer.mozilla.org/en-US/docs/Web/CSS/width">width</a> of the dialog box.
        /// The width CSS property sets an element's width.
        /// Default value is <c>300px</c>.
        /// </summary>
        [Parameter]
        public string Width { get; set; } = "300px";

        /// <summary>
        /// String value that specifies the CSS <a href="https://developer.mozilla.org/en-US/docs/Web/CSS/background-color">background-color</a>
        /// value used to render the overlay of the dialog box.
        /// The background-color CSS property sets the background color of an element.
        /// Applicable when the <see cref="IsModal"/> parameter is set to true. 
        /// The default value is <c>slategray</c>.
        /// </summary>
        [Parameter]
        public string OverlayBackgroundColor { get; set; } = "slategray";

        /// <summary>
        /// String value that specifies the CSS <a href="https://developer.mozilla.org/en-US/docs/Web/CSS/opacity">opacity</a>
        /// value of dialog box's overlay.
        /// The opacity CSS property sets the opacity of an element. Opacity is the degree to which content behind an element is hidden,
        /// and is the opposite of transparency.
        /// Applicable when the <see cref="IsModal"/> parameter is set to true.  
        /// The value must be between 0.0 and 1.0. 
        /// The default value is <c>0.6</c>.
        /// </summary>
        [Parameter]
        public string OverlayOpacity { get; set; } = "0.6";

        /// <summary>
        /// String value that specifies the CSS <a href="https://developer.mozilla.org/en-US/docs/Web/CSS/font-size">font-size</a>
        /// value used to render the close button in the header portion of the dialog box.
        /// The font-size CSS property sets the size of the font.
        /// It is recommended that pixel units are used since the parent element is not directly addressable so relative units (em)
        /// are not related to any addressable content style.
        /// The default value is <c>14px</c>.
        /// </summary>
        [Parameter]
        public string CloseButtonFontSize { get; set; } = "14px";

        /// <summary>
        /// String value that specifies the CSS <a href="https://developer.mozilla.org/en-US/docs/Web/CSS/color">color</a>
        /// value used to render the close button in the header portion of the dialog box.
        /// The color CSS property sets the foreground color value of an element's text and text decorations, and sets the
        /// currentcolor value. 
        /// The default value is <c>black</c>.
        /// </summary>
        [Parameter]
        public string CloseButtonColor { get; set; } = "black";

        /// <summary>
        /// String value that specifies the CSS <a href="https://developer.mozilla.org/en-US/docs/Web/CSS/font-size">font-size</a>
        /// value used to render the text in the the dialog box.
        /// Sets the base font size for the entire dialog box container which means that subordinate elements can use relative font
        /// sizes (e.g., em) for both the section font sizes (e.g., <see cref="HeaderFontSize"/> and <see cref="ContentFontSize"/>)
        /// as well as the associated RenderFragments (e.g., <see cref="HeaderFragment"/>, <see cref="ContentFragment"/> and
        /// <see cref="FooterFragment"/>)
        /// The default value is <c>12px</c>.
        /// </summary>
        [Parameter]
        public string DialogBaseFontSize { get; set; } = "12px";

        /// <summary>
        /// String value that specifies the CSS <a href="https://developer.mozilla.org/en-US/docs/Web/CSS/border">border</a>
        /// value set used for the dialog box border.
        /// The border shorthand CSS property sets an element's border. It sets the values of border-width, border-style,
        /// and border-color.
        /// Default value is <c>1px solid rgba(0, 0, 0, 0.2)</c>.
        /// </summary>
        [Parameter]
        public string DialogBorder { get; set; } = "1px solid rgba(0, 0, 0, 0.2)";

        /// <summary>
        /// String value that specifies the CSS <a href="https://developer.mozilla.org/en-US/docs/Web/CSS/border-radius">border-radius</a>
        /// value used for the dialog box border radius.
        /// The border-radius CSS property rounds the corners of an element's outer border edge.
        /// Default value is <c>3px</c>.
        /// <remarks>
        /// For visual consistency the radii defined in the <see cref="HeaderBorderRadius"/>
        /// and <see cref="FooterBorderRadius"/> should match this parameter.
        /// </remarks>
        /// </summary>
        [Parameter]
        public string DialogBorderRadius { get; set; } = "3px";

        /// <summary>
        /// String value that specifies the CSS <a href="https://developer.mozilla.org/en-US/docs/Web/CSS/box-shadow">box-shadow</a>
        /// value used for the dialog box.
        /// The box-shadow CSS property adds shadow effects around an element's frame. 
        /// You can set multiple effects separated by commas. A box shadow is described by X and Y offsets relative to the element,
        /// blur and spread radius, and color.
        /// Default value is: <c>0 5px 15px rgba(0,0,0,0.5)</c>.
        /// </summary>
        [Parameter]
        public string DialogBoxShadow { get; set; } = "0 5px 15px rgba(0,0,0,0.5)";

        /// <summary>
        /// String value that specifies a CSS <a href= "https://developer.mozilla.org/en-US/docs/Web/CSS/color">color</a>
        /// value used to render the text in the header section of the dialog box.
        /// The color CSS property sets the foreground color value of an element's text and text decorations, and sets the
        /// currentcolor value.
        /// The default value is <c>black</c>.
        /// </summary>
        [Parameter]
        public string HeaderFontColor { get; set; } = "black";

        /// <summary>
        /// String value that specifies the CSS <a href= "https://developer.mozilla.org/en-US/docs/Web/CSS/font-size">font-size</a>
        /// value used to render the text in the the header section of the dialog box.
        /// Sets the base font size for the entire header section which means that subordinate elements can use relative font sizes
        /// (e.g., em) for the <see cref="HeaderFragment"/>.
        /// The default value is <c>1.25em</c> (relative to the <see cref="DialogBaseFontSize"/> parameter)
        /// </summary>
        [Parameter]
        public string HeaderFontSize { get; set; } = "1.25em";

        /// <summary>
        /// String value that specifies the CSS <a href= "https://developer.mozilla.org/en-US/docs/Web/CSS/font-weight">font-weight</a>
        /// value used to render the text in the header section of the dialog box.
        /// The font-weight CSS property sets the weight (or boldness) of the font. The weights available depend on the font-family
        /// that is currently set.
        /// The default value is <c>normal</c>.
        /// </summary>
        [Parameter]
        public string HeaderFontWeight { get; set; } = "normal";

        /// <summary>
        /// String value that specifies the CSS <a href= "https://developer.mozilla.org/en-US/docs/Web/CSS/line-height">line-height</a>
        /// value applied to the header section of the dialog box.
        /// The line-height CSS property sets the height of a line box. It's commonly used to set the distance between lines of text.
        /// The default value is <c>normal</c>.
        /// </summary>
        [Parameter]
        public string HeaderLineHeight { get; set; } = "normal";

        /// <summary>
        /// String value that specifies a CSS <a href= "https://developer.mozilla.org/en-US/docs/Web/CSS/background-color">background-color</a>
        /// value used to render the background in the header section of the dialog box.
        /// The background-color CSS property sets the background color of an element.
        /// The default value is <c>transparent</c>.
        /// </summary>
        [Parameter]
        public string HeaderBackgroundColor { get; set; } = "transparent";

        /// <summary>
        /// String containing the CSS <a href="https://developer.mozilla.org/en-US/docs/Web/CSS/padding">padding</a>
        /// value used for the dialog header.
        /// The padding CSS shorthand property sets the padding area on all four sides of an element at once.
        /// Specify the padding in top, right, bottom, left order.
        /// Default value is: <c>14px</c> (all sides).
        /// </summary>
        [Parameter]
        public string HeaderPadding { get; set; } = "14px";

        /// <summary>
        /// String value that specifies the CSS <a href="https://developer.mozilla.org/en-US/docs/Web/CSS/border-bottom">border-bottom</a>
        /// value set used for the border below the header section.
        /// The border-bottom shorthand CSS property sets an element's bottom border. It sets the values of border-bottom-width,
        /// border-bottom-style and border-bottom-color.
        /// Default value is <c>1px solid #E9ECEF</c>.
        /// </summary>
        [Parameter]
        public string HeaderBottomBorder { get; set; } = "1px solid #E9ECEF";

        /// <summary>
        /// String value that specifies the CSS <a href="https://developer.mozilla.org/en-US/docs/Web/CSS/border-radius">border-radius</a>
        /// value used for the header border radius.
        /// The border-radius CSS property rounds the corners of an element's outer border edge.
        /// Default value is <c>3px 3px 0px 0px</c>.
        /// <remarks>
        /// For visual consistency the top-left and top-right radii should be the same
        /// as <see cref="DialogBorderRadius"/>.
        /// </remarks>
        /// </summary>
        [Parameter]
        public string HeaderBorderRadius { get; set; } = "3px 3px 0px 0px";

        /// <summary>
        /// String value that specifies a CSS <a href= "https://developer.mozilla.org/en-US/docs/Web/CSS/color">color</a>
        /// value used to render the text in the content section of the dialog box.
        /// The color CSS property sets the foreground color value of an element's text and text decorations, and sets the
        /// currentcolor value.
        /// The default value is <c>black</c>.
        /// </summary>
        [Parameter]
        public string ContentFontColor { get; set; } = "black";

        /// <summary>
        /// String value that specifies the CSS <a href= "https://developer.mozilla.org/en-US/docs/Web/CSS/font-size">font-size</a>
        /// value used to render the text in the the content section of the dialog box.
        /// Sets the base font size for the entire content section which means that subordinate elements can use relative font sizes
        /// (e.g., em) for the <see cref="ContentFragment"/>.
        /// The default value is <c>1em</c> (relative to the <see cref="DialogBaseFontSize"/> parameter)
        /// </summary>
        [Parameter]
        public string ContentFontSize { get; set; } = "1em";

        /// <summary>
        /// String value that specifies the CSS <a href= "https://developer.mozilla.org/en-US/docs/Web/CSS/font-weight">font-weight</a>
        /// value used to render the text in the content section of the dialog box.
        /// The font-weight CSS property sets the weight (or boldness) of the font. The weights available depend on the font-family
        /// that is currently set.
        /// The default value is <c>normal</c>.
        /// </summary>
        [Parameter]
        public string ContentFontWeight { get; set; } = "normal";

        /// <summary>
        /// String value that specifies the CSS <a href= "https://developer.mozilla.org/en-US/docs/Web/CSS/line-height">line-height</a>
        /// value applied to the content section of the dialog box.
        /// The line-height CSS property sets the height of a line box. It's commonly used to set the distance between lines of text.
        /// The default value is <c>normal</c>.
        /// </summary>
        [Parameter]
        public string ContentLineHeight { get; set; } = "normal";

        /// <summary>
        /// String value that specifies a CSS <a href= "https://developer.mozilla.org/en-US/docs/Web/CSS/background-color">background-color</a>
        /// value used to render the background in the content section of the dialog box.
        /// The background-color CSS property sets the background color of an element.
        /// The default value is <c>transparent</c>.
        /// </summary>
        [Parameter]
        public string ContentBackgroundColor { get; set; } = "transparent";

        /// <summary>
        /// String containing the CSS <a href="https://developer.mozilla.org/en-US/docs/Web/CSS/padding">padding</a>
        /// value used for the dialog content.
        /// The padding CSS shorthand property sets the padding area on all four sides of an element at once.
        /// Specify the padding in top, right, bottom, left order.
        /// Default value is: <c>14px</c> (all sides).
        /// </summary>
        [Parameter]
        public string ContentPadding { get; set; } = "14px";

        /// <summary>
        /// String value that specifies a CSS <a href= "https://developer.mozilla.org/en-US/docs/Web/CSS/background-color">background-color</a>
        /// value used to render the background in the footer portion of the dialog box.
        /// The background-color CSS property sets the background color of an element.
        /// The default value is <c>transparent</c>.
        /// </summary>
        [Parameter]
        public string FooterBackgroundColor { get; set; } = "transparent";

        /// <summary>
        /// String containing the CSS <a href="https://developer.mozilla.org/en-US/docs/Web/CSS/padding">padding</a>
        /// value used for the dialog footer.
        /// The padding CSS shorthand property sets the padding area on all four sides of an element at once.
        /// Specify the padding in top, right, bottom, left order.
        /// Default value is: <c>14px</c> (all sides).
        /// </summary>
        [Parameter]
        public string FooterPadding { get; set; } = "14px";

        /// <summary>
        /// String value that specifies the CSS <a href="https://developer.mozilla.org/en-US/docs/Web/CSS/border-top">border=top</a>
        /// value set used for the border above the footer section.
        /// The border shorthand CSS property sets an element's border. It sets the values of border-width, border-style, and border-color.
        /// Default value is <c>1px solid #E9ECEF</c>.
        /// </summary>
        [Parameter]
        public string FooterTopBorder { get; set; } = "1px solid #E9ECEF";

        /// <summary>
        /// String value that specifies the CSS <a href="https://developer.mozilla.org/en-US/docs/Web/CSS/border-radius">border-radius</a>
        /// value used for the footer section border radius.
        /// The border-radius CSS property rounds the corners of an element's outer border edge.
        /// Default value is <c>0px 0px 3px 3px</c>.
        /// <remarks>
        /// For visual consistency the bottom-left and bottom-right radii should be the same
        /// as <see cref="DialogBorderRadius"/>.
        /// </remarks>
        /// </summary>
        [Parameter]
        public string FooterBorderRadius { get; set; } = "0px 0px 3px 3px";

        #endregion
        
        #endregion


        #region Instance Variables

        private SfDialog sfDialog;
        private string masterCssSelector;
        private string dialogPositionValue;

        #endregion


        #region Constructors

        protected override void OnParametersSet()
        {
            // Perform error-checking on OverlayOpacity
            if (double.TryParse(OverlayOpacity, out double temp) == false)
            {
                OverlayOpacity = "0.6";
            }
            else if (temp is < 0.0 or > 1.0)
            {
                OverlayOpacity = "0.6";
            }

            // Build the master selector
            masterCssSelector = (CssClass == string.Empty) ? ".e-dialog" : $".{ CssClass }.e-dialog";

            // Set the dialog position value
            dialogPositionValue = MaintainPositionOnScroll ? "fixed" : "absolute";

            // Set the default value for the ResizeHandles parameter
            ResizeHandles ??= new[] { ResizeDirection.All };
        }

        #endregion


        #region Public Methods Providing Access to the Underlying Components to the Consumer

        /// <summary>
        /// Sets the visibility of the dialog box <see cref="Visible"/>.
        /// </summary>
        /// <param name="value">
        /// Boolean value indicating the visibility.
        /// </param>
        public async Task SetVisibilityAsync(bool value)
        {
            Visible = value;
            await InvokeAsync(StateHasChanged);
        }

        /// <summary>
        /// Opens the dialog if it is in a hidden state.
        /// </summary>
        /// <param name="isFullScreen">
        /// Boolean value specifying if the dialog is rendered full screen.
        /// Default value is false.
        /// </param>
        public async Task ShowAsync(bool isFullScreen = false) => 
            await sfDialog.ShowAsync(isFullScreen);

        /// <summary>
        /// Closes the dialog if it is in a visible state.
        /// </summary>
        public async Task HideAsync() => 
            await sfDialog.HideAsync();

        /// <summary>
        /// Refreshes the dialog's position when the user changes its height and width dynamically.
        /// </summary>
        public async Task RefreshPositionAsync() => 
            await sfDialog.RefreshPositionAsync();

        /// <summary>
        /// Gets the current height and width of the dialog box.
        /// </summary>
        /// <returns>
        /// A <see cref="DialogDimension"/> object containing the width and height of the dialog box.
        /// </returns>
        public async Task<DialogDimension> GetDimensionAsync() => 
            await sfDialog.GetDimension();

        /// <summary>
        /// Retrieve the <see cref="DialogButton"/> data for the specified dialog button.
        /// </summary>
        /// <param name="index">
        /// Integer value specifying the index of the dialog button.
        /// </param>
        /// <returns>
        /// A <see cref="DialogButton"/> object containing the data for the specified dialog button.
        /// </returns>
        public DialogButton GetButton(int index) => 
            sfDialog.GetButton(index);

        /// <summary>
        /// Retrieve a collection of <see cref="DialogButton"/> objects containing the data for
        /// all dialog buttons defined for the dialog box.
        /// </summary>
        /// <returns>
        /// A <see cref="List{T}"/> containing the <see cref="DialogButtons"/> defined for the
        /// dialog box.
        /// </returns>
        public List<DialogButton> GetButtonItems() => 
            sfDialog.GetButtonItems();

        #endregion
    }
}
