using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using MaterialDesignThemes.Wpf;
using WorldYachts.Helpers;
using WorldYachts.Helpers.Commands;
using WorldYachts.Model;
using WorldYachts.View.MessageDialogs;
using WorldYachts.ViewModel.BoatManagementViewModels;
using WorldYachts.ViewModel.MessageDialog;


namespace WorldYachts.ViewModel.AccessoryControlViewModels
{
    class PartnersManagementViewModel:BaseViewModel
    {
        #region Поля

        private ObservableCollection<SelectablePartnerViewModel> _partnersCollection;
        private readonly PartnerModel _partnersModel = new PartnerModel();

        private AsyncRelayCommand _getPartnersCollection;
        private AsyncRelayCommand _removePartners;
        private DelegateCommand _openPartnerEditorDialog;
        private AsyncRelayCommand _removePartner;

        private Visibility _progressBarVisibility = Visibility.Collapsed;
        private string _filterText;


        #endregion

        #region Конструкторы

        public PartnersManagementViewModel()
        {
            GetPartnersCollection.Execute(null);
            SelectablePartnerViewModel.OnItemChanged = () =>
            {
                RemovePartners.Execute(null);

                GetPartnersCollection.Execute(null);
            };
        }

        #endregion

        #region Свойства

        public ObservableCollection<SelectablePartnerViewModel> FilteredPartners
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(_filterText))
                    return Filter(_filterText);
                return _partnersCollection;
            }
            set
            {
                _partnersCollection = value;
                RemovePartners.Execute(null);
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
                if(_filterText != null)
                    OnPropertyChanged(nameof(FilteredPartners));
            }
        }
        #endregion

        #region Команды

        public AsyncRelayCommand GetPartnersCollection
        {
            get
            {
                return _getPartnersCollection ??= new AsyncRelayCommand(GetCollectionMethod, (ex) =>
                {
                    ExecuteRunDialog(new MessageDialogProperty() {Title = "Ошибка", Message = ex.Message});
                });
            }
        }

        public AsyncRelayCommand RemovePartners
        {
            get
            {
                return _removePartners ??= new AsyncRelayCommand(RemovePartnersMethod, (ex) =>
                {
                    ExecuteRunDialog(new MessageDialogProperty() { Title = "Ошибка", Message = ex.Message });
                });
            }
        }

        public DelegateCommand OpenPartnerEditorDialog
        {
            get
            {
                return _openPartnerEditorDialog ??= new DelegateCommand(ExecuteRunEditorDialog);
            }
        }

        public AsyncRelayCommand RemovePartner
        {
            get
            {
                return _removePartner ??= new AsyncRelayCommand(RemovePartnersMethod, (ex) =>
                {
                    ExecuteRunDialog(new MessageDialogProperty() { Title = "Ошибка", Message = ex.Message });
                });
            }
        }
        #endregion

        #region Методы

        private async Task GetCollectionMethod(object parameter)
        {
            ProgressBarVisibility = Visibility.Visible;

            var partnersList = await _partnersModel.LoadAsync();
            _partnersCollection = new ObservableCollection<SelectablePartnerViewModel>();
            foreach (var partner in partnersList)
            {
                _partnersCollection.Add(new SelectablePartnerViewModel(partner));
                OnPropertyChanged(nameof(FilteredPartners));
            }

            ProgressBarVisibility = Visibility.Collapsed;
        }

        private async Task RemovePartnersMethod(object parameters)
        {
            //Получаем список удаляемых или удаленных лодок
            var partnerList = _partnersCollection.Where(p => p.IsSelected || p.IsDeleted)
                .Select(p => p.Partner);

            var partnerModel = new PartnerModel();

            if (partnerList.Any())
            {
                await Task.Run(() => partnerModel.RemoveAsync(partnerList));

                //Получаем главное окно для показа уведомления о удалении
                var mainWindow = (MainWindow)Application.Current.MainWindow;

                //Обновляем список лодок
                GetPartnersCollection.Execute(null);

                mainWindow.SendSnackbar($"Успешно удалено.");
            }
        }
        /// <summary>
        /// Фильтрация партнеров по id или названию
        /// </summary>
        /// <param name="filterText">Поисковая строка</param>
        /// <returns></returns>
        private ObservableCollection<SelectablePartnerViewModel> Filter(string filterText)
        {
            var filteredCollection = _partnersCollection.Where(p =>
                p.Name.ToLower().Contains(filterText.ToLower()) ||
                p.Id.ToString() == filterText);
            
            var partnersCollection = new ObservableCollection<SelectablePartnerViewModel>();
            foreach (var selectablePartnerViewModel in filteredCollection)
            {
                partnersCollection.Add(selectablePartnerViewModel);
            }

            return partnersCollection;
        }

        private void CheckForDeletePartners(object sender, NotifyCollectionChangedEventArgs e)
        {
            RemovePartners.Execute(null);
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
            GetPartnersCollection.Execute(null);
            OnPropertyChanged(nameof(FilteredPartners));
        }

        /// <summary>
        /// Открытие диалога редактора
        /// </summary>
        /// <param name="o"></param>
        private async void ExecuteRunEditorDialog(object o)
        {
            BaseViewModel bvm = new PartnerEditorViewModel();

            var view = new View.MessageDialogs.MessageDialog()
            {
                DataContext = new MessageDialogViewModel(bvm)
            };

            var result = await DialogHost.Show(view, "RootDialog", ClosingEventHandler);
        }

        #endregion
    }
}
