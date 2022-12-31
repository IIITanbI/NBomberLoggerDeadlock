using NBomber.CSharp;

namespace CSharpProd.HTTP;

internal class Program
{
    static void Main(string[] args)
    {
        var scenario = Scenario.Create("scenario", async context =>
        {
            var logger = context.Logger;

            logger.Information("Before request");
            await Task.Delay(500);
            logger.Information("After request");

            return Response.Ok();
        })
        .WithoutWarmUp()
        .WithLoadSimulations(Simulation.Inject(rate: 1000, interval: TimeSpan.FromSeconds(1), during: TimeSpan.FromSeconds(5)));

        NBomberRunner
            .RegisterScenarios(scenario)
            .Run();
    }
}