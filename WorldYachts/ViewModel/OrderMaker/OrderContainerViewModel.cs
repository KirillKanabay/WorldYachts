using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorldYachts.Data.Entities;

namespace WorldYachts.ViewModel.OrderMaker
{
    public class OrderContainerViewModel
    {
        public OrderContainerViewModel()
        {
            
        }
        public Data.Entities.Boat Boat { get; set; }
        public Customer Customer { get; set; }
        public List<Data.Entities.Accessory> SelectedAccessories { get; set; } = new List<Data.Entities.Accessory>();
        public string DeliveryAddress { get; set; }
        public string City { get; set; }
        public void Reset()
        {
            Boat = null;
            Customer = null;
            SelectedAccessories = null;
            DeliveryAddress = "";
            City = "";
        }

        public override string ToString()
        {
            return $"Заказчик: {Customer?.FirstName} {Customer?.SecondName}{Environment.NewLine}" +
                   $"Лодка: {Boat?.Model} {Boat?.Color} (Стоимость: {Boat?.BasePrice}₽) {Environment.NewLine}" +
                   $"Аксессуары: {string.Join(',', SelectedAccessories?.Select(a => a.Name) ?? new List<string>())} {Environment.NewLine}" +
                   $"Город доставки: {City} {Environment.NewLine}" +
                   $"Адрес доставки: {DeliveryAddress} {Environment.NewLine}";
        }

        public decimal Price => (Boat != null) 
            ? Boat.BasePrice + SelectedAccessories.Select(a => a.Price).Sum()
            : 0;
        public decimal PriceInclVat => (Boat != null) 
        ?Boat.BasePrice + (Boat.BasePrice * Convert.ToDecimal(Boat.Vat * 0.01)) +
                                       SelectedAccessories.Select(a => a.PriceInclVat).Sum()
        : 0;

    }
}
