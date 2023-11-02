using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;

namespace TesteEuEstudoAPI.AccountControllerTests
{
    public class TesteServer<TStartup> : IDisposable where TStartup : class
    {
        public readonly  TestServer Server;
        private readonly HttpClient _client;

        public TesteServer()
        {
            var builder = new WebHostBuilder()
                .UseContentRoot($"..\\..\\..\\..\\..\\src\\EuEstudo.API\\")
                .UseStartup<TStartup>();

            Server = new TestServer(builder);
            _client = new HttpClient();

            _client.BaseAddress = Server.BaseAddress;
        }

        public void Dispose()
        {
            _client.Dispose();
            Server.Dispose();
        }
    }
}
   
