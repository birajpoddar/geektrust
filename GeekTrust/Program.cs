using System;
using GeekTrust.Models;
using GeekTrust.Repositories;
using GeekTrust.Utils;
using GeekTrust.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.IO;

[assembly: InternalsVisibleTo("GeekTrustTests")]

namespace GeekTrust
{
    public class Program
    {
        public static List<string> ErrorMessages
        {
            get { return RequestValidator.validationErrors; }
            set { RequestValidator.validationErrors = value; }
        }
        public static List<string> SuccessMessages
        {
            get { return ProgramHelper.responseMessages; }
            set { ProgramHelper.responseMessages = value; }
        }


        public static void Main(params string[] args)
        {
            try
            {
                // Load Environment Variables
                if (args.Length == 2)
                    DotNetEnv.Env.Load(args[1]);
                else
                    DotNetEnv.Env.Load();

                // Add Services to DI and get the provider
                var provider = ServiceBuilder();

                // Gets the ProgramHelper instance
                var helper = provider.GetService<ProgramHelper>();

                // Seed Data and display Plans & Topups
                helper.SeedData();

                // Deserialize the request
                var req = helper.DeserializeInput(args[0]);

                // Calculate the Renewal Details
                var res = helper.CalculateSubscriptionRenewalDateAmount(req);

                // Print Renewal Details
                Utils.UtilityFunctions.PrintStringList(res);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadLine();
            }
        }

        // Add Services to DI Container
        internal static ServiceProvider ServiceBuilder()
        {
            ServiceCollection services = new();
            ServiceProvider serviceProvider;

            // Add Services to DI Container
            services.AddLogging(options =>
            {
                options.AddConsole();
                options.SetMinimumLevel(LogLevel.Critical);
            });
            services.AddDbContext<IDbContext, DoReMiContext>(options =>
            {
                options.UseInMemoryDatabase("DoReMi");
            });
            services.AddSingleton<IDataSeeder, DataSeeder>();
            services.AddSingleton<IDataViewer, DataViewer>();
            services.AddSingleton<IRequestCategorizer, RequestCategorizer>();
            services.AddSingleton<IRequestValidator, RequestValidator>();
            services.AddSingleton<IRequestDeserializer, RequestDeserializer>();
            services.AddSingleton<ISubscriptionCalculator, SubscriptionCalculator>();
            services.AddSingleton<ProgramHelper>();

            // Build the Service Provider
            serviceProvider = services.BuildServiceProvider();

            // Return provider
            return serviceProvider;
        }
    }
}
