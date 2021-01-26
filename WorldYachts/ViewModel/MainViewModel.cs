using System;
using System.Collections.Generic;
using System.Text;
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
        private BaseViewModel _selectedViewModel = new DashboardViewModel();
        private DelegateCommand _updateViewCommand;
        public BaseViewModel SelectedViewModel
        {
            get => _selectedViewModel;
            set
            {
                _selectedViewModel = value;
                OnPropertyChanged(nameof(SelectedViewModel));
            }
        }

        public DelegateCommand UpdateViewCommand
        {
            get
            {
                return _updateViewCommand ??= new DelegateCommand((arg) =>
                {
                    var lvi = (ListViewItem) arg;
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
                        case "CatalogManagement":
                            SelectedViewModel = new CatalogManagementViewModel();
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
    }
}
