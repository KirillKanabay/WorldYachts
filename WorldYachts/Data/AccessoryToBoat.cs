using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using WorldYachts.Data.Entities;

namespace WorldYachts.Data
{
    public class AccessoryToBoat:IComparable, IComparer
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
        public virtual Boat Boat { get; set; }
        // /// <summary>
        // /// Ссылка на аксессуар
        // /// </summary>
        // [ForeignKey("AccessoryId")]
        public virtual Accessory Accessory { get; set; }

        /// <summary>
        /// Является ли предмет удаленным
        /// </summary>
        [Required] public bool IsDeleted { get; set; }

        public int CompareTo(object? obj)
        {
            return (Compare(this, obj));
        }

        public int Compare(object? x, object? y)
        {
            var atb1 = (AccessoryToBoat) x;
            var atb2 = (AccessoryToBoat) y;

            return (atb1.AccessoryId == atb2.AccessoryId && atb1.BoatId == atb2.BoatId) ? 0 : 1;
        }

    }
}
