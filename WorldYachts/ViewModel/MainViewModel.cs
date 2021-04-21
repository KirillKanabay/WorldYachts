using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using WorldYachts.Data;
using WorldYachts.Helpers;
using WorldYachts.Services;
using WorldYachts.View;
using WorldYachts.ViewModel.BaseViewModels;
using WorldYachts.ViewModel.CatalogControlViewModels;
using WorldYachts.ViewModel.DashboardControlViewModels;
using WorldYachts.ViewModel.OrderControlViewModels;

namespace WorldYachts.ViewModel
{
    /// <summary>
    /// VM главного окна
    /// </summary>
    class MainViewModel:BaseViewModel
    {
        #region Поля

        private DelegateCommand _updateViewCommand;
        private DelegateCommand _logout;

        private BaseViewModel _currentViewModel;
        #endregion

        public MainViewModel()
        {
            _currentViewModel = new DashboardViewModel();
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
            (AuthUser.GetInstance().TypeOfUser == TypeOfUser.Customer) ? Visibility.Collapsed : Visibility.Visible;
        
        public Visibility AccessoryManagementVisibility =>
            (AuthUser.GetInstance().TypeOfUser == TypeOfUser.Customer) ? Visibility.Collapsed : Visibility.Visible;
        
        public Visibility UserManagementVisibility =>
            (AuthUser.GetInstance().TypeOfUser == TypeOfUser.Admin) ? Visibility.Visible : Visibility.Collapsed;

        public Visibility OrdersManagementVisibility =>
            (AuthUser.GetInstance().TypeOfUser == TypeOfUser.SalesPerson) ? Visibility.Visible : Visibility.Collapsed;
        
        public Visibility OrdersVisibility =>
            (AuthUser.GetInstance().TypeOfUser == TypeOfUser.Customer) ? Visibility.Visible : Visibility.Collapsed;

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
                            CurrentViewModel = new DashboardViewModel();
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
                        //case "BoatManagement":
                        //    new NavigateCommand<BoatManagementViewModel>(_navigationStore,
                        //        () => new BoatManagementViewModel()).Execute(null);
                        //    break;
                        //case "AccessoryManagement":
                        //    new NavigateCommand<AccessoryControlViewModel>(_navigationStore,
                        //        () => new AccessoryControlViewModel()).Execute(null);
                        //    break;
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
