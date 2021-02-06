using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace WorldYachts.Data
{
    public class OrderDetails: IComparable, IComparer
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
        //[ForeignKey("AccessoryId")]
        public Accessory Accessory { get; set; }
        // /// <summary>
        // /// Ссылка на заказ
        // /// </summary>
        // [ForeignKey("OrderId")]
        public virtual Order Order { get; set; }
        public int CompareTo(object? obj)
        {
            return Compare(this, obj);
        }

        public int Compare(object? x, object? y)
        {
            var od1 = (OrderDetails) x;
            var od2 = (OrderDetails) y;

            if (od1.AccessoryId == od2.AccessoryId && od1.OrderId == od2.OrderId)
            {
                return 0;
            }

            return 1;
        }
    }
}
