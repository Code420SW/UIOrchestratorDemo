namespace Code420.UIOrchestrator.Server.Code.Models.Menus
{
    /// <summary>
    /// Model for menu items displayed in the Favorites Menu.
    /// The model is consumed by the <see cref="FavoritesMenuDefinition"/> model
    /// which maintains a <see cref="List{T}"/> of this model.
    /// <remarks>
    /// <para>
    /// The <see cref="ItemId"/> is the key property which is analogous to the
    /// <see cref="OrchestratorMenuItem.ItemId"/> and <see cref="MenuItemDefinitionDto.MenuItemId"/>
    /// properties and serves the same purpose: it identifies the menu item which is passed
    /// to to the callback handler defined by the <see cref="MenuItemCallback"/> property
    /// which typically points to the <see cref="UIOrchestrator.MenuItemCallbackHandlerAsync"/>
    /// method. The ItemId is passed to the callback handler which must be able to handle it.
    /// </para>
    /// <para>
    /// The <see cref="ItemId"/> must also be present in <see cref="OrchestratorMenuItem"/>.
    /// </para>
    /// <para>
    /// The rest of the properties are used to configure the visual presentation of the
    /// menu item.
    /// </para>
    /// </remarks>
    /// </summary>
    internal sealed class FavoritesMenuItem
    {
        /// <summary>
        /// String value containing the unique identifier for this menu item.
        /// Default value is string.Empty.
        /// </summary>
        public string ItemId { get; init; } = string.Empty;

        /// <summary>
        /// An <see cref="Action{T}"/> accepting an <see cref="object"/> parameter.
        /// The delegate will be invoked when the menu item is selected.
        /// Default value is null.
        /// </summary>
        public Action<object> MenuItemCallback { get; init; }

        /// <summary>
        /// String value containing the X-offset of the menu item relative to the
        /// center image. The property is calculated when the Favorites Menu is
        /// constructed.
        /// Default value is 0px.
        /// </summary>
        public string OffsetX { get; set; } = "0px";

        /// <summary>
        /// String value containing the Y-offset of the menu item relative to the
        /// center image. The property is calculated when the Favorites Menu is
        /// constructed.
        /// Default value is 0px.
        /// </summary>
        public string OffsetY { get; set; } = "0px";

        /// <summary>
        /// String value containing CSS icon definition.
        /// Default value is string.Empty.
        /// </summary>
        public string IconCss { get; init; } = string.Empty;

        /// <summary>
        /// String containing the CSS <a href="https://developer.mozilla.org/en-US/docs/Web/CSS/font-size">font-size</a>
        /// value for the menu item icon.
        /// The font-size CSS property sets the size of the font.
        /// Default value is 24px.
        /// </summary>
        public string IconFontSize { get; init; } = "24px";

        /// <summary>
        /// String value that specifies the CSS <a href="https://developer.mozilla.org/en-US/docs/Web/CSS/color">color</a>
        /// value applied to the menu item icon.
        /// The color CSS property sets the foreground color value of an element's text and text decorations,
        /// and sets the currentcolor value.
        /// Default value is white.
        /// </summary>
        public string IconColor { get; init; } = "white";

        /// <summary>
        /// String value containing the text displayed when the menu item is hovered.
        /// Default value is string.Empty.
        /// </summary>
        public string HoverText { get; init; } = string.Empty;

        /// <summary>
        /// String containing the CSS <a href="https://developer.mozilla.org/en-US/docs/Web/CSS/font-size">font-size</a>
        /// value for the menu item text.
        /// The font-size CSS property sets the size of the font.
        /// Default value is 12px.
        /// </summary>
        public string HoverTextFontSize { get; init; } = "12px";

        /// <summary>
        /// String value that specifies the CSS <a href="https://developer.mozilla.org/en-US/docs/Web/CSS/color">color</a>
        /// value applied to the menu item text.
        /// The color CSS property sets the foreground color value of an element's text and text decorations,
        /// and sets the currentcolor value.
        /// Default value is white.
        /// </summary>
        public string HoverTextColor { get; init; } = "white";

        /// <summary>
        /// String containing the CSS <a href="https://developer.mozilla.org/en-US/docs/Web/CSS/background-color">background-color</a>
        /// value for the menu item icon.
        /// The background-color CSS property sets the background color of an element.
        /// Default value is black.
        /// </summary>
        public string MenuItemIconBackgroundColor { get; init; } = "black";

        /// <summary>
        /// String containing the CSS <a href="https://developer.mozilla.org/en-US/docs/Web/CSS/background-color">background-color</a>
        /// value for the menu item text.
        /// The background-color CSS property sets the background color of an element.
        /// Default value is black.
        /// </summary>
        public string MenuItemTextBackgroundColor { get; init; } = "black";
    }
}
