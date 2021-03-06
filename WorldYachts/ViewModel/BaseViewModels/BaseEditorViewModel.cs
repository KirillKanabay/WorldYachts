﻿using System.Threading.Tasks;
using System.Windows;
using MaterialDesignThemes.Wpf;
using WorldYachts.Helpers.Commands;
using WorldYachts.Model;
using WorldYachts.View.MessageDialogs;
using WorldYachts.ViewModel.MessageDialog;

namespace WorldYachts.ViewModel.BaseViewModels
{
    public abstract class BaseEditorViewModel<TEntity> : BaseViewModel
    {
        #region Поля

        private Visibility _progressBarVisibility = Visibility.Collapsed;

        /// <summary>
        /// Флаг редактирования
        /// </summary>
        protected bool _isEdit;

        private AsyncRelayCommand _saveItem;
        private IDataModel<TEntity> _dataModel;
        #endregion

        #region Конструкторы

        protected BaseEditorViewModel(bool isEdit = false)
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
        public abstract IDataModel<TEntity> ModelItem { get; } 

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
        protected virtual async Task SaveMethod(object parameter)
        {
            ProgressBarVisibility = Visibility.Visible;
            var item = GetSaveItem(_isEdit);
            try
            {
                if (_isEdit)
                {
                    await Task.Run(() => ModelItem.UpdateAsync(item));
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

            MainWindow.SendSnackbarAction?.Invoke(GetSaveSnackbarMessage(_isEdit));
            
            //Закрываем диалог редактирования
            MainWindow.GetMainWindow?.Invoke().DialogHost.CurrentSession.Close();
        }

        /// <summary>
        /// Получение объекта сохраняемого предмета
        /// </summary>
        /// <returns></returns>
        protected abstract TEntity GetSaveItem(bool isEdit);

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
        protected async void ExecuteRunDialog(object o)
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