using System;
using System.Collections.Generic;
using System.Text;
using WorldYachts.ViewModel;
using WorldYachts.ViewModel.BaseViewModels;

namespace WorldYachts.View.MessageDialogs
{
    /// <summary>
    /// Диалог сообщения на вход которого приходит VM
    /// </summary>
    class MessageDialogViewModel:BaseViewModel
    {
        private BaseViewModel _selectedViewModel;

        public BaseViewModel SelectedViewModel
        {
            get => _selectedViewModel;
            set
            {
                _selectedViewModel = value;
                OnPropertyChanged(nameof(SelectedViewModel));
            }
        }

        public MessageDialogViewModel()
        {
        }
        public MessageDialogViewModel(BaseViewModel viewModel)
        {
            SelectedViewModel = viewModel;
        }
    }
}
