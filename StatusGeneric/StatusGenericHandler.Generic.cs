// Copyright (c) 2019 Jon P Smith, GitHub: JonPSmith, web: http://www.thereformedprogrammer.net/
// Licensed under MIT license. See License.txt in the project root for license information.

using System;
using System.ComponentModel.DataAnnotations;

namespace Code420.StatusGeneric
{
    /// <summary>
    /// This contains the error handling part of the GenericBizRunner
    /// </summary>
    public class StatusGenericHandler<T> : StatusGenericHandler, IStatusGeneric<T>
    {
        private T _result;


        //  Contructors
        //public StatusGenericHandler() : base()
        //{ }

        //public StatusGenericHandler(string header = "") : base(header)
        //{ }

        //public StatusGenericHandler(T model, IList<string> errors = null) : base(errors)
        //{
        //    _result = model;
        //}

        /// <summary>
        /// This is the returned result
        /// </summary>
        public T Result => IsValid ? _result : default(T);

        /// <summary>
        /// This sets the result to be returned
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        public StatusGenericHandler<T> SetResult(T result)
        {
            _result = result;
            return this;
        }


        /// <summary>
        /// Returns the underlying _result property.
        /// </summary>
        /// <returns>
        /// The underlying _result property.
        /// </returns>
        /// <remarks>
        /// The underlying _result property is typically accessed via the Result property but
        /// the Result property will return <see cref="default"/> when there are errors. There
        /// are times when the stored result is needed even in an error state.
        /// </remarks>
        public T GetResult() => _result;


        /// <summary>
        /// This adds one error to the Errors collection
        /// </summary>
        /// <param name="errorMessage">The text of the error message</param>
        /// <param name="propertyNames">optional. A list of property names that this error applies to</param>
        public new IStatusGeneric<T> AddError(string errorMessage, params string[] propertyNames)
        {
            if (errorMessage is null) throw new ArgumentNullException(nameof(errorMessage));

            _errors.Add(new ErrorGeneric(Header, new ValidationResult(errorMessage, propertyNames)));

            return this;
        }
    }
}
