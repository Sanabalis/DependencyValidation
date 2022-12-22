using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using System.Text;
using DependencyChecker.App;
using DependencyChecker.App.Controllers;
using Microsoft.Extensions.Logging.Console;

namespace DependencyChecker.Tests;

[TestFixture]
public class DependencyTests
{
    [Test]
    public void ValidateDependencies()
    {


        var app = new WebApplicationFactory<IApiMarker>()
            .WithWebHostBuilder(builder => builder.ConfigureTestServices(serviceCollection =>
            {
                var dependencyValidator = new DependencyValidator(serviceCollection);


                
                // Validate entire collection (but this does not validate controllers)
                dependencyValidator.ValidateServiceCollection();
                
                // Scan an assembly and validate all controllers, both the constructor injected dependencies and dependencies injected into endpoints 
                // marked with FromService attribute
                dependencyValidator.ValidateControllers(typeof(IApiMarker).Assembly);

                // Different way to validate some classes manually:
                // This makes it check that IWeatherService is registered, not caring what the implementation or service lifetime is
                dependencyValidator.ValidateService(typeof(IWeatherService));

                // This checks that IDiagnostics is registered, not caring what the implementation is, but checking that it is registered as Singleton 
                dependencyValidator.ValidateService(typeof(IDiagnostics), ServiceLifetime.Singleton);

                // This checks that ILoggerProvider is implemented with ConsoleLoggerProvider, even if ILoggerProvider has registered other implementations 
                // (such as DebugLoggerProvider)
                dependencyValidator.ValidateService(typeof(ILoggerProvider), typeof(ConsoleLoggerProvider));

                // This checks that IWeatherService is registered, that Implementation is WeatherService and that it's registered with Scoped service lifetime
                dependencyValidator.ValidateService(typeof(IWeatherService), typeof(WeatherService), ServiceLifetime.Scoped);




                var sb = new StringBuilder();
                foreach (var validation in dependencyValidator.FailedValidations.OrderByDescending(x => x.Severity))
                {
                    sb.AppendLine($"[{validation.Severity}] {validation.ServiceType}: {validation.Message}");
                }

                if (!dependencyValidator.IsValid)
                {
                    Assert.Fail(sb.ToString());
                }
                Assert.Pass(sb.ToString());
            }));

        app.CreateClient();
    }
}
