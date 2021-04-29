using System.Collections.Generic;

namespace WorldYachts.Data.Entities
{
    public class Boat
    {
        public int Id { get; set; }
        public string Model { get; set; }
        public int TypeId { get; set; }
        public int NumberOfRowers { get; set; }
        public bool Mast { get; set; }
        public string Color { get; set; }
        public int WoodId { get; set; }
        public decimal BasePrice { get; set; }
        public double Vat { get; set; }
        public IEnumerable<AccessoryToBoat> AccessoryToBoat { get; set; }
        public BoatType BoatType { get; set; }
        public BoatWood BoatWood { get; set; }
        public IEnumerable<Order> Orders { get; set; }

        public override string ToString()
        {
            return $"Id: {Id}\n" +
                   $"Модель: {Model}\n" +
                   $"Тип: {BoatType.Type}\n" +
                   $"Количество гребцов: {NumberOfRowers}\n" +
                   $"Наличие мачты: {Mast}\n" +
                   $"Цвет: {Color}\n" +
                   $"Тип дерева: {BoatWood.Wood}\n" +
                   $"Цена без НДС: {BasePrice}";
        }

    }
}
