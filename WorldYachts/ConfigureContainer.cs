﻿using Autofac;
using WorldYachts.Model;
using WorldYachts.Services;
using WorldYachts.Services.Accessory;
using WorldYachts.Services.Boat;
using WorldYachts.Services.Users;
using WorldYachts.View.DashboardControlViews;
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
            builder.RegisterType<AuthUser>().AsSelf().SingleInstance();
            builder.RegisterType<ViewModelContainer>().AsSelf();
            
            RegisterViewModels(builder);
            RegisterServices(builder);
            RegisterModels(builder);
            RegisterViews(builder);
            
        }

        private void RegisterViewModels(ContainerBuilder builder)
        {
            builder.RegisterType<LoginViewModel>().AsSelf().InstancePerDependency();
            builder.RegisterType<MainViewModel>().AsSelf().InstancePerDependency();
            builder.RegisterType<AccountSettingsViewModel>().AsSelf().InstancePerDependency();
            builder.RegisterType<AboutProgramViewModel>().AsSelf().InstancePerDependency();
            builder.RegisterType<AccessoryControlViewModel>().AsSelf().InstancePerDependency();
            builder.RegisterType<AccessoryEditorViewModel>().AsSelf().InstancePerDependency();
            builder.RegisterType<AccessoryFitEditorViewModel>().AsSelf().InstancePerDependency();
            builder.RegisterType<AccessoryManagementViewModel>().AsSelf().InstancePerDependency();
            builder.RegisterType<PartnerEditorViewModel>().AsSelf().InstancePerDependency();
            builder.RegisterType<PartnersManagementViewModel>().AsSelf().InstancePerDependency();
            builder.RegisterType<SelectableAccessoryFitViewModel>().AsSelf().InstancePerDependency();
            builder.RegisterType<SelectableAccessoryViewModel>().AsSelf().InstancePerDependency();
            builder.RegisterType<SelectablePartnerViewModel>().AsSelf().InstancePerDependency();
            builder.RegisterType<BoatEditorViewModel>().AsSelf().InstancePerDependency();
            builder.RegisterType<BoatManagementViewModel>().AsSelf().InstancePerDependency();
            builder.RegisterType<SelectableBoatViewModel>().AsSelf().InstancePerDependency();
            builder.RegisterType<BoatViewModel>().AsSelf().InstancePerDependency();
            builder.RegisterType<CatalogControlViewModel>().AsSelf().InstancePerDependency();
            builder.RegisterType<DashboardViewModel>().AsSelf().InstancePerDependency();
            builder.RegisterType<ContractEditorViewModel>().AsSelf().InstancePerDependency();
            builder.RegisterType<ContractManagementViewModel>().AsSelf().InstancePerDependency();
            builder.RegisterType<ContractViewModel>().AsSelf().InstancePerDependency();
            builder.RegisterType<DepositEditorViewModel>().AsSelf().InstancePerDependency();
            builder.RegisterType<InvoiceManagementViewModel>().AsSelf().InstancePerDependency();
            builder.RegisterType<InvoiceViewModel>().AsSelf().InstancePerDependency();
            builder.RegisterType<OrderControlViewModel>().AsSelf().InstancePerDependency();
            builder.RegisterType<OrderManagementViewModel>().AsSelf().InstancePerDependency();
            builder.RegisterType<OrderManagementControlViewModel>().AsSelf().InstancePerDependency();
            builder.RegisterType<OrderViewModel>().AsSelf().InstancePerDependency();
            builder.RegisterType<ProductProcessEditorViewModel>().AsSelf().InstancePerDependency();
            builder.RegisterType<SelectableContractViewModel>().AsSelf().InstancePerDependency();
            builder.RegisterType<SelectableInvoiceViewModel>().AsSelf().InstancePerDependency();
            builder.RegisterType<SelectableOrderViewModel>().AsSelf().InstancePerDependency();
            builder.RegisterType<SalesPersonEditorViewModel>().AsSelf().InstancePerDependency();
            builder.RegisterType<SalesPersonManagementViewModel>().AsSelf().InstancePerDependency();
            builder.RegisterType<SelectableSalesPersonViewModel>().AsSelf().InstancePerDependency();
            builder.RegisterType<UserControlViewModel>().AsSelf().InstancePerDependency();
        }

        private void RegisterServices(ContainerBuilder builder)
        {
            builder.RegisterType<UserService>().As<IUserService>();
            builder.RegisterType<BoatService>().As<IBoatService>();
            builder.RegisterType<AccessoryService>().As<IAccessoryService>();
        }

        private void RegisterModels(ContainerBuilder builder)
        {
            builder.RegisterType<UserModel>().AsSelf();
            builder.RegisterType<AccessoryModel>().AsSelf();
        }

        private void RegisterViews(ContainerBuilder builder)
        {
            builder.RegisterType<MainWindow>().InstancePerDependency();
            builder.RegisterType<DashboardView>().InstancePerDependency();
        }
    }
}
