using Autofac;
using AutoMapper.Contrib.Autofac.DependencyInjection;
using WorldYachts.DependencyInjections.Helpers;
using WorldYachts.DependencyInjections.Models;
using WorldYachts.DependencyInjections.Services;
using WorldYachts.Model;
using WorldYachts.Services;
using WorldYachts.Services.Serialization;
using WorldYachts.Services.Users;
using WorldYachts.Services.WebApiServices;
using WorldYachts.View.DashboardControlViews;
using WorldYachts.ViewModel;
using WorldYachts.ViewModel.Accessory;
using WorldYachts.ViewModel.Boat;
using WorldYachts.ViewModel.Boat.BoatType;
using WorldYachts.ViewModel.Boat.Type;
using WorldYachts.ViewModel.Boat.Wood;
using WorldYachts.ViewModel.CatalogControlViewModels;
using WorldYachts.ViewModel.DashboardControlViewModels;
using WorldYachts.ViewModel.OrderControlViewModels;
using WorldYachts.ViewModel.Partner;
using WorldYachts.ViewModel.Users;
using WorldYachts.ViewModel.Users.SalesPersons;
using IPartnerModel = WorldYachts.DependencyInjections.Models.IPartnerModel;

namespace WorldYachts.Helpers
{
    internal class ConfigureContainer : Module
    {
        [System.Obsolete]
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<WebClientService>().As<IWebClientService>().SingleInstance();
            builder.RegisterType<AuthUser>().AsSelf().SingleInstance();
            builder.RegisterType<ViewModelContainer>().As<IViewModelContainer>().SingleInstance();

            builder.AddAutoMapper(typeof(BoatMapper).Assembly);

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

            #region Accessory

            builder.RegisterType<AccessoryControlViewModel>().AsSelf().InstancePerDependency();
            builder.RegisterType<AccessoryEditorViewModel>().AsSelf().InstancePerDependency();
            builder.RegisterType<AccessoryManagementViewModel>().AsSelf().InstancePerDependency();
            builder.RegisterType<SelectableAccessoryViewModel>().AsSelf().InstancePerDependency();

            #endregion

            #region Partners

            builder.RegisterType<PartnerEditorViewModel>().AsSelf().InstancePerDependency();
            builder.RegisterType<PartnersManagementViewModel>().AsSelf().InstancePerDependency();
            builder.RegisterType<SelectablePartnerViewModel>().AsSelf().InstancePerDependency();

            #endregion

            #region Boat

            builder.RegisterType<BoatEditorViewModel>().AsSelf().InstancePerDependency();
            builder.RegisterType<BoatManagementViewModel>().AsSelf().InstancePerDependency();
            builder.RegisterType<SelectableBoatViewModel>().AsSelf().InstancePerDependency();

            #endregion

            #region Boat type

            builder.RegisterType<BoatTypeManagementViewModel>().AsSelf().InstancePerDependency();
            builder.RegisterType<BoatTypeEditorViewModel>().AsSelf().InstancePerDependency();
            builder.RegisterType<SelectableBoatTypeViewModel>().AsSelf().InstancePerDependency();

            #endregion

            #region Boat wood

            builder.RegisterType<BoatWoodManagementViewModel>().AsSelf().InstancePerDependency();
            builder.RegisterType<BoatWoodEditorViewModel>().AsSelf().InstancePerDependency();
            builder.RegisterType<SelectableBoatWoodViewModel>().AsSelf().InstancePerDependency();

            #endregion

            #region Accessory To Boat
            builder.RegisterType<AccessoryFitManagementViewModel>().AsSelf().InstancePerDependency();
            builder.RegisterType<AccessoryFitEditorViewModel>().AsSelf().InstancePerDependency();
            builder.RegisterType<SelectableAccessoryFitViewModel>().AsSelf().InstancePerDependency();

            #endregion

            #region Sales Person

            builder.RegisterType<SalesPersonEditorViewModel>().AsSelf().InstancePerDependency();
            builder.RegisterType<SalesPersonManagementViewModel>().AsSelf().InstancePerDependency();
            builder.RegisterType<SelectableSalesPersonViewModel>().AsSelf().InstancePerDependency();

            #endregion

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
            
            builder.RegisterType<UserControlViewModel>().AsSelf().InstancePerDependency();
            builder.RegisterType<BoatControlViewModel>().AsSelf().InstancePerDependency();
        }

        private void RegisterServices(ContainerBuilder builder)
        {
            builder.RegisterType<UserService>().As<IUserService>();
            builder.RegisterType<BoatWebService>().As<IBoatService>();
            builder.RegisterType<AccessoryWebService>().As<IAccessoryService>();
            builder.RegisterType<AccessoryToBoatWebService>().As<IAccessoryToBoatService>();
            builder.RegisterType<PartnerWebService>().As<IPartnerService>();
            builder.RegisterType<BoatWoodWebService>().As<IBoatWoodService>();
            builder.RegisterType<BoatTypeWebService>().As<IBoatTypeService>();
            builder.RegisterType<AdminWebService>().As<IAdminService>();
            builder.RegisterType<SalesPersonWebService>().As<ISalesPersonService>();
        }

        private void RegisterModels(ContainerBuilder builder)
        {
            builder.RegisterType<UserModel>().AsSelf();
            builder.RegisterType<PartnerModel>().As<IPartnerModel>().InstancePerLifetimeScope();
            builder.RegisterType<AccessoryModel>().As<IAccessoryModel>().InstancePerLifetimeScope();
            builder.RegisterType<BoatModel>().As<IBoatModel>().InstancePerLifetimeScope();
            builder.RegisterType<BoatWoodModel>().As<IBoatWoodModel>().InstancePerLifetimeScope();
            builder.RegisterType<BoatTypeModel>().As<IBoatTypeModel>().InstancePerLifetimeScope();
            builder.RegisterType<AccessoryToBoatModel>().As<IAccessoryToBoatModel>().InstancePerLifetimeScope();
            builder.RegisterType<SalesPersonModel>().As<ISalesPersonModel>().InstancePerLifetimeScope();
        }

        private void RegisterViews(ContainerBuilder builder)
        {
            builder.RegisterType<MainWindow>().InstancePerDependency();
            builder.RegisterType<AccessoryManagementViewModel>();
            builder.RegisterType<DashboardView>().InstancePerDependency();
        }
    }
}