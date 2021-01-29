using System;
using System.Collections.Generic;
using System.Net.Mime;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using MaterialDesignThemes.Wpf;
using WorldYachts.Helpers;

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
        private MainWindow _mainWindowView = (MainWindow)Application.Current.MainWindow;
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
                            SelectedViewModel = new CatalogViewModel();
                            break;
                        case "Orders":
                            SelectedViewModel = new OrdersViewModel();
                            break;
                        case "BoatManagement":
                            SelectedViewModel = new BoatManagementViewModel();
                            break;
                        case "UserManagement":
                            SelectedViewModel = new UserManagementViewModel();
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

        #endregion

    }
}
