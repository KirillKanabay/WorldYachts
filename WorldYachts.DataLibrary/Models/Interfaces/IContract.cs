using System;

namespace WorldYachts.DataLibrary.Models.Interfaces
{
    public interface IContract
    {
        /// <summary>
        /// Идентификатор заказа
        /// </summary>
        int Id { get; set; }

        /// <summary>
        /// Является ли предмет удаленным
        /// </summary>
        bool IsDeleted { get; set; }

        /// <summary>
        /// Идентификатор доставки
        /// </summary>
        int OrderId { get; set; }

        /// <summary>
        /// Дата оформления контракта
        /// </summary>
        DateTime Date { get; set; }

        /// <summary>
        /// Погашенная часть денег
        /// </summary>
        decimal DepositPayed { get; set; }

        /// <summary>
        /// Общая стоимость заказа
        /// </summary>
        decimal ContractTotalPrice { get; set; }

        /// <summary>
        /// Общая стоимость заказа включая НДС
        /// </summary>
        decimal ContractTotalPriceInclVat { get; set; }

        /// <summary>
        /// Процесс выполнения заказа
        /// </summary>
        string ProductionProcess { get; set; }

        /// <summary>
        /// Ссылка на доставку
        /// </summary>
        IOrder Order { get; set; }
    }
}