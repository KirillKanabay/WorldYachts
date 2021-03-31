using System;
using System.Collections.Generic;

namespace WorldYachts.DataLibrary.Models.Interfaces
{
    public interface IOrder
    {
        /// <summary>
        /// Идентификатор доставки
        /// </summary>
        int Id { get; set; }

        /// <summary>
        /// Является ли предмет удаленным
        /// </summary>
        bool IsDeleted { get; set; }

        /// <summary>
        /// Идентификатор заказчика
        /// </summary>
        int CustomerId { get; set; }

        /// <summary>
        /// Идентификатор менеджера
        /// </summary>
        int SalesPersonId { get; set; }

        /// <summary>
        /// Дата формирования доставки
        /// </summary>
        DateTime Date { get; set; }

        /// <summary>
        /// Идентификатор лодки
        /// </summary>
        int BoatId { get; set; }

        /// <summary>
        /// Адрес доставки
        /// </summary>
        string DeliveryAddress { get; set; }

        /// <summary>
        /// Город доставки
        /// </summary>
        string City { get; set; }

        /// <summary>
        /// Статус заказа
        /// </summary>
        int Status { get; set; }

        /// <summary>
        /// Ссылка на менеджера
        /// </summary>

        ISalesPerson SalesPerson { get; set; }

        ICustomer Customer { get; set; }

        /// <summary>
        /// Ссылка на лодку
        /// </summary>

        IBoat Boat { get; set; }

        /// <summary>
        /// Ссылка на список деталей заказа
        /// </summary>
        List<IOrderDetails> OrderDetails { get; set; }
    }
}