using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using MaterialDesignThemes.Wpf;
using WorldYachts.Helpers;
using WorldYachts.Helpers.Commands;
using WorldYachts.Model;
using WorldYachts.View.MessageDialogs;
using WorldYachts.ViewModel.MessageDialog;

namespace WorldYachts.ViewModel
{
    abstract class BaseManagementViewModel<TData, TSelectableData> : BaseViewModel
        where TSelectableData : BaseSelectableViewModel<TData>
    {
        #region Поля

        /// <summary>
        /// Коллекция управляемых предметов
        /// </summary>
        protected ObservableCollection<TSelectableData> _itemsCollection;

        /// <summary>
        /// Модель предмета
        /// </summary>
        protected readonly IDataModel<TData> _model;

        /// <summary>
        /// Редактор предмета
        /// </summary>
        protected readonly BaseViewModel _editor;

        /// <summary>
        /// Поисковая строка
        /// </summary>
        private string _filterText;

        private AsyncRelayCommand _getCollection;
        private AsyncRelayCommand _removeItem;
        private AsyncRelayCommand _removeCollection;
        private DelegateCommand _openEditorDialog;


        private Visibility _progressBarVisibility;

        #endregion

        #region Конструкторы

        protected BaseManagementViewModel()
        {
        }

        #endregion

        #region Свойства

        /// <summary>
        /// Фильтрованная коллекция
        /// </summary>
        public ObservableCollection<TSelectableData> FilteredCollection
        {
            get
            {
                if (string.IsNullOrWhiteSpace(_filterText))
                    return Filter(_filterText);

                return _itemsCollection;
            }
        }

        public string FilterText
        {
            get => _filterText;
            set
            {
                _filterText = value;
                if (_filterText != null)
                {
                    OnPropertyChanged(nameof(FilteredCollection));
                }
            }
        }

        /// <summary>
        /// Видимость прогресс бара
        /// </summary>
        public Visibility ProgressBarVisibility
        {
            get => _progressBarVisibility;
            set
            {
                _progressBarVisibility = value;
                OnPropertyChanged(nameof(ProgressBarVisibility));
            }
        }

        #endregion

        #region Команды

        //public AsyncRelayCommand GetCollection
        //{
        //    get
        //    {
        //        return _getCollection ??= new AsyncRelayCommand(GetCollectionMethod,
        //            (ex) =>
        //            {
        //                ExecuteRunDialog(new MessageDialogProperty() {Title = "Ошибка", Message = ex.Message});
        //            });
        //    }
        //}

        #endregion

        #region Методы

        private async Task GetCollection(object parameter)
        {
            ProgressBarVisibility = Visibility.Visible;
            var itemsList = await _model.LoadAsync();
            _itemsCollection = new ObservableCollection<TSelectableData>();
            foreach (var data in itemsList)
            {
                //_itemsCollection.Add(new TSelectableData(data));
            }
        }

        /// <summary>
        /// Метод фильтрации по поисковой строке
        /// </summary>
        /// <param name="filterText"></param>
        /// <returns></returns>
        protected abstract ObservableCollection<TSelectableData> Filter(string filterText);

        /// <summary>
        /// Запуск диалога сообщения
        /// </summary>
        /// <param name="o"></param>
        private async void ExecuteRunDialog(object o)
        {
            var view = new SampleMessageDialog()
            {
                DataContext = new SampleMessageDialogViewModel((MessageDialogProperty) o)
            };
            var result = await DialogHost.Show(view, "MessageDialogRoot", ClosingEventHandler);
        }

        /// <summary>
        /// Запуск редактора
        /// </summary>
        /// <param name="o"></param>
        private async void ExecuteRunEditorDialog(object o)
        {
            var view = new View.MessageDialogs.MessageDialog()
            {
                DataContext = new MessageDialogViewModel(_editor)
            };

            var result = await DialogHost.Show(view, "RootDialog", ClosingEventHandler);
        }

        /// <summary>
        /// Обработчик события закрытия диалога
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="eventArgs"></param>
        private void ClosingEventHandler(object sender, DialogClosingEventArgs eventArgs)
        {
            GetCollection.Execute(null);
            OnPropertyChanged(nameof(FilteredCollection));
        }

        #endregion
    }
}