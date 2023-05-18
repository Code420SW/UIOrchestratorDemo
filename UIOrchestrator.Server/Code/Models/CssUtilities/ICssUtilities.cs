namespace Code420.UIOrchestrator.Server.Code.Models.CssUtilities
{
    public interface ICssUtilities
    {
        Task<string> ConvertToRgba(string backgroundColor, decimal backgroundOpacity);
    }
}