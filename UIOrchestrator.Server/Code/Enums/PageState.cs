using System.ComponentModel;

namespace Code420.UIOrchestrator.Server.Code.Enums
{
    public enum PageState
    {
        [Description("Loading")]
        Loading,

        [Description("Operating")]
        Operating,

        [Description("Error")]
        Error
    }
}
