// Copyright (c) 2019 Jon P Smith, GitHub: JonPSmith, web: http://www.thereformedprogrammer.net/
// Licensed under MIT license. See License.txt in the project root for license information.

using System.Runtime.Serialization;

namespace Code420.StatusGeneric
{
    [Serializable]
    internal class AuthPermissionsBadDataException : Exception
    {
        public AuthPermissionsBadDataException()
        {
        }

        public AuthPermissionsBadDataException(string message) : base(message)
        {
        }

        public AuthPermissionsBadDataException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected AuthPermissionsBadDataException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}