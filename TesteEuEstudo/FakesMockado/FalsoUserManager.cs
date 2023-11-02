using EuEstudo.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;


namespace TesteEuEstudo.FakesMockado
{
    public class FalsoUserManager : UserManager<Usuario>
    {
        public FalsoUserManager() : base(
                  new Mock<IUserStore<Usuario>>().Object,
                  new Mock<IOptions<IdentityOptions>>().Object,
                  new Mock<IPasswordHasher<Usuario>>().Object,
                  new IUserValidator<Usuario>[0],
                  new IPasswordValidator<Usuario>[0],
                  new Mock<ILookupNormalizer>().Object,
                  new Mock<IdentityErrorDescriber>().Object,
                  new Mock<IServiceProvider>().Object,
                  new Mock<ILogger<UserManager<Usuario>>>().Object)
        {
            
        }
    }
}
