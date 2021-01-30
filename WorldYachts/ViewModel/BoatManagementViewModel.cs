using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using MaterialDesignThemes.Wpf;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using WorldYachts.Data;
using WorldYachts.Helpers;
using WorldYachts.Helpers.Commands;
using WorldYachts.Model;
using WorldYachts.View.CatalogManagementViews;
using WorldYachts.View.MessageDialogs;
using WorldYachts.ViewModel.BoatManagementViewModels;
using WorldYachts.ViewModel.MessageDialog;

namespace WorldYachts.ViewModel
{
    class BoatManagementViewModel:BaseViewModel
    {
        #region Поля
        private ObservableCollection<SelectableBoatViewModel> _boatsCollection;
        private readonly BoatModel _boatModel = new BoatModel();

        private string _filterText;

        private AsyncRelayCommand _getBoatsCollection;
        private AsyncRelayCommand _removeBoats;
        private DelegateCommand _openBoatEditorDialog;
        private AsyncRelayCommand _removeBoat;

        private Visibility _progressBarVisibility;
        #endregion

        #region Конструкторы

        public BoatManagementViewModel()
        {
            GetBoatsCollection.Execute(null);
            SelectableBoatViewModel.OnItemChanged = () =>
            {
                RemoveBoats.Execute(null);
            };
        }
        #endregion

        #region Свойства
        /// <summary>
        /// Список лодок
        /// </summary>
        public ObservableCollection<SelectableBoatViewModel> FilteredBoats
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(_filterText)) 
                    return FilterBoats(_filterText);
                
                return _boatsCollection;
            }
            set
            {
                _boatsCollection = value;
                RemoveBoats.Execute(null);
            }
        }

        public Visibility ProgressBarVisibility
        {
            get => _progressBarVisibility;
            set
            {
                _progressBarVisibility = value;
                OnPropertyChanged(nameof(ProgressBarVisibility));
            }
        }

        public string FilterText
        {
            get => _filterText;
            set
            {
                _filterText = value;
                if (_filterText != null)
                    OnPropertyChanged(nameof(FilteredBoats));
            }
        }

        #endregion

        #region Команды
        /// <summary>
        /// Команда получения коллекции лодок
        /// </summary>
        public AsyncRelayCommand GetBoatsCollection
        {
            get
            {
                return _getBoatsCollection ??= new AsyncRelayCommand(GetBoatsMethod, (ex) =>
                {
                    ExecuteRunDialog(new MessageDialogProperty() {Title = "Ошибка", Message = ex.Message});
                });
            }
        }
        /// <summary>
        /// Команда удаления лодки
        /// </summary>
        public AsyncRelayCommand RemoveBoats
        {
            get
            {
                return _removeBoats ??= new AsyncRelayCommand(RemoveBoatsMethod, (ex) =>
                {
                    ExecuteRunDialog(new MessageDialogProperty() { Title = "Ошибка", Message = ex.Message });
                });
            }
        }

        public DelegateCommand OpenBoatEditorDialog
        {
            get
            {
                return _openBoatEditorDialog ??= new DelegateCommand(ExecuteRunEditorDialog);
            }
        }
        /// <summary>
        /// Удаление одной лодки
        /// </summary>
        public AsyncRelayCommand RemoveBoat
        {
            get
            {
                return _removeBoat??=new AsyncRelayCommand(RemoveBoatsMethod, (ex) =>
                {
                    ExecuteRunDialog(new MessageDialogProperty() { Title = "Ошибка", Message = ex.Message });
                });
            }
        }
        #endregion
        
        #region Методы
        /// <summary>
        /// Получение списка лодок из модели
        /// </summary>
        private async Task GetBoatsMethod(object parameter)
        {
            ProgressBarVisibility = Visibility.Visible;

            var boatList = await _boatModel.LoadBoatsAsync();
            _boatsCollection = new ObservableCollection<SelectableBoatViewModel>();
            foreach (var boat in boatList)
            {
                _boatsCollection.Add(new SelectableBoatViewModel(boat));
                OnPropertyChanged(nameof(FilteredBoats));
            }

            ProgressBarVisibility = Visibility.Collapsed;
            
            _boatsCollection.CollectionChanged += CheckForDeleteBoats;
        }

        /// <summary>
        /// Удаляет выделенные лодки
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        private async Task RemoveBoatsMethod(object parameter)
        {
            //Получаем список отмеченных лодок
            var boatList = _boatsCollection.Where(b => b.IsSelected || b.IsDeleted)
                .Select(b => b.Boat);

            await Task.Run(() => BoatModel.RemoveBoatsAsync(boatList));
            
            //Получаем главное окно для показа уведомления о удалении
            var mainWindow = (MainWindow)Application.Current.MainWindow;
            
            //Обновляем список лодок
            GetBoatsCollection.Execute(null);
            
            mainWindow.SendSnackbar($"Успешно удалено.");
        }

        private ObservableCollection<SelectableBoatViewModel> FilterBoats(string filterText)
        {
            
            var filteredCollection = _boatsCollection.Where(b => 
                                                                                b.Model.ToLower().Contains(filterText.ToLower()) ||
                                                                                b.Id.ToString() == filterText);
            var boatsCollection = new ObservableCollection<SelectableBoatViewModel>();
            foreach (var selectableBoatViewModel in filteredCollection)
            {
                boatsCollection.Add(selectableBoatViewModel);
            }

            return boatsCollection;
        }

        private void CheckForDeleteBoats(object sender, NotifyCollectionChangedEventArgs e)
        {
            RemoveBoats.Execute(null);
        }
        #endregion

        #region MessageDialog

        /// <summary>
        /// Запуск диалога сообщения
        /// </summary>
        /// <param name="o"></param>
        private async void ExecuteRunDialog(object o)
        {
            var view = new SampleMessageDialog()
            {
                DataContext = new SampleMessageDialogViewModel((MessageDialogProperty)o)
            };
            var result = await DialogHost.Show(view, "MessageDialogRoot", ClosingEventHandler);
        }
        /// <summary>
        /// При закрытии сообщения уведомляем об этом список лодок
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="eventArgs"></param>
        private void ClosingEventHandler(object sender, DialogClosingEventArgs eventArgs)
        {
            GetBoatsCollection.Execute(null);
            OnPropertyChanged(nameof(FilteredBoats));
        }

        /// <summary>
        /// Открытие диалога редактора
        /// </summary>
        /// <param name="o"></param>
        private async void ExecuteRunEditorDialog(object o)
        {
            BaseViewModel bvm = new BoatEditorViewModel();
            
            var view = new View.MessageDialogs.MessageDialog()
            {
                DataContext = new MessageDialogViewModel(bvm)
            };
            
            var result = await DialogHost.Show(view, "RootDialog", ClosingEventHandler);
        }

        #endregion

    }
}
