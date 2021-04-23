using System;
using System.IO;
using System.Windows;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WorldYachts.Services;
using WorldYachts.Services.Users;
using WorldYachts.View;
using WorldYachts.ViewModel;

namespace WorldYachts
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public IConfiguration Configuration { get; private set; }

        protected override void OnStartup(StartupEventArgs e)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            Configuration = builder.Build();

            WebClientService.GetInstance(Configuration);

            Window startWindow = new LoginWindow();
            startWindow.DataContext = new LoginViewModel();
            startWindow.Show();
            
            base.OnStartup(e);
        }


    }
}
