namespace Code420.UIOrchestrator.Server.Code.Models.Menus
{
    /// <summary>
    /// Model for the Favorites Menu that encapsulates the general favorites
    /// menu parameters as well as a list of the favorites menu items.
    /// <remarks>
    /// The class is constructed in the <see cref="MenuManager.BuildFavoritesMenu"/> method.
    /// </remarks>
    /// </summary>
    
    internal sealed class FavoritesMenuDefinition
    {
        /// <summary>
        /// Integer value containing the number of menu items.
        /// This is calculated when the Favorites Menu is build.
        /// </summary>
        public int MenuItemCount { get; set; }

        /// <summary>
        /// Integer value specifying the radius of the menu in pixels.
        /// Default value is 200.
        /// </summary>
        public int MenuRadius { get; init; } = 200;

        /// <summary>
        /// String value that defines the path to the image displayed in the
        /// center of the Favorites Menu in the normal state.
        /// </summary>
        public string PrimaryCenterImage { get; init; } = "./images/code420-white.jpg";

        /// <summary>
        /// String value that defines the path to the image displayed in the
        /// center of the Favorites Menu when the center image is hovered.
        /// </summary>
        public string SecondaryCenterImage { get; init; } = "./images/code420-black.jpg";

        /// <summary>
        /// String value containing the CSS <a href="https://developer.mozilla.org/en-US/docs/Web/CSS/transform-function/scale">scale()</a>
        /// applied to each menu item in the animation.
        /// The scale() CSS function defines a transformation that resizes an element on the 2D plane.
        /// Default value is 1.05.
        /// <remarks>
        /// A value of 1.05 means each menu items will animate its size from
        /// 100% to 105% and back to 100% to produce a throbbing effect.
        /// </remarks>
        /// </summary>
        public string ThrobScale { get; init; } = "1.05";

        /// <summary>
        /// String value containing the minimum height of the Favorites Menu in pixels.
        /// Default value is 700px.
        /// </summary>
        public string MenuMinimumHeight { get; init; } = "700px";

        /// <summary>
        /// String value containing the height of the Favorites Menu center image in pixels.
        /// Default value is 200px.
        /// </summary>
        public string CenterImageSize { get; init; } = "200px";

        /// <summary>
        /// String value containing the CSS <a href="https://developer.mozilla.org/en-US/docs/Web/CSS/animation-duration">animation-duration</a>
        /// for the animation of the menu items around the center image.
        /// The animation-duration CSS property sets the length of time that an animation takes to complete one cycle.
        /// Default value is 30s.
        /// <remarks>
        /// A value of 30s means that the menu items will complete one revolution around the center
        /// image every 30 seconds.
        /// </remarks>
        /// </summary>
        public string MenuItemRotationSpeed { get; init; } = "30s";

        /// <summary>
        /// String value containing the CSS <a href="https://developer.mozilla.org/en-US/docs/Web/CSS/transition-duration">transition-duration</a>
        /// for center image to translate from the primary center image to the secondary image when hovered.
        /// The transition-duration CSS property sets the length of time a transition animation should take to complete.
        /// Default value is 500ms.
        /// <remarks>
        /// A value of 500ms means that the center image will transition from the
        /// <see cref="PrimaryCenterImage"/> primary image to the <see cref="SecondaryCenterImage"/>
        /// secondary image in half a second when the center image is hovered.
        /// </remarks>
        /// </summary>
        public string CenterImageTransitionSpeed { get; init; } = "500ms";

        /// <summary>
        /// String value containing the size (diameter) of a menu item in pixels.
        /// Default value is 30px.
        /// </summary>
        public string MenuItemSize { get; init; } = "30px";

        /// <summary>
        /// String value containing the CSS <a href="https://developer.mozilla.org/en-US/docs/Web/CSS/transition-duration">transition-duration</a>
        /// for a menu item to transition from the icon display to the text display when hovered.
        /// The transition-duration CSS property sets the length of time a transition animation should take to complete.
        /// Default value is 500ms.
        /// <remarks>
        /// A value of 500ms means that a menu item will transition from displaying its icon
        /// to displaying its text in half a second when the menu item is hovered.
        /// </remarks>
        /// </summary>
        public string MenuItemTransitionSpeed { get; init; } = "500ms";

        /// <summary>
        /// A <see cref="List{T}"/> of <see cref="FavoritesMenuItem"/> objects that define
        /// each menu item.
        /// </summary>
        public List<FavoritesMenuItem> FavoritesMenuItems { get; init; }
    }
}
