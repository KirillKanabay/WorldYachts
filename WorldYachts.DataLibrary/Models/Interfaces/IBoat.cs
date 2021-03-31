using System.Collections.Generic;

namespace WorldYachts.DataLibrary.Models.Interfaces
{
    public interface IBoat
    {
        /// <summary>
        /// Идентификатор яхты
        /// </summary>
        int Id { get; set; }

        /// <summary>
        /// Является ли предмет удаленным
        /// </summary>
        bool IsDeleted { get; set; }

        /// <summary>
        /// Модель яхты
        /// </summary>
        string Model { get; set; }

        /// <summary>
        /// Тип лодки
        /// </summary>
        string Type { get; set; }

        /// <summary>
        /// Количество мест для гребцов
        /// </summary>
        int NumberOfRowers { get; set; }

        /// <summary>
        /// Наличие мачты
        /// </summary>
        bool Mast { get; set; }

        /// <summary>
        /// Цвет яхты
        /// </summary>
        string Color { get; set; }

        /// <summary>
        /// Тип дерева
        /// </summary>
        string Wood { get; set; }

        /// <summary>
        /// Цена без НДС
        /// </summary>
        decimal BasePrice { get; set; }

        /// <summary>
        /// НДС%
        /// </summary>
        double Vat { get; set; }

        /// <summary>
        /// Ссылка на доступные аксессуары для определенных лодок
        /// </summary>
        List<IAccessoryToBoat> AccessoryToBoat { get; set; }
    }
}