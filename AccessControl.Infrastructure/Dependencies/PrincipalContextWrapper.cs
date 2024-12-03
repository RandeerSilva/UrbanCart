using System;
using System.DirectoryServices.Protocols;
using System.Net;
using AccessControl.Application.Persistence;
using AccessControl.Application.ViewModels;

namespace AccessControl.Infrastructure.Dependencies
{
    public class PrincipalContextWrapper : IPrincipalContextWrapper
    {

        public async Task<UserDetailsVM> ValidateCredentialsAndGetUserDetailsAsync(string ldapHost, string domain, string username, string password)
        {
            return await Task.Run(() =>
            {
                try
                {
                    using (var ldapConnection = new LdapConnection(ldapHost))
                    {
                        var credential = new NetworkCredential($"{domain}\\{username}", password);
                        ldapConnection.Credential = credential;
                        ldapConnection.AuthType = AuthType.Negotiate;

                        // Attempt to bind with the provided credentials
                        ldapConnection.Bind();

                        // If the bind is successful, search for the user details
                        var searchRequest = new SearchRequest(
                            "DC=your-domain,DC=com", // Change to your actual base DN
                            $"(sAMAccountName={username})", // Filter by username
                            SearchScope.Subtree,
                            null // Retrieve all attributes
                        );

                        var searchResponse = (SearchResponse)ldapConnection.SendRequest(searchRequest);

                        if (searchResponse.Entries.Count > 0)
                        {
                            // Get the first matching entry
                            var entry = searchResponse.Entries[0];

                            // Return user details
                            return new UserDetailsVM(entry.Attributes["displayName"][0]?.ToString(), entry.Attributes["mail"][0]?.ToString(), entry.Attributes["sAMAccountName"][0]?.ToString(),true);
                        }

                        return null; // User not found
                    }
                }
                catch (LdapException)
                {
                    return new UserDetailsVM(); // Invalid credentials or connection issue
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Unexpected error: {ex.Message}");
                    return new UserDetailsVM();
                }
            });
        }
    }
}
