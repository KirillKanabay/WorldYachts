using System.Collections.Generic;
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

namespace WorldYachts.ViewModel.Boat.BoatType
{
    public class BoatTypeEditorViewModel:BaseViewModel
    {
        #region Поля
        private readonly IBoatTypeModel _boatTypeModel;

        private readonly Data.Entities.BoatType _boatType;

        private Visibility _progressBarVisibility = Visibility.Collapsed;

        private readonly bool _isEdit;
        #endregion

        #region Конструкторы

        public BoatTypeEditorViewModel(IBoatTypeModel boatTypeModel)
        {
            _boatTypeModel = boatTypeModel;
            if (!EntityContainer.IsEmpty)
            {
                _boatType = EntityContainer.Pop<Data.Entities.BoatType>();
                _isEdit = true;
            }
            else
            {
                _boatType = new Data.Entities.BoatType();
            }
        }

        #endregion

        #region Свойства
        public Visibility ProgressBarVisibility
        {
            get => _progressBarVisibility;
            set
            {
                _progressBarVisibility = value;
                OnPropertyChanged(nameof(ProgressBarVisibility));
            }
        }

        public string Type
        {
            get => _boatType.Type;
            set
            {
                _boatType.Type = value;
                OnPropertyChanged(nameof(Type));
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
                await _boatTypeModel.UpdateAsync(_boatType);
            }
            else
            {
                await _boatTypeModel.AddAsync(_boatType);
            }

            ProgressBarVisibility = Visibility.Collapsed;

            string snackbarMessage = _isEdit
                ? $"Тип лодки \"{Type}\" успешно отредактирован."
                : $"Тип лодки \"{Type}\" успешно добавлен.";

            SendSnackbar(snackbarMessage);
            CloseCurrentDialog();
        }

        public async void ExecuteRunDialog(object o)
        {
            var view = new SampleMessageDialog()
            {
                DataContext = new SampleMessageDialogViewModel((MessageDialogProperty)o)
            };
            var result = await DialogHost.Show(view, "EditorDialog");
        }

        #endregion

        #region Валидация
        public string Error { get; }

        private readonly Dictionary<string, string> _errors = new Dictionary<string, string>();

        public string this[string columnName]
        {
            get
            {
                string error = columnName switch
                {
                    nameof(Type) => new Validation(new NotEmptyFieldValidationRule(Type)).Validate(),
                    _ => null
                };
                _errors.Remove(columnName);
                if (!string.IsNullOrWhiteSpace(error))
                    _errors.Add(columnName, error);
                OnPropertyChanged(nameof(SaveButtonIsEnabled));
                return error;
            }
        }

        #endregion

    }
}
