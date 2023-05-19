using Microsoft.AspNetCore.Components;
using Syncfusion.Blazor.Navigations;

namespace Code420.UIOrchestrator.Server.Components.BaseComponents.MenuBase
{
    /// <summary>
    /// <para>
    /// Encapsulates the SfMenu component with the goal of exposing various CSS styling options,
    /// and to overload menu events. 
    /// </para>
    ///<para>
    /// The ChildContent parameter captures the menu item definition either through one of the
    /// <c>MenuItems</c> and <c>MenuItem</c> component, through the <c>MenuFieldSettings</c> component,
    /// or through the <c>MenuTemplates</c> component. An example for <c>MenuTemplates</c> can be found
    /// at: Menu Templates: https://blazor.syncfusion.com/demos/menu-bar/templates?theme=bootstrap4
    /// </para>
    ///<para>
    /// Note that only one of these menu definition schemes should be used. I don't know that they
    /// can be intermixed.
    /// </para>
    ///<para>
    /// The <c>MenuEvents</c> component should not be defined. Use this components event parameters instead.
    /// </para>
    /// </summary>
    /// <typeparam name="TValue">
    /// A generic that defines the object type used define the menu contents.
    /// </typeparam>

    public partial class MenuBase<TValue> : ComponentBase
    {

        #region Component Parameters

        #region Base Parameters

        /// <summary>
        /// Boolean value specifying whether to enable or disable the scrollable option in menu bar.
        /// Default value is false.
        /// </summary>
        [Parameter]
        public bool EnableScrolling { get; set; }

        /// <summary>
        /// Contains the <see cref="RenderFragment" /> composing the Menu contents.
        /// The default value is null.
        /// </summary>
        [Parameter]
        public RenderFragment ChildContent { get; set; }

        /// <summary>
        /// Boolean value specifying if hamburger mode is enabled in the Menu.
        /// The default value is false.
        /// </summary>
        [Parameter]
        public bool HamburgerMode { get; set; }

        /// <summary>
        /// A <see cref="List{T}"/> where T is <see cref="TValue"/> containing the menu items
        /// with their properties to be rendered in the Menu.
        /// The default value is null.
        /// </summary>
        [Parameter]
        public List<TValue> Items { get; set; }

        /// <summary>
        /// One of the <see cref="Syncfusion.Blazor.Navigations.Orientation"/> enums specifying
        /// the orientation of the Menu.
        /// The default value is Horizontal.
        /// </summary>
        [Parameter]
        public Orientation Orientation { get; set; } = Orientation.Horizontal;

        /// <summary>
        /// Boolean value specifying whether or not to show the submenu item on click.
        /// When set to true, the submenu will open only on mouse click.
        /// The default value is false.
        /// </summary>
        [Parameter]
        public bool ShowItemOnClick { get; set; } = true;

        /// <summary>
        /// String value specifying the CSS class whose HTML element the Menu will open/close
        /// when <see cref="HamburgerMode"/> is enabled.
        /// This must be a full CSS class specification (e.g., <c>.sidebar__menu</c>).
        /// Default value is string.Empty.
        /// </summary>
        [Parameter]
        public string Target { get; set; } = string.Empty;

        /// <summary>
        /// String value specifying the title text displayed when <see cref="HamburgerMode"/>
        /// is enabled.
        /// Default value is string.Empty.
        /// </summary>
        [Parameter]
        public string Title { get; set; } = string.Empty;

        #endregion


        #region Event Callback Parameters

        /// <summary>
        /// An <see cref="EventCallback"/> containing the consumer's method invoked when
        /// closing the menu.
        /// </summary>
        [Parameter]
        public EventCallback<OpenCloseMenuEventArgs<TValue>> Closed { get; set; }

        /// <summary>
        /// An <see cref="EventCallback"/> containing the consumer's method invoked once
        /// the Menu rendering is completed.
        /// </summary>
        [Parameter]
        public EventCallback<object> Created { get; set; }

        /// <summary>
        /// An <see cref="EventCallback"/> containing the consumer's method invoked while
        /// selecting menu item.
        /// </summary>
        [Parameter]
        public EventCallback<MenuEventArgs<TValue>> ItemSelected { get; set; }

        /// <summary>
        /// An <see cref="EventCallback"/> containing the consumer's method invoked before
        /// closing the menu.
        /// </summary>
        [Parameter]
        public EventCallback<BeforeOpenCloseMenuEventArgs<TValue>> OnClose { get; set; }

        /// <summary>
        /// An <see cref="EventCallback"/> containing the consumer's method invoked while
        /// rendering each menu item.
        /// </summary>
        [Parameter]
        public EventCallback<MenuEventArgs<TValue>> OnItemRender { get; set; }

        /// <summary>
        /// An <see cref="EventCallback"/> containing the consumer's method invoked before
        /// opening the menu item.
        /// </summary>
        [Parameter]
        public EventCallback<BeforeOpenCloseMenuEventArgs<TValue>> OnOpen { get; set; }

        /// <summary>
        /// An <see cref="EventCallback"/> containing the consumer's method invoked while
        /// opening the menu item.
        /// </summary>
        [Parameter]
        public EventCallback<OpenCloseMenuEventArgs<TValue>> Opened { get; set; }

        #endregion


        #region CSS Parameters

        /// <summary>
        /// String value containing CSS class definition(s) that will be injected in the root
        /// HTML div element of the Menu container and the Popup container.
        /// Default value is string.Empty.
        /// </summary>
        [Parameter]
        public string CssClass { get; set; } = string.Empty;


        // ==================== Menu Container Styles ====================


        /// <summary>
        /// String value that specifies the CSS <a href="https://developer.mozilla.org/en-US/docs/Web/CSS/border">border</a>
        /// value used for the border around the Menu container.
        /// The border shorthand CSS property sets an element's border. It sets the values of border-width, border-style,
        /// and border-color.
        /// Default value is none.
        /// </summary>
        [Parameter]
        public string MenuBorder { get; set; } = "none";

        /// <summary>
        /// String value that specifies the CSS <a href="https://developer.mozilla.org/en-US/docs/Web/CSS/border-radius">border-radius</a>
        /// value used for the border around the Menu container.
        /// The border-radius CSS property rounds the corners of an element's outer border edge.
        /// Default value is 4px.
        /// </summary>
        [Parameter]
        public string MenuBorderRadius { get; set; } = "4px";

        /// <summary>
        /// String value that specifies the CSS <a href="https://developer.mozilla.org/en-US/docs/Web/CSS/color">color</a>
        /// value applied to the text element of each Menu Item.
        /// The color CSS property sets the foreground color value of an element's text and text decorations, and
        /// sets the currentcolor value.
        /// Default value is #007BFF.
        /// </summary>
        [Parameter]
        public string MenuFontColor { get; set; } = "#007BFF";

        /// <summary>
        /// String value that specifies the CSS <a href="https://developer.mozilla.org/en-US/docs/Web/CSS/font-size">font-size</a>
        /// value applied to the text element of each Menu Item.
        /// The font-size CSS property sets the size of the font.
        /// Default value is 14px.
        /// </summary>
        [Parameter]
        public string MenuFontSize { get; set; } = "14px";

        /// <summary>
        /// String value that specifies the CSS <a href="https://developer.mozilla.org/en-US/docs/Web/CSS/font-weight">font-weight</a>
        /// value applied to the text element of each Menu Item.
        /// The font-weight CSS property sets the weight (or boldness) of the font.
        /// Default value is normal.
        /// </summary>
        [Parameter]
        public string MenuFontWeight { get; set; } = "normal";

        /// <summary>
        /// String value that specifies the CSS <a href="https://developer.mozilla.org/en-US/docs/Web/CSS/text-align">text-align</a>
        /// value applied to the text element of each Menu Item.
        /// The text-align CSS property sets the horizontal alignment of the content inside a
        /// block element or table-cell box.
        /// Default value is left.
        /// </summary>
        [Parameter]
        public string MenuTextAlign { get; set; } = "left";

        /// <summary>
        /// String value that specifies the CSS <a href="https://developer.mozilla.org/en-US/docs/Web/CSS/background-color">background-color</a>
        /// value applied to the Menu container.
        /// The background-color CSS property sets the background color of an element.
        /// Default value is #FFF.
        /// </summary>
        [Parameter]
        public string MenuBackgroundColor { get; set; } = "#FFF";

        /// <summary>
        /// String value that specifies the CSS <a href="https://developer.mozilla.org/en-US/docs/Web/CSS/min-width">min-width</a>
        /// value applied to the Menu container.
        /// The min-width CSS property sets the minimum width of an element.
        /// Default value is 120px.
        /// </summary>
        [Parameter]
        public string MenuMinimumWidth { get; set; } = "120px";

        /// <summary>
        /// String value that specifies the CSS <a href="https://developer.mozilla.org/en-US/docs/Web/CSS/width">width</a>
        /// value applied to the Menu container.
        /// The width CSS property sets an element's width.
        /// Default value is inherit.
        /// </summary>
        [Parameter]
        public string MenuWidth { get; set; } = "inherit";

        /// <summary>
        /// String value that specifies the CSS <a href="https://developer.mozilla.org/en-US/docs/Web/CSS/margin">margin</a>
        /// value applied to the Menu container.
        /// The text-align CSS property sets the horizontal alignment of the content inside a block element or table-cell box.
        /// Default value is 0px.
        /// </summary>
        [Parameter]
        public string MenuMargin { get; set; } = "0px";

        /// <summary>
        /// String value that specifies the CSS <a href="https://developer.mozilla.org/en-US/docs/Web/CSS/padding">padding</a>
        /// value applied to the Menu container.
        /// The padding CSS shorthand property sets the padding area on all four sides of an element at once.
        /// Default value is 0px.
        /// </summary>
        [Parameter]
        public string MenuPadding { get; set; } = "0px";


        // ==================== Menu Item Styles ====================


        /// <summary>
        /// String value that specifies the CSS <a href="https://developer.mozilla.org/en-US/docs/Web/CSS/height">height</a>
        /// value applied to the Menu container.
        /// The height CSS property specifies the height of an element. 
        /// Default value is 30px.
        /// </summary>
        [Parameter]
        public string MenuItemHeight { get; set; } = "30px";
        
        /// <summary>
        /// String value that specifies the CSS <a href="https://developer.mozilla.org/en-US/docs/Web/CSS/line-height">line-height</a>
        /// value applied to the Menu container.
        /// The line-height CSS property sets the height of a line box.
        /// Default value is 30px.
        /// </summary>
        [Parameter]
        public string MenuItemLineHeight { get; set; } = "30px";
        
        /// <summary>
        /// String value that specifies the CSS <a href="https://developer.mozilla.org/en-US/docs/Web/CSS/padding">padding</a>
        /// value applied to each Menu Item.
        /// The padding CSS shorthand property sets the padding area on all four sides of an element at once.
        /// Default value is 0px 12px.
        /// </summary>
        [Parameter]
        public string MenuItemPadding { get; set; } = "0px 12px";


        // ==================== Menu Item Icon Styles ====================


        /// <summary>
        /// String value that specifies the CSS <a href="https://developer.mozilla.org/en-US/docs/Web/CSS/color">color</a>
        /// value applied to the icon element of each Menu Item.
        /// The color CSS property sets the foreground color value of an element's text
        /// and text decorations, and sets the currentcolor value.
        /// Default value is #007BFF.
        /// </summary>
        [Parameter]
        public string MenuIconFontColor { get; set; } = "#007BFF";

        /// <summary>
        /// String value that specifies the CSS <a href="https://developer.mozilla.org/en-US/docs/Web/CSS/font-size">font-size</a>
        /// value applied to the icon element of each Menu Item.
        /// The font-size CSS property sets the size of the font.
        /// Default value is 14px.
        /// </summary>
        [Parameter]
        public string MenuIconFontSize { get; set; } = "14px";

        /// <summary>
        /// String value that specifies the CSS <a href="https://developer.mozilla.org/en-US/docs/Web/CSS/height">height</a>
        /// value applied to the icon element of each Menu Item.
        /// The height CSS property specifies the height of an element. 
        /// Default value is auto.
        /// </summary>
        [Parameter]
        public string MenuIconHeight { get; set; } = "auto";

        /// <summary>
        /// String value that specifies the CSS <a href="https://developer.mozilla.org/en-US/docs/Web/CSS/line-height">line-height</a>
        /// value applied to the icon element of each Menu Item.
        /// The line-height CSS property sets the height of a line box.
        /// Default value is 30px.
        /// </summary>
        [Parameter]
        public string MenuIconLineHeight { get; set; } = "30px";

        /// <summary>
        /// String value that specifies the CSS <a href="https://developer.mozilla.org/en-US/docs/Web/CSS/width">width</a>
        /// value applied to the icon element of each Menu Item.
        /// The width CSS property sets an element's width.
        /// Default value is 1em.
        /// </summary>
        [Parameter]
        public string MenuIconWidth { get; set; } = "1em";

        /// <summary>
        /// String value that specifies the CSS <a href="https://developer.mozilla.org/en-US/docs/Web/CSS/text-align">text-align</a>
        /// value applied to the icon element of each Menu Item.
        /// The text-align CSS property sets the horizontal alignment of the content inside a
        /// block element or table-cell box.
        /// Default value is center.
        /// </summary>
        [Parameter]
        public string MenuIconTextAlign { get; set; } = "center";

        /// <summary>
        /// String value that specifies the CSS <a href="https://developer.mozilla.org/en-US/docs/Web/CSS/margin-right">margin-right</a>
        /// value applied to the icon element of each Menu Item.
        /// The margin-right CSS property sets the margin area on the right side of an element.
        /// Default value is 8px.
        /// </summary>
        [Parameter]
        public string MenuIconRightMargin { get; set; } = "8px";

        /// <summary>
        /// String value that specifies the CSS <a href="https://developer.mozilla.org/en-US/docs/Web/CSS/vertical-align">vertical-align</a>
        /// value applied to the icon element of each Menu Item.
        /// The vertical-align CSS property sets vertical alignment of an inline, inline-block or table-cell box.
        /// Default value is middle.
        /// </summary>
        [Parameter]
        public string MenuIconVerticalAlign { get; set; } = "middle";


        // ==================== Menu Item Caret Styles ====================


        /// <summary>
        /// String value that specifies the CSS <a href="https://developer.mozilla.org/en-US/docs/Web/CSS/color">color</a>
        /// value applied to the caret element of each Menu Item.
        /// The color CSS property sets the foreground color value of an element's text and text
        /// decorations, and sets the currentcolor value.
        /// Default value is #007BFF.
        /// </summary>
        [Parameter]
        public string MenuCaretFontColor { get; set; } = "#007BFF";

        /// <summary>
        /// String value that specifies the CSS <a href="https://developer.mozilla.org/en-US/docs/Web/CSS/font-size">font-size</a>
        /// value applied to the caret element of each Menu Item.
        /// The font-size CSS property sets the size of the font.
        /// Default value is 9px.
        /// </summary>
        [Parameter]
        public string MenuCaretFontSize { get; set; } = "9px";

        /// <summary>
        /// String value that specifies the CSS <a href="https://developer.mozilla.org/en-US/docs/Web/CSS/height">height</a>
        /// value applied to the caret element of each Menu Item.
        /// The height CSS property specifies the height of an element. 
        /// Default value is auto.
        /// </summary>
        [Parameter]
        public string MenuCaretHeight { get; set; } = "auto";

        /// <summary>
        /// String value that specifies the CSS <a href="https://developer.mozilla.org/en-US/docs/Web/CSS/line-height">line-height</a>
        /// value applied to the caret element of each Menu Item.
        /// The line-height CSS property sets the height of a line box.
        /// Default value is 30px.
        /// </summary>
        [Parameter]
        public string MenuCaretLineHeight { get; set; } = "30px";

        /// <summary>
        /// String value that specifies the CSS <a href="https://developer.mozilla.org/en-US/docs/Web/CSS/width">width</a>
        /// value applied to the caret element of each Menu Item.
        /// The width CSS property sets an element's width.
        /// Default value is auto.
        /// </summary>
        [Parameter]
        public string MenuCaretWidth { get; set; } = "auto";

        /// <summary>
        /// String value that specifies the CSS <a href="https://developer.mozilla.org/en-US/docs/Web/CSS/right">right</a>
        /// value applied to the caret element of each Menu Item.
        /// The right CSS property participates in specifying the horizontal position of a positioned element.
        /// Default value is 12px.
        /// </summary>
        [Parameter]
        public string MenuCaretRight { get; set; } = "12px";

        /// <summary>
        /// String value that specifies the CSS <a href="https://developer.mozilla.org/en-US/docs/Web/CSS/top">top</a>
        /// value applied to the caret element of each Menu Item.
        /// The top CSS property participates in specifying the vertical position of a
        /// positioned element.
        /// Default value is 0px.
        /// </summary>
        [Parameter]
        public string MenuCaretTop { get; set; } = "0px";


        // ==================== Menu Separator Styles ====================


        /// <summary>
        /// String value that specifies the CSS <a href="https://developer.mozilla.org/en-US/docs/Web/CSS/border">border</a>
        /// value used for the Menu Separator.
        /// The border shorthand CSS property sets an element's border. It sets the values of
        /// border-width, border-style, and border-color.
        /// Default value is 1px solid #E9ECEF.
        /// </summary>
        [Parameter]
        public string SeparatorBorder { get; set; } = "1px solid #E9ECEF";

        /// <summary>
        /// String value that specifies the CSS <a href="https://developer.mozilla.org/en-US/docs/Web/CSS/height">height</a>
        /// value used for the Menu Separator.
        /// The height CSS property specifies the height of an element. 
        /// Default value is auto.
        /// </summary>
        [Parameter]
        public string SeparatorHeight { get; set; } = "auto";

        /// <summary>
        /// String value that specifies the CSS <a href="https://developer.mozilla.org/en-US/docs/Web/CSS/margin">margin</a>
        /// value used for the Menu Separator.
        /// The text-align CSS property sets the horizontal alignment of the content inside a
        /// block element or table-cell box.
        /// Default value is 6px 0px.
        /// </summary>
        [Parameter]
        public string SeparatorMargin { get; set; } = "6px 0px";

        /// <summary>
        /// String value that specifies the CSS <a href="https://developer.mozilla.org/en-US/docs/Web/CSS/line-height">line-height</a>
        /// value used for the Menu Separator.
        /// The line-height CSS property sets the height of a line box.
        /// Default value is normal.
        /// </summary>
        [Parameter]
        public string SeparatorLineHeight { get; set; } = "normal";


        // ==================== Selected Menu Item Styles ====================


        /// <summary>
        /// String value that specifies the CSS <a href="https://developer.mozilla.org/en-US/docs/Web/CSS/color">color</a>
        /// value applied to the text element of the focused/selected Menu Item.
        /// The color CSS property sets the foreground color value of an element's text and
        /// text decorations, and sets the currentcolor value.
        /// Default value is #FFF.
        /// </summary>
        [Parameter]
        public string MenuItemSelectedFontColor { get; set; } = "#FFF";

        /// <summary>
        /// String value that specifies the CSS <a href="https://developer.mozilla.org/en-US/docs/Web/CSS/background-color">background-color</a>
        /// value applied to the text element of the focused/selected Menu Item.
        /// The background-color CSS property sets the background color of an element.
        /// Default value is "#007BFF.
        /// </summary>
        [Parameter]
        public string MenuItemSelectedBackgroundColor { get; set; } = "#007BFF";


        // ==================== Menu Popup Container Styles ====================


        /// <summary>
        /// String value that specifies the CSS <a href="https://developer.mozilla.org/en-US/docs/Web/CSS/border">border</a>
        /// value used for the border around the Menu popup container.
        /// The border shorthand CSS property sets an element's border. It sets the values of
        /// border-width, border-style, and border-color.
        /// Default value is 1px solid rgba(0,0,0,0.15).
        /// </summary>
        [Parameter]
        public string MenuPopupBorder { get; set; } = "1px solid rgba(0,0,0,0.15)";

        /// <summary>
        /// String value that specifies the CSS <a href="https://developer.mozilla.org/en-US/docs/Web/CSS/border-radius">border-radius</a>
        /// value used for the border around the Menu popup container.
        /// The border-radius CSS property rounds the corners of an element's outer border edge.
        /// Default value is 4px.
        /// </summary>
        [Parameter]
        public string MenuPopupBorderRadius { get; set; } = "4px";

        /// <summary>
        /// String value that specifies the CSS <a href="https://developer.mozilla.org/en-US/docs/Web/CSS/color">color</a>
        /// value applied to the text element of each Menu Item in the popup.
        /// The color CSS property sets the foreground color value of an element's text and
        /// text decorations, and sets the currentcolor value.
        /// Default value is #007BFF.
        /// </summary>
        [Parameter]
        public string MenuPopupFontColor { get; set; } = "#007BFF";

        /// <summary>
        /// String value that specifies the CSS <a href="https://developer.mozilla.org/en-US/docs/Web/CSS/font-size">font-size</a>
        /// value applied to the text element of each Menu Item in the popup.
        /// The font-size CSS property sets the size of the font.
        /// Default value is 14px.
        /// </summary>
        [Parameter]
        public string MenuPopupFontSize { get; set; } = "14px";

        /// <summary>
        /// String value that specifies the CSS <a href="https://developer.mozilla.org/en-US/docs/Web/CSS/font-weight">font-weight</a>
        /// value applied to the text element of each Menu Item in the popup.
        /// The font-weight CSS property sets the weight (or boldness) of the font.
        /// Default value is normal.
        /// </summary>
        [Parameter]
        public string MenuPopupFontWeight { get; set; } = "normal";

        /// <summary>
        /// String value that specifies the CSS <a href="https://developer.mozilla.org/en-US/docs/Web/CSS/text-align">text-align</a>
        /// value applied to the text element of each Menu Item in the popup.
        /// The text-align CSS property sets the horizontal alignment of the content inside a
        /// block element or table-cell box.
        /// Default value is left.
        /// </summary>
        [Parameter]
        public string MenuPopupTextAlign { get; set; } = "left";

        /// <summary>
        /// String value that specifies the CSS <a href="https://developer.mozilla.org/en-US/docs/Web/CSS/background-color">background-color</a>
        /// value applied to the Menu popup container.
        /// The background-color CSS property sets the background color of an element.
        /// Default value is #FFF.
        /// </summary>
        [Parameter]
        public string MenuPopupBackgroundColor { get; set; } = "#FFF";

        /// <summary>
        /// String value that specifies the CSS <a href="https://developer.mozilla.org/en-US/docs/Web/CSS/box-shadow">box-shadow</a>
        /// value applied to the Menu popup container.
        /// The box-shadow CSS property adds shadow effects around an element's frame.
        /// Default value is none.
        /// </summary>
        [Parameter]
        public string MenuPopupBoxShadow { get; set; } = "none";

        /// <summary>
        /// String value that specifies the CSS <a href="https://developer.mozilla.org/en-US/docs/Web/CSS/margin-left">margin-left</a>
        /// value applied to the Menu popup container.
        /// The margin-left CSS property sets the margin area on the left side of an element.
        /// This will shift the left side of the popup menu. Use a negative value to move the popup closer to the menu.
        /// Default value is 0px.
        /// </summary>
        [Parameter]
        public string MenuPopupLeftMargin { get; set; } = "0px";

        #endregion

        #endregion


        #region Instance Variables

        private SfMenu<TValue> menu;
        private string masterCssSelector;

        #endregion


        #region Constructors

        protected override void OnParametersSet()
        {
            masterCssSelector = (CssClass == string.Empty) ? ".e-menu-container" : $".{ CssClass }.e-menu-container";
        }

        #endregion



        #region Public Methods Providing Access to the Underlying Components to the Consumer

        /// <summary>
        /// Closes the Menu if it is opened in hamburger mode.
        /// </summary>
        public async Task CloseAsync() => await this.menu.CloseAsync();

        /// <summary>
        /// Opens the Menu in hamburger mode.
        /// </summary>
        public async Task OpenAsync() => await this.menu.OpenAsync();

        /// <summary>
        /// Get the index of the menu item in the Menu based on the argument.
        /// </summary>
        /// <param name="item">
        /// Item be passed to get the index.
        /// </param>
        /// <param name="isUniqueId">
        /// Specifies if the item should be looked up by its ItemId field, 
        /// as defined in the MenuFieldSettings, (<c>true</c>) or by the Text field (<c>false</c>).
        /// </param>
        /// <returns>
        /// A <see cref="List{T}"/> of <see cref="int"/> containing the indices for the
        /// passed item and all its children. 
        /// </returns>
        public List<int> GetItemIndex(TValue item, bool isUniqueId = false) => 
            menu.GetItemIndex(item, isUniqueId);

        #endregion
    }
}
