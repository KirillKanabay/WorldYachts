using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MaterialDesignThemes.Wpf;
using WorldYachts.Data;
using WorldYachts.Data.Entities;
using WorldYachts.Helpers;
using WorldYachts.Helpers.Commands;
using WorldYachts.View.Editors;
using WorldYachts.View.MessageDialogs;
using WorldYachts.ViewModel.AccessoryControlViewModels;
using WorldYachts.ViewModel.BaseViewModels;
using WorldYachts.ViewModel.CatalogControlViewModels;
using WorldYachts.ViewModel.MessageDialog;

namespace WorldYachts.ViewModel.BoatManagementViewModels
{
    public class SelectableBoatViewModel : BaseSelectableViewModel<Boat>
    {
        #region Поля

        private int _id;
        private string _model;
        private string _type;
        private int _numberOfRower;
        private string _mast;
        private string _color;
        private string _wood;
        private decimal _basePrice;
        private double _vat;

        #endregion

        #region Конструкторы

        public SelectableBoatViewModel(Boat boat) : base(boat)
        {
            Id = boat.Id;
            Model = boat.Model;
            Type = boat.BoatType.Type;
            NumberOfRower = boat.NumberOfRowers;
            Mast = boat.Mast ? "Присутствует" : "Отсутствует";
            Color = boat.Color;
            Wood = boat.BoatWood.Wood;
            BasePrice = boat.BasePrice;
            Vat = boat.Vat;
        }

        #endregion

        #region Свойства

        /// <summary>
        /// Идентификатор лодки
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
        /// Модель лодки
        /// </summary>
        public string Model
        {
            get => _model;
            set
            {
                _model = value;
                OnPropertyChanged(nameof(Model));
            }
        }

        /// <summary>
        /// Тип лодки
        /// </summary>
        public string Type
        {
            get => _type;
            set
            {
                _type = value;
                OnPropertyChanged(nameof(Type));
            }
        }

        /// <summary>
        /// Количество гребцов
        /// </summary>
        public int NumberOfRower
        {
            get => _numberOfRower;
            set
            {
                _numberOfRower = value;
                OnPropertyChanged(nameof(NumberOfRower));
            }
        }

        /// <summary>
        /// Наличие мачты
        /// </summary>
        public string Mast
        {
            get => _mast;
            set
            {
                _mast = value;
                OnPropertyChanged(nameof(_mast));
            }
        }

        /// <summary>
        /// Цвет лодки
        /// </summary>
        public string Color
        {
            get => _color;
            set
            {
                _color = value;
                OnPropertyChanged(nameof(Color));
            }
        }

        /// <summary>
        /// Тип дерева
        /// </summary>
        public string Wood
        {
            get => _wood;
            set
            {
                _wood = value;
                OnPropertyChanged(nameof(Wood));
            }
        }

        /// <summary>
        /// Базовая цена без НДС
        /// </summary>
        public decimal BasePrice
        {
            get => _basePrice;
            set
            {
                _basePrice = value;
                OnPropertyChanged(nameof(BasePrice));
            }
        }

        /// <summary>
        /// Процентная ставка НДС
        /// </summary>
        public double Vat
        {
            get => _vat;
            set
            {
                _vat = value;
                OnPropertyChanged(nameof(Vat));
            }
        }

        /// <summary>
        /// Цена с НДС
        /// </summary>
        public decimal PriceInclVat => BasePrice + (BasePrice * (decimal) Vat);

        
        public override BaseEditorViewModel<Boat> Editor => new BoatEditorViewModel();

        #endregion

        public AsyncRelayCommand OpenViewCommand => new AsyncRelayCommand(OpenViewBoat, null);

        #region Методы

        public override string ToString()
        {
            return $"Id: {Id}\n" +
                   $"Модель: {Model}\n" +
                   $"Тип: {Type}\n" +
                   $"Количество гребцов: {NumberOfRower}\n" +
                   $"Наличие мачты: {Mast}\n" +
                   $"Цвет: {Color}\n" +
                   $"Тип дерева: {Wood}\n" +
                   $"Цена без НДС: {BasePrice}";
        }

        protected override void ToggleViewEditorAfterLoaded()
        {
            if (BoatEditorView.EditorAfterLoad != null)
            {
                BoatEditorView.EditorAfterLoad = null;
            }
            else
            {
                BoatEditorView.EditorAfterLoad = GetEditorViewModel;
            }
        }

        protected override BaseViewModel GetEditorViewModel() => new BoatEditorViewModel(_item);
        
        protected override MessageDialogProperty GetConfirmDeleteDialogProperty()
        {
            return new MessageDialogProperty()
            {
                Title = "Подтверждение удаления",
                Message = "Будет удалена следующая лодка:\n\n" + this
            };
        }

        private async Task OpenViewBoat(object parameter)
        {
            var view = new View.MessageDialogs.MessageDialog()
            {
                DataContext = new MessageDialogViewModel(new BoatViewModel(Item))
            };

            var result = await DialogHost.Show(view, "RootDialog", ClosingEventHandler);
        }

        #endregion
    }
}