using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using WorldYachts.Data;
using WorldYachts.Helpers;
using WorldYachts.Services;
using WorldYachts.View;
using WorldYachts.ViewModel.AccessoryControlViewModels;
using WorldYachts.ViewModel.BaseViewModels;
using WorldYachts.ViewModel.BoatManagementViewModels;
using WorldYachts.ViewModel.CatalogControlViewModels;
using WorldYachts.ViewModel.DashboardControlViewModels;
using WorldYachts.ViewModel.OrderControlViewModels;

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

        private readonly DashboardViewModel _dashboardViewModel;
        private readonly CatalogControlViewModel _catalogControlViewModel;
        #endregion

        public MainViewModel(AuthUser authUser)
        {
            _authUser = authUser;
            CurrentViewModel = new DashboardViewModel(_authUser);
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
                            CurrentViewModel = new BoatManagementViewModel();
                            break;
                        case "AccessoryManagement":
                            CurrentViewModel = new AccessoryManagementViewModel();
                            break;
                        //case "UserManagement":
                        //    new NavigateCommand<UserControlViewModel>(_navigationStore,
                        //        () => new UserControlViewModel()).Execute(null);
                        //    break;
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
