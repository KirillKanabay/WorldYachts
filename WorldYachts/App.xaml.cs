using System;
using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using WorldYachts.Services;
using WorldYachts.View;
using WorldYachts.ViewModel;

namespace WorldYachts
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            IServiceProvider serviceProvider = CreateServiceProvider();

            IWebClientService wcs = serviceProvider.GetRequiredService<IWebClientService>();

            //Console.WriteLine();
            Window window = new LoginWindow();
            window.DataContext = serviceProvider.GetRequiredService<LoginViewModel>();
            window.Show();
            
            base.OnStartup(e);
        }

        private IServiceProvider CreateServiceProvider()
        {
            IServiceCollection services = new ServiceCollection();

            services.AddSingleton<IWebClientService, WebClientService>();

            services.AddScoped<LoginViewModel>();

            return services.BuildServiceProvider();
        }
    }
}
