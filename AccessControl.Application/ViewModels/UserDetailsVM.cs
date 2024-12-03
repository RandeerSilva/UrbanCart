using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccessControl.Application.ViewModels
{
    public class UserDetailsVM
    {
        public string? UserName { get; set; }
        public string? DisplayName { get; set; }
        public string? Email { get; set; }

        public bool IsAuthenticated { get; set; }
        public UserDetailsVM(string? displayName = null, string? email = null, string? userName = null, bool isAuthenticated = false)
        {
            UserName = userName;
            DisplayName = displayName;
            Email = email;
            DisplayName = displayName;
            IsAuthenticated = isAuthenticated;
        }


    }
}
