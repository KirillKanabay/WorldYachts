namespace WorldYachts.DataLibrary.Models.Interfaces
{
    public interface IOrderDetails
    {
        /// <summary>
        /// Идентификатор критериев доставки
        /// </summary>
        int Id { get; set; }

        /// <summary>
        /// Является ли предмет удаленным
        /// </summary>
        bool IsDeleted { get; set; }

        /// <summary>
        /// Идентификатор аксессуара
        /// </summary>
        int AccessoryId { get; set; }

        /// <summary>
        /// Идентификатор доставки
        /// </summary>
        int OrderId { get; set; }

        /// <summary>
        /// Ссылка на аксессуар
        /// </summary>

        IAccessory Accessory { get; set; }

        IOrder Order { get; set; }
    }
}