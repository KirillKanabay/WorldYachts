using System;
using System.Collections.Generic;
using System.Text;

namespace WorldYachts.Data.Entities
{
    public class Invoice
    {
        public int Id { get; set; }
        /// <summary>
        /// Идентификатор заказа 
        /// </summary>
        public int ContractId { get; set; }

        /// <summary>
        /// Оплачен ли?
        /// </summary>
        public bool Settled { get; set; }

        /// <summary>
        /// Сумма заказа
        /// </summary>
        public decimal Sum { get; set; }

        /// <summary>
        /// Сумма заказа с НДС
        /// </summary>
        public decimal SumInclVat { get; set; }

        /// <summary>
        /// Дата оплаты
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Ссылка на счет
        /// </summary>

        public Contract Contract { get; set; }
    }
}