namespace WorldYachts.DataLibrary.Models.Interfaces
{
    public interface IAccessoryToBoat
    {
        /// <summary>
        /// Идентификатор связи аксессуара и лодки
        /// </summary>
        int Id { get; set; }

        /// <summary>
        /// Идентификатор лодки
        /// </summary>
        int BoatId { get; set; }

        /// <summary>
        /// Идентификатор аксессуара
        /// </summary>
        int AccessoryId { get; set; }

        IBoat Boat { get; set; }
        IAccessory Accessory { get; set; }

        /// <summary>
        /// Является ли предмет удаленным
        /// </summary>
        bool IsDeleted { get; set; }
    }
}