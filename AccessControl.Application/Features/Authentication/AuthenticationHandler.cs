using AccessControl.Application.ViewModels;
using AccessControl.Application.Persistence;
using AccessControl.Domain.Entities;
using AutoMapper;

namespace AccessControl.Application.Features.Authentication
{
    public class AuthenticationHandler : IAuthenticationHandler
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IPrincipalContextWrapper _principalContextWrapper;
        public AuthenticationHandler(IUserRepository userRepository, IMapper mapper, IPrincipalContextWrapper principalContextWrapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _principalContextWrapper = principalContextWrapper;
        }

        public async Task<bool> IsLoginSuccessfulAsync(string ldapHost, string domain, string username, string password)
        {
            try
            {

                var response = await _principalContextWrapper.ValidateCredentialsAndGetUserDetailsAsync(ldapHost, domain, username, password);

                if (response is not { IsAuthenticated: true, UserName: not null }) return false;

                var userDetails = await _userRepository.GetByUserNameAsync(response.UserName);
                var user = _mapper.Map<User>(response);
                if (userDetails != null)
                {
                    await _userRepository.UpdateAsync(user);
                }
                else
                    await _userRepository.AddUserAsync(user);
                return true;

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }


    }
}
