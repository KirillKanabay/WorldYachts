using System.Windows;

namespace WorldYachts.ViewModel.AccessoryControlViewModels
{
    class AccessoryManagementViewModel : BaseViewModel
    {
        #region Поля

        private Visibility _progressBarVisibility = Visibility.Collapsed;
        #endregion

        #region PartnerManagement

        

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

        #endregion
    }
}