// Copyright (c) 2021 Jon P Smith, GitHub: JonPSmith, web: http://www.thereformedprogrammer.net/
// Licensed under MIT license. See License.txt in the project root for license information.

namespace Code420.UIOrchestrator.Core.Models.AuthP
{
    public interface ILoginUserModel
    {
        string Email { get; set; }
        string Password { get; set; }
    }
}