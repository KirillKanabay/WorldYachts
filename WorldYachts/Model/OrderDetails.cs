using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace WorldYachts.Model
{
    class OrderDetails
    {
        /// <summary>
        /// Идентификатор критериев доставки
        /// </summary>
        [Required] public int Id { get; set; }
        /// <summary>
        /// Идентификатор аксессуара
        /// </summary>
        [Required] public int AccessoryId { get; set; }
        /// <summary>
        /// Идентификатор доставки
        /// </summary>
        [Required] public int OrderId { get; set; }

        /// <summary>
        /// Ссылка на аксессуар
        /// </summary>
        [ForeignKey("AccessoryId")]
        public Accessory Accessory { get; set; }
        /// <summary>
        /// Ссылка на заказ
        /// </summary>
        [ForeignKey("OrderId")]
        public Order Order { get; set; }
    }
}
