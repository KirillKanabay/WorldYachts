using Autofac;
using WorldYachts.Model;
using WorldYachts.Services;
using WorldYachts.Services.Boat;
using WorldYachts.Services.Users;
using WorldYachts.ViewModel;
using WorldYachts.ViewModel.AccessoryControlViewModels;
using WorldYachts.ViewModel.BoatManagementViewModels;
using WorldYachts.ViewModel.CatalogControlViewModels;
using WorldYachts.ViewModel.DashboardControlViewModels;
using WorldYachts.ViewModel.OrderControlViewModels;
using WorldYachts.ViewModel.UserControlViewModels;

namespace WorldYachts
{
    internal class ConfigureContainer:Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<WebClientService>().As<IWebClientService>().SingleInstance();
            
            RegisterViewModels(builder);
            RegisterServices(builder);
            RegisterModels(builder);
        }

        private void RegisterViewModels(ContainerBuilder builder)
        {
            builder.RegisterType<LoginViewModel>().AsSelf();
            builder.RegisterType<MainViewModel>().AsSelf();
            builder.RegisterType<AccountSettingsViewModel>().AsSelf();
            builder.RegisterType<AboutProgramViewModel>().AsSelf();
            builder.RegisterType<AccessoryControlViewModel>().AsSelf();
            builder.RegisterType<AccessoryEditorViewModel>().AsSelf();
            builder.RegisterType<AccessoryFitEditorViewModel>().AsSelf();
            builder.RegisterType<AccessoryManagementViewModel>().AsSelf();
            builder.RegisterType<PartnerEditorViewModel>().AsSelf();
            builder.RegisterType<PartnersManagementViewModel>().AsSelf();
            builder.RegisterType<SelectableAccessoryFitViewModel>().AsSelf();
            builder.RegisterType<SelectableAccessoryViewModel>().AsSelf();
            builder.RegisterType<SelectablePartnerViewModel>().AsSelf();
            builder.RegisterType<BoatEditorViewModel>().AsSelf();
            builder.RegisterType<BoatManagementViewModel>().AsSelf();
            builder.RegisterType<SelectableBoatViewModel>().AsSelf();
            builder.RegisterType<BoatViewModel>().AsSelf();
            builder.RegisterType<CatalogControlViewModel>().AsSelf();
            builder.RegisterType<DashboardViewModel>().AsSelf();
            builder.RegisterType<ContractEditorViewModel>().AsSelf();
            builder.RegisterType<ContractManagementViewModel>().AsSelf();
            builder.RegisterType<ContractViewModel>().AsSelf();
            builder.RegisterType<DepositEditorViewModel>().AsSelf();
            builder.RegisterType<InvoiceManagementViewModel>().AsSelf();
            builder.RegisterType<InvoiceViewModel>().AsSelf();
            builder.RegisterType<OrderControlViewModel>().AsSelf();
            builder.RegisterType<OrderManagementViewModel>().AsSelf();
            builder.RegisterType<OrderManagementControlViewModel>().AsSelf();
            builder.RegisterType<OrderViewModel>().AsSelf();
            builder.RegisterType<ProductProcessEditorViewModel>().AsSelf();
            builder.RegisterType<SelectableContractViewModel>().AsSelf();
            builder.RegisterType<SelectableInvoiceViewModel>().AsSelf();
            builder.RegisterType<SelectableOrderViewModel>().AsSelf();
            builder.RegisterType<SalesPersonEditorViewModel>().AsSelf();
            builder.RegisterType<SalesPersonManagementViewModel>().AsSelf();
            builder.RegisterType<SelectableSalesPersonViewModel>().AsSelf();
            builder.RegisterType<UserControlViewModel>().AsSelf();
        }

        private void RegisterServices(ContainerBuilder builder)
        {
            builder.RegisterType<UserService>().As<IUserService>();
            builder.RegisterType<BoatService>().As<IBoatService>();
        }

        private void RegisterModels(ContainerBuilder builder)
        {
            builder.RegisterType<UserModel>().AsSelf();
        }
    }
}
