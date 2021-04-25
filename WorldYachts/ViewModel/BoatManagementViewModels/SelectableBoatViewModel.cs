using System.Threading.Tasks;
using MaterialDesignThemes.Wpf;
using WorldYachts.Data.Entities;
using WorldYachts.Helpers.Commands;
using WorldYachts.View.Editors;
using WorldYachts.View.MessageDialogs;
using WorldYachts.ViewModel.BaseViewModels;
using WorldYachts.ViewModel.CatalogControlViewModels;

namespace WorldYachts.ViewModel.BoatManagementViewModels
{
    public class SelectableBoatViewModel : BaseSelectableViewModel<Boat>
    {
        #region Поля

        private readonly Boat _boat;

        #endregion

        #region Конструкторы

        public SelectableBoatViewModel(Boat boat) : base(boat)
        {
            _boat = boat;
        }

        #endregion

        #region Свойства

        /// <summary>
        /// Идентификатор лодки
        /// </summary>
        public int Id => _boat.Id;

        /// <summary>
        /// Модель лодки
        /// </summary>
        public string Model => _boat.Model;

        /// <summary>
        /// Тип лодки
        /// </summary>
        public string Type => _boat.BoatType.Type;

        /// <summary>
        /// Количество гребцов
        /// </summary>
        public int NumberOfRower => _boat.NumberOfRowers;

        /// <summary>
        /// Наличие мачты
        /// </summary>
        public string Mast => _boat.Mast ? "Присутствует" : "Отсутствует";

        /// <summary>
        /// Цвет лодки
        /// </summary>
        public string Color => _boat.Color;

        /// <summary>
        /// Тип дерева
        /// </summary>
        public string Wood => _boat.BoatWood.Wood;

        /// <summary>
        /// Базовая цена без НДС
        /// </summary>
        public decimal BasePrice => _boat.BasePrice;

        /// <summary>
        /// Процентная ставка НДС
        /// </summary>
        public double Vat => _boat.Vat;

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