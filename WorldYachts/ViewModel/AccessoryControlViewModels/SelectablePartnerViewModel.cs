using System;
using System.Threading.Tasks;
using MaterialDesignThemes.Wpf;
using WorldYachts.Data;
using WorldYachts.Helpers.Commands;
using WorldYachts.View.Editors;
using WorldYachts.View.MessageDialogs;

namespace WorldYachts.ViewModel.AccessoryControlViewModels
{
    class SelectablePartnerViewModel : BaseSelectableViewModel<Partner>
    {
        #region Поля

        private int _id;
        private string _name;
        private string _address;
        private string _city;
        
        #endregion

        #region Конструкторы

        public SelectablePartnerViewModel(Partner partner):base(partner)
        {
            Id = partner.Id;
            Name = partner.Name;
            Address = partner.Address;
            City = partner.City;
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
        
        #endregion
        
        #region Методы

        public override string ToString()
        {
            return $"Id: {Id}\n" +
                   $"Название: {Name}\n" +
                   $"Адрес: {Address}\n" +
                   $"Город: {City}";
        }

        public override BaseEditorViewModel<Partner> Editor => new PartnerEditorViewModel();
        
        protected override void ToggleViewEditorAfterLoaded()
        {
            if (PartnerEditorView.PartnerEditorAfterLoad != null)
            {
                PartnerEditorView.PartnerEditorAfterLoad = null;
            }
            else
            {
                PartnerEditorView.PartnerEditorAfterLoad = GetEditorViewModel;
            }
        }

        protected override BaseViewModel GetEditorViewModel() => new PartnerEditorViewModel(_item);

        protected override MessageDialogProperty GetConfirmDeleteDialogProperty()
        {
            return new MessageDialogProperty(){Title = "Подтверждение удаления", 
                                               Message = "Будет удален следующий партнер:\n\n" + this};
        }
        
        #endregion
    }
}