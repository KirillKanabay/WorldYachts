﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace WorldYachts.Model
{
    public class AccessoryToBoat
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
    }
}