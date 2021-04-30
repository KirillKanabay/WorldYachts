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

namespace WorldYachts.ViewModel.Boat.Wood
{
    public class BoatWoodEditorViewModel:BaseViewModel
    {
        #region Поля
        private readonly IBoatWoodModel _boatWoodModel;

        private readonly Data.Entities.BoatWood _boatWood;

        private Visibility _progressBarVisibility = Visibility.Collapsed;

        private readonly bool _isEdit;
        #endregion

        #region Конструкторы

        public BoatWoodEditorViewModel(IBoatWoodModel boatWoodModel)
        {
            _boatWoodModel = boatWoodModel;
            if (!EntityContainer.IsEmpty)
            {
                _boatWood = EntityContainer.Pop<Data.Entities.BoatWood>();
                _isEdit = true;
            }
            else
            {
                _boatWood = new Data.Entities.BoatWood();
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

        public string Wood
        {
            get => _boatWood.Wood;
            set
            {
                _boatWood.Wood = value;
                OnPropertyChanged(nameof(Wood));
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
                await _boatWoodModel.UpdateAsync(_boatWood);
            }
            else
            {
                await _boatWoodModel.AddAsync(_boatWood);
            }

            ProgressBarVisibility = Visibility.Collapsed;

            string snackbarMessage = _isEdit
                ? $"Тип дерева лодки \"{Wood}\" успешно отредактирован."
                : $"Тип дерева лодки \"{Wood}\" успешно добавлен.";

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
                    nameof(Wood) => new Validation(new NotEmptyFieldValidationRule(Wood)).Validate(),
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
