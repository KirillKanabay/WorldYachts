using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Mime;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using MaterialDesignThemes.Wpf;
using WorldYachts.Helpers;
using WorldYachts.Infrastructure;
using WorldYachts.View;
using WorldYachts.ViewModel.AccessoryControlViewModels;
using WorldYachts.ViewModel.BaseViewModels;
using WorldYachts.ViewModel.BoatManagementViewModels;
using WorldYachts.ViewModel.CatalogControlViewModels;
using WorldYachts.ViewModel.DashboardControlViewModels;
using WorldYachts.ViewModel.OrderControlViewModels;
using WorldYachts.ViewModel.UserControlViewModels;

namespace WorldYachts.ViewModel
{
    /// <summary>
    /// VM главного окна
    /// </summary>
    class MainViewModel:BaseViewModel
    {
        #region Поля
        private BaseViewModel _selectedViewModel = new DashboardViewModel();
        private DelegateCommand _updateViewCommand;
        private DelegateCommand _logout;
        #endregion

        #region Свойства
        /// <summary>
        /// Текущий выбранный VM
        /// </summary>
        public BaseViewModel SelectedViewModel
        {
            get => _selectedViewModel;
            set
            {
                _selectedViewModel = value;
                OnPropertyChanged(nameof(SelectedViewModel));
            }
        }

        public Visibility BoatManagementVisibility =>
            (AuthUser.TypeOfUser == TypeOfUser.Customer) ? Visibility.Collapsed : Visibility.Visible;
        
        public Visibility AccessoryManagementVisibility =>
            (AuthUser.TypeOfUser == TypeOfUser.Customer) ? Visibility.Collapsed : Visibility.Visible;
        
        public Visibility UserManagementVisibility =>
            (AuthUser.TypeOfUser == TypeOfUser.Admin) ? Visibility.Visible : Visibility.Collapsed;

        public Visibility OrdersManagementVisibility =>
            (AuthUser.TypeOfUser == TypeOfUser.SalesPerson) ? Visibility.Visible : Visibility.Collapsed;
        
        public Visibility OrdersVisibility =>
            (AuthUser.TypeOfUser == TypeOfUser.Customer) ? Visibility.Visible : Visibility.Collapsed;

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
                            SelectedViewModel = new DashboardViewModel();
                            break;
                        case "Catalog":
                            SelectedViewModel = new CatalogControlViewModel();
                            break;
                        case "Orders":
                            SelectedViewModel = new OrderControlViewModel();
                            break;
                        case "OrdersManagement":
                            SelectedViewModel = new OrderManagementControlViewModel();
                            break;
                        case "BoatManagement":
                            SelectedViewModel = new BoatManagementViewModel();
                            break;
                        case "AccessoryManagement":
                            SelectedViewModel = new AccessoryControlViewModel();
                            break;
                        case "UserManagement":
                            SelectedViewModel = new UserControlViewModel();
                            break;
                        case "AccountSettings":
                            SelectedViewModel = new AccountSettingsViewModel();
                            break;
                        case "AboutProgram":
                            SelectedViewModel = new AboutProgramViewModel();
                            break;
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
