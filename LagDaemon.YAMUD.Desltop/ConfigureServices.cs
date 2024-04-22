using LagDaemon.YAMUD.Desltop.ViewModels;
using LagDaemon.YAMUD.Desltop.Views;
using Microsoft.Extensions.DependencyInjection;

namespace LagDaemon.YAMUD.Desltop
{
    public class ConfigureServices
    {
        public static IServiceCollection Configure()
        {
            var services = new ServiceCollection();

            // Register your view models and services here
            services.AddTransient<MainWindow>();
            services.AddTransient<LoginView>(); 
            services.AddTransient<LoginViewModel>();
            services.AddTransient<MainWindowViewModel>();

            return services;
        }
    }

}
