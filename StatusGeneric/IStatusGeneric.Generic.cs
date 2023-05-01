// Copyright (c) 2019 Jon P Smith, GitHub: JonPSmith, web: http://www.thereformedprogrammer.net/
// Licensed under MIT license. See License.txt in the project root for license information.

namespace Code420.StatusGeneric
{
    /// <summary>
    /// This is a version of <see cref="IStatusGeneric"/> that contains a result.
    /// Useful if you want to return something with the status
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IStatusGeneric<out T> : IStatusGeneric
    {
        /// <summary>
        /// This contains the return result, or if there are errors it will returns default(T)
        /// </summary>
        T Result { get; }


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
        T GetResult();
    }
}
