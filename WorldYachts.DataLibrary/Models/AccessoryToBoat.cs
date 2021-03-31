using System.ComponentModel.DataAnnotations;
using WorldYachts.DataLibrary.Models.Interfaces;

namespace WorldYachts.DataLibrary.Models
{
    public class AccessoryToBoat : IAccessoryToBoat
    {
        /// <summary>
        /// Идентификатор связи аксессуара и лодки
        /// </summary>
        [Required] public int Id { get; set; }
        /// <summary>
        /// Идентификатор лодки
        /// </summary>
        [Required] public int BoatId { get; set; }
        /// <summary>
        /// Идентификатор аксессуара
        /// </summary>
        [Required] public int AccessoryId { get; set; }

        // /// <summary>
        // /// Ссылка на лодку
        // /// </summary>
        // [ForeignKey("BoatId")]
        public virtual IBoat Boat { get; set; }
        // /// <summary>
        // /// Ссылка на аксессуар
        // /// </summary>
        // [ForeignKey("AccessoryId")]
        public virtual IAccessory Accessory { get; set; }

        /// <summary>
        /// Является ли предмет удаленным
        /// </summary>
        [Required] public bool IsDeleted { get; set; }
        
    }
}
