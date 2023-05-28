using Microsoft.AspNetCore.Components;
using Syncfusion.Blazor.Navigations;
using ChangeEventArgs = Syncfusion.Blazor.Navigations.ChangeEventArgs;
using EventArgs = Syncfusion.Blazor.Navigations.EventArgs;

namespace Code420.UIOrchestrator.Server.Components.BaseComponents.SidebarBase
{
    /// <summary>
    /// <para>
    /// Responsible for rendering a SfSidebar component in the element identified
    /// by the <see cref="Target"/> parameter. The contents of the sidebar is defined
    /// by the <see cref="ChildContent"/> parameter.
    /// </para>
    /// <para>
    /// The component exposes a number of parameters that can be used to style the
    /// sidebar and control its behavior. All provide default values that can be overridden.
    /// </para>
    /// <para>
    /// The <see cref="IsOpenChanged"/> event callback handler is overridden by the component.
    /// If the consumer does not handle this event, the component will update the <see cref="IsOpen"/>
    /// parameter when the sidebar is opened or closed.
    /// </para>
    /// </summary>
    /// <remarks>
    /// The following parameters must be set:<br />
    /// <see cref="ChildContent"/> -- The content rendered inside the sidebar.<br />
    /// </remarks>
    /// <remarks>
    /// Consider setting the following parameters:<br />
    /// <see cref="Target"/> -- Identifies the HTML element in which the sidebar is rendered.
    /// </remarks>
    public partial class SidebarBase : ComponentBase
    {

        #region Component Parameters

        #region Base Parameters

        /// <summary>
        /// Boolean value specifying if animation transitions when expanding or collapsing
        /// the Sidebar are enabled.
        /// Default value is true.
        /// </summary>
        [Parameter]
        public bool Animate { get; set; } = true;

        /// <summary>
        /// Contains the <see cref="RenderFragment" /> composing the Sidebar contents.
        /// The default value is null.
        /// </summary>
        [Parameter]
        [EditorRequired]
        public RenderFragment ChildContent { get; set; }

        /// <summary>
        /// Boolean value specifying if the Sidebar is closed when the document area is clicked.
        /// Default value is false.
        /// </summary>
        [Parameter]
        public bool CloseOnDocumentClick { get; set; }

        /// <summary>
        /// Boolean value specifying the docking state of the Sidebar. 
        /// When disabled, the Sidebar is not visible on the page in the closed state. A mechanism
        /// must be provided to change the Sidebar state,
        /// When enabled, a portion of the Sidebar, specified by the <see cref="DockSize"/> parameter,
        /// is visible on the page.
        /// Default value is true.
        /// </summary>
        [Parameter]
        public bool EnableDock { get; set; } = true;

        /// <summary>
        /// Boolean value specifying if swiping gestures to expand/collapse the Sidebar on touch
        /// devices are enabled.
        /// Default value is false.
        /// </summary>
        [Parameter]
        public bool EnableGestures { get; set; }

        /// <summary>
        /// Boolean value specifying if the Sidebar's opened/closed state is persisted between
        /// page reloads.
        /// Default value is false.
        /// </summary>
        /// <remarks>
        /// The <see cref="CssId"/> parameter is used as the key in the browser local storage.
        /// Best practice is to set the CssId parameter if persistence is enabled.
        /// </remarks>
        [Parameter]
        public bool EnablePersistence { get; set; }

        /// <summary>
        /// Boolean value specifying if the Sidebar is open or closed.
        /// When the Sidebar type is set to Auto, the component will be expanded in the desktop
        /// and collapsed in the mobile mode regardless of the IsOpen property.
        /// Default value is false.
        /// </summary>
        [Parameter]
        public bool IsOpen { get; set; }

        /// <summary>
        /// One of the <see cref="SidebarPosition"/> values specifying the position of the Sidebar.
        /// Default value is Left.
        /// </summary>
        [Parameter]
        public SidebarPosition Position { get; set; } = SidebarPosition.Left;

        /// <summary>
        /// Boolean value specifying whether to display an overlay on the main content when the
        /// Sidebar is open.
        /// Default value is false.
        /// </summary>
        [Parameter]
        public bool ShowBackdrop { get; set; }

        /// <summary>
        /// String value specifying the CSS class whose HTML element the Sidebar will be placed within.
        /// This must be a full CSS class specification (e.g., <c>.sidebar__target</c>).
        /// Default value is string.Empty indicating the Sidebar will associated with the <c>body</c>
        /// HTML element.
        /// </summary>
        [Parameter]
        public string Target { get; set; } = string.Empty;

        /// <summary>
        /// One of the <see cref="SidebarType"/> values specifying the interaction of the Sidebar
        /// and other HTML elements when open/closed.
        /// Default value is Auto.
        /// </summary>
        [Parameter]
        public SidebarType Type { get; set; } = SidebarType.Auto;

        #endregion


        #region Event Callback Parameters

        /// <summary>
        /// An <see cref="EventCallback"/> containing the consumer's method invoked when the state
        /// (expand/collapse) of the Sidebar is changed.
        /// </summary>
        [Parameter]
        public EventCallback<ChangeEventArgs> Changed { get; set; }

        /// <summary>
        /// An <see cref="EventCallback"/> containing the consumer's method invoked when the
        /// Sidebar is created.
        /// </summary>
        [Parameter]
        public EventCallback<object> Created { get; set; }

        /// <summary>
        /// An <see cref="EventCallback"/> containing the consumer's method invoked when the
        /// Sidebar is destroyed.
        /// </summary>
        [Parameter]
        public EventCallback<object> Destroyed { get; set; }

        /// <summary>
        /// An <see cref="EventCallback{TValue}"/> where TValue is an <see cref="bool"/>.
        /// The consumer's method is invoked when the Sidebar's open/closed state changes.
        /// The parameter is an boolean value which indicates the state of the Sidebar.
        /// Handling this callback adds the responsibility of updating the <see cref="IsOpen"/>
        /// parameter.
        /// </summary>
        [Parameter]
        public EventCallback<bool> IsOpenChanged { get; set; }

        /// <summary>
        /// An <see cref="EventCallback"/> containing the consumer's method invoked when the
        /// Sidebar is ready to close.
        /// </summary>
        [Parameter]
        public EventCallback<EventArgs> OnClose { get; set; }

        /// <summary>
        /// An <see cref="EventCallback"/> containing the consumer's method invoked when the
        /// Sidebar is ready to open.
        /// </summary>
        [Parameter]
        public EventCallback<EventArgs> OnOpen { get; set; }

        #endregion


        #region CSS Parameters

        /// <summary>
        /// String value containing CSS class definition(s) that will be injected in the root
        /// HTML div element of the Sidebar container.
        /// Default value is string.Empty.
        /// </summary>
        [Parameter]
        public string CssClass { get; set; } = string.Empty;

        /// <summary>
        /// String value containing CSS ID that will be injected in the root HTML div element
        /// of the Sidebar container.
        /// Default value is string.Empty.
        /// </summary>
        /// <remarks>
        /// The <see cref="CssId"/> parameter is used as the key in the browser local storage.
        /// Best practice is to set the CssId parameter if <see cref="EnablePersistence"/> is true.
        /// </remarks>
        [Parameter]
        public string CssId { get; set; } = string.Empty;

        /// <summary>
        /// Collection of additional HTML attributes such as styles, class, and more that are injected
        /// in root element. 
        /// If both property and equivalent HTML attribute are configured, the component considers the
        /// property value. 
        /// This is a <see cref="Dictionary{TKey, TValue}"/> where TKey is a <see cref="string"/> and
        /// TValue is an <see cref="object"/>.
        /// Default value is an empty dictionary.
        /// </summary>
        [Parameter]
        public Dictionary<string, object> HtmlAttributes { get; set; }

        /// <summary>
        /// String value that specifies the CSS <a href="https://developer.mozilla.org/en-US/docs/Web/CSS/length">length</a>
        /// data type used to specify the size of the Sidebar in the docked state (<see cref="IsOpen"/> is false).
        /// The length CSS data type represents a distance value.
        /// Note that only a single pixel value (e.g., 4px) should be used.
        /// Default value is 50px.
        /// </summary>
        [Parameter]
        public string DockSize { get; set; } = "50px";

        /// <summary>
        /// String value that specifies the CSS <a href="https://developer.mozilla.org/en-US/docs/Web/CSS/Media_Queries/Using_media_queries">media query</a>
        /// used to specify the media query string for the resolution when the Sidebar opens.
        /// Media queries are useful when you want to modify your site or app depending on a device's
        /// general type (such as print vs. screen) or specific characteristics and parameters (such as
        /// screen resolution or browser viewport width).
        /// For example: setting the parameter to '(min-width: 600px)' will open the Sidebar only when
        /// the provided resolution is met else the Sidebar will be in closed state.
        /// Default value is string.Empty.
        /// </summary>
        [Parameter]
        public string MediaQuery { get; set; } = string.Empty;

        /// <summary>
        /// String value that specifies the CSS <a href="https://developer.mozilla.org/en-US/docs/Web/CSS/width">width</a>
        /// value specifying the width of the Sidebar. 
        /// The width CSS property sets an element's width.
        /// By default, the width of the Sidebar sets based on the size of its content. 
        /// Width can also be set in pixel values.
        /// Default value is string.Empty.
        /// </summary>
        [Parameter]
        public string Width { get; set; } = string.Empty;

        /// <summary>
        /// Integer value that specifies the CSS <a href="https://developer.mozilla.org/en-US/docs/Web/CSS/z-index">z-index</a>
        /// value of the Sidebar. 
        /// The z-index CSS property sets the z-order of a positioned element and its descendants or flex items.
        /// It is applicable only when sidebar act as the overlay type (e.g., <see cref="Type"/> is
        /// <see cref="SidebarType.Over"/> in any application, or <see cref="SidebarType.Auto"/> in a mobile
        /// application).
        /// Default value is 0.
        /// </summary>
        [Parameter]
        public int ZIndex { get; set; }


        // ==================== Sidebar Styles ====================


        /// <summary>
        /// String value that specifies the CSS <a href="https://developer.mozilla.org/en-US/docs/Web/CSS/border-width">border-width</a>
        /// value used for the border on the opening edge of the Sidebar.
        /// The border-width shorthand CSS property sets the width of an element's border.
        /// Default value is 1px.
        /// </summary>
        [Parameter]
        public string SidebarBorderWidth { get; set; } = "1px";

        /// <summary>
        /// String value that specifies the CSS <a href="https://developer.mozilla.org/en-US/docs/Web/CSS/border-color">border-color</a>
        /// value used for the border on the opening edge of the Sidebar.
        /// The border-color shorthand CSS property sets the color of an element's border.
        /// Default value is #DEE2E6.
        /// </summary>
        [Parameter]
        public string SidebarBorderColor { get; set; } = "#DEE2E6";

        /// <summary>
        /// String value that specifies the CSS <a href="https://developer.mozilla.org/en-US/docs/Web/CSS/border-style">border-style</a>
        /// value used for the border on the opening edge of the Sidebar.
        /// The border-style shorthand CSS property sets the line style for all four sides of an element's border.
        /// Default value is solid.
        /// </summary>
        [Parameter]
        public string SidebarBorderStyle { get; set; } = "solid";

        /// <summary>
        /// String value that specifies the CSS <a href="https://developer.mozilla.org/en-US/docs/Web/CSS/background-color">background-color</a>
        /// value used for the border on the opening edge of the Sidebar.
        /// The background-color CSS property sets the background color of an element.
        /// Default value is #FFF.
        /// </summary>
        [Parameter]
        public string SidebarBackgroundColor { get; set; } = "#FFF";

        /// <summary>
        /// String value that specifies the CSS <a href="https://developer.mozilla.org/en-US/docs/Web/CSS/transition">transition</a>
        /// value used for the Sidebar when opening.
        /// The transition CSS property is a shorthand property for transition-property, transition-duration,
        /// transition-timing-function, and transition-delay.
        /// Default value is: <c>transform .5s ease</c>.
        /// </summary>
        [Parameter]
        public string SidebarOpenTransition { get; set; } = "transform .5s ease";

        /// <summary>
        /// String value that specifies the CSS <a href="https://developer.mozilla.org/en-US/docs/Web/CSS/transition">transition</a>
        /// value used for the Sidebar when closing.
        /// The transition CSS property is a shorthand property for transition-property, transition-duration,
        /// transition-timing-function, and transition-delay.
        /// Default value is: <c>width .5s ease, visibility 500ms</c>.
        /// </summary>
        [Parameter]
        public string SidebarCloseTransition { get; set; } = "width .5s ease, visibility 500ms";

        /// <summary>
        /// String value that specifies the CSS <a href="https://developer.mozilla.org/en-US/docs/Web/CSS/background-color">background-color</a>
        /// value used for the backdrop (overlay).
        /// The background-color CSS property sets the background color of an element.
        /// Default value is: <c>rgba(0, 0, 0, 0.5)</c>.
        /// </summary>
        [Parameter]
        public string BackdropBackgroundColor { get; set; } = "rgba(0, 0, 0, 0.5)";

        /// <summary>
        /// String value that specifies the CSS <a href="https://developer.mozilla.org/en-US/docs/Web/CSS/opacity">opacity</a>
        /// value used for the backdrop (overlay).
        /// The opacity CSS property sets the opacity of an element. Opacity is the degree to which content behind an element is hidden.
        /// Default value is 0.5.
        /// </summary>
        [Parameter]
        public string BackdropOpacity { get; set; } = "0.5";

        #endregion

        #endregion



        #region Callback Events Invoked from Underlying Components

        private async Task IsOpenChangedHandler(bool state)
        {
            if (IsOpenChanged.HasDelegate) await IsOpenChanged.InvokeAsync(state);
            else IsOpen = state;
        }

        #endregion



        #region Instance Variables

        private SfSidebar sidebar;
        private string masterCssSelector;

        #endregion



        #region Constructors

        protected override void OnParametersSet()
        {
            // Build the master selectors
            masterCssSelector = (CssClass == string.Empty) ? ".e-sidebar" : $".{ CssClass }.e-sidebar";

            // If the HtmlAttributes parameter is not set, create it.
            HtmlAttributes ??= new();

            // If the CssClass parameter is set, add it to HtmlAttributes if it doesn't already exist.
            if ((CssClass != string.Empty) &&
                ((HtmlAttributes.TryGetValue("class", out object _) == false) && 
                 (HtmlAttributes.TryGetValue("CLASS", out object _) == false)))
            {
                HtmlAttributes.Add("class", CssClass);
            }
        }

        #endregion

    }
}
