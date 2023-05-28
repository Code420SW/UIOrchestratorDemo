using System.ComponentModel;
using Code420.UIOrchestrator.Server.Components.BaseComponents.ButtonBase;
using Syncfusion.Blazor.Buttons;
using Code420.UIOrchestrator.Server.Code.Extensions;

namespace Code420.UIOrchestrator.Server.Code.Enums
{
    /// <summary>
    /// Defines a set of standard button types used with the
    /// <see cref="ButtonBase"/> component to set the
    /// <see cref="ButtonBase.ButtonStyle"/> parameter.
    /// <remarks>
    /// <para>
    /// The description for each enum element contains the CSS class
    /// used by the underlying <see cref="SfButton"/> component for
    /// styling.
    /// </para>
    /// <para>
    /// The <see cref="EnumExtensions.ToCssString(ButtonStyle)"/> extension method
    /// extracts the CSS class.
    /// </para>
    /// <para>
    /// The <see cref="ButtonBase"/> component captures this CSS class,
    /// based on the <see cref="ButtonBase.ButtonStyle"/> parameter
    /// and overrides the default styling.
    /// </para>
    /// </remarks>
    /// </summary>
    public enum ButtonStyle
    {
        [Description("e-primary")]
        Primary = 0,
        
        [Description("e-success")]
        Success = 1,
        
        [Description("e-info")]
        Information = 2,
        
        [Description("e-warning")]
        Warning = 3,
        
        [Description("e-danger")]
        Danger = 4,
        
        [Description("e-link")]
        Link = 5,
        
        [Description("e-secondary")]
        Secondary = 6,
        
        [Description("e-outline")]
        Outline = 7,
        
        [Description("e-flat")]
        Flat = 8,
        
        [Description("e-block")]
        Block = 9
    }
}
