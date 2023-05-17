using System.ComponentModel;
using Code420.UIOrchestrator.Server.Code.Enums;

namespace Code420.UIOrchestrator.Server.Code.Extensions
{
    /// <summary>
    /// Set of extension methods for various enum types that extract the
    /// CSS class from an enum's description.
    /// </summary>
    internal static class EnumExtensions
    {
        /// <summary>
        /// Extracts the CSS class from the definition attribute for the
        /// passed <see cref="ButtonStyle"/> enum member.
        /// </summary>
        /// <param name="val">
        /// The <see cref="ButtonStyle"/> enum member whose CSS class
        /// is to be retrieved.
        /// </param>
        /// <returns>
        /// String value containing the enum member's CSS class.
        /// </returns>
        public static string ToCssString(this ButtonStyle val)
        {
            var temp1 = val.GetType().GetField(val.ToString());
            var attributes = (DescriptionAttribute[])temp1?.GetCustomAttributes(typeof(DescriptionAttribute), false);
            return (attributes?.Length > 0) ? attributes[0].Description : string.Empty;
        }

        /// <summary>
        /// Extracts the CSS class from the definition attribute for the
        /// passed <see cref="IconButtonStyle"/> enum member.
        /// </summary>
        /// <param name="val">
        /// The <see cref="IconButtonStyle"/> enum member whose CSS class
        /// is to be retrieved.
        /// </param>
        /// <returns>
        /// String value containing the enum member's CSS class.
        /// </returns>
        public static string ToCssString(this IconButtonStyle val)
        {
            var temp1 = val.GetType().GetField(val.ToString());
            var attributes = (DescriptionAttribute[])temp1?.GetCustomAttributes(typeof(DescriptionAttribute), false);
            return (attributes?.Length > 0) ? attributes[0].Description : string.Empty;
        }
    }
}
