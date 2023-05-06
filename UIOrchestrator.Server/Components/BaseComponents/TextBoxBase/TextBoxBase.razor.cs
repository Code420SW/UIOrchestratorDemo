using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Syncfusion.Blazor.Inputs;

namespace Code420.UIOrchestrator.Server.Components.BaseComponents.TextBoxBase
{
    /// <summary>
    /// <para>
    /// Responsible for rendering a Textbox element based on a minimal set of user-supplied 
    /// parameters with the option to override the default settings using a wider array of parameters 
    /// that provide fine-grained control over the look and feel of the element.
    /// </para>
    /// <para>
    /// The component is NOT setup to use parameter binding for the Value parameter
    /// (e.g., <code>TextBoxBase @bind-Value=@myValue</code>).
    /// This means that the ValueChanged event handler is NOT exposed. If you want to handle binding to
    /// the Value parameter yourself, uncomment the ValueChangedHandler method in the cs file and
    /// its use in the razor file.
    /// </para>
    /// <para>
    /// The component exposes the <see cref="Placeholder"/> parameter containing the string value displayed in
    /// the textbox element when it is empty. The placeholder location and behavior is controlled by the
    /// <see cref="FloatLabelType"/> parameter. The <see cref="UpdatePlaceholderAsync"/> method invokes a
    /// JavaScript method that will update the placeholder at the DOM level which is useful when you want to
    /// update the placeholder text.
    /// </para>
    /// <para>
    /// The component provides the <see cref="AddIconAsync"/> method that allows you to prepend/append
    /// one or more icons in the textbox, specify the events an icon responds to (e.g., onclick) and define the
    /// event callback method invoked when the event is triggered. The documentation for AddIconAsync()
    /// has example usage. You will have to provide CSS elements in your code to style the icon(s).
    /// It is recommended using the <see cref="CssClass"/> parameter to aid in selection.
    /// For example, the CSS selector needed to access the icon would be:
    /// ".e-control-wrapper.{CssClass} .e-input-in-wrap .{icons}" where icons is the second
    /// parameter to the <see cref="AddIconAsync"/> method.
    /// </para>
    /// </summary>
    /// <remarks>
    /// The following parameters must be set:<br />
    /// <see cref="CssClass"/> -- Provides CSS isolation for the card<br />
    /// <see cref="Placeholder"/> -- Defines the prompt displayed<br />
    /// <see cref="CssId"/> -- If <see cref="EnablePersistence"/> is true
    /// </remarks>

    public partial class TextBoxBase : ComponentBase
    {
        #region Component Parameters

        #region Base Parameters

        // ==================================================
        // Base Parameters
        // ==================================================

        /// <summary>
        /// One of the <see cref="Syncfusion.Blazor.Inputs.AutoComplete"/> enum values indicating if the browser
        /// is allowed to automatically enter or select a value for the textbox.
        /// Default value is <see cref="Syncfusion.Blazor.Inputs.AutoComplete.Off"/>.
        /// </summary>
        [Parameter]
        public AutoComplete Autocomplete { get; set; } = AutoComplete.Off;

        /// <summary>
        /// Boolean value indicating if the Textbox is enabled.
        /// Default value is true.
        /// </summary>
        [Parameter]
        public bool Enabled { get; set; } = true;

        /// <summary>
        /// Boolean value indicating if the Textbox <see cref="Value"/> parameter is persisted between page reloads.
        /// Default value is false.
        /// <remarks>
        /// The <see cref="CssId"/> parameter is used as the key in the browser local storage.
        /// Best practice is to set the CssId parameter if persistence is enabled.
        /// </remarks>
        /// </summary>
        [Parameter]
        public bool EnablePersistence { get; set; }

        /// <summary>
        /// Boolean value indicating if right-to-left formatting is enabled for the textbox.
        /// Setting the parameter to true is typically used to place the icon after the button text.
        /// Default value is false.
        /// </summary>
        [Parameter]
        public bool EnableRtl { get; set; }

        /// <summary>
        /// A <see cref="Syncfusion.Blazor.Inputs.FloatLabelType"/> that specifies the floating label behavior of the TextBox.
        /// Valid values are Never, Always and Auto (label floats above the textbox after focusing it or when enters the value in it).
        /// Default value is <see cref="Syncfusion.Blazor.Inputs.FloatLabelType.Auto"/>.
        /// </summary>
        [Parameter]
        public FloatLabelType FloatLabelType { get; set; } = FloatLabelType.Auto;

        /// <summary>
        /// Boolean value that enables or disables the multiline on the TextBox. 
        /// The TextBox changes from a single line to multiline when enabling this multiline mode.
        /// Default value is false.
        /// </summary>
        [Parameter]
        public bool Multiline { get; set; }

        /// <summary>
        /// String value containing the text that is shown as a hint or placeholder until the user focuses or enters a value in the Textbox. 
        /// Use of this property depends on the <see cref="FloatLabelType"/> property setting.
        /// Default value is string.Empty.
        /// </summary>
        [Parameter]
        public string Placeholder { get; set; } = string.Empty;

        /// <summary>
        /// Boolean value indicating if the Textbox allows the user to change the text.
        /// Default value is false.
        /// </summary>
        [Parameter]
        public bool ReadOnly { get; set; }

        /// <summary>
        /// Boolean value indicating if the clear button is displayed in the Textbox.
        /// Default value is false.
        /// </summary>
        [Parameter]
        public bool ShowClearButton { get; set; }

        /// <summary>
        /// Integer value specifying the tab order of the Textbox relative to other HTML elements on the page.
        /// Default value is 0.
        /// </summary>
        [Parameter]
        public int TabIndex { get; set; }

        /// <summary>
        /// One of the <see cref="Syncfusion.Blazor.Inputs.InputType"/> enums that specifies the behavior
        /// of the TextBox such as text, password, email, and more.
        /// Default value is <see cref="Syncfusion.Blazor.Inputs.InputType.Text"/>.
        /// </summary>
        [Parameter]
        public InputType Type { get; set; } = InputType.Text;

        /// <summary>
        /// Boolean value that indicates whether the component validates the input or not.
        /// If the ValidateOnInput is enabled for form validation, then the model value will be updated on 
        /// entering the value to the input.
        /// The default value is false.
        /// </summary>
        [Parameter]
        public bool ValidateOnInput { get; set; }

        /// <summary>
        /// String value containing the contents of the Textbox.
        /// Default value is null.
        /// </summary>
        [Parameter]
        public string Value { get; set; }

        #endregion


        #region Event Callback Parameters

        // ==================================================
        // Event Callback Parameters
        // ==================================================

        /// <summary>
        /// An <see cref="EventCallback"/> containing the consumer's method invoked when the TextBox loses focus.
        /// </summary>
        [Parameter]
        public EventCallback<FocusOutEventArgs> Blur { get; set; }

        /// <summary>
        /// An <see cref="EventCallback"/> containing the consumer's method invoked when the TextBox component is created.
        /// </summary>
        [Parameter]
        public EventCallback<object> Created { get; set; }

        /// <summary>
        /// An <see cref="EventCallback"/> containing the consumer's method invoked when the TextBox component is destroyed.
        /// </summary>
        [Parameter]
        public EventCallback<object> Destroyed { get; set; }

        /// <summary>
        /// An <see cref="EventCallback"/> containing the consumer's method invoked when the TextBox
        /// gains focus.
        /// </summary>
        [Parameter]
        public EventCallback<FocusInEventArgs> Focus { get; set; }

        /// <summary>
        /// An <see cref="EventCallback"/> containing the consumer's method invoked each time when the
        /// value of TextBox has changed.
        /// </summary>
        [Parameter]
        public EventCallback<InputEventArgs> Input { get; set; }
        
        /// <summary>
        /// An <see cref="EventCallback"/> containing the consumer's method invoked when the Textbox value
        /// changes and loses focus.
        /// <remarks>
        /// If the consuming component specifies a method for this event handler, that method is
        /// responsible for all updated to the <see cref="Value"/> parameter. If a callback
        /// method is not specified, the TextBoxBase component will update the Value parameter.
        /// </remarks>
        /// </summary>
        [Parameter]
        public EventCallback<ChangedEventArgs> ValueChange { get; set; }
        
        // [Parameter]
        // public EventCallback<string> ValueChanged { get; set; }


        #endregion


        #region CSS Parameters

        // ==================================================
        // CSS Styling Parameters
        // ==================================================

        /// <summary>
        /// String value containing CSS class definition(s) that will be injected in the root HTML div element of the Textbox.
        /// Default value is string.Empty.
        /// </summary>
        [Parameter]
        public string CssClass { get; set; } = string.Empty;

        /// <summary>
        /// String value that specifies the CSS ID assigned to the HTML input element of the Textbox. 
        /// The Id is also injected into the HTML label element when the <see cref="FloatLabelType"/>
        /// parameter is set to Always or Auto.
        /// The Id is used by the UpdatePlaceholderAsync() method.
        /// Default value is string.Empty.
        /// <remarks>
        /// The <see cref="CssId"/> parameter is used as the key in the browser local storage.
        /// Best practice is to set the CssId parameter if <see cref="EnablePersistence"/> is true.
        /// </remarks>
        /// </summary>
        [Parameter]
        public string CssId { get; set; } = string.Empty;

        /// <summary>
        /// Collection of additional HTML attributes such as styles, class, and more that are injected in root element. 
        /// If both property and equivalent HTML attribute are configured, the component considers the property value. 
        /// This is a <see cref="Dictionary{TKey, TValue}"/> where TKey is a <see cref="string"/> and TValue is an <see cref="object"/>.
        /// Default value is null.
        /// </summary>
        [Parameter]
        public Dictionary<string, object> HtmlAttributes { get; set; }

        /// <summary>
        /// Collection of additional input attributes such as disabled, value, and more that are injected in root element. 
        /// If both property and equivalent input attribute are configured, the component considers the property value. 
        /// This is a <see cref="Dictionary{TKey, TValue}"/> where TKey is a <see cref="string"/> and TValue is an <see cref="object"/>.
        /// Default value is null.
        /// </summary>
        [Parameter]
        public Dictionary<string, object> InputAttributes { get; set; }

        /// <summary>
        /// String value that specifies the CSS <a href="https://developer.mozilla.org/en-US/docs/Web/CSS/width">width</a>
        /// value used for the textbox.
        /// The width CSS property sets an element's width.
        /// Default value is 200px.
        /// </summary>
        [Parameter]
        public string Width { get; set; } = "200px";

        /// <summary>
        /// String value that specifies the CSS <a href="https://developer.mozilla.org/en-US/docs/Web/CSS/height">height</a>
        /// value used for the textbox.
        /// The height CSS property specifies the height of an element.
        /// Default value is 29px.
        /// </summary>
        [Parameter]
        public string Height { get; set; } = "29px";

        /// <summary>
        /// String value that specifies the CSS <a href="https://developer.mozilla.org/en-US/docs/Web/CSS/background-color">background</a> 
        /// value used for the textbox.
        /// The background-color CSS property sets the background color of an element.
        /// Default value is #FFF.
        /// </summary>
        [Parameter]
        public string TextboxBackgroundColor { get; set; } = "#FFF";

        /// <summary>
        /// String value that specifies the CSS <a href="https://developer.mozilla.org/en-US/docs/Web/CSS/border-color">border-color</a> 
        /// value used for the textbox.
        /// The border-color shorthand CSS property sets the color of an element's border.
        /// Default value is black.
        /// </summary>
        [Parameter]
        public string TextboxBorderColor { get; set; } = "black";

        /// <summary>
        /// String value that specifies the CSS <a href="https://developer.mozilla.org/en-US/docs/Web/CSS/border-width">border-width</a> 
        /// value used for the textbox.
        /// The border-width shorthand CSS property sets the width of an element's border.
        /// Default value is 1px.
        /// </summary>
        [Parameter]
        public string TextboxBorderWidth { get; set; } = "1px";

        /// <summary>
        /// String value that specifies the CSS <a href="https://developer.mozilla.org/en-US/docs/Web/CSS/border-radius">border-radius</a> 
        /// value used for the textbox.
        /// The border-radius CSS property rounds the corners of an element's outer border edge.
        /// Default value is 4px.
        /// </summary>
        [Parameter]
        public string TextboxBorderRadius { get; set; } = "4px";

        /// <summary>
        /// String value that specifies the CSS <a href="https://developer.mozilla.org/en-US/docs/Web/CSS/border-color">border-color</a> 
        /// value used for the textbox when active.
        /// The border-color shorthand CSS property sets the color of an element's border..
        /// Default value is #80BDFF.
        /// </summary>
        [Parameter]
        public string TextboxActiveBorderColor { get; set; } = "#80BDFF";

        /// <summary>
        /// String value that specifies the CSS <a href="https://developer.mozilla.org/en-US/docs/Web/CSS/font-size">font-size</a> 
        /// value used for the textbox font size.
        /// The font-size CSS property sets the size of the font.
        /// Default value is 14px.
        /// </summary>
        [Parameter]
        public string TextboxFontSize { get; set; } = "14px";

        /// <summary>
        /// String value that specifies the CSS <a href="https://developer.mozilla.org/en-US/docs/Web/CSS/color">color</a> 
        /// value used for the textbox font color.
        /// The color CSS property sets the foreground color value of an element's text and text decorations.
        /// Default value is #495057.
        /// </summary>
        [Parameter]
        public string TextboxFontColor { get; set; } = "#495057";

        /// <summary>
        /// String value that specifies the CSS <a href="https://developer.mozilla.org/en-US/docs/Web/CSS/font-weight">font-weigh</a> 
        /// value used for the textbox font.
        /// The font-weight CSS property sets the weight (or boldness) of the font. The weights available depend on the font-family that is currently set.
        /// Default value is 400.
        /// </summary>
        [Parameter]
        public string TextboxFontWeight { get; set; } = "400";

        /// <summary>
        /// String value that specifies the CSS <a href="https://developer.mozilla.org/en-US/docs/Web/CSS/line-height">line-height</a>
        /// value used for the textbox line height.
        /// The line-height CSS property sets the height of a line box. It's commonly used to set the distance between lines of text.
        /// Default value is 1.4.
        /// </summary>
        [Parameter]
        public string TextboxLineHeight { get; set; } = "1.4";

        /// <summary>
        /// String value that specifies the CSS <a href="https://developer.mozilla.org/en-US/docs/Web/CSS/margin">margin</a> 
        /// value used for the textbox margin.
        /// The margin CSS shorthand property sets the margin area on all four sides of an element
        /// Default value is 24px 0px 0px 0px.
        /// <remarks>
        /// When the <see cref="FloatLabelType"/> parameter is set to <see cref="Syncfusion.Blazor.Inputs.FloatLabelType.Auto"/>
        /// or <see cref="Syncfusion.Blazor.Inputs.FloatLabelType.Always"/>, make sure to leave enough margin above the component
        /// for the floating label.
        /// </remarks>
        /// </summary>
        [Parameter]
        public string TextboxMargin { get; set; } = "24px 0px 0px 0px";

        /// <summary>
        /// String value that specifies the CSS <a href="https://developer.mozilla.org/en-US/docs/Web/CSS/padding-top">padding-top</a> 
        /// value used for the textbox padding.
        /// The padding-top CSS property sets the height of the padding area on the top of an element.
        /// Default value is 0px.
        /// </summary>
        [Parameter]
        public string TextboxPaddingTop { get; set; } = "0px";

        /// <summary>
        /// String value that specifies the CSS <a href="https://developer.mozilla.org/en-US/docs/Web/CSS/padding-left">padding-left</a> 
        /// value used for the textbox padding.
        /// The padding-left CSS property sets the width of the padding area to the left of an element.
        /// Default value is 8px.
        /// </summary>
        [Parameter]
        public string TextboxPaddingLeft { get; set; } = "8px";

        /// <summary>
        /// String value that specifies the CSS <a href="https://developer.mozilla.org/en-US/docs/Web/CSS/text-indent">text-indent</a> 
        /// value used for the textbox contents.
        /// The text-indent CSS property sets the length of empty space (indentation) that is put before lines of text in a block.
        /// Default value is 0px.
        /// </summary>
        [Parameter]
        public string TextboxTextIndent { get; set; } = "0px";

        /// <summary>
        /// String value that specifies the CSS <a href="https://developer.mozilla.org/en-US/docs/Web/CSS/font-size">font-size</a> 
        /// value used for the textbox placeholder font.
        /// The font-size CSS property sets the size of the font.
        /// Default value is 14px.
        /// </summary>
        [Parameter]
        public string PlaceholderFontSize { get; set; } = "14px";

        /// <summary>
        /// String value that specifies the CSS <a href="https://developer.mozilla.org/en-US/docs/Web/CSS/font-weight">font-weigh</a> 
        /// value used for the textbox placeholder font.
        /// The font-weight CSS property sets the weight (or boldness) of the font. The weights available depend on the font-family
        /// that is currently set.
        /// Default value is 400.
        /// </summary>
        [Parameter]
        public string PlaceholderFontWeight { get; set; } = "400";

        /// <summary>
        /// String value that specifies the CSS <a href="https://developer.mozilla.org/en-US/docs/Web/CSS/color">color</a> 
        /// value used for the textbox placeholder font its normal (non-floating) position.
        /// The color CSS property sets the foreground color value of an element's text and text decorations.
        /// Default value is #6C757D.
        /// </summary>
        [Parameter]
        public string PlaceholderNormalColor { get; set; } = "#6C757D";

        /// <summary>
        /// String value that specifies the CSS <a href="https://developer.mozilla.org/en-US/docs/Web/CSS/font-size">font-size</a> 
        /// value used for the textbox placeholder font in its floating position.
        /// The font-size CSS property sets the size of the font.
        /// Default value is 14px.
        /// </summary>
        [Parameter]
        public string PlaceholderFloatingFontSize { get; set; } = "14px";

        /// <summary>
        /// String value that specifies the CSS <a href="https://developer.mozilla.org/en-US/docs/Web/CSS/font-weight">font-weigh</a> 
        /// value used for the textbox placeholder font in its floating position.
        /// The font-weight CSS property sets the weight (or boldness) of the font. The weights available depend on the font-family
        /// that is currently set.
        /// Default value is 400.
        /// </summary>
        [Parameter]
        public string PlaceholderFloatingFontWeight { get; set; } = "400";

        /// <summary>
        /// String value that specifies the CSS <a href="https://developer.mozilla.org/en-US/docs/Web/CSS/color">color</a> 
        /// value used for the textbox placeholder font its floating position.
        /// The color CSS property sets the foreground color value of an element's text and text decorations.
        /// Default value is #212529.
        /// </summary>
        [Parameter]
        public string PlaceholderFloatingColor { get; set; } = "#212529";

        /// <summary>
        /// String value that specifies the CSS <a href="https://developer.mozilla.org/en-US/docs/Web/CSS/font-size">font-size</a> 
        /// value used for the close button's <see cref="ShowClearButton"/> icon.
        /// The font-size CSS property sets the size of the font.
        /// Default value is 12px.
        /// </summary>
        [Parameter]
        public string ClearButtonIconSize { get; set; } = "12px";

        /// <summary>
        /// String value that specifies the CSS <a href="https://developer.mozilla.org/en-US/docs/Web/CSS/color">color</a> 
        /// value used for the close button's <see cref="ShowClearButton"/> icon..
        /// The color CSS property sets the foreground color value of an element's text and text decorations.
        /// Default value is rgba(0,0,0,0.5).
        /// </summary>
        [Parameter]
        public string ClearButtonIconColor { get; set; } = "rgba(0,0,0,0.5)";

        /// <summary>
        /// String value that specifies the CSS <a href="https://developer.mozilla.org/en-US/docs/Web/CSS/background-color">background</a> 
        /// value used for the close button's <see cref="ShowClearButton"/> icon.
        /// The background-color CSS property sets the background color of an element.
        /// Default value is transparent.
        /// </summary>
        [Parameter]
        public string ClearButtonIconBackgroundColor { get; set; } = "transparent";

        /// <summary>
        /// String value that specifies the CSS <a href="https://developer.mozilla.org/en-US/docs/Web/CSS/min-width">min-width</a> 
        /// value used for the close button's <see cref="ShowClearButton"/> icon.
        /// The min-width CSS property sets the minimum width of an element. It prevents the used value of the width property from
        /// becoming smaller than the value specified for min-width.
        /// Default value is 24px.
        /// </summary>
        [Parameter]
        public string ClearButtonMinimumWidth { get; set; } = "24px";

        /// <summary>
        /// String value that specifies the CSS <a href="https://developer.mozilla.org/en-US/docs/Web/CSS/padding">padding</a> 
        /// value used for the close button padding.
        /// The padding CSS shorthand property sets the padding area on all four sides of an element at once.
        /// Default value is 0px on all sides.
        /// </summary>
        [Parameter]
        public string ClearButtonPadding { get; set; } = "0px";

        #endregion

        #endregion



        #region Callback Events Invoked from Underlying Components

        // ==================================================
        // Methods used as Callback Events from the underlying component(s)
        //
        //	These methods act as intermediaries for the child component's event
        //	callbacks when this base component needs to inject some behavior.
        //	Defining/using an intermediary when behavior injection is not required
        //	is bloat...simply pass the event callback directly to the child component,
        // ==================================================

        /// <summary>
        /// Passes the event to the consumer's method.
        /// </summary>
        /// <param name="args">
        /// The passed <see cref="ChangedEventArgs"/> object.
        /// </param>
        /// <returns></returns>
        private async Task ValueChangeHandler(ChangedEventArgs args)
        {
            if (ValueChange.HasDelegate)
            {
                await ValueChange.InvokeAsync(args);
                return;
            }
            Value = args.Value;
        }

        #endregion


        #region Instance Variables

        // ==================================================
        // Instance variables
        // ==================================================

        private SfTextBox sfTextBox;                            // SF Textbox component
        private string masterCssSelector = string.Empty;        // The master selector for the HTML div element
        private string boxShadowRgba = string.Empty;            // The RGBA value when styling the box shadow used when the textbox is active


        #endregion


        #region Injected Dependencies

        [Inject] private IJSRuntime jsRuntime { get; set; }

        [Inject] private ICssUtilities cssUtilities { get; set; }

        #endregion


        #region Constructors


        // ==================================================
        // Constructors
        // ==================================================

        // This method will be executed immediately after OnInitializedAsync if this is a new
        //  instance of a component. If it is an existing component that is being re-rendered because
        //  its parent is re-rendering then the OnInitialized* methods will not be executed, and this
        //  method will be executed immediately after SetParametersAsync instead
        protected override void OnParametersSet()
        {
            // Build the master selector
            masterCssSelector = (CssClass == string.Empty) ? ".e-control-wrapper" : $".e-control-wrapper.{CssClass}";

            // If the InputAttributes parameter is not set, create it.
            InputAttributes ??= new();
        }

        // This is the first place that the State should be changed
        //
        // This method is executed every time Blazor has re-generated the component’s RenderTree.
        //  This can be as a result of the component’s parent re-rendering, the user interacting with the component
        //  (e.g. a mouse-click), or if the component executes its StateHasChanged method to invoke a re-render.
        // This method has a single parameter named firstRender. This parameter is true only the first time the
        //  method is called on the current component, from there onwards it will always be false. In cases where
        //  additional component hook-up is required (for example, via JavaScript) it is useful to know this is the
        //  first render.
        // It is not until after the OnAfterRender method have executed that it is safe to use any references to
        //  components set via the @ref directive. And it is not until after the OnAfterRender method have been
        //  executed with firstRender set to true that it is safe to use any references to HTML elements set via
        //  the @ref directive
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            // Pre-calculate the box-shadow RGBA values used when the textbox is active
            boxShadowRgba = await cssUtilities.ConvertToRgba(TextboxActiveBorderColor, 0.25m);
        }
        #endregion


        #region Public Methods Providing Access to the Underlying Components to the Consumer

        /// <summary>
        /// Disables the Textbox.
        /// </summary>
        public async Task DisableAsync()
        {
            Enabled = false;
            await InvokeAsync(StateHasChanged);
        }

        /// <summary>
        /// Enables the Textbox.
        /// </summary>
        public async Task EnableAsync()
        {
            Enabled = true;
            await InvokeAsync(StateHasChanged);
        }

        /// <summary>
        /// Adding the icons to the TextBox component.
        /// </summary>
        /// <param name="position">
        /// The adding icons to the component based on position for prepend/append.
        /// </param>
        /// <param name="icons">
        /// The icons class is added to icon element.
        /// </param>
        /// <param name="events">
        /// The icon events are added to the events element
        /// </param>
        /// <remarks>
        /// There isn't a mechanism to adding icons when the component is defined.
        /// The best method to add icons is to 
        /// </remarks>
        /// <example>
        /// The below code demonstrates usage of AddIconAsync method through component instance. While passing
        /// Events parameter, onclick denotes the Event type and the Click denotes the EventHandler method.
        /// <code>
        /// <![CDATA[
        /// var Click = EventCallback.Factory.Create<MouseEventArgs>(this, SearchClick);
        /// await myTextBox.AddIconAsync("append", "e-search-icon", new Dictionary<string, object>() { { "onclick", Click } });
        /// public void SearchClick()  { // Icon Click event triggered  }
        /// ]]>
        /// </code>
        /// </example>
        /// <returns></returns>
        public async Task AddIconAsync(string position, string icons, Dictionary<string, object> events = null) 
            => await sfTextBox.AddIconAsync(position, icons, events);

        /// <summary>
        /// Sets the focus to TextBox component for interaction.
        /// </summary>
        /// <returns></returns>
        public async Task FocusAsync() => await sfTextBox.FocusAsync();

        /// <summary>
        /// Remove the focus from TextBox component, if the component is in focus state.
        /// </summary>
        /// <returns></returns>
        public async Task FocusOutAsync() => await sfTextBox.FocusOutAsync();

        /// <summary>
        /// Gets the properties to be maintained in the persisted state
        /// </summary>
        public async Task<string> GetPersistDataAsync() => await sfTextBox.GetPersistDataAsync();

        /// <summary>
        /// Update the value of the Placeholder property. 
        /// </summary>
        /// <param name="value">The new Placeholder property value</param>
        /// <remarks>
        /// Uses JavaScript to find the label_CssId HTML Id element and sets its innerHTML to the passed value.
        /// To ensure proper operation, ensure the CssId property is set to a unique value.
        /// Example: The placeholder displayed when the textbox is empty should be "Enter your email address..."
        /// whereas when the textbox has content, the placeholder should be: "Email address".
        /// </remarks>
        public async Task UpdatePlaceholderAsync(string value)
        {
            string id = "label_" + sfTextBox.ID;
            await jsRuntime.InvokeVoidAsync("Code420.setMaskedTextboxPlaceholder", id, value);
        }

        #endregion


        #region Private Methods for Internal Use Only
        #endregion

    }
}
