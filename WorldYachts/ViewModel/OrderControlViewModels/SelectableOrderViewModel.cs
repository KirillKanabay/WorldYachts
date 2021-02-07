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
        public SelectableOrderViewModel(Order item):base(item)
        {
            
        }

        #region Свойства

        public Boat Boat => _item.Boat;
        public List<OrderDetails> OrderDetails => _item.OrderDetails;
        public override BaseEditorViewModel<Order> Editor { get; }

        #endregion

        #region Методы
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
            return new MessageDialogProperty()
            {
                Title = "Подтверждение удаления",
                Message = "Будет удален следующий заказ:\n\n" + this
            };
        }


        #endregion

    }
}
