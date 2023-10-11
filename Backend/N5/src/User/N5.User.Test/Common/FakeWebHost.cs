using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using N5.Eda.Interfaces;
using System.Diagnostics.CodeAnalysis;

using MillionAndUp.Realty.Test.Extend;
using Moq;
using System.Diagnostics.CodeAnalysis;
using N5.User.Application;

namespace N5.User.Test.Common;

/// <summary>
/// WebHost for tests with Dependency Injection
/// </summary>
[ExcludeFromCodeCoverage]
public static class FakeWebHost
{
    private static TestServer? _testServer;

    public static TestServer TestServer =>
        _testServer ??=
        new BasicWebApplicationFactory().Server;

    public static IServiceScope ServiceScope => TestServer.Services.GetService<IServiceScopeFactory>().CreateScope();

    public class BasicWebApplicationFactory : WebApplicationFactory<ProgramApi>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                // Create a new service provider.
                var serviceProvider = new ServiceCollection()
                    .AddEntityFrameworkInMemoryDatabase()
                    .BuildServiceProvider();

                Mock<IBroker> mockBroker = new();
                services.AddTransient(sp => mockBroker.Object);
               // services.AddTransient(sp => UserContextExtend.UnitOfWork);


                services.AddApplication();
            });
        }
    }
}