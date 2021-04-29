using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using WorldYachts.Data;
using WorldYachts.DependencyInjections.Helpers;
using WorldYachts.Helpers;
using WorldYachts.Model;
using WorldYachts.Services;
using WorldYachts.View;
using WorldYachts.ViewModel.Accessory;
using WorldYachts.ViewModel.AccessoryControlViewModels;
using WorldYachts.ViewModel.BaseViewModels;
using WorldYachts.ViewModel.Boat;
using WorldYachts.ViewModel.CatalogControlViewModels;
using WorldYachts.ViewModel.DashboardControlViewModels;
using WorldYachts.ViewModel.OrderControlViewModels;
using WorldYachts.ViewModel.UserControlViewModels;

namespace WorldYachts.ViewModel
{
    /// <summary>
    /// VM главного окна
    /// </summary>
    public class MainViewModel:BaseViewModel
    {
        #region Поля

        private DelegateCommand _updateViewCommand;
        private DelegateCommand _logout;

        private BaseViewModel _currentViewModel;
        private readonly AuthUser _authUser;

        private readonly IViewModelContainer _viewModelContainer;
        #endregion

        public MainViewModel(AuthUser authUser, IViewModelContainer viewModelContainer)
        {
            _authUser = authUser;
            _viewModelContainer = viewModelContainer;
            CurrentViewModel = _viewModelContainer.GetViewModel<DashboardViewModel>();
        }

        #region Свойства

        /// <summary>
        /// Текущий выбранный VM
        /// </summary>
        public BaseViewModel CurrentViewModel
        {
            get => _currentViewModel;
            set
            {
                _currentViewModel = value;
                OnPropertyChanged(nameof(CurrentViewModel));
            }
        }

        public Visibility BoatManagementVisibility =>
            (_authUser.TypeOfUser == TypeOfUser.Customer) ? Visibility.Collapsed : Visibility.Visible;
        
        public Visibility AccessoryManagementVisibility =>
            (_authUser.TypeOfUser == TypeOfUser.Customer) ? Visibility.Collapsed : Visibility.Visible;
        
        public Visibility UserManagementVisibility =>
            (_authUser.TypeOfUser == TypeOfUser.Admin) ? Visibility.Visible : Visibility.Collapsed;

        public Visibility OrdersManagementVisibility =>
            (_authUser.TypeOfUser == TypeOfUser.SalesPerson) ? Visibility.Visible : Visibility.Collapsed;
        
        public Visibility OrdersVisibility =>
            (_authUser.TypeOfUser == TypeOfUser.Customer) ? Visibility.Visible : Visibility.Collapsed;

        #endregion

        #region Команды
        /// <summary>
        /// Обновление текущего VM
        /// </summary>
        public DelegateCommand UpdateViewCommand
        {
            get
            {
                return _updateViewCommand ??= new DelegateCommand((arg) =>
                {
                    var lvi = (ListViewItem)arg;
                    lvi.IsSelected = true;

                    string viewModelName = lvi.Name;
                    switch (viewModelName)
                    {
                        case "Dashboard":
                            CurrentViewModel = new DashboardViewModel(_authUser);
                            break;
                        case "Catalog":
                            CurrentViewModel = new CatalogControlViewModel();
                            break;
                        case "Orders":
                            CurrentViewModel = new OrderControlViewModel();
                            break;
                        case "OrdersManagement":
                            CurrentViewModel = new OrderManagementControlViewModel();
                            break;
                        case "BoatManagement":
                            CurrentViewModel = _viewModelContainer.GetViewModel<BoatControlViewModel>();
                            break;
                        case "AccessoryManagement":
                            CurrentViewModel = _viewModelContainer.GetViewModel<AccessoryControlViewModel>();
                            break;
                        case "UserManagement":
                            CurrentViewModel = _viewModelContainer.GetViewModel<UserControlViewModel>();
                            break;
                        //case "AccountSettings":
                        //    new NavigateCommand<AccountSettingsViewModel>(_navigationStore,
                        //        () => new AccountSettingsViewModel()).Execute(null);
                        //    break;
                        //case "AboutProgram":
                        //    new NavigateCommand<AboutProgramViewModel>(_navigationStore,
                        //        () => new AboutProgramViewModel()).Execute(null);
                        //    break;
                        default:
                            throw new ArgumentException("404");
                    }
                });
            }
        }

        public DelegateCommand Logout
        {
            get
            {
                return _logout ??= new DelegateCommand((arg) =>
                {
                    if (File.Exists("bin"))
                    {
                        File.Delete("bin");
                    }
                    var thisWindow = (Window)arg;
                    LoginWindow.ShowWindow();
                    thisWindow.Close();
                });
            }
        }

        #endregion

    }
}
