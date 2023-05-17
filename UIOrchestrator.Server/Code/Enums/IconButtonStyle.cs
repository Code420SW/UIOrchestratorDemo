using System.ComponentModel;
using Code420.UIOrchestrator.Server.Components.BaseComponents.IconButtonBase;
using Syncfusion.Blazor.Buttons;
using Code420.UIOrchestrator.Server.Code.Extensions;

namespace Code420.UIOrchestrator.Server.Code.Enums
{
    /// <summary>
    /// Defines a set of standard button types used with the
    /// <see cref="IconButtonBase"/> component to set the
    /// <see cref="IconButtonBase.IconButtonStyle"/> parameter.
    /// <remarks>
    /// <para>
    /// The description for each enum element contains the CSS class
    /// used by the underlying <see cref="SfButton"/> component for
    /// styling.
    /// </para>
    /// <para>
    /// The <see cref="EnumExtensions.ToCssString(IconButtonStyle)"/> extension method
    /// extracts the CSS class.
    /// </para>
    /// <para>
    /// The <see cref="IconButtonBase"/> component captures this CSS class,
    /// based on the <see cref="IconButtonBase.IconButtonStyle"/> parameter
    /// and overrides the default styling.
    /// </para>
    /// </remarks>
    /// </summary>
    public enum IconButtonStyle
    {
        [Description("e-round")]
        Round = 0,
        
        [Description("e-icon-only")]
        Default = 1
    }
}
