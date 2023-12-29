using EmailService.Messaging;
using System.Runtime.CompilerServices;

namespace EmailService.Extensions
{
    public static class AzureServiceBusExtension
    {
        public static IAzureBus azureBus { get; set; }
        public static IApplicationBuilder useAzure (this IApplicationBuilder application)
        {
            azureBus = application.ApplicationServices.GetService<IAzureBus>();
            var HostLifetime = application.ApplicationServices.GetService<IHostApplicationLifetime>();
            HostLifetime.ApplicationStarted.Register(OnAppStart);
            HostLifetime.ApplicationStopping.Register(OnAppStopping);
            return application;
        }

        private static void OnAppStopping()
        {
            azureBus.stop();
        }

        private static void OnAppStart()
        {
            azureBus.start();
        }
    }
}
