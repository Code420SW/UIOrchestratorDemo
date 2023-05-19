using Microsoft.AspNetCore.Components;

namespace Code420.UIOrchestrator.Server.Components.BaseComponents.LabelBase
{
    /// <summary>
    /// Responsible for rendering a simple HTML label tag.
    /// Provides parameters to control CSS styling elements.
    /// Provides a method for the consumer to update the label text.
    /// </summary>

    public partial class LabelBase : ComponentBase
    {
         #region Component Parameters

         #region Base Parameters
         
        /// <summary>
        /// String value containing the label text.
        /// Default value is string.Empty.
        /// </summary>
        [Parameter]
        public string LabelText { get; set; } = string.Empty;

        #endregion

         #region CSS Parameters
        
        /// <summary>
        /// String value containing CSS class definition that will be injected in the root
        /// HTML div element of the Label.
        /// Default value is string.Empty.
        /// </summary>
        [Parameter]
        public string CssClass { get; set; } = string.Empty;

        /// <summary>
        /// String value that specifies the CSS <a href="https://developer.mozilla.org/en-US/docs/Web/CSS/color">color</a>
        /// value for the label font color.
        /// The color CSS property sets the foreground color value of an element's text and text decorations,
        /// and sets the currentcolor value.
        /// Default value is black.
        /// </summary>
        [Parameter]
        public string FontColor { get; set; } = "black";

        /// <summary>
        /// String value that specifies the CSS <a href="https://developer.mozilla.org/en-US/docs/Web/CSS/font-size">font-size</a>
        /// value for the label.
        /// The font-size CSS property sets the size of the font.
        /// Default value is 1em.
        /// </summary>
        [Parameter]
        public string FontSize { get; set; } = "1em";

        /// <summary>
        /// String value that specifies the CSS <a href="https://developer.mozilla.org/en-US/docs/Web/CSS/font-weight">font-weight</a>
        /// value for the label.
        /// The font-weight CSS property sets the weight (or boldness) of the font. The weights available
        /// depend on the font-family that is currently set.
        /// Default value is normal.
        /// </summary>
        [Parameter]
        public string FontWeight { get; set; } = "normal";

        /// <summary>
        /// String value that specifies the CSS <a href="https://developer.mozilla.org/en-US/docs/Web/CSS/font-style">font-style</a>
        /// value for the label.
        /// The font-style CSS property sets whether a font should be styled with a normal, italic, or
        /// oblique face from its font-family.
        /// Default value is normal.
        /// </summary>
        [Parameter]
        public string FontStyle { get; set; } = "normal";

        /// <summary>
        /// String value that specifies the CSS <a href="https://developer.mozilla.org/en-US/docs/Web/CSS/text-align">text-align</a>
        /// value for the label.
        /// The text-align CSS property sets the horizontal alignment of the content inside a block
        /// element or table-cell box.
        /// Default value is left.
        /// </summary>
        [Parameter]
        public string TextAlign { get; set; } = "left";

        /// <summary>
        /// String value that specifies the CSS <a href="https://developer.mozilla.org/en-US/docs/Web/CSS/text-decoration">text-decoration</a>
        /// value for the label.
        /// The text-decoration shorthand CSS property sets the appearance of decorative lines on text.
        /// It is a shorthand for text-decoration-line, text-decoration-color, text-decoration-style,
        /// and the newer text-decoration-thickness property.
        /// Default value is none.
        /// </summary>
        [Parameter]
        public string TextDecoration { get; set; } = "none";

        /// <summary>
        /// String value that specifies the CSS <a href="https://developer.mozilla.org/en-US/docs/Web/CSS/text-transform">text-transform</a>
        /// value for the label.
        /// The text-transform CSS property specifies how to capitalize an element's text. It can be used
        /// to make text appear in all-uppercase or all-lowercase, or with each word capitalized.
        /// Default value is none.
        /// </summary>
        [Parameter]
        public string TextTransform { get; set; } = "none";

        /// <summary>
        /// String value that specifies the CSS <a href="https://developer.mozilla.org/en-US/docs/Web/CSS/background-color">background-color</a>
        /// value for the label.
        /// The background-color CSS property sets the background color of an element.
        /// Default value is transparent.
        /// </summary>
        [Parameter]
        public string LabelBackgroundColor { get; set; } = "transparent";

        /// <summary>
        /// String value that specifies the CSS <a href="https://developer.mozilla.org/en-US/docs/Web/CSS/border">border</a>
        /// value for the label.
        /// The border shorthand CSS property sets an element's border. It sets the values of
        /// border-width, border-style, and border-color.
        /// Default value is none.
        /// </summary>
        [Parameter]
        public string LabelBorder { get; set; } = "none";

        /// <summary>
        /// String value that specifies the CSS <a href="https://developer.mozilla.org/en-US/docs/Web/CSS/border-radius">border-radius</a>
        /// value for the label.
        /// The border-radius CSS property rounds the corners of an element's outer border edge.
        /// Default value is none.
        /// </summary>
        [Parameter]
        public string LabelBorderRadius { get; set; } = "0";

        /// <summary>
        /// String containing the CSS <a href="https://developer.mozilla.org/en-US/docs/Web/CSS/margin">margin</a>
        /// value used for the label.
        /// The margin CSS shorthand property sets the margin area on all four sides of an element.
        /// Specify the margin in top, right, bottom, left order. Or use any of the margin shorthands.
        /// Default value is: 0px on all sides.
        /// </summary>
        [Parameter]
        public string LabelMargin { get; set; } = "0px";

        /// <summary>
        /// String containing the CSS <a href="https://developer.mozilla.org/en-US/docs/Web/CSS/padding">padding</a>
        /// value for the label.
        /// The padding CSS shorthand property sets the padding area on all four sides of an element at once.
        /// Specify the padding in top, right, bottom, left order. Or use any of the padding shorthands.
        /// Default value is: 0px on all sides.
        /// </summary>
        [Parameter]
        public string LabelPadding { get; set; } = "0px";

        #endregion
        
        #endregion


        #region Instance Variables

        private string masterCssSelector = string.Empty;
        private string elementClass = string.Empty;

        #endregion


        #region Constructors

        protected override void OnParametersSet()
        {
            elementClass = (CssClass == string.Empty) ? "label__" : CssClass;
            masterCssSelector = $".{ elementClass }";
        }

        #endregion


        #region Public Methods Providing Access to the Underlying Components to the Consumer

        public async Task SetLabelText(string value)
        {
            LabelText = value;
            await InvokeAsync(StateHasChanged);
        }
        
        #endregion
    }
}
