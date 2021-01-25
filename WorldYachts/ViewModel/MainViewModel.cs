﻿using System;
using System.Collections.Generic;
using System.Text;
using WorldYachts.Helpers;

namespace WorldYachts.ViewModel
{
    /// <summary>
    /// VM главного окна
    /// </summary>
    class MainViewModel:BaseViewModel
    {
        private BaseViewModel _selectedViewModel;
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
                    string viewModelName = arg.ToString();
                    switch (arg)
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
