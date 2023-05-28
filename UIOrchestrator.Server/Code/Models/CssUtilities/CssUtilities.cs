using System.Drawing;
using System.Text.RegularExpressions;
using Microsoft.JSInterop;

namespace Code420.UIOrchestrator.Server.Code.Models.CssUtilities
{
    /// <summary>
    /// Responsible for providing a set of utility functions to convert
    /// different ways of specifying a CSS color value into an RGBA value.
    /// </summary>
    public class CssUtilities : ICssUtilities
    {
        private readonly IJSRuntime jsRuntime;

        public CssUtilities(IJSRuntime jSRuntime)
        {
            jsRuntime = jSRuntime;
        }

        
        /// <summary>
        /// Converts the passed CSS color value from hex format to RGB and assigns the passed
        /// opacity value to create an RGBA value.
        /// </summary>
        /// <param name="backgroundColor">
        /// A CSS color value in either hex format of as a CSS
        /// var() that can be reduced to a hex color value.
        /// </param>
        /// <param name="backgroundOpacity">
        /// A CSS opacity value that will be used to construct
        /// an RGBA color value.
        /// </param>
        /// <returns>
        /// String value containing an RGBA color value.
        /// If any errors occur preventing creation of the RGBA color value, black is returned.
        /// </returns>
        public async Task<string> ConvertToRgba(string backgroundColor, decimal backgroundOpacity)
        {
            const string prefix = "var(";
            const string errorValue = "black;";
            string convertedColorValue;


            //  Consider the case where a CSS var() was passed in.
            if (backgroundColor.StartsWith(prefix))
            {
                //  Extract the variable from the var()
                int pos = backgroundColor.IndexOf(")", StringComparison.Ordinal);
                if (pos is -1) return errorValue;
                
                var passedColorValue = backgroundColor.Substring(prefix.Length, (pos - prefix.Length));

                //  Call our JS helper routine to convert the variable to its root value
                //  If an error occurred (like bad variable name), the default error value is returned
                convertedColorValue = await jsRuntime.InvokeAsync<string>("getCssVariable", passedColorValue);
                if (string.IsNullOrEmpty(convertedColorValue)) return errorValue;
            }
            else
            {
                convertedColorValue = backgroundColor;
            }

            //  Convert the color value into RGBA.
            //  Return the default error value if an exception is thrown
            try
            {
                return GenerateRgba(convertedColorValue, backgroundOpacity);
            }
            catch (Exception)
            {
                return errorValue;
            }
        }

        
        /// <summary>
        /// Converts a CSS color value in hex format to RGBA format.
        /// </summary>
        /// <param name="backgroundColor">
        /// CSS color value in hex format.
        /// </param>
        /// <param name="backgroundOpacity">
        /// CSS opacity value in decimal format.
        /// The value is bounded between 0.0 and 1.0.
        /// Any value outside the bounds will be adjusted to the
        /// nearest boundary value.
        /// </param>
        /// <returns>
        /// String value containing the RGBA color value.
        /// </returns>
        private string GenerateRgba(string backgroundColor, decimal backgroundOpacity)
        {
            decimal opacity = backgroundOpacity;
            if (backgroundOpacity < 0.0m) opacity = 0.0m;
            else if (backgroundOpacity > 1.0m) opacity = 1.0m;
            
            string cleanString = RemoveWhitespace(backgroundColor);

            //  Handle hex formatted color code
            if (cleanString.StartsWith("#"))
            {
                //  Sanity-check that cleanString is longer than one character
                //  If this fails, execution will fall to shade of black return value
                if(cleanString.Length is not 1)
                {
                    Color color = ColorTranslator.FromHtml(cleanString);
                    int r = Convert.ToInt16(color.R);
                    int g = Convert.ToInt16(color.G);
                    int b = Convert.ToInt16(color.B);
                    return $"rgba({ r }, { g }, { b }, { opacity });";
                }
            }
            
            //  Handle RGB formatted color code
            if (cleanString.StartsWith("rgb("))
            {
                //  Use RegEx to parse the rgb() values
                Regex regex = new Regex(@"rgb\((?<r>\d{1,3}),(?<g>\d{1,3}),(?<b>\d{1,3})\)");
                Match match = regex.Match(cleanString);
                if (match.Success)
                {
                    int r = int.Parse(match.Groups["r"].Value);
                    int g = int.Parse(match.Groups["g"].Value);
                    int b = int.Parse(match.Groups["b"].Value);

                    return $"rgba({ r }, { g }, { b }, { opacity });";
                }
            }
            
            //  Since the color code is neither hex- or rgb-formatted, return a shade of black
            return $"rgba(0, 0, 0, { opacity });";
        }


        /// <summary>
        /// Remove all whitespace characters from a string.
        /// </summary>
        /// <param name="input">
        /// String value containing whitespaces.
        /// </param>
        /// <returns>
        /// Passed parameter with whitespaces removed.
        /// </returns>
        private string RemoveWhitespace(string input)
        {
            if (input is null) return "";
            return new string(input.ToCharArray()
                .Where(c => !Char.IsWhiteSpace(c))
                .ToArray());
        }
    }
}
