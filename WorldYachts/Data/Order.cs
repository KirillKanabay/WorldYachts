using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using WorldYachts.Infrastructure;

namespace WorldYachts.Data
{
    public class Order
    {
        /// <summary>
        /// Идентификатор доставки
        /// </summary>
        [Required] public int Id { get; set; }
        
        /// <summary>
        /// Идентификатор заказчика
        /// </summary>
        [Required] public int CustomerId { get; set; }
        
        /// <summary>
        /// Идентификатор менеджера
        /// </summary>
        [Required] public int SalesPersonId { get; set; }

        /// <summary>
        /// Дата формирования доставки
        /// </summary>
        [Required] public DateTime Date { get; set; }
        
        /// <summary>
        /// Идентификатор лодки
        /// </summary>
        [Required] public int BoatId { get; set; }
        
        /// <summary>
        /// Адрес доставки
        /// </summary>
        [Required] public string DeliveryAddress { get; set; }

        /// <summary>
        /// Город доставки
        /// </summary>
        [Required] public string City { get; set; }
        /// <summary>
        /// Статус заказа
        /// </summary>
        [Required] public int Status { get; set; }
        /// <summary>
        /// Ссылка на менеджера
        /// </summary>
        //[ForeignKey("SalesPersonId")]
        public virtual SalesPerson SalesPerson { get; set; }

        // /// <summary>
        // /// Ссылка на заказчика
        // /// </summary>
        // [ForeignKey("CustomerId")]
         public Customer Customer { get; set; }

        /// <summary>
        /// Ссылка на лодку
        /// </summary>
        //[ForeignKey("BoatId")] 
        public Boat Boat { get; set; }

        /// <summary>
        /// Ссылка на список деталей заказа
        /// </summary>
        public List<OrderDetails> OrderDetails { get; set; }

        public string OrderName => $"{Boat.Model} (Заказ #{Id})";

        public string StatusString
        {
            get
            {
                switch (Status)
                {
                    case (int)OrderStatus.InProcessing:
                        return "В обработке.";
                    case (int)OrderStatus.Accepted:
                        return "Принят";
                    case (int)OrderStatus.Canceled:
                        return "Отменен";
                    case (int)OrderStatus.Completed:
                        return "Выполнен";
                    default:
                        return "Ошибка";
                }
            }
        }

        public string SalesPersonString => $"{SalesPerson.Name} {SalesPerson.SecondName}";
    }
}
