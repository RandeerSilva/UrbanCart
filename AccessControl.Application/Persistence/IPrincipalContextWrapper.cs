using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AccessControl.Application.ViewModels;

namespace AccessControl.Application.Persistence
{
    public interface IPrincipalContextWrapper
    {
        Task<UserDetailsVM> ValidateCredentialsAndGetUserDetailsAsync(string ldapHost, string domain, string username,
            string password);
    }
}
