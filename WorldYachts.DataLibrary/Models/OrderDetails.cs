using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using WorldYachts.DataLibrary.Models.Interfaces;

namespace WorldYachts.DataLibrary.Models
{
    public class OrderDetails : IOrderDetails
    {
        /// <summary>
        /// Идентификатор критериев доставки
        /// </summary>
        [Required] public int Id { get; set; }
        /// <summary>
        /// Является ли предмет удаленным
        /// </summary>
        [Required] public bool IsDeleted { get; set; }
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
        public IAccessory Accessory { get; set; }
        // /// <summary>
        // /// Ссылка на заказ
        // /// </summary>
        public virtual IOrder Order { get; set; }
    }
}
