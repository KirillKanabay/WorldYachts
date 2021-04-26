using System;
using System.IO;
using System.Windows;
using Autofac;
using Microsoft.Extensions.Configuration;
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
        public IContainer Container { get; private set; }

        protected override void OnStartup(StartupEventArgs e)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            Configuration = builder.Build();

            var containerBuilder = new ContainerBuilder();
            containerBuilder.RegisterModule<ConfigureContainer>();
            Container = containerBuilder.Build();

            WebClientService.GetInstance(Configuration);

            using (var scope = Container.BeginLifetimeScope())
            {
                Window startWindow = new LoginWindow();
                startWindow.DataContext = Container.Resolve<LoginViewModel>();
                startWindow.Show();
            }
            base.OnStartup(e);
        }


    }
}
