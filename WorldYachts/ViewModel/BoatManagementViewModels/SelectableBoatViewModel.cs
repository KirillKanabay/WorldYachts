using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MaterialDesignThemes.Wpf;
using WorldYachts.Data;
using WorldYachts.Helpers;
using WorldYachts.Helpers.Commands;
using WorldYachts.View.CatalogManagementViews;
using WorldYachts.View.MessageDialogs;
using WorldYachts.ViewModel.MessageDialog;

namespace WorldYachts.ViewModel.BoatManagementViewModels
{
    class SelectableBoatViewModel:BaseViewModel
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
        
        private bool _isSelected;
        private bool _isDeleted = false;

        private AsyncRelayCommand _removeBoat;
        private AsyncRelayCommand _editBoat;

        public static Action OnItemChanged;
        public static Action OnItemEditing;
        #endregion

        #region Конструкторы
        public SelectableBoatViewModel(Boat boat)
        {
            Id = boat.Id;
            Model = boat.Model;
            Type = boat.Type;
            NumberOfRower = boat.NumberOfRowers;
            Mast = boat.Mast ? "Присутствует" : "Отсутствует";
            Color = boat.Color;
            Wood = boat.Wood;
            BasePrice = boat.BasePrice;
            Vat = boat.Vat;

            IsSelected = false;

            Boat = boat;
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
        /// Выбрана ли лодка
        /// </summary>
        public bool IsSelected
        {
            get => _isSelected;
            set
            {
                _isSelected = value;
                OnPropertyChanged(nameof(_isSelected));
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
        public decimal PriceInclVat => BasePrice + (BasePrice * (decimal)Vat);

        /// <summary>
        /// Была ли удалена лодка
        /// </summary>
        public bool IsDeleted
        {
            get => _isDeleted;
            set
            {
                _isDeleted = value;
                OnPropertyChanged(nameof(IsDeleted));
            }
        } 
        public Boat Boat { get; set; }

        #endregion

        #region Команды
        /// <summary>
        /// Команда удаления лодки
        /// </summary>
        public AsyncRelayCommand RemoveBoat
        {
            get
            {
                return _removeBoat ??= new AsyncRelayCommand(ShowConfirmDeleteDialog, null);
            }
        }

        public AsyncRelayCommand EditBoat
        {
            get
            {
                return _editBoat ??= new AsyncRelayCommand(ExecuteRunEditorDialog, null);
            }
        }

        #endregion

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
        
        private async Task ExecuteRunEditorDialog(object o)
        {
            BaseViewModel bvm = new BoatEditorViewModel(Boat);

            var view = new View.MessageDialogs.MessageDialog()
            {
                DataContext = new MessageDialogViewModel(bvm)
            };
            //Добавляем метод обновления редактора при загрузке при редактировании
            BoatEditorView.BoatEditorViewAfterLoad += GetBoatEditorViewModel;
            var result = await DialogHost.Show(view, "RootDialog", ClosingEventHandler);
            
            //Убираем метод обновления редактора при загрузке при редактировании
            BoatEditorView.BoatEditorViewAfterLoad = null;

            OnItemChanged?.Invoke();
        }

        private BaseViewModel GetBoatEditorViewModel() => new BoatEditorViewModel(Boat); 
        private BaseViewModel GetBoatEditorDefaultViewModel() => new BoatEditorViewModel(); 
        private async Task ShowConfirmDeleteDialog(object parameter)
        {
            var view = new MessageDialogOkCancel()
            {
                DataContext = new SampleMessageDialogViewModel("Подтверждение удаления", $"Будет удалена следующая лодка:\n\n" + this)
            };
            var result = await DialogHost.Show(view, "RootDialog", ClosingDeleteDialogEventHandler);
            OnItemChanged?.Invoke();
        }

        private void ClosingEventHandler(object sender, DialogOpenedEventArgs eventargs)
        {
            
        }

        private void ClosingDeleteDialogEventHandler(object sender, DialogClosingEventArgs eventargs)
        {
            if (Equals((eventargs.Parameter), true))
                IsDeleted = true;
        }
        #endregion
    }
}
