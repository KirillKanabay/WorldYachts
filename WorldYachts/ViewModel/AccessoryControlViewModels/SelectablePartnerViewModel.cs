using System;
using System.Threading.Tasks;
using MaterialDesignThemes.Wpf;
using WorldYachts.Data;
using WorldYachts.Helpers.Commands;
using WorldYachts.View.Editors;
using WorldYachts.View.MessageDialogs;

namespace WorldYachts.ViewModel.AccessoryControlViewModels
{
    class SelectablePartnerViewModel : BaseViewModel
    {
        #region Поля

        private int _id;
        private string _name;
        private string _address;
        private string _city;

        private bool _isSelected;
        private bool _isDeleted = false;

        private AsyncRelayCommand _removeCommand;
        private AsyncRelayCommand _editCommand;

        public static Action OnItemChanged;

        #endregion

        #region Конструкторы

        public SelectablePartnerViewModel(Partner partner)
        {
            Id = partner.Id;
            Name = partner.Name;
            Address = partner.Address;
            City = partner.City;

            IsSelected = false;

            Partner = partner;
        }

        #endregion

        #region Свойства

        /// <summary>
        /// Идентификатор партнера
        /// </summary>
        public int Id
        {
            get => _id;
            set
            {
                _id = value;
                OnPropertyChanged(nameof(Id));
            }
        }

        /// <summary>
        /// Название организации партнера
        /// </summary>
        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        /// <summary>
        /// Адрес партнера
        /// </summary>
        public string Address
        {
            get => _address;
            set
            {
                _address = value;
                OnPropertyChanged(nameof(Address));
            }
        }

        /// <summary>
        /// Город партнера
        /// </summary>
        public string City
        {
            get => _city;
            set
            {
                _city = value;
                OnPropertyChanged(nameof(City));
            }
        }

        public bool IsSelected
        {
            get => _isSelected;
            set
            {
                _isSelected = value;
                OnPropertyChanged(nameof(IsSelected));
            }
        }

        /// <summary>
        /// Был ли удален партнер
        /// </summary>
        public bool IsDeleted
        {
            get => _isDeleted;
            set
            {
                _isDeleted = value;
                if(_isDeleted)
                    OnItemChanged?.Invoke();
                OnPropertyChanged(nameof(IsDeleted));
            }
        }

        /// <summary>
        /// Экземпляр партнера
        /// </summary>
        public Partner Partner;

        #endregion

        #region Команды

        /// <summary>
        /// Команда удаления партнера
        /// </summary>
        public AsyncRelayCommand RemoveCommand
        {
            get { return _removeCommand ??= new AsyncRelayCommand(ShowConfirmDeleteDialog, null); }
        }

        /// <summary>
        /// Команда редактирования партнера
        /// </summary>
        public AsyncRelayCommand EditCommand
        {
            get { return _editCommand ??= new AsyncRelayCommand(ExecuteRunEditorDialog, null); }
        }

        #endregion

        #region Методы

        public override string ToString()
        {
            return $"Id: {Id}\n" +
                   $"Название: {Name}\n" +
                   $"Адрес: {Address}\n" +
                   $"Город: {City}";
        }

        /// <summary>
        /// Метод запуска редактора
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        private async Task ExecuteRunEditorDialog(object o)
        {
            BaseViewModel bvm = new PartnerEditorViewModel();

            var view = new View.MessageDialogs.MessageDialog()
            {
                DataContext = new MessageDialogViewModel(bvm)
            };
            //Добавляем метод обновления редактора при загрузке при редактировании
            PartnerEditorView.PartnerEditorAfterLoad += GetPartnerEditorViewModel;
            var result = await DialogHost.Show(view, "RootDialog", ClosingEventHandler);

            //Убираем метод обновления редактора при загрузке при редактировании
            PartnerEditorView.PartnerEditorAfterLoad = null;

            OnItemChanged?.Invoke();
        }

        /// <summary>
        /// Создание экземпляра VM редактора партнеров с текущим партнером
        /// </summary>
        /// <returns></returns>
        private BaseViewModel GetPartnerEditorViewModel() => new PartnerEditorViewModel(Partner);

        /// <summary>
        /// Показ диалога подтверждения у пользователя при удалении
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        private async Task ShowConfirmDeleteDialog(object parameter)
        {
            var view = new MessageDialogOkCancel()
            {
                DataContext = new SampleMessageDialogViewModel("Подтверждение удаления",
                    "Будет удален следующий партнер:\n\n" + this),
            };
            var result = await DialogHost.Show(view, "RootDialog", ClosingDeleteDialogEventHandler);
        }

        private void ClosingEventHandler(object sender, DialogOpenedEventArgs eventArgs)
        {

        }

        /// <summary>
        /// Проверка результата подтверждения
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="eventArgs"></param>
        private void ClosingDeleteDialogEventHandler(object sender, DialogClosingEventArgs eventArgs)
        {
            if (Equals((eventArgs.Parameter), true))
                IsDeleted = true;
        }

        #endregion
    }
}