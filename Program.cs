using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using Quartz;

namespace ConsoleWindowsHosting;

public class Program
{
    private static async Task Main(string[] args)
    {
        IHost Host = CreateHostBuilder(args).Build();
        await Host.RunAsync();
    }

    public static IHostBuilder CreateHostBuilder(string[] args) => Host.CreateDefaultBuilder(args)
                                                                       .UseWindowsService()
                                                                       .ConfigureServices(services =>
                                                                        {
                                                                            ConfigureQuartzService(services);
                                                                            services.AddScoped<IEmailService, SendgridEmailService>();
                                                                        });

    private static void ConfigureQuartzService(IServiceCollection services)
    {
        // Add the required Quartz.NET services
        services.AddQuartz(q =>
        {
            // Use a Scoped container to create jobs.
            // Create a "key" for the job
            var jobKey = new JobKey(nameof(LongRunningJob));
            // Register the job with the DI container
            q.AddJob<LongRunningJob>(opts => opts.WithIdentity(jobKey));
            // Create a trigger for the job
            q.AddTrigger(opts => opts.ForJob(jobKey) // link to the Task1
                .WithIdentity($"{nameof(LongRunningJob)}-Trigger") // give the trigger a unique name
                .WithCronSchedule("0/40 * * * * ?")); // run every 25 seconds
        });
        // Add the Quartz.NET hosted service
        services.AddQuartzHostedService(q => q.WaitForJobsToComplete = true);
    }
}