﻿using System.Threading.Tasks;
using MaterialDesignThemes.Wpf;
using WorldYachts.DependencyInjections.Helpers;
using WorldYachts.DependencyInjections.Models;
using WorldYachts.Helpers;
using WorldYachts.Helpers.Commands;
using WorldYachts.View.MessageDialogs;
using WorldYachts.ViewModel.BaseViewModels;
using WorldYachts.ViewModel.MessageDialog;

namespace WorldYachts.ViewModel.Boat
{
    public class SelectableBoatViewModel : BaseViewModel
    {
        #region Поля
        private AsyncRelayCommand _removeCommand;
        private AsyncRelayCommand _editCommand;

        private readonly IBoatModel _boatModel;
        private readonly IViewModelContainer _viewModelContainer;

        private bool _isSelected;
        #endregion

        #region Конструкторы

        public SelectableBoatViewModel(Data.Entities.Boat boat, 
            IBoatModel boatModel,
            IViewModelContainer viewModelContainer)
        {
            Boat = boat;
            _boatModel = boatModel;
            _viewModelContainer = viewModelContainer;
        }

        #endregion

        #region Свойства

        public Data.Entities.Boat Boat { get; }
        public string Info => Boat.ToString();

        public bool IsSelected
        {
            get => _isSelected;
            set
            {
                _isSelected = value;
                OnPropertyChanged(nameof(IsSelected));
            }
        }
        #endregion

        #region Методы

        private MessageDialogProperty GetConfirmDeleteDialogProperty()
        {
            return new MessageDialogProperty()
            {
                Title = "Подтверждение удаления",
                Message = "Будет удалена следующая лодка:\n\n" + Info
            };
        }
        #endregion

        #region Команды

        public AsyncRelayCommand RemoveCommand
        {
            get
            {
                return _removeCommand ??= new AsyncRelayCommand(ShowConfirmDeleteDialog,
                    (ex) =>
                    {
                        ExecuteRunDialog(new MessageDialogProperty() { Title = "Ошибка", Message = ex.Message });
                    });
            }
        }

        public AsyncRelayCommand EditCommand
        {
            get
            {
                return _editCommand ??= new AsyncRelayCommand(ExecuteRunEditorDialog,
                    (ex) =>
                    {
                        ExecuteRunDialog(new MessageDialogProperty() { Title = "Ошибка", Message = ex.Message });
                    });
            }
        }

        #endregion

        #region Диалоги

        private async Task ExecuteRunEditorDialog(object o)
        {
            EntityContainer.Push(Boat);
            var view = new View.MessageDialogs.MessageDialog()
            {
                DataContext = new MessageDialogViewModel(_viewModelContainer.GetViewModel<BoatEditorViewModel>())
            };

            var result = await DialogHost.Show(view, "RootDialog");
        }

        /// <summary>
        /// Показывает простой диалог сообщения
        /// </summary>
        /// <param name="o"></param>
        private async void ExecuteRunDialog(object o)
        {
            var view = new SampleMessageDialog()
            {
                DataContext = new SampleMessageDialogViewModel((MessageDialogProperty)o)
            };
            var result = await DialogHost.Show(view, "RootDialog");
        }

        /// <summary>
        /// Показывает диалог подтверждения удаления предмета
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        private async Task ShowConfirmDeleteDialog(object parameter)
        {
            var view = new MessageDialogOkCancel()
            {
                DataContext = new SampleMessageDialogViewModel(GetConfirmDeleteDialogProperty())
            };
            var result = await DialogHost.Show(view, "RootDialog", ClosingDeleteDialogEventHandler);
        }

        /// <summary>
        /// Обработчик события закрытия диалога подтверждения удаления
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="eventargs"></param>
        private void ClosingDeleteDialogEventHandler(object sender, DialogClosingEventArgs eventargs)
        {
            if (Equals((eventargs.Parameter), true))
                _boatModel.DeleteAsync(Boat);
        }

        #endregion
    }
}