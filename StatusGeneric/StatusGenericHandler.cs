// Copyright (c) 2019 Jon P Smith, GitHub: JonPSmith, web: http://www.thereformedprogrammer.net/
// Licensed under MIT license. See License.txt in the project root for license information.

using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Text;

namespace Code420.StatusGeneric
{
    /// <summary>
    /// This contains the error handling part of the GenericBizRunner
    /// </summary>
    public class StatusGenericHandler : IStatusGenericHandler
    {
        /// <summary>
        /// This is the default success message.
        /// </summary>
        public const string DefaultSuccessMessage = "Success";
        protected readonly List<ErrorGeneric> _errors = new List<ErrorGeneric>();
        private string _successMessage = DefaultSuccessMessage;

        //public StatusGenericHandler() { }


        /// <summary>
        /// This creates a StatusGenericHandler, with optional header (see Header property, and CombineStatuses)
        /// </summary>
        /// <param name="header"></param>
        public StatusGenericHandler(string header = "")
        {
            Header = header;
        }

        //public StatusGenericHandler(IList<string> errors = null)
        //{
        //    if (errors is not null)
        //    {
        //        foreach (var error in errors) AddError(error);
        //    }
        //}

        /// <summary>
        /// The header provides a prefix to any errors you add. Useful if you want to have a general prefix to all your errors
        /// e.g. a header if "MyClass" would produce error messages such as "MyClass: This is my error message."
        /// </summary>
        public string Header { get; set; }

        /// <summary>
        /// This holds the list of ValidationResult errors. If the collection is empty, then there were no errors
        /// </summary>
        public IReadOnlyList<ErrorGeneric> Errors => _errors.AsReadOnly();

        /// <summary>
        /// This is true if there are no errors 
        /// </summary>
        public bool IsValid => (_errors.Any() is false);

        /// <summary>
        /// This is true if any errors have been added 
        /// </summary>
        public bool HasErrors => _errors.Any();

        /// <summary>
        /// This is true if the errors are validation errors. 
        /// </summary>
        public bool HasValidationErrors { get; set; } = false;

        /// <summary>
        /// Contains the HTTP status code that can be used by the API. 
        /// </summary>
        public HttpStatusCode HttpStatusCode { get; private set; } = HttpStatusCode.OK;

        /// <summary>
        /// On success this returns the message as set by the business logic, or the default messages set through the constructor.
        /// If there are errors it contains the message "Failed with NN errors"
        /// </summary>
        public string Message
        {
            get => IsValid
                ? _successMessage
                : $"Failed with {_errors.Count} error" + (_errors.Count == 1 ? "" : "s");
            set => _successMessage = value;
        }

        /// <summary>
        /// This sets the HttpStatusCode property
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        public void SetHttpStatusCode(HttpStatusCode httpStatusCode) => HttpStatusCode = httpStatusCode;

        /// <summary>
        /// This returns the HttpStatusCode property
        /// </summary>
        /// <returns>Integer value representing the HttpStatusCode.</returns>
        public int GetHttpStatusCodeAsValue() => (int)HttpStatusCode;

        /// <summary>
        /// This allows statuses to be combined. Copies over any errors and replaces the Message if the current message is null
        /// If you are using Headers then it will combine the headers in any errors in combines
        /// e.g. Status1 with header "MyClass" combines Status2 which has header "MyProp" and status2 has errors.
        /// The result would be error message in status2 would be updates to start with "MyClass>MyProp: This is my error message."
        /// </summary>
        /// <param name="status"></param>
        public IStatusGeneric CombineStatuses(IStatusGeneric status)
        {
            if (status.IsValid is false)
            {
                _errors.AddRange(string.IsNullOrEmpty(Header)
                    ? status.Errors
                    : status.Errors.Select(x => new ErrorGeneric(Header, x)));

                HttpStatusCode = status.HttpStatusCode;
            }

            if (IsValid && status.Message != DefaultSuccessMessage)
                Message = status.Message;

            return this;
        }

        /// <summary>
        /// This is a simple method to output all the errors as a single string - null if no errors
        /// Useful for feeding back all the errors in a single exception (also nice in unit testing)
        /// </summary>
        /// <param name="separator">if null then each errors is separated by Environment.NewLine, otherwise uses the separator you provide</param>
        /// <returns>a single string with all errors separated by the 'separator' string</returns>
        public string GetAllErrors(string separator = null)
        {
            separator = separator ?? Environment.NewLine;
            return _errors.Any()
                ? string.Join(separator, Errors)
                : null;
        }

        /// <summary>
        /// This adds one error to the Errors collection
        /// NOTE: This is virtual so that the StatusGenericHandler.Generic can override it. That allows both to return a IStatusGeneric result
        /// </summary>
        /// <param name="errorMessage">The text of the error message</param>
        /// <param name="propertyNames">optional. A list of property names that this error applies to</param>
        public virtual IStatusGeneric AddError(string errorMessage, params string[] propertyNames)
        {
            if (errorMessage is null) throw new ArgumentNullException(nameof(errorMessage));

            _errors.Add(new ErrorGeneric(Header, new ValidationResult(errorMessage, propertyNames)));

            return this;
        }

        /// <summary>
        /// This adds one error to the Errors collection and saves the exception's data to the DebugData property
        /// </summary>
        /// <param name="ex">The exception that you want to turn into a IStatusGeneric error.</param>
        /// <param name="errorMessage">The user-friendly text for the error message</param>
        /// <param name="propertyNames">optional. A list of property names that this error applies to</param>
        public IStatusGeneric AddError(Exception ex, string errorMessage, params string[] propertyNames)
        {
            if (errorMessage == null) throw new ArgumentNullException(nameof(errorMessage));

            ErrorGeneric errorGeneric = new ErrorGeneric(Header, new ValidationResult(errorMessage, propertyNames));
            errorGeneric.CopyExceptionToDebugData(ex);

            _errors.Add(errorGeneric);

            return this;
        }

        /// <summary>
        /// This adds one ValidationResult to the Errors collection
        /// </summary>
        /// <param name="validationResult"></param>
        public void AddValidationResult(ValidationResult validationResult)
        {
            _errors.Add(new ErrorGeneric(Header, validationResult));
        }

        /// <summary>
        /// This appends a collection of ValidationResults to the Errors collection
        /// </summary>
        /// <param name="validationResults"></param>
        public void AddValidationResults(IEnumerable<ValidationResult> validationResults)
        {
            _errors.AddRange(validationResults.Select(x => new ErrorGeneric(Header, x)));
        }


        public string BuildStatusErrorMessage()
        {
            StringBuilder result = new("<b>The following errors occurred:</b><ul>");

            foreach (var error in _errors)
            {
                result.Append("<li>");
                result.Append(error.ToString());
                result.Append("</li>");
            }
            result.Append("</ul><br>");
            result.Append("Please correct all errors and try the operation again.<br>");
            result.Append("Contact your system administrator if the problem persists.");

            return result.ToString();
        }


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
        public string BuildValidationErrorMessage(string header = null)
        {
            const string defaultHeader = "The following input errors must be corrected:";
            StringBuilder result = new($"<b>{header ?? defaultHeader}</b><ul>");

            foreach (var error in _errors)
            {
                result.Append("<li>");
                result.Append(error.ToString());
                result.Append("</li>");
            }
            result.Append("</ul>");

            return result.ToString();
        }
    }
}
