using System.ComponentModel;
using Code420.UIOrchestrator.Server.Code.Enums;

namespace Code420.UIOrchestrator.Server.Code.Extensions
{
    internal static class EnumExtensions
    {
        public static string ToCssString(this ButtonStyle val)
        {
            var temp1 = val.GetType().GetField(val.ToString());
            var attributes = (DescriptionAttribute[])temp1?.GetCustomAttributes(typeof(DescriptionAttribute), false);
            return (attributes?.Length > 0) ? attributes[0].Description : string.Empty;
        }

        public static string ToCssString(this MySpinnerType val)
        {
            var temp1 = val.GetType().GetField(val.ToString());
            var attributes = (DescriptionAttribute[])temp1?.GetCustomAttributes(typeof(DescriptionAttribute), false);
            return (attributes?.Length > 0) ? attributes[0].Description : string.Empty;
        }

        public static string ToCssString(this IconButtonStyle val)
        {
            var temp1 = val.GetType().GetField(val.ToString());
            var attributes = (DescriptionAttribute[])temp1?.GetCustomAttributes(typeof(DescriptionAttribute), false);
            return (attributes?.Length > 0) ? attributes[0].Description : string.Empty;
        }
    }
}
