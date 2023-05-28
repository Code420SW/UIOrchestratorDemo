using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Components;
using Syncfusion.Blazor;
using Syncfusion.Blazor.Navigations;

namespace Code420.UIOrchestrator.Server.Components.BaseComponents.TabBase
{
    /// <summary>
    /// <para>
    /// Encapsulates the SfTab component with the goal of exposing various CSS styling options,
    /// and to overload tab events. 
    /// </para>
    ///<para>
    /// Tabs can be defined using either of two methods. The <see cref="TabItems"/> parameter allows
    /// the consume to specify a <see cref="List{T}"/> of <see cref="TabItem"/> objects. The second
    /// method is to use the <c>TabItems</c> and <c>TabItem</c> components which is passed through
    /// the <see cref="ChildContent"/> parameter.
    /// </para>
    ///<para>
    /// Note that only one of these methods can be used. If the <see cref="TabItems"/> parameter is
    /// defined (not null) it will take precedence over the <see cref="ChildContent"/> parameter.
    /// </para>
    /// <para>
    /// The <see cref="SelectedItemChanged"/> event callback handler is overridden by the component.
    /// If the consumer does not handle this event, the component will update the <see cref="SelectedItem"/>
    /// parameter when a tab is selected.
    /// </para>
    /// </summary>
    /// <remarks>
    /// The following parameters must be set:<br />
    /// <see cref="TabItems"/> -- A <see cref="List{T}"/> of <see cref="TabItem"/> objects defining
    /// the tabs.<br />
    /// <b>OR</b><br />
    /// <see cref="ChildContent"/> -- A <c>TabItems</c> component defining the tabs.<br />
    /// </remarks>
    /// <remarks>
    /// Consider setting the following parameters:<br />
    /// <see cref="CssClass"/> -- Provides CSS isolation.
    /// </remarks>
    
    // TODO: Understand why the various OverflowMode settings aren't working
    public partial class TabBase : ComponentBase
    {

        #region Component Parameters

        #region Base Parameters

        /// <summary>
        /// Boolean value indicating if drag-and-drop operations are allowed to reorder tabs.
        /// Default value is false.
        /// </summary>
        [Parameter]
        public bool AllowDragAndDrop { get; set; }

        /// <summary>
        /// An integer value that specifies the duration (in milliseconds) applied to the
        /// animation of the Tabs component.
        /// Default value is 600 (milliseconds).
        /// </summary>
        [Parameter]
        public int AnimationDuration { get; set; } = 600;

        /// <summary>
        /// One of the <see cref="AnimationEffect"/> enum values that specify the animation
        /// type applied when animating the Tabs component. This effect is applied when moving
        /// to the next tab component.
        /// Default value is SlideRightIn.
        /// </summary>
        [Parameter]
        public AnimationEffect AnimationNextEffect { get; set; } = AnimationEffect.SlideRightIn;

        /// <summary>
        /// One of the <see cref="AnimationEffect"/> enum values that specify the animation type
        /// applied when animating the Tabs component. This effect is applied when moving to the
        /// previous tab component.
        /// Default value is SlideLeftIn.
        /// </summary>
        [Parameter]
        public AnimationEffect AnimationPreviousEffect { get; set; } = AnimationEffect.SlideLeftIn;

        /// <summary>
        /// A <see cref="RenderFragment"/> object containing the child content for the tab. 
        /// Default value is null.
        /// </summary>
        [Parameter]
        public RenderFragment ChildContent { get; set; }

        /// <summary>
        /// String value that specifies the area in which the draggable element movement will be
        /// occurring. Outside that area will be restricted for the draggable element movement. 
        /// By default, the draggable element movement occurs with TabItems. 
        /// Default value is null.
        /// </summary>
        [Parameter]
        public string DragArea { get; set; }

        /// <summary>
        /// Boolean value specifying the persistence of the component's state between page reloads. 
        /// If enabled, the tab’s selected item will be persisted.
        /// Default value is false.
        /// <remarks>
        /// The <see cref="CssId"/> parameter is used as the key in the browser local storage.
        /// Best practice is to set the CssId parameter if persistence is enabled.
        /// </remarks>
        /// </summary>
        [Parameter]
        public bool EnablePersistence { get; set; }

        /// <summary>
        /// A <see cref="HeaderPosition"/> object specifying the position of the Tab headers.
        /// Default value is Top.
        /// </summary>
        [Parameter]
        public HeaderPosition HeaderPlacement { get; set; } = HeaderPosition.Top;

        /// <summary>
        /// A read-only <see cref="List{T}"/> where T is a <see cref="TabItem"/> containing a list
        /// of items used to configure the Tabs component.
        /// </summary>
        [Parameter]
        public List<TabItem> TabItems { get; set; }

        /// <summary>
        /// One of the <see cref="ContentLoad"/> enums specifying the loading modes for Tab contents.
        /// Default value is Dynamic (only the content of the current tab is loaded in the DOM).
        /// <remarks>
        /// See <a href="https://blazor.syncfusion.com/documentation/tabs/content-render-modes">Content Render Modes</a>
        /// for additional information.
        /// </remarks>
        /// </summary>
        [Parameter]
        public ContentLoad LoadOn { get; set; } = ContentLoad.Dynamic;

        /// <summary>
        /// One of the <see cref="OverflowMode"/> enums specifying the display mode applied when the
        /// Tabs exceeds the viewing area.
        /// Default value is Scrollable (All the elements will be displayed in a single line with
        /// horizontal scrolling enabled).
        /// <remarks>
        /// See <a href="https://help.syncfusion.com/cr/blazor/Syncfusion.Blazor.Navigations.OverflowMode.html">Overflow Modes</a>
        /// for additional information.
        /// </remarks>
        /// </summary>
        [Parameter]
        public OverflowMode OverflowMode { get; set; } = OverflowMode.Scrollable;

        /// <summary>
        /// Boolean value specifying whether to re-order tab items to show active tab item in the
        /// Tab Header area or popup (when <see cref="OverflowMode"/> is Popup). 
        /// Default value is true (active tab item should be visible in header area).
        /// </summary>
        [Parameter]
        public bool ReorderActiveTab { get; set; } = true;

        /// <summary>
        /// Integer value specifying the scrolling distance (in pixels) applied when scrolling in Tab
        /// and <see cref="OverflowMode"/> is Scrollable.
        /// Default value is 200 (pixels).
        /// </summary>
        [Parameter]
        public int ScrollStep { get; set; } = 200;

        /// <summary>
        /// Integer value specifying  the index of the active Tab item.
        /// Default value is 0 (the first Tab).
        /// </summary>
        [Parameter]
        public int SelectedItem { get; set; }

        /// <summary>
        /// Boolean value specifying whether to show the close button in the Tab header or not.
        /// Default value is false.
        /// </summary>
        [Parameter]
        public bool ShowCloseButton { get; set; }

        #endregion


        #region Event Callback Parameters

        /// <summary>
        /// An <see cref="EventCallback{TValue}"/> where TValue is an <see cref="AddEventArgs"/>.
        /// The consumer's method is invoked when a tab is added to the tabs collection.
        /// </summary>
        [Parameter]
        public EventCallback<AddEventArgs> Added { get; set; }

        /// <summary>
        /// An <see cref="EventCallback{TValue}"/> where TValue is an <see cref="AddEventArgs"/>.
        /// The consumer's method is invoked before a tab is added to the tabs collection.
        /// </summary>
        [Parameter]
        public EventCallback<AddEventArgs> Adding { get; set; }

        /// <summary>
        /// An <see cref="EventCallback{TValue}"/> where TValue is an <see cref="object"/>.
        /// The consumer's method is invoked after the tab component is rendered.
        /// </summary>
        [Parameter]
        public EventCallback<object> Created { get; set; }

        /// <summary>
        /// An <see cref="EventCallback{TValue}"/> where TValue is an <see cref="object"/>.
        /// The consumer's method is invoked after the tab component is destroyed.
        /// </summary>
        [Parameter]
        public EventCallback<object> Destroyed { get; set; }

        /// <summary>
        /// An <see cref="EventCallback{TValue}"/> where TValue is an <see cref="DragEventArgs"/>.
        /// The consumer's method is invoked after the tab header element is dropped.
        /// </summary>
        [Parameter]
        public EventCallback<DragEventArgs> Dragged { get; set; }

        /// <summary>
        /// An <see cref="EventCallback{TValue}"/> where TValue is an <see cref="DragEventArgs"/>.
        /// The consumer's method is invoked before the tab header element is dragged.
        /// </summary>
        [Parameter]
        public EventCallback<DragEventArgs> OnDragStart { get; set; }

        /// <summary>
        /// An <see cref="EventCallback{TValue}"/> where TValue is an <see cref="RemoveEventArgs"/>.
        /// The consumer's method is invoked after removing a tab from the tab collection.
        /// </summary>
        [Parameter]
        public EventCallback<RemoveEventArgs> Removed { get; set; }

        /// <summary>
        /// An <see cref="EventCallback{TValue}"/> where TValue is an <see cref="RemoveEventArgs"/>.
        /// The consumer's method is invoked before removing a tab from the tab collection.
        /// </summary>
        [Parameter]
        public EventCallback<RemoveEventArgs> Removing { get; set; }

        /// <summary>
        /// An <see cref="EventCallback{TValue}"/> where TValue is an <see cref="SelectEventArgs"/>.
        /// The consumer's method is invoked after a tab item is selected.
        /// </summary>
        [Parameter]
        public EventCallback<SelectEventArgs> Selected { get; set; }

        /// <summary>
        /// An <see cref="EventCallback{TValue}"/> where TValue is an <see cref="SelectingEventArgs"/>.
        /// The consumer's method is invoked before a tab item is selected.
        /// </summary>
        [Parameter]
        public EventCallback<SelectingEventArgs> Selecting { get; set; }

        /// <summary>
        /// An <see cref="EventCallback{TValue}"/> where TValue is an <see cref="int"/>.
        /// The consumer's method is invoked when the selected tab changes.
        /// The parameter is an integer value which is the index of the selected tab.
        /// Handling this callback adds the responsibility of updating the Selected parameter.
        /// </summary>
        [Parameter]
        public EventCallback<int> SelectedItemChanged { get; set; }

        #endregion


        #region CSS Parameters

        /// <summary>
        /// String value containing CSS class definition(s) that will be injected in the root
        /// HTML div element of the Tab container.
        /// Default value is string.Empty.
        /// </summary>
        [Parameter]
        public string CssClass { get; set; } = string.Empty;

        /// <summary>
        /// String value containing CSS ID that will be injected in the root HTML div element
        /// of the Tab container.
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
        /// in root element. If both property and equivalent HTML attribute are configured, the component
        /// considers the property value. This is a <see cref="Dictionary{TKey, TValue}"/> where TKey is
        /// a <see cref="string"/> and TValue is an <see cref="object"/>.
        /// Default value is an empty dictionary.
        /// </summary>
        [Parameter]
        public Dictionary<string, object> HtmlAttributes { get; set; } = new();

        /// <summary>
        /// String value that specifies the CSS <a href="https://developer.mozilla.org/en-US/docs/Web/CSS/height">height</a>
        /// value of the Tab container. 
        /// The height CSS property specifies the height of an element.
        /// By default, Tab height is set based on the height of its parent.
        /// Default value is 100%
        /// </summary>
        [Parameter]
        public string Height { get; set; } = "100%";

        /// <summary>
        /// String value that specifies the CSS <a href="https://developer.mozilla.org/en-US/docs/Web/CSS/width">width</a> value 
        /// specifying the width of the Tab container. 
        /// The width CSS property sets an element's width.
        /// By default, Tab width sets based on the width of its parent.
        /// Default value is 100%
        /// </summary>
        [Parameter]
        public string Width { get; set; } = "100%";


        // ==================== Tab Header Styles ====================


        /// <summary>
        /// String value that specifies the CSS <a href="https://developer.mozilla.org/en-US/docs/Web/CSS/border-width">border-width</a>
        /// value used for the border between the Tab Items and the Tab Content, and around the active Tab Item.
        /// The border-width shorthand CSS property sets the width of an element's border.
        /// Default value is 1px.
        /// </summary>
        [Parameter]
        public string TabHeaderBorderWidth { get; set; } = "1px";

        /// <summary>
        /// String value that specifies the CSS <a href="https://developer.mozilla.org/en-US/docs/Web/CSS/border-color">border-color</a>
        /// value used for the border between the Tab Items and the Tab Content, and around the active Tab Item.
        /// The border-color shorthand CSS property sets the color of an element's border.
        /// Default value is #DEE2E6.
        /// </summary>
        [Parameter]
        public string TabHeaderBorderColor { get; set; } = "#DEE2E6";

        /// <summary>
        /// String value that specifies the CSS <a href="https://developer.mozilla.org/en-US/docs/Web/CSS/border-style">border-style</a>
        /// value used for the border between the Tab Items and the Tab Content, and around the active Tab Item.
        /// The border-style shorthand CSS property sets the line style for all four sides of an element's border.
        /// Default value is solid.
        /// </summary>
        [Parameter]
        public string TabHeaderBorderStyle { get; set; } = "solid";

        /// <summary>
        /// String value that specifies the CSS <a href="https://developer.mozilla.org/en-US/docs/Web/CSS/border-radius">border-radius</a>
        /// value used for the border around the Tab Header.
        /// The border-radius CSS property rounds the corners of an element's outer border edge.
        /// Default value is 0px.
        /// </summary>
        [Parameter]
        public string TabHeaderBorderRadius { get; set; } = "0px";

        /// <summary>
        /// String value that specifies the CSS <a href="https://developer.mozilla.org/en-US/docs/Web/CSS/background-color">background-color</a>
        /// value used for the Tab Header.
        /// The background-color CSS property sets the background color of an element.
        /// Default value is #FFF.
        /// </summary>
        [Parameter]
        public string TabHeaderBackgroundColor { get; set; } = "#FFF";

        /// <summary>
        /// String value that specifies the CSS <a href="https://developer.mozilla.org/en-US/docs/Web/CSS/border-width">border-width</a>
        /// value used for the border around the inactive, hovered Tab Item.
        /// The border-width shorthand CSS property sets the width of an element's border.
        /// Default value is 1px.
        /// </summary>
        [Parameter]
        public string TabHeaderInactiveHoveredBorderWidth { get; set; } = "1px";

        /// <summary>
        /// String value that specifies the CSS <a href="https://developer.mozilla.org/en-US/docs/Web/CSS/border-color">border-color</a>
        /// value used for the border around the inactive, hovered Tab Item.
        /// The border-color shorthand CSS property sets the color of an element's border.
        /// Default value is #E9ECEF.
        /// </summary>
        [Parameter]
        public string TabHeaderInactiveHoveredBorderColor { get; set; } = "#E9ECEF";

        /// <summary>
        /// String value that specifies the CSS <a href="https://developer.mozilla.org/en-US/docs/Web/CSS/border-style">border-style</a>
        /// value used for the border around the inactive, hovered Tab Item.
        /// The border-style shorthand CSS property sets the line style for all four sides of an element's border.
        /// Default value is solid.
        /// </summary>
        [Parameter]
        public string TabHeaderInactiveHoveredBorderStyle { get; set; } = "solid";

        /// <summary>
        /// String value that specifies the CSS <a href="https://developer.mozilla.org/en-US/docs/Web/CSS/length">length</a>
        /// data type used to add extra spacing around the Tab Header.
        /// The length CSS data type represents a distance value.
        /// Note that only a single pixel value should be set (e.g., 4px). The component will apply the
        /// margin as appropriate based on the <see cref="HeaderPlacement"/> parameter.
        /// Default value is 4px.
        /// </summary>
        [Parameter]
        public string TabHeaderExtraMargin { get; set; } = "4px";


        // ==================== Tab Item Styles ====================


        /// <summary>
        /// String value that specifies the CSS <a href="https://developer.mozilla.org/en-US/docs/Web/CSS/length">length</a>
        /// data type used for the border around the active Tab Item.
        /// The length CSS data type represents a distance value.
        /// Note that only a single pixel value (e.g., 4px) should be set. The proper application
        /// of this value is handled by the component
        /// Default value is 4px.
        /// </summary>
        [Parameter]
        public string TabItemBorderRadius { get; set; } = "4px";

        /// <summary>
        /// String value that specifies the CSS <a href="https://developer.mozilla.org/en-US/docs/Web/CSS/border-color">border-color</a>
        /// value used for the bottom border for the active Tab Item.
        /// The border-color shorthand CSS property sets the color of an element's border.
        /// Default value is string.Empty indicating the <see cref="TabHeaderBackgroundColor"/> parameter
        /// value will be used.
        /// </summary>
        [Parameter]
        public string TabItemActiveBottomBorderColor { get; set; } = string.Empty;

        /// <summary>
        /// String value that specifies the CSS <a href="https://developer.mozilla.org/en-US/docs/Web/CSS/margin-right">margin-right</a>
        /// value used for spacing between the Tab Items.
        /// The margin-right CSS property sets the margin area on the right side of an element.
        /// The margin value will be applied as needed based on the <see cref="HeaderPlacement"/> parameter.
        /// Default value is 2px.
        /// </summary>
        [Parameter]
        public string TabItemMargin { get; set; } = "2px";

        /// <summary>
        /// String value that specifies the CSS <a href="https://developer.mozilla.org/en-US/docs/Web/CSS/height">height</a>
        /// value of the Tab Items. 
        /// The height CSS property specifies the height of an element.
        /// Default value is 32px
        /// </summary>
        [Parameter]
        public string TabItemHeight { get; set; } = "32px";


        // ==================== Tab Text Styles ====================


        /// <summary>
        /// String value that specifies the CSS <a href="https://developer.mozilla.org/en-US/docs/Web/CSS/font-size">font-size</a>
        /// value used for the active Tab Item font size.
        /// The font-size CSS property sets the size of the font.
        /// Default value is 14px.
        /// </summary>
        [Parameter]
        public string TabTextActiveFontSize { get; set; } = "14px";

        /// <summary>
        /// String value that specifies the CSS <a href="https://developer.mozilla.org/en-US/docs/Web/CSS/color">color</a>
        /// value used for the active Tab Item font color.
        /// The color CSS property sets the foreground color value of an element's text and text decorations.
        /// Default value is #777.
        /// </summary>
        [Parameter]
        public string TabTextActiveFontColor { get; set; } = "#777";

        /// <summary>
        /// String value that specifies the CSS <a href="https://developer.mozilla.org/en-US/docs/Web/CSS/font-weight">font-weight</a>
        /// value used for the active Tab Item font color.
        /// The font-weight CSS property sets the weight (or boldness) of the font.
        /// Default value is 400.
        /// </summary>
        [Parameter]
        public string TabTextActiveFontWeight { get; set; } = "400";

        /// <summary>
        /// String value that specifies the CSS <a href="https://developer.mozilla.org/en-US/docs/Web/CSS/font-size">font-size</a>
        /// value used for the inactive Tab Item font size.
        /// The font-size CSS property sets the size of the font.
        /// Default value is 14px.
        /// </summary>
        [Parameter]
        public string TabTextInactiveFontSize { get; set; } = "14px";

        /// <summary>
        /// String value that specifies the CSS <a href="https://developer.mozilla.org/en-US/docs/Web/CSS/color">color</a>
        /// value used for the inactive Tab Item font color.
        /// The color CSS property sets the foreground color value of an element's text and text decorations.
        /// Default value is #007BFF.
        /// </summary>
        [Parameter]
        public string TabTextInactiveFontColor { get; set; } = "#007BFF";

        /// <summary>
        /// String value that specifies the CSS <a href="https://developer.mozilla.org/en-US/docs/Web/CSS/font-weight">font-weight</a>
        /// value used for the inactive Tab Item font color.
        /// The font-weight CSS property sets the weight (or boldness) of the font.
        /// Default value is 400.
        /// </summary>
        [Parameter]
        public string TabTextInactiveFontWeight { get; set; } = "400";

        /// <summary>
        /// String value that specifies the CSS <a href="https://developer.mozilla.org/en-US/docs/Web/CSS/font-size">font-size</a>
        /// value used for the active, hovered Tab Item font size.
        /// The font-size CSS property sets the size of the font.
        /// Default value is 14px.
        /// </summary>
        [Parameter]
        public string TabTextActiveHoveredFontSize { get; set; } = "14px";

        /// <summary>
        /// String value that specifies the CSS <a href="https://developer.mozilla.org/en-US/docs/Web/CSS/color">color</a>
        /// value used for the active, hovered Tab Item font color.
        /// The color CSS property sets the foreground color value of an element's text and text decorations.
        /// Default value is #495057.
        /// </summary>
        [Parameter]
        public string TabTextActiveHoveredFontColor { get; set; } = "#495057";

        /// <summary>
        /// String value that specifies the CSS <a href="https://developer.mozilla.org/en-US/docs/Web/CSS/font-weight">font-weight</a>
        /// value used for the active, hovered Tab Item font color.
        /// The font-weight CSS property sets the weight (or boldness) of the font.
        /// Default value is 400.
        /// </summary>
        [Parameter]
        public string TabTextActiveHoveredFontWeight { get; set; } = "400";

        /// <summary>
        /// String value that specifies the CSS <a href="https://developer.mozilla.org/en-US/docs/Web/CSS/font-size">font-size</a>
        /// value used for the inactive, hovered Tab Item font size.
        /// The font-size CSS property sets the size of the font.
        /// Default value is 14px.
        /// </summary>
        [Parameter]
        public string TabTextInactiveHoveredFontSize { get; set; } = "14px";

        /// <summary>
        /// String value that specifies the CSS <a href="https://developer.mozilla.org/en-US/docs/Web/CSS/color">color</a>
        /// value used for the inactive, hovered Tab Item font color.
        /// The color CSS property sets the foreground color value of an element's text and text decorations.
        /// Default value is #007BFF.
        /// </summary>
        [Parameter]
        public string TabTextInactiveHoveredFontColor { get; set; } = "#007BFF";

        /// <summary>
        /// String value that specifies the CSS <a href="https://developer.mozilla.org/en-US/docs/Web/CSS/font-weight">font-weight</a>
        /// value used for the inactive, hovered Tab Item font color.
        /// The font-weight CSS property sets the weight (or boldness) of the font.
        /// Default value is 400.
        /// </summary>
        [Parameter]
        public string TabTextInactiveHoveredFontWeight { get; set; } = "400";

        /// <summary>
        /// String containing the CSS <a href="https://developer.mozilla.org/en-US/docs/Web/CSS/padding">padding</a>
        /// value used for the text.
        /// The padding CSS shorthand property sets the padding area on all four sides of an element at once.
        /// Specify the padding in top, right, bottom, left order. Or use any of the padding shorthands.
        /// Default value is: 0px on all sides.
        /// </summary>
        [Parameter]
        public string TabTextPadding { get; set; } = "0px";


        // ==================== Scroll Button Styles ====================


        /// <summary>
        /// String value that specifies the CSS <a href="https://developer.mozilla.org/en-US/docs/Web/CSS/color">color</a>
        /// value used for the Scroll Button's icon when not hovered.
        /// The color CSS property sets the foreground color value of an element's text and text decorations.
        /// Default value is #212529.
        /// </summary>
        [Parameter]
        public string ScrollButtonNormalFontColor { get; set; } = "#212529";

        /// <summary>
        /// String value that specifies the CSS <a href="https://developer.mozilla.org/en-US/docs/Web/CSS/color">color</a>
        /// value used for the Scroll Button's icon when hovered.
        /// The color CSS property sets the foreground color value of an element's text and text decorations.
        /// Default value is #FFF.
        /// </summary>
        [Parameter]
        public string ScrollButtonHoveredFontColor { get; set; } = "#FFF";

        /// <summary>
        /// String value that specifies the CSS <a href="https://developer.mozilla.org/en-US/docs/Web/CSS/background">background</a>
        /// value used for the Scroll Button when not hovered.
        /// The background shorthand CSS property sets all background style properties at once, such as color,
        /// image, origin and size, or repeat method.
        /// Default value is none.
        /// </summary>
        [Parameter]
        public string ScrollButtonNormalBackground { get; set; } = "none";

        /// <summary>
        /// String value that specifies the CSS <a href="https://developer.mozilla.org/en-US/docs/Web/CSS/background">background</a>
        /// value used for the Scroll Button when hovered.
        /// The background shorthand CSS property sets all background style properties at once, such as color,
        /// image, origin and size, or repeat method.
        /// Default value is #5A6268.
        /// </summary>
        [Parameter]
        public string ScrollButtonHoveredBackground { get; set; } = "#5A6268";

        /// <summary>
        /// String value that specifies the CSS <a href="https://developer.mozilla.org/en-US/docs/Web/CSS/border-radius">border-radius</a>
        /// value used for the Scroll Buttons.
        /// The border-radius CSS property rounds the corners of an element's outer border edge.
        /// Default value is 0px.
        /// </summary>
        [Parameter]
        public string ScrollButtonBorderRadius { get; set; } = "0px";

        /// <summary>
        /// String value that specifies the CSS <a href="https://developer.mozilla.org/en-US/docs/Web/CSS/font-size">font-size</a>
        /// value used for the Scroll Buttons' icon font size.
        /// The font-size CSS property sets the size of the font.
        /// Default value is 10px.
        /// </summary>
        [Parameter]
        public string ScrollButtonFontSize { get; set; } = "10px";

        #endregion

        #endregion


        #region Callback Events Invoked from Underlying Components

        private async Task OnSelectedItemChangedHandler(int index)
        {
            //Debug.WriteLine("OnSelectedItemChangedHandler method invoked.");
            if (SelectedItemChanged.HasDelegate) await SelectedItemChanged.InvokeAsync(index);
            else SelectedItem = index;
        }

        #endregion


        #region Instance Variables

        private SfTab tab;
        private TabAnimationSettings tabAnimationSettings;
        private string masterCssSelector = string.Empty;
        private string tabHeaderBorderWidth;
        private string activeTabBorder;
        private string activeTabBottomBorder;
        private string activeTabBorderRadius;
        private string inactiveTabBorderHoveredBorders;
        private string tabItemMargin;

        #endregion


        #region Constructors

        [SuppressMessage("Usage", "BL0005:Component parameter should not be set outside of its component.")]
        protected override void OnParametersSet()
        {
            // Build the master selectors
            //  Since it is possible for a TabBase to contain a Tab which contains a TabBase the
            //  method for CSS isolation needs to change from the normal approach.
            //  The normal approach assumes use of the same component on a page is a
            //  side - bt - side application.In the case of parent - child applications the
            //  CSS for the parent will have greater specificity that the child's CSS since
            //  a child element(e.g., e - tab - header) is also addressable using the parent's
            //  CSS class (the.mychild.e-tab.e-tab-header is addressable by.myparent, e-tab e-tab-header).
            //  Given that the style for the parent comes after the child's style, the parent's
            //  style ultimately wins out.
            //  The solution to using the same component in a parent-child scenario is to
            //  use the direct child relationship in the CSS selectors(.mychild.e-tab.e-tab-header
            //  is not a direct child of.myparent.e-tab).
            masterCssSelector = (CssClass == string.Empty) ? ".e-tab >" : $".{ CssClass }.e-tab >";

            // Construct the Animation parameter
            // Handle case when Effect = None
            tabAnimationSettings = new() { Next = new(), Previous = new() };
            tabAnimationSettings.Next.Duration = tabAnimationSettings.Previous.Duration = AnimationDuration;
            tabAnimationSettings.Next.Easing = tabAnimationSettings.Previous.Easing = "ease";
            tabAnimationSettings.Next.Effect = AnimationNextEffect;
            tabAnimationSettings.Previous.Effect = AnimationPreviousEffect;

            // Construct the tab header border widths for horizontal and vertical headers
            tabHeaderBorderWidth = HeaderPlacement switch
            {
                HeaderPosition.Top => $"0px 0px { TabHeaderBorderWidth }",
                HeaderPosition.Bottom => $"{ TabHeaderBorderWidth } 0px 0px 0px",
                HeaderPosition.Left => $"0px { TabHeaderBorderWidth } 0px 0px",
                HeaderPosition.Right => $"0px 0px 0px { TabHeaderBorderWidth }",
                _ => "",
            };

            // Construct the border styling for the active tab
            activeTabBorder = $"{ TabHeaderBorderWidth } { TabHeaderBorderStyle } { TabHeaderBorderColor }";

            // Construct the active tab item bottom border.
            // If TabItemActiveBottomBorderColor has been set, use that, otherwise use TabHeaderBackgroundColor
            activeTabBottomBorder = (TabItemActiveBottomBorderColor == string.Empty) ?
                $"{ TabHeaderBorderWidth } { TabHeaderBorderStyle } { TabHeaderBackgroundColor }" :
                $"{ TabHeaderBorderWidth } { TabHeaderBorderStyle } { TabItemActiveBottomBorderColor }";

            // Construct the inactive tab hovered left, to and right borders
            inactiveTabBorderHoveredBorders = $"{ TabHeaderInactiveHoveredBorderWidth } { TabHeaderInactiveHoveredBorderStyle } { TabHeaderInactiveHoveredBorderColor }";

            // Construct the border radii for the active tab
            activeTabBorderRadius = HeaderPlacement switch
            {
                HeaderPosition.Top => $"{ TabItemBorderRadius } { TabItemBorderRadius } 0px 0px",
                HeaderPosition.Bottom => $"0px 0px { TabItemBorderRadius } { TabItemBorderRadius }",
                HeaderPosition.Left => $"{ TabItemBorderRadius } 0px 0px { TabItemBorderRadius }",
                HeaderPosition.Right => $"0px { TabItemBorderRadius } { TabItemBorderRadius } 0px",
                _ => "",
            };

            // Construct the Tab Item margins
            tabItemMargin = HeaderPlacement switch
            {
                HeaderPosition.Top or HeaderPosition.Bottom => $"0px { TabItemMargin } 0px 0px",
                HeaderPosition.Left or HeaderPosition.Right => $"0px 0px { TabItemMargin } 0px",
                _ => "",
            };
        }

        #endregion


        #region Public Methods Providing Access to the Underlying Components to the Consumer

        /// <summary>
        /// Adds a list of Tab items to the Tab.
        /// </summary>
        /// <param name="items">
        /// List of <see cref="TabItem"/> to add to the Tab.
        /// </param>
        /// <param name="index">
        /// Index value that determines where the items to be added
        /// </param>
        public async Task AddTabAsync(List<TabItem> items, int index) => 
            await tab.AddTab(items, index);

        /// <summary>
        /// Disables/enables the Tab component.
        /// </summary>
        /// <param name="disable">
        /// Boolean value specifying is the Tab component is disabled (true) or enabled.
        /// </param>
        public async Task DisableAsync(bool disable) => 
            await tab.DisableAsync(disable);

        /// <summary>
        /// Enables or disables a specific tab item.
        /// </summary>
        /// <param name="index">
        /// Integer value specifying the index of the tab.
        /// </param>
        /// <param name="isEnable">
        /// Boolean value specifying is the tab is enabled (true) or disabled.
        /// </param>
        public async Task EnableTabAsync(int index, bool isEnable = true) => 
            await tab.EnableTabAsync(index, isEnable);

        /// <summary>
        /// Returns the content element of the tab with the specified index.
        /// </summary>
        /// <param name="index">
        /// Integer value specifying the index of the tab whose content is to returned.
        /// </param>
        /// <returns>
        /// A <see cref="DOM"/> object containing the contents of the tab.
        /// </returns>
        public async Task<DOM> GetTabContentAsync(int index) =>
            await tab.GetTabContent(index);

        /// <summary>
        /// Returns the header element of the tab with the specified index.
        /// </summary>
        /// <param name="index">
        /// Integer value specifying the index of the tab whose content is to returned.
        /// </param>
        /// <returns>
        /// A <see cref="DOM"/> object containing the header element of the tab.
        /// </returns>
        public async Task<DOM> GetTabItemAsync(int index) =>
            await tab.GetTabItem(index);

        /// <summary>
        /// Show/hides a tab based on the specified index.
        /// </summary>
        /// <param name="index">
        /// Integer value specifying the index of the tab to show/hide.
        /// </param>
        /// <param name="show">
        /// Boolean value specifying if the tab is shown (true) or hidden.
        /// </param>
        public async Task HideTabAsync(int index, bool show) => 
            await tab.HideTabAsync(index, show);

        /// <summary>
        /// Refresh the entire Tab component.
        /// </summary>
        public async Task RefreshAsync() => 
            await tab.RefreshAsync();

        /// <summary>
        /// Removes an entire tab from the collection of tabs.
        /// </summary>
        /// <param name="index">
        /// Integer value specifying the index of the tab to remove.
        /// </param>
        public async Task RemoveTabAsync(int index) => 
            await tab.RemoveTab(index);

        /// <summary>
        /// Activates a tab based on the specified index.
        /// </summary>
        /// <param name="index">
        /// Integer value specifying the index of the tab to activate.
        /// </param>
        public async Task SelectAsync(int index) => 
            await tab.SelectAsync(index);

        #endregion
    }
}
