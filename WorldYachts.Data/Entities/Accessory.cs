using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace WorldYachts.Data.Entities
{
    public class Accessory
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public double Vat { get; set; }
        public int Inventory { get; set; }
        public int PartnerId { get; set; }
        public Partner Partner { get; set; }
        public IEnumerable<AccessoryToBoat> AccessoryToBoat { get; set; }
        public IEnumerable<OrderDetail> OrderDetails { get; set; }

        [JsonIgnore]
        public decimal PriceInclVat => Price + (Price * Convert.ToDecimal(Vat * 0.01));
        public override string ToString()
        {
            return $"Id: {Id}\n" +
                   $"Название: {Name}\n" +
                   $"Описание: {Description}\n" +
                   $"Базовая стоимость: {Price} ₽\n" +
                   $"НДС: {Vat} %\n" +
                   $"Инвентарный номер: {Inventory}\n";
        }
    }
}
