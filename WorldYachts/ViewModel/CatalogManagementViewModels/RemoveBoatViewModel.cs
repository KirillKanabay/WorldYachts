using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using MaterialDesignThemes.Wpf;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using WorldYachts.Data;
using WorldYachts.Helpers.Commands;
using WorldYachts.Model;
using WorldYachts.View.MessageDialogs;
using WorldYachts.ViewModel.MessageDialog;

namespace WorldYachts.ViewModel.CatalogManagementViewModels
{
    class RemoveBoatViewModel:BaseViewModel
    {
        #region Поля
        private ObservableCollection<SelectableBoatViewModel> _boatsCollection;
        private readonly BoatModel _boatModel = new BoatModel();

        private string _filterText;

        private AsyncRelayCommand _getBoatsCollection;
        
        private Visibility _progressBarVisibility;
        private Visibility _elementVisibility;
        #endregion

        #region Конструкторы

        public RemoveBoatViewModel()
        {
            GetBoatsCollection.Execute(null);
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

        /// <summary>
        /// Видимость элементов
        /// </summary>
        public Visibility ElementVisibility
        {
            get => _elementVisibility;
            set
            {
                _elementVisibility = value;
                OnPropertyChanged(nameof(ElementVisibility));
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

        #endregion
        #region Методы
        /// <summary>
        /// Получение списка лодок из модели
        /// </summary>
        private async Task GetBoatsMethod(object parameter)
        {
            ElementVisibility = Visibility.Collapsed;
            ProgressBarVisibility = Visibility.Visible;

            var boatList = await _boatModel.LoadBoatsAsync();
            _boatsCollection = new ObservableCollection<SelectableBoatViewModel>();
            foreach (var boat in boatList)
            {
                _boatsCollection.Add(new SelectableBoatViewModel(boat));
                OnPropertyChanged(nameof(FilteredBoats));
            }

            ProgressBarVisibility = Visibility.Collapsed;
            ElementVisibility = Visibility.Visible;
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
        /// При закрытии сообщения открываем главное окно при успешной регистрации
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="eventArgs"></param>
        private void ClosingEventHandler(object sender, DialogClosingEventArgs eventArgs)
        {

        }

        #endregion

    }
}
