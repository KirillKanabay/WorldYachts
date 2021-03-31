using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WorldYachts.DataLibrary.Models.Interfaces;

namespace WorldYachts.DataLibrary.Models
{
    public class Accessory : IAccessory
    {
        /// <summary>
        /// Идентификатор аксессуара
        /// </summary>
        [Required] public int Id { get; set; }
        /// <summary>
        /// Название аксессуара
        /// </summary>
        [Required] public string Name { get; set; }
        /// <summary>
        /// Описание аксессуара
        /// </summary>
        [Required] public string Description { get; set; }
        /// <summary>
        /// Стоимость аксессуара
        /// </summary>
        [Required] public decimal Price { get; set; }
        /// <summary>
        /// НДС
        /// </summary>
        [Required] public double Vat { get; set; }
        /// <summary>
        /// Инвентарный  номер
        /// </summary>
        [Required] public int Inventory { get; set; }
        /// <summary>
        /// Уровень доставки
        /// </summary>
        [Required] public int OrderLevel { get; set; }
        /// <summary>
        /// Партия заказа
        /// </summary>
        [Required] public int OrderBatch { get; set; }
        /// <summary>
        /// Идентификатор партнера
        /// </summary>
        [Required] public int PartnerId { get; set; }

        /// <summary>
        /// Является ли предмет удаленным
        /// </summary>
        [Required] public bool IsDeleted { get; set; }

        [ForeignKey("PartnerId")]
        public IPartner Partner { get; set; }

        /// <summary>
        /// Ссылка на доступные аксессуары для определенных лодок
        /// </summary>
        [ForeignKey("AccessoryId")]
        public List<IAccessoryToBoat> AccessoryToBoat { get; set; }
    }
}
