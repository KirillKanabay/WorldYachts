using System;
using System.Collections.Generic;
using System.Text;
using WorldYachts.Data;
using WorldYachts.View.MessageDialogs;
using WorldYachts.ViewModel.BaseViewModels;

namespace WorldYachts.ViewModel.OrderControlViewModels
{
    class SelectableOrderViewModel:BaseSelectableViewModel<Order>
    {
        public SelectableOrderViewModel(Order item) : base(item)
        {
        }

        public override BaseEditorViewModel<Order> Editor { get; }
        protected override void ToggleViewEditorAfterLoaded()
        {
            throw new NotImplementedException();
        }

        protected override BaseViewModel GetEditorViewModel()
        {
            throw new NotImplementedException();
        }

        protected override MessageDialogProperty GetConfirmDeleteDialogProperty()
        {
            throw new NotImplementedException();
        }
    }
}
