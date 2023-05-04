using System.Text;
using FluentValidation.Results;

namespace Code420.UIOrchestrator.Server.Validators
{
    /// <summary>
    /// Provides a set of extensions to the <see cref="ValidationResult"/> object.
    /// </summary>
    public static class ValidatorExtensions
    {
        /// <summary>
        /// Convert all error messages in a <see cref="ValidationResult"/> objects to
        /// basic HTML.
        /// Typically used with the GenericErrorMessageDisplay custom toast.
        /// </summary>
        /// <param name="validationResult">
        /// The <see cref="ValidationResult"/> object containing the errors.
        /// </param>
        /// <param name="header">
        /// Optional string value used as the error message header.
        /// </param>
        /// <returns>
        /// String value containing all error messaged in HTML format.
        /// The header is bold.
        /// All errors are presented as an unordered list.
        /// </returns>
        public static string BuildValidationErrorMessage(this ValidationResult validationResult, string header = null)
        {
            const string defaultHeader = "The following input errors must be corrected:";
            StringBuilder result = new($"<b>{ header ?? defaultHeader }</b><ul>");

            foreach (var error in validationResult.Errors)
            {
                result.Append("<li>");
                result.Append(error.ErrorMessage);
                result.Append("</li>");
            }
            result.Append("</ul>");

            return result.ToString();
        }
    }
}
