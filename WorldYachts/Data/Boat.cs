using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using WorldYachts.Helpers.Commands;
using WorldYachts.Model;

namespace WorldYachts.Data
{
    public class Boat : IComparable, IComparer
    {
        /// <summary>
        /// Идентификатор яхты
        /// </summary>
        [Required]
        public int Id { get; set; }

        /// <summary>
        /// Модель яхты
        /// </summary>
        [Required]
        public string Model { get; set; }

        /// <summary>
        /// Тип лодки
        /// </summary>
        [Required]
        public string Type { get; set; }

        /// <summary>
        /// Количество мест для гребцов
        /// </summary>
        [Required]
        public int NumberOfRowers { get; set; }

        /// <summary>
        /// Наличие мачты
        /// </summary>
        [Required]
        public bool Mast { get; set; }

        /// <summary>
        /// Цвет яхты
        /// </summary>
        [Required]
        public string Color { get; set; }

        /// <summary>
        /// Тип дерева
        /// </summary>
        [Required]
        public string Wood { get; set; }

        /// <summary>
        /// Цена без НДС
        /// </summary>
        [Required]
        public decimal BasePrice { get; set; }

        /// <summary>
        /// НДС%
        /// </summary>
        [Required]
        public double Vat { get; set; }

        /// <summary>
        /// Ссылка на доступные аксессуары для определенных лодок
        /// </summary>
        [ForeignKey("BoatId")]
        public List<AccessoryToBoat> AccessoryToBoat { get; set; }

        public int CompareTo(object? obj)
        {
            return Compare(this, obj);
        }

        public int Compare(object? x, object? y)
        {
            var boat1 = (Boat) x;
            var boat2 = (Boat) y;
            if (String.Compare(boat1.Model, boat2.Model, CultureInfo.CurrentCulture, CompareOptions.IgnoreCase) > 0)
            {
                return 1;
            }

            if (String.Compare(boat1.Model, boat2.Model, CultureInfo.CurrentCulture, CompareOptions.IgnoreCase) ==
                0 &&
                String.Compare(boat1.Type, boat2.Type, CultureInfo.CurrentCulture, CompareOptions.IgnoreCase) ==
                0 &&
                String.Compare(boat1.Color, boat2.Color, CultureInfo.CurrentCulture, CompareOptions.IgnoreCase) ==
                0 &&
                String.Compare(boat1.Wood, boat2.Wood, CultureInfo.CurrentCulture, CompareOptions.IgnoreCase) ==
                0 &&
                String.Compare(boat1.Type, boat2.Type, CultureInfo.CurrentCulture, CompareOptions.IgnoreCase) ==
                0 &&
                (boat1.Mast == boat2.Mast) &&
                boat1.NumberOfRowers == boat2.NumberOfRowers)
            {
                return 0;
            }

            return -1;
        }

        #region Команды

        public AsyncRelayCommand DeleteAccessoryToBoat
        {
            get
            {
                return new AsyncRelayCommand(RemoveATB,null);
            }
        }

        #endregion

        #region Методы

        public async Task RemoveATB(object parameter)
        {
            //Получаем название аксессуара
            var accessoryName = ((Expander) parameter).Header.ToString();

            var atb = new AccessoryToBoatModel().Load().Where(i=>i.Boat.Model == Model);
            
            await Task.Run(() => new AccessoryToBoatModel().RemoveAsync(atb));
        }

        #endregion
    }
}
