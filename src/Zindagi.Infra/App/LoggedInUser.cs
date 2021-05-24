using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Authorization;
using Zindagi.SeedWork;

namespace Zindagi.Infra.App
{
    public class LoggedInUser : ILoggedInUser
    {
        private readonly AuthenticationStateProvider _authProvider;

        public LoggedInUser(AuthenticationStateProvider authProvider) => _authProvider = authProvider;

        public async Task<VendorId> GetId()
        {
            var user = (await _authProvider.GetAuthenticationStateAsync()).User;
            return user?.Identity?.IsAuthenticated ?? false ? user.GetOpenId() : null;
        }

        public async Task<long> GetSerialNumber()
        {
            var user = (await _authProvider.GetAuthenticationStateAsync()).User;
            return user?.Identity?.IsAuthenticated ?? false ? user.GetSerialNumber() : null;
        }

        public async Task<Result<OpenIdUser>> GetUser()
        {
            var authState = await _authProvider.GetAuthenticationStateAsync();

            if (authState.User.Identity?.IsAuthenticated ?? false)
                return Result<OpenIdUser>.Success(OpenIdUser.Create(authState.User));

            return Result<OpenIdUser>.Error("User not logged in");
        }
    }
}
