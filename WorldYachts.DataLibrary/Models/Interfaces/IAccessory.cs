using System.Collections.Generic;

namespace WorldYachts.DataLibrary.Models.Interfaces
{
    public interface IAccessory
    {
        /// <summary>
        /// Идентификатор аксессуара
        /// </summary>
        int Id { get; set; }

        /// <summary>
        /// Название аксессуара
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// Описание аксессуара
        /// </summary>
        string Description { get; set; }

        /// <summary>
        /// Стоимость аксессуара
        /// </summary>
        decimal Price { get; set; }

        /// <summary>
        /// НДС
        /// </summary>
        double Vat { get; set; }

        /// <summary>
        /// Инвентарный  номер
        /// </summary>
        int Inventory { get; set; }

        /// <summary>
        /// Уровень доставки
        /// </summary>
        int OrderLevel { get; set; }

        /// <summary>
        /// Партия заказа
        /// </summary>
        int OrderBatch { get; set; }

        /// <summary>
        /// Идентификатор партнера
        /// </summary>
        int PartnerId { get; set; }

        /// <summary>
        /// Является ли предмет удаленным
        /// </summary>
        bool IsDeleted { get; set; }

        IPartner Partner { get; set; }

        /// <summary>
        /// Ссылка на доступные аксессуары для определенных лодок
        /// </summary>
        List<IAccessoryToBoat> AccessoryToBoat { get; set; }
    }
}