using EuEstudo.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;


namespace TesteEuEstudo.FakesMockado
{
    public class FalsoSignInManager : SignInManager<Usuario>
    {
        public FalsoSignInManager(FalsoUserManager userManager) : base(
            new Mock<FalsoUserManager>().Object,
            new HttpContextAccessor(),
            new Mock<IUserClaimsPrincipalFactory<Usuario>>().Object,
            new Mock<IOptions<IdentityOptions>>().Object,
            new Mock<ILogger<SignInManager<Usuario>>>().Object,
            new Mock<Microsoft.AspNetCore.Authentication.IAuthenticationSchemeProvider>().Object,
            new Mock<IUserConfirmation<Usuario>>().Object)
        { }
    }
}
