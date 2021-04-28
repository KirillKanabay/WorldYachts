using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows;
using MaterialDesignThemes.Wpf;
using WorldYachts.DependencyInjections.Models;
using WorldYachts.Helpers;
using WorldYachts.Helpers.Commands;
using WorldYachts.Helpers.Validators;
using WorldYachts.View.MessageDialogs;
using WorldYachts.ViewModel.BaseViewModels;
using WorldYachts.ViewModel.MessageDialog;

namespace WorldYachts.ViewModel.Partner
{
    public class PartnerEditorViewModel : BaseViewModel, IDataErrorInfo
    {
        #region Поля

        private readonly IPartnerModel _partnerModel;
        
        private readonly Data.Entities.Partner _partner;
        
        private readonly bool _isEdit;
        
        private Visibility _progressBarVisibility = Visibility.Collapsed;

        #endregion

        #region Конструкторы

        public PartnerEditorViewModel(IPartnerModel partnerModel)
        {
            _partnerModel = partnerModel;

            if (!EntityContainer.IsEmpty)
            {
                _partner = EntityContainer.Pop<Data.Entities.Partner>();
                _isEdit = true;
            }
            else
            {
                _partner = new Data.Entities.Partner();
            }
        }

        #endregion

        #region Свойства

        /// <summary>
        /// Название партнера
        /// </summary>
        public string Name
        {
            get => _partner.Name;
            set
            {
                _partner.Name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        /// <summary>
        /// Город партнера
        /// </summary>
        public string City
        {
            get => _partner.City;
            set
            {
                _partner.City = value;
                OnPropertyChanged(nameof(City));
            }
        }

        /// <summary>
        /// Адрес партнера
        /// </summary>
        public string Address
        {
            get => _partner.Address;
            set
            {
                _partner.Address = value;
                OnPropertyChanged(nameof(Address));
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

        public bool SaveButtonIsEnabled => _errors.Count == 0;

        #endregion

        #region Команды
        public AsyncRelayCommand SaveItem => new AsyncRelayCommand(SaveMethod,
            (ex) => { ExecuteRunDialog(new MessageDialogProperty() { Title = "Ошибка", Message = ex.Message }); });

        #endregion

        #region Методы

        private async Task SaveMethod(object parameter)
        {
            ProgressBarVisibility = Visibility.Visible;
            if (_isEdit)
            {
                await _partnerModel.UpdateAsync(_partner);
            }
            else
            {
                await _partnerModel.AddAsync(_partner);
            }

            ProgressBarVisibility = Visibility.Collapsed;

            string snackbarMessage = _isEdit
                ? $"Партнер \"{Name}\" успешно отредактирован."
                : $"Партнер \"{Name}\" успешно добавлен.";

            SendSnackbar(snackbarMessage);
            CloseCurrentDialog();
        }

        public async void ExecuteRunDialog(object o)
        {
            var view = new SampleMessageDialog()
            {
                DataContext = new SampleMessageDialogViewModel((MessageDialogProperty)o)
            };
            var result = await DialogHost.Show(view, "DialogRoot");
        }

        #endregion

        #region Валидация полей

        private readonly Dictionary<string, string> _errors = new Dictionary<string, string>();

        public string Error { get; }

        public string this[string columnName]
        {
            get
            {
                string error = columnName switch
                {
                    "Name" => new Validation(new NotEmptyFieldValidationRule(Name)).Validate(),
                    "Address" => new Validation(new NotEmptyFieldValidationRule(Address)).Validate(),
                    "City" => new Validation(new NotEmptyFieldValidationRule(City)).Validate(),
                    _ => null
                };

                _errors.Remove(columnName);
                if (!string.IsNullOrWhiteSpace(error))
                    _errors.Add(columnName, error);
                OnPropertyChanged(nameof(SaveButtonIsEnabled));
                return error;
            }
        }
    }

    #endregion
}