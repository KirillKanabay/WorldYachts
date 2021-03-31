using System;

namespace WorldYachts.DataLibrary.Models.Interfaces
{
    public interface IInvoice
    {
        /// <summary>
        /// Идентификатор счета
        /// </summary>
        int Id { get; set; }

        /// <summary>
        /// Является ли предмет удаленным
        /// </summary>
        bool IsDeleted { get; set; }

        /// <summary>
        /// Идентификатор заказа 
        /// </summary>
        int ContractId { get; set; }

        /// <summary>
        /// Оплачен ли?
        /// </summary>
        bool Settled { get; set; }

        /// <summary>
        /// Сумма заказа
        /// </summary>
        decimal Sum { get; set; }

        /// <summary>
        /// Сумма заказа с НДС
        /// </summary>
        decimal SumInclVat { get; set; }

        /// <summary>
        /// Дата оплаты
        /// </summary>
        DateTime Date { get; set; }

        /// <summary>
        /// Ссылка на счет
        /// </summary>
        IContract Contract { get; set; }
    }
}