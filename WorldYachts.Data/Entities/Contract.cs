using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace WorldYachts.Data.Entities
{
    public class Contract
    {
        public int Id { get; set; }
        /// <summary>
        /// Идентификатор доставки
        /// </summary>
        public int OrderId { get; set; }

        /// <summary>
        /// Дата оформления контракта
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Погашенная часть денег
        /// </summary>
        public decimal DepositPayed { get; set; }

        /// <summary>
        /// Общая стоимость заказа
        /// </summary>
        public decimal ContractTotalPrice { get; set; }

        /// <summary>
        /// Общая стоимость заказа включая НДС
        /// </summary>
       public decimal ContractTotalPriceInclVat { get; set; }

        /// <summary>
        /// Процесс выполнения заказа
        /// </summary>
        public string ProductionProcess { get; set; }

        
        public Order Order { get; set; }

        public Invoice Invoice { get; set; }

    }
}
