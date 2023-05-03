// Copyright (c) 2021 Jon P Smith, GitHub: JonPSmith, web: http://www.thereformedprogrammer.net/
// Licensed under MIT license. See License.txt in the project root for license information.

using System.ComponentModel.DataAnnotations;
using Code420.StatusGeneric;
using MediatR;

namespace Code420.UIOrchestrator.Core.Models.AuthP
{
    public class LoginUserModel : ILoginUserModel, IRequest<StatusGenericHandler>
    {
        /// <summary>
        /// String value containing the user's email address.
        /// </summary>
        [Required(AllowEmptyStrings = false)]
        [EmailAddress]
        // [MaxLength(256)]
        [MaxLength(UIOrchestratorConstants.UIOrchestratorConstants.EmailSize)]
        public string Email { get; set; }

        /// <summary>
        /// String value containing the user's password.
        /// </summary>
        [Required(AllowEmptyStrings = false)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
