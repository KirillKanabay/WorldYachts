using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using MaterialDesignThemes.Wpf;
using WorldYachts.Helpers.Commands;
using WorldYachts.Model;
using WorldYachts.View.MessageDialogs;
using WorldYachts.ViewModel.MessageDialog;

namespace WorldYachts.ViewModel
{
    public abstract class BaseEditorViewModel<TItem> : BaseViewModel
    {
        #region Поля

        private Visibility _progressBarVisibility = Visibility.Collapsed;

        /// <summary>
        /// Флаг редактирования партнера
        /// </summary>
        private bool _isEdit;

        private AsyncRelayCommand _saveItem;

        #endregion

        #region Конструкторы

        protected BaseEditorViewModel(bool isEdit)
        {
            _isEdit = isEdit;
        }

        #endregion

        #region Свойства

        /// <summary>
        /// Доступность сохранения
        /// </summary>
        public abstract bool SaveButtonIsEnabled { get; }

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

        /// <summary>
        /// Модель предмета
        /// </summary>
        public abstract IDataModel<TItem> ModelItem { get; } 

        #endregion

        #region Команды

        /// <summary>
        /// Команда сохранения предмета
        /// </summary>
        public AsyncRelayCommand SaveItem
        {
            get
            {
                return _saveItem ??= new AsyncRelayCommand(SaveMethod, (ex) =>
                {
                    ExecuteRunDialog(new MessageDialogProperty() { Title = "Ошибка", Message = ex.Message });
                });
            }
        }

        #endregion

        #region Методы
        /// <summary>
        /// Метод сохранения предмета
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        private async Task SaveMethod(object parameter)
        {
            ProgressBarVisibility = Visibility.Visible;
            var item = GetSaveItem(_isEdit);
            try
            {
                if (_isEdit)
                {
                    await Task.Run(() => ModelItem.SaveAsync(item));
                }
                else
                {
                    await Task.Run((() => ModelItem.AddAsync(item)));
                }
            }
            finally
            {
                ProgressBarVisibility = Visibility.Collapsed;
            }

            var mainWindow = (MainWindow)Application.Current.MainWindow;
            
            mainWindow.SendSnackbar(GetSaveSnackbarMessage(_isEdit));
            //Закрываем диалог редактирования партнера
            mainWindow.DialogHost.CurrentSession.Close();
        }

        /// <summary>
        /// Получение объекта сохраняемого предмета
        /// </summary>
        /// <returns></returns>
        protected abstract TItem GetSaveItem(bool isEdit);

        /// <summary>
        /// Получение сообщения об успешном сохранении предмета
        /// </summary>
        /// <param name="_isEdit"></param>
        /// <returns></returns>
        protected abstract string GetSaveSnackbarMessage(bool _isEdit);

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
        private void ClosingEventHandler(object sender, DialogOpenedEventArgs eventargs)
        {
        }

        #endregion
    }
}