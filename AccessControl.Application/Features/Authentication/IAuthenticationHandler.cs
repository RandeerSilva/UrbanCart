using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccessControl.Application.Features.Authentication
{
    public interface IAuthenticationHandler
    {
        Task<bool> IsLoginSuccessfulAsync(string ldapHost, string domain, string username, string password);
    }
}
