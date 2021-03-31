using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using WorldYachts.DataLibrary.Models.Interfaces;

namespace WorldYachts.DataLibrary.Models
{
    public class Order : IOrder
    {
        /// <summary>
        /// Идентификатор доставки
        /// </summary>
        [Required]
        public int Id { get; set; }
        /// <summary>
        /// Является ли предмет удаленным
        /// </summary>
        [Required] public bool IsDeleted { get; set; }
        /// <summary>
        /// Идентификатор заказчика
        /// </summary>
        [Required]
        public int CustomerId { get; set; }

        /// <summary>
        /// Идентификатор менеджера
        /// </summary>
        [Required]
        public int SalesPersonId { get; set; }

        /// <summary>
        /// Дата формирования доставки
        /// </summary>
        [Required]
        public DateTime Date { get; set; }

        /// <summary>
        /// Идентификатор лодки
        /// </summary>
        [Required]
        public int BoatId { get; set; }

        /// <summary>
        /// Адрес доставки
        /// </summary>
        [Required]
        public string DeliveryAddress { get; set; }

        /// <summary>
        /// Город доставки
        /// </summary>
        [Required]
        public string City { get; set; }

        /// <summary>
        /// Статус заказа
        /// </summary>
        [Required]
        public int Status { get; set; }

        /// <summary>
        /// Ссылка на менеджера
        /// </summary>
        //[ForeignKey("SalesPersonId")]
        public virtual ISalesPerson SalesPerson { get; set; }

        // /// <summary>
        // /// Ссылка на заказчика
        // /// </summary>
        // [ForeignKey("CustomerId")]
        public ICustomer Customer { get; set; }

        /// <summary>
        /// Ссылка на лодку
        /// </summary>
        public IBoat Boat { get; set; }

        /// <summary>
        /// Ссылка на список деталей заказа
        /// </summary>
        public List<IOrderDetails> OrderDetails { get; set; }
        
    }
}